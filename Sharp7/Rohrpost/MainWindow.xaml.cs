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

        }
    }
}