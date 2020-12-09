using _AlleManConfigTesten.Model;

namespace _AlleManConfigTesten
{
    public partial class MainWindow
    {
        private Model.AlleManConfigTesten _alleManConfigTesten;

        public MainWindow()
        {
            _alleManConfigTesten=new AlleManConfigTesten(this);

            InitializeComponent();

            _alleManConfigTesten.Anzeigen();
        }
    }
}
