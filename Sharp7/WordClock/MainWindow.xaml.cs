using Kommunikation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WordClock
{
    public partial class MainWindow : Window
    {

        public bool FensterAktiv = true;

        private Logikfunktionen logikfunktionen;
        private DatenRangieren datenRangieren;

        private ViewModel.WordClockViewModel wordClockViewModel;

        public MainWindow()
        {
            wordClockViewModel = new ViewModel.WordClockViewModel(this);

            logikfunktionen = new Logikfunktionen();
            datenRangieren = new DatenRangieren(logikfunktionen);

            InitializeComponent();
            DataContext = wordClockViewModel;

            S7_1200 s7_1200 = new S7_1200(9, 0, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

           // System.Threading.Tasks.Task.Run(() => logikfunktionen.LogikFunktionenTask(FensterAktiv));
          //  System.Threading.Tasks.Task.Run(() => Display_Task(s7_1200, logikfunktionen));

            for (double i = 0; i < 360; i += 30) RotiertesRechteckHinzufuegen(8, 30, i);
            for (double i = 0; i < 360; i += 10) RotiertesRechteckHinzufuegen(2, 10, i);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FensterAktiv = false;
        }

        private void ZeitUebernehmen_Click(object sender, RoutedEventArgs e)
        {
           // logikfunktionen.ZeitUebernehmen();
        }

        private void sldGeschwindigkeit_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
           // logikfunktionen.setGeschwindigkeit(sldGeschwindigkeit.Value);
        }


        private void RotiertesRechteckHinzufuegen(double breite, double hoehe, double winkel)
        {

            Rectangle rectangle = new Rectangle();
            rectangle.Width = breite;
            rectangle.Height = hoehe;
            rectangle.Fill = Brushes.Black;

            RotateTransform rotateTransform = new RotateTransform(winkel, breite/2, 150);
            rectangle.RenderTransform = rotateTransform;

            Canvas.SetLeft(rectangle, 150 - breite / 2);
            Canvas.SetTop(rectangle, 0);



            canvasUhr.Children.Add(rectangle);





        }
    }
}