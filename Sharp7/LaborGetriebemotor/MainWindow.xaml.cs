using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Kommunikation;
using TestAutomat;

namespace LaborGetriebemotor
{
    public partial class MainWindow
    { public IPlc Plc { get; set; }
        public string VersionInfoLokal { get; set; }
        public string VersionNummer { get; set; }
        public ManualMode.ManualMode ManualMode { get; set; }
        public Datenstruktur Datenstruktur { get; set; }

        public bool TestWindowAktiv { get; set; }
        public DirectoryInfo AktuellesProjekt { get; set; }
        
        public TestAutomat.Model.OrdnerLesen AlleOrdnerLesen { get; set; }

        private AutoTesterWindow _autoTesterWindow;

        private readonly DatenRangieren _datenRangieren;
        private const int AnzByteDigInput = 1;
        private const int AnzByteDigOutput = 1;
        private const int AnzByteAnalogInput = 0;
        private const int AnzByteAnalogOutput = 0;
        public MainWindow()
        {
            const string versionText = "Labor Getriebemotor";
            VersionNummer = "V2.0";

            VersionInfoLokal = versionText + " " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput)
            {
                VersionInputSps = Encoding.ASCII.GetBytes(VersionInfoLokal)
            };

            AlleOrdnerLesen = new TestAutomat.Model.OrdnerLesen();
            
            var viewModel = new ViewModel.ViewModel(this);
            _datenRangieren = new DatenRangieren(viewModel);

            InitializeComponent();
            DataContext = viewModel;

            Plc = new S71200(Datenstruktur, _datenRangieren.RangierenInput, _datenRangieren.RangierenOutput);

            ManualMode = new ManualMode.ManualMode(Datenstruktur);

            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Di, "./ManualConfig/DI.json");
            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Da, "./ManualConfig/DA.json");
            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Ai, "./ManualConfig/AI.json");
            ManualMode.SetManualConfig(global::ManualMode.ManualMode.ManualModeConfig.Aa, "./ManualConfig/AA.json");

            BtnManualMode.Visibility = System.Diagnostics.Debugger.IsAttached ? Visibility.Visible : Visibility.Hidden;


            
            
        }

        private void ManualModeOeffnen(object sender, RoutedEventArgs e)
        {
            if (Plc.GetPlcModus() == "S7-1200")
            {
                Plc.SetTaskRunning(false);
                Plc = new Manual(Datenstruktur, _datenRangieren.RangierenInput, _datenRangieren.RangierenOutput);
            }

            ManualMode.FensterAnzeigen();
        }



        private void ProjekteAnzeigen()
        {
            foreach (var projekt in AlleOrdnerLesen.AlleTestOrdner)
            {
                var rdo = new RadioButton
                {
                    GroupName = "TestProjekte",
                    Name = projekt.Name,
                    FontSize = 14,
                    Content = projekt.Name,
                    VerticalAlignment = VerticalAlignment.Top,
                    Tag = projekt
                };
                rdo.Checked += RadioButton_Checked;
                StackPanel.Children.Add(rdo);
            }
        }

        public void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (!(sender is RadioButton rb) || !(rb.Tag is DirectoryInfo)) return;
            BtnTestWindow.IsEnabled = true;
            BtnTestWindow.Background = new SolidColorBrush(Colors.LawnGreen);

            AktuellesProjekt = rb.Tag as DirectoryInfo;

            if (AktuellesProjekt == null) return;
            var dateiName = $@"{AktuellesProjekt.FullName}\index.html";

            var htmlSeite = File.Exists(dateiName) ? File.ReadAllText(dateiName) : "--??--";

            var dataHtmlSeite = Encoding.UTF8.GetBytes(htmlSeite);
            var stmHtmlSeite = new MemoryStream(dataHtmlSeite, 0, dataHtmlSeite.Length);

            WebBrowser.NavigateToStream(stmHtmlSeite);
        }

        private void TestWindowOeffnen(object sender, RoutedEventArgs e)
        {
            TestWindowAktiv = true;
            _autoTesterWindow = new AutoTesterWindow(AktuellesProjekt);
            _autoTesterWindow.Show();

        }
    }
}
