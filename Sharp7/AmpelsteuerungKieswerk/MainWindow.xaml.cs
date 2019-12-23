using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace AmpelsteuerungKieswerk
{
    public partial class MainWindow : Window
    {
        public bool TaskAktiv { get; set; }
        public bool DatenRangierenAktiv { get; set; } = true;
        public bool FensterAktiv { get; set; } = true;

        public int FahrzeugPersonGeklickt { get; set; } = -1;

        private readonly S7_1200 S7_1200;
        private DatenRangieren DatenRangieren;

        public MainWindow()
        {
            InitializeComponent();
            AlleLKWInitialisieren();
            DatenRangieren = new DatenRangieren(this);
            S7_1200 = new S7_1200(this, DatenRangieren);
            
            System.Threading.Tasks.Task.Run(() => Logikfunktionen_Task());
            System.Threading.Tasks.Task.Run(() => Display_Task());
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            var lkw = btn.Tag as LKW;
            lkw?.Losfahren();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) { 
            
            FensterAktiv = false;
            Application.Current.Shutdown();
        
        
        }
        private void AlleLinksParken_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button btn in gAlleButton)
            {
                var lkw = btn.Tag as LKW;
                lkw?.LinksParken();
            }
        }
        private void AlleRechtsParken_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button btn in gAlleButton)
            {
                var lkw = btn.Tag as LKW;
                lkw?.RechtsParken();
            }
        }

        public void SpsDatenschreiben(string text)
        {
            Dispatcher.Invoke(() =>
            {
                lbl_PlcPing.Content = text;
            });
        }
    }
}

