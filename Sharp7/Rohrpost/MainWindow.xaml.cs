using System.Windows;

namespace Rohrpost
{
    public partial class MainWindow : Window
    {

        Logikfunktionen logikfunktionen;
        DatenRangieren datenRangieren;

        public MainWindow()
        {
            logikfunktionen = new Logikfunktionen();
            datenRangieren = new DatenRangieren(logikfunktionen);

            InitializeComponent();

            S7_1200 s7_1200 = new S7_1200(10, 0, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }
    }
}
