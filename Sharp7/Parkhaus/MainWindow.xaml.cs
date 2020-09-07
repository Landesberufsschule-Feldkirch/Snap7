using Kommunikation;

namespace Parkhaus
{
    public partial class MainWindow
    {

        public S7_1200 S71200 { get; set; }
        public string VersionInfo { get; set; }
        public string VersionNummer { get; set; }

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

            _viewModel = new ViewModel.ViewModel(this);
            var datenRangieren = new DatenRangieren(this, _viewModel);

            InitializeComponent();
            DataContext = _viewModel;

            S71200 = new S7_1200(VersionInfo.Length, AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

        }
    }
}
