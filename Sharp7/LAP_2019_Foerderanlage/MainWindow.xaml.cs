using BeschriftungPlc;
using Kommunikation;
using LAP_2019_Foerderanlage.SetManual;
using System.Windows;
using WpfAnimatedGif;

namespace LAP_2019_Foerderanlage
{
    public partial class MainWindow
    {
        public PlcDaemon PlcDaemon { get; set; }
        public ConfigPlc.Plc ConfigPlc { get; set; }
        public SetManualWindow SetManualWindow { get; set; }
        public bool DebugWindowAktiv { get; set; }
        public bool AnimationGestartet { get; set; }
        public ImageAnimationController Controller { get; set; }
        public string VersionInfoLokal { get; set; }
        public Datenstruktur Datenstruktur { get; set; }
        public DisplayPlc.DisplayPlc DisplayPlc { get; set; }
        public BeschriftungenPlc BeschriftungenPlc { get; set; }
        public DatenRangieren DatenRangieren { get; set; }

        private readonly ViewModel.ViewModel _viewModel;


        public MainWindow()
        {
            const string versionText = "LAP 2019 Foerderanlage";
            const string versionNummer = "V2.0";

            const int anzByteDigInput = 2;
            const int anzByteDigOutput = 2;
            const int anzByteAnalogInput = 2;
            const int anzByteAnalogOutput = 2;


            VersionInfoLokal = versionText + " " + versionNummer;

            Datenstruktur = new Datenstruktur(anzByteDigInput, anzByteDigOutput, anzByteAnalogInput, anzByteAnalogOutput);
            ConfigPlc = new ConfigPlc.Plc("./ConfigPlc");
            BeschriftungenPlc = new BeschriftungenPlc();

            _viewModel = new ViewModel.ViewModel(this);
            InitializeComponent();
            DataContext = _viewModel;

            DatenRangieren = new DatenRangieren(this, _viewModel);
            PlcDaemon = new PlcDaemon(Datenstruktur, DatenRangieren.Rangieren);
            DatenRangieren.ReferenzUebergeben(PlcDaemon.Plc);

            /*
            DisplayPlc = new DisplayPlc.DisplayPlc(Datenstruktur, ConfigPlc, BeschriftungenPlc);

            TestAutomat = new TestAutomat.TestAutomat(Datenstruktur, DisplayPlc.EventBeschriftungAktualisieren, BeschriftungenPlc, PlcDaemon.Plc);
            TestAutomat.SetTestConfig("./ConfigTests/");
            TestAutomat.TabItemFuellen(TabItemAutomatischerSoftwareTest, DisplayPlc);
            */
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            SetManualWindow = new SetManualWindow(_viewModel);
            SetManualWindow.Show();
        }

        private void AnimatedLoaded(object sender, RoutedEventArgs e)
        {
            AnimationGestartet = true;
            Controller = ImageBehavior.GetAnimationController(ImgSchneckenfoerderer);
        }

        // ReSharper disable once UnusedParameter.Local
        private void TabItem_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            //
        }
    }
}