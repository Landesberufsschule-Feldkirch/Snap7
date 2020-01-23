using Kommunikation;
using System.Windows;

namespace WordClock
{
    public partial class MainWindow : Window
    {
        private ZweitesFenster zweitesFenster = new ZweitesFenster();

        public bool FensterAktiv = true;

        private Logikfunktionen logikfunktionen;
        private DatenRangieren datenRangieren;

        public MainWindow()
        {
            zweitesFenster.Show();

            logikfunktionen = new Logikfunktionen();
            datenRangieren = new DatenRangieren(logikfunktionen);

            InitializeComponent();

            S7_1200 s7_1200 = new S7_1200(10, 0, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            System.Threading.Tasks.Task.Run(() => logikfunktionen.LogikFunktionenTask(FensterAktiv));
            System.Threading.Tasks.Task.Run(() => Display_Task(s7_1200, logikfunktionen));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FensterAktiv = false;
        }

        private void ZeitUebernehmen_Click(object sender, RoutedEventArgs e)
        {
            logikfunktionen.ZeitUebernehmen();
        }

        private void sldGeschwindigkeit_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            logikfunktionen.setGeschwindigkeit(sldGeschwindigkeit.Value);
        }
    }
}