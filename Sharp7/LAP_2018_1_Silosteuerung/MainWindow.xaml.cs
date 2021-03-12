using Kommunikation;
using System.Text;
using System.Windows;
using WpfAnimatedGif;

namespace LAP_2018_1_Silosteuerung
{
    public partial class MainWindow
    {
        public bool AnimationGestartet { get; set; }
        public ImageAnimationController Controller { get; set; }
        public IPlc Plc { get; set; }
        public string VersionInfoLokal { get; set; }
        public string VersionNummer { get; set; }
        public Datenstruktur Datenstruktur { get; set; }
        public ConfigPlc.Plc ConfigPlc { get; set; }

        public DatenRangieren DatenRangieren { get; set; }
        private const int AnzByteDigInput = 1;
        private const int AnzByteDigOutput = 1;
        private const int AnzByteAnalogInput = 2;
        private const int AnzByteAnalogOutput = 0;

        public MainWindow()
        {
            const string versionText = "2018/1 Silosteuerung";
            VersionNummer = "V2.0";
            VersionInfoLokal = versionText + " " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput);

            var viewModel = new ViewModel.ViewModel(this);
            DatenRangieren = new DatenRangieren(viewModel);

            InitializeComponent();
            DataContext = viewModel;

            Plc = new S71200(Datenstruktur, DatenRangieren.RangierenInput, DatenRangieren.RangierenOutput);

            ConfigPlc = new ConfigPlc.Plc("./ConfigPlc");

            Datenstruktur.BetriebsartProjekt = BetriebsartProjekt.Simulation;
        }
        private void AnimatedLoaded(object sender, RoutedEventArgs e)
        {
            AnimationGestartet = true;
            Controller = ImageBehavior.GetAnimationController(ImgSchneckenfoerderer);
        }
    }
}