using System.Windows.Controls;
using System.Windows.Media;

namespace ManualMode
{
    public partial class FensterDi
    {


        public FensterDi(Model.ConfigDi configDi)
        {
            InitializeComponent();

            var posX = 150;
            var posY = 40;
            var abstand = 30;
            foreach (var config in configDi.DigitaleEingaenge)
            {

                var label = new Label
                {
                    Content = config.Bezeichnung, FontFamily = new FontFamily("Arial"), FontSize = 12
                };



                Canvas.SetLeft(label, posX);
                Canvas.SetTop(label, posY );
                Canvas.Children.Add(label);

                posY += abstand;
            }

        }
    }
}
