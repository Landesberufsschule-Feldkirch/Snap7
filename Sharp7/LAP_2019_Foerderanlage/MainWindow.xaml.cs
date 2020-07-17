using Kommunikation;
using System.Windows;
using WpfAnimatedGif;

namespace LAP_2019_Foerderanlage
{
    public partial class MainWindow : Window
    {
        public SetManualWindow SetManualWindow { get; set; }
        public bool DebugWindowAktiv { get; set; }
        public bool AnimationGestartet { get; set; }
        public WpfAnimatedGif.ImageAnimationController Controller { get; set; }
        public S7_1200 S7_1200 { get; set; }

        private readonly DatenRangieren datenRangieren;
        private readonly ViewModel.ViewModel viewModel;
        private const int anzByteVersion = 10;
        private const int anzByteDigInput = 2;
        private const int anzByteDigOutput = 2;
        private const int anzByteAnalogInput = 2;
        private const int anzByteAnalogOutput = 2;
        public MainWindow()
        {
            viewModel = new ViewModel.ViewModel(this);
            datenRangieren = new DatenRangieren(this, viewModel);

            InitializeComponent();
            DataContext = viewModel;

            S7_1200 = new S7_1200(anzByteVersion, anzByteDigInput, anzByteDigOutput, anzByteAnalogInput, anzByteAnalogOutput, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            if (System.Diagnostics.Debugger.IsAttached) btnDebugWindow.Visibility = System.Windows.Visibility.Visible;
            else btnDebugWindow.Visibility = System.Windows.Visibility.Hidden;
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

        private void TabItem_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            //
        }
    }
}