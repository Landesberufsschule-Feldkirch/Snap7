using Kommunikation;
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
        public S7_1200 S71200 { get; set; }
        public string VersionInfo { get; set; }
        public string VersionNummer { get; set; }

        private readonly ViewModel.ViewModel viewModel;
        private const int anzByteDigInput = 2;
        private const int anzByteDigOutput = 2;
        private const int anzByteAnalogInput = 2;
        private const int anzByteAnalogOutput = 2;

        public MainWindow()
        {
            var versionText = "LAP 2019 Foerderanlage";
            VersionNummer = "V2.0";
            VersionInfo = versionText + " - " + VersionNummer;

            viewModel = new ViewModel.ViewModel(this);
            var datenRangieren = new DatenRangieren(this, viewModel);

            InitializeComponent();
            DataContext = viewModel;

            S71200 = new S7_1200(VersionInfo.Length, anzByteDigInput, anzByteDigOutput, anzByteAnalogInput, anzByteAnalogOutput, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            btnDebugWindow.Visibility = System.Diagnostics.Debugger.IsAttached ? Visibility.Visible : Visibility.Hidden;
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            SetManualWindow = new SetManualWindow(viewModel);
            SetManualWindow.Show();
        }

        private void AnimatedLoaded(object sender, RoutedEventArgs e)
        {
            AnimationGestartet = true;
            Controller = ImageBehavior.GetAnimationController(imgSchneckenfoerderer);
        }

        // ReSharper disable once UnusedParameter.Local
        private void TabItem_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            //
        }
    }
}