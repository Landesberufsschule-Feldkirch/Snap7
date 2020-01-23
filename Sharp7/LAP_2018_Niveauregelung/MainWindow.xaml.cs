using Kommunikation;
using System.Windows;
using System.Windows.Controls;

namespace LAP_2018_Niveauregelung
{
    public partial class MainWindow : Window
    {
        public double Pegel = 0.95;

        public bool B1 = false;
        public bool B2 = false;
        public bool B3 = false;
        public bool F1 = true;
        public bool F2 = true;
        public bool M1 = false;
        public bool M2 = false;
        public bool Y1 = false;

        public bool S1 = false;
        public bool S2 = false;
        public bool S3 = false;
        public bool P1 = false;
        public bool P2 = false;
        public bool P3 = false;

        public bool FensterAktiv = true;

        private Logikfunktionen logikfunktionen;
        private DatenRangieren datenRangieren;

        public MainWindow()
        {
            logikfunktionen = new Logikfunktionen(this);
            datenRangieren = new DatenRangieren(this);

            InitializeComponent();

            S7_1200 s7_1200 = new S7_1200(2, 2, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            System.Threading.Tasks.Task.Run(() => logikfunktionen.Logikfunktionen_Task());
            System.Threading.Tasks.Task.Run(() => Display_Task());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FensterAktiv = false;
        }

        private void VentilClick(object sender, RoutedEventArgs e)
        {
            Y1 = !Y1;
        }

        private void BtnF1_Click(object sender, RoutedEventArgs e)
        {
            F1 = !F1;
        }

        private void BtnF2_Click(object sender, RoutedEventArgs e)
        {
            F2 = !F2;
        }

        private void btnS1_Click(object sender, RoutedEventArgs e)
        {
            S1 = ButtonFunktionPressReleaseAendern(btnS1);
        }

        private void btnS2_Click(object sender, RoutedEventArgs e)
        {
            S2 = !ButtonFunktionPressReleaseAendern(btnS2);
        }

        private void btnS3_Click(object sender, RoutedEventArgs e)
        {
            S3 = ButtonFunktionPressReleaseAendern(btnS3);
        }

        private bool ButtonFunktionPressReleaseAendern(Button knopf)
        {
            if (knopf.ClickMode == System.Windows.Controls.ClickMode.Press)
            {
                knopf.ClickMode = System.Windows.Controls.ClickMode.Release;
                return true;
            }
            else
            {
                knopf.ClickMode = System.Windows.Controls.ClickMode.Press;
                return false;
            }
        }
    }
}