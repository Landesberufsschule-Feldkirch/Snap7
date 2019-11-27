using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PlcConnect
{
    public partial class MainWindow
    {

        public const float Rahmenbreite_1px = 1;
        public const float Rahmenbreite_5px = 5;

        public void AnzeigeAktualisieren()
        {
            this.Dispatcher.Invoke(() =>
            {
                ButtonRahmenAendern(M1, ButtonStart, Rahmenbreite_5px, Rahmenbreite_1px);
                ButtonRahmenAendern(P1, ButtonStop, Rahmenbreite_5px, Rahmenbreite_1px);

                KreisFarbeUmschalten(M1, KreisBetrieb, Colors.Green, Colors.White);
                KreisFarbeUmschalten(P1, KreisStoerung, Colors.Red, Colors.White);
            });
        }

        void KreisFarbeUmschalten(bool Wert, Ellipse ellipse, Color FarbeEin, Color FarbeAus)
        {
            if (Wert) ellipse.Fill = new SolidColorBrush(FarbeEin);
            else ellipse.Fill = new SolidColorBrush(FarbeAus);
        }

        void ButtonRahmenAendern(bool Wert, Button button, double StaerkeEin, double StaerkeAus)
        {
            if (Wert) button.BorderThickness = new System.Windows.Thickness(StaerkeEin);
            else button.BorderThickness = new System.Windows.Thickness(StaerkeAus);
        }

    }
}
