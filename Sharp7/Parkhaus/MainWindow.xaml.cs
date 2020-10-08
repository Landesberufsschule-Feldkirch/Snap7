using Kommunikation;
using System.Text;

namespace Parkhaus
{
    public partial class MainWindow
    {

        public IPlc Plc { get; set; }
        public string VersionInfo { get; set; }
        public string VersionNummer { get; set; }
        public Datenstruktur Datenstruktur { get; set; }

        private readonly DatenRangieren _datenRangieren;
        private readonly ViewModel.ViewModel _viewModel;

        private const int AnzByteDigInput = 2;
        private const int AnzByteDigOutput = 2;
        private const int AnzByteAnalogInput = 2;
        private const int AnzByteAnalogOutput = 2;


        public MainWindow()
        {
            const string versionText = "LAP 2019 Foerderanlage";
            VersionNummer = "V2.0";
            VersionInfo = versionText + " - " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput)
            {
                VersionInput = Encoding.ASCII.GetBytes(VersionInfo)
            };

            _viewModel = new ViewModel.ViewModel(this);
            _datenRangieren = new DatenRangieren(this, _viewModel);

            InitializeComponent();
            DataContext = _viewModel;

            Plc = new S7_1200(Datenstruktur, _datenRangieren.RangierenInput, _datenRangieren.RangierenOutput);

        }
    }
}
