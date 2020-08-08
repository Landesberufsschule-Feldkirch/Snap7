using Kommunikation;
using System.Windows;
using LAP_2019_Foerderanlage.SetManual;
using WpfAnimatedGif;

namespace LAP_2019_Foerderanlage
{
    public partial class MainWindow
    {
        public SetManualWindow SetManualWindow { get; set; }
        public bool DebugWindowAktiv { get; set; }
        public bool AnimationGestartet { get; set; }
        public ImageAnimationController Controller { get; set; }
        public S7_1200 S71200 { get; set; }
        public string VersionInfo { get; set; }
        public string VersionNummer { get; set; }

        private readonly ViewModel.ViewModel _viewModel;
        private const int AnzByteDigInput = 2;
        private const int AnzByteDigOutput = 2;
        private const int AnzByteAnalogInput = 2;
        private const int AnzByteAnalogOutput = 2;

        public MainWindow()
        {
            const string versionText = "LAP 2019 Foerderanlage";
            VersionNummer = "V2.0";
            VersionInfo = versionText + " - " + VersionNummer;

            _viewModel = new ViewModel.ViewModel(this);
            var datenRangieren = new DatenRangieren(this, _viewModel);

            InitializeComponent();
            DataContext = _viewModel;

            S71200 = new S7_1200(VersionInfo.Length, AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            BtnDebugWindow.Visibility = System.Diagnostics.Debugger.IsAttached ? Visibility.Visible : Visibility.Hidden;
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