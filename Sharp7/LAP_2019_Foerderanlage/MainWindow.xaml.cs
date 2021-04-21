using Kommunikation;
using LAP_2019_Foerderanlage.SetManual;
using System.Text;
using System.Windows;
using WpfAnimatedGif;

namespace LAP_2019_Foerderanlage
{
    public partial class MainWindow
    {
        public SetManualWindow SetManualWindow { get; set; }
        public bool DebugWindowAktiv { get; set; }
        public bool AnimationGestartet { get; set; }
        public ImageAnimationController Controller { get; set; }
        public S71200 Plc { get; set; }
        public string VersionInfoLokal { get; set; }
        public string VersionNummer { get; set; }
        public Datenstruktur Datenstruktur { get; set; }

        public DatenRangieren DatenRangieren { get; set; }
        private readonly ViewModel.ViewModel _viewModel;
        private const int AnzByteDigInput = 2;
        private const int AnzByteDigOutput = 2;
        private const int AnzByteAnalogInput = 2;
        private const int AnzByteAnalogOutput = 2;

        public MainWindow()
        {
            const string versionText = "LAP 2019 Foerderanlage";
            VersionNummer = "V2.0";
            VersionInfoLokal = versionText + " " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput);

            _viewModel = new ViewModel.ViewModel(this);
            DatenRangieren = new DatenRangieren(this, _viewModel);

            InitializeComponent();
            DataContext = _viewModel;

            Plc = new S71200(Datenstruktur, DatenRangieren.RangierenInput, DatenRangieren.RangierenOutput);

            BtnDebugWindow.Visibility = System.Diagnostics.Debugger.IsAttached ? Visibility.Visible : Visibility.Hidden;

            Datenstruktur.BetriebsartProjekt = BetriebsartProjekt.Simulation;
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