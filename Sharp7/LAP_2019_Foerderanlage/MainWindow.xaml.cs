using Kommunikation;
using System.Windows;

namespace LAP_2019_Foerderanlage
{
    public partial class MainWindow : Window
    {
        public SetManualWindow setManualWindow;
        public bool DebugWindowAktiv;

        /*
      

        public short MaterialsiloPegel; // für die SPS1
        public int FuSpeed; // von der SPS


     

       

    

        public bool FensterAktiv = true;

       
        */

 public bool AnimationGestartet;

        private DatenRangieren datenRangieren;
        private ViewModel.FoerderanlageViewModel foerderanlageViewModel;
        public S7_1200 S7_1200 { get; set; }

        public MainWindow()
        {
            foerderanlageViewModel = new ViewModel.FoerderanlageViewModel(this);
            datenRangieren = new DatenRangieren(this, foerderanlageViewModel);

            InitializeComponent();
            DataContext = foerderanlageViewModel;

            S7_1200 = new S7_1200(2, 2, 2, 2, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);


            if (System.Diagnostics.Debugger.IsAttached)
            {
                btnDebugWindow.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                btnDebugWindow.Visibility = System.Windows.Visibility.Hidden;
            }
        }


        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            setManualWindow = new SetManualWindow();
            setManualWindow.Show();
        }
        /*
        private void WagenNachLinks_Click(object sender, RoutedEventArgs e)
        {
            WagenRichtung = Richtung.nachLinks;
        }

        private void WagenNachRechts_Click(object sender, RoutedEventArgs e)
        {
            WagenRichtung = Richtung.nachRechts;
        }

       

    */ 
        
        private void AnimatedLoaded(object sender, RoutedEventArgs e)
        {
            AnimationGestartet = true;
        }
        private void TabItem_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {

        }

    }
}