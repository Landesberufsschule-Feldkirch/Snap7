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
        private readonly ViewModel.ViewModel foerderanlageViewModel;

        public MainWindow()
        {
            foerderanlageViewModel = new ViewModel.ViewModel(this);
            datenRangieren = new DatenRangieren(this, foerderanlageViewModel);

            InitializeComponent();
            DataContext = foerderanlageViewModel;

            S7_1200 = new S7_1200(2, 2, 2, 2, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            SetManualWindow = new SetManualWindow(foerderanlageViewModel);
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