using BeschriftungPlc;
using Kommunikation;
using System.Windows;
using System.Windows.Controls;

namespace LAP_2018_3_Hydraulikaggregat
{
    public partial class MainWindow
    {
        public PlcDaemon PlcDaemon { get; set; }
        public string VersionInfoLokal { get; set; }
        public ConfigPlc.Plc ConfigPlc { get; set; }
        public Datenstruktur Datenstruktur { get; set; }
        public TestAutomat.TestAutomat TestAutomat { get; set; }
        public DisplayPlc.DisplayPlc DisplayPlc { get; set; }
        public BeschriftungenPlc BeschriftungenPlc { get; set; }
        public DatenRangieren DatenRangieren { get; set; }



        public MainWindow()
        {
            const string versionText = "LAP 2018/3 Hydraulikaggregat";
            const string versionNummer = "V2.0";

            const int anzByteDigInput = 2;
            const int anzByteDigOutput = 2;
            const int anzByteAnalogInput = 0;
            const int anzByteAnalogOutput = 0;


            VersionInfoLokal = versionText + " " + versionNummer;

            Datenstruktur = new Datenstruktur(anzByteDigInput, anzByteDigOutput, anzByteAnalogInput, anzByteAnalogOutput);
            ConfigPlc = new ConfigPlc.Plc("./ConfigPlc");
            BeschriftungenPlc = new BeschriftungenPlc();

            var viewModel = new ViewModel.ViewModel(this);
            InitializeComponent();
            DataContext = viewModel;

            DatenRangieren = new DatenRangieren(viewModel);
            PlcDaemon = new PlcDaemon(Datenstruktur, DatenRangieren.Rangieren);
            DatenRangieren.ReferenzUebergeben(PlcDaemon.Plc);

            DisplayPlc = new DisplayPlc.DisplayPlc(Datenstruktur, ConfigPlc, BeschriftungenPlc);

            TestAutomat = new TestAutomat.TestAutomat(Datenstruktur, DisplayPlc.EventBeschriftungAktualisieren, BeschriftungenPlc, PlcDaemon.Plc);
            TestAutomat.SetTestConfig("./ConfigTests/");
            TestAutomat.TabItemFuellen(TabItemAutomatischerSoftwareTest, DisplayPlc);

            Closing += (_, e) =>
            {
                e.Cancel = true;
                Schliessen();
            };
        }
        private void BetriebsartProjektChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not TabControl tc) return;

            // ReSharper disable once ConvertSwitchStatementToSwitchExpression
            switch (tc.SelectedIndex)
            {
                case 0: Datenstruktur.BetriebsartProjekt = BetriebsartProjekt.Simulation; break;
                case 1: Datenstruktur.BetriebsartProjekt = BetriebsartProjekt.AutomatischerSoftwareTest; break;
            }

            DisplayPlc.SetBetriebsartProjekt(Datenstruktur);
        }
        private void PlcButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (DisplayPlc.FensterAktiv) DisplayPlc.Schliessen();
            else DisplayPlc.Oeffnen();
        }
        private void Schliessen()
        {
            DisplayPlc.TaskBeenden();
            TestAutomat.TaskBeenden();
            Application.Current.Shutdown();
        }
    }
}