using Kommunikation;
using System.Windows;

namespace Rohrpost
{
    public partial class MainWindow : Window
    {
        private Logikfunktionen logikfunktionen;
        private DatenRangieren datenRangieren;

        public MainWindow()
        {
            logikfunktionen = new Logikfunktionen(this);
            datenRangieren = new DatenRangieren(this);

            InitializeComponent();

            S7_1200 s7_1200 = new S7_1200(10, 0, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }
    }
}