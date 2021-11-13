using BeschriftungPlc;
using Kommunikation;
using System;

namespace PaternosterLager
{
    public partial class MainWindow
    {
        public PlcDaemon PlcDaemon { get; set; }
        public ConfigPlc.Plc ConfigPlc { get; set; }
        public bool FensterAktiv { get; set; }
        public string VersionInfoLokal { get; set; }
        public const double AnzahlKisten = 16;
        public DisplayPlc.DisplayPlc DisplayPlc { get; set; }
        public BeschriftungenPlc BeschriftungenPlc { get; set; }
        public Datenstruktur Datenstruktur { get; set; }

        public DatenRangieren DatenRangieren { get; set; }
        private readonly ViewModel.ViewModel _viewModel;


        public MainWindow()
        {
            const string versionText = "Paternosterlager";
            const string versionNummer = "V2.0";


            const int anzByteDigInput = 2;
            const int anzByteDigOutput = 2;
            const int anzByteAnalogInput = 2;
            const int anzByteAnalogOutput = 2;

            VersionInfoLokal = versionText + " " + versionNummer;

            Datenstruktur = new Datenstruktur(anzByteDigInput, anzByteDigOutput, anzByteAnalogInput, anzByteAnalogOutput);
            ConfigPlc = new ConfigPlc.Plc("./ConfigPlc");
            BeschriftungenPlc = new BeschriftungenPlc();

            var viewModel = new ViewModel.ViewModel(this, AnzahlKisten);
            InitializeComponent();
            DataContext = viewModel;

            DatenRangieren = new DatenRangieren(viewModel);
            PlcDaemon = new PlcDaemon(Datenstruktur, DatenRangieren.Rangieren);
            DatenRangieren.ReferenzUebergeben(PlcDaemon.Plc);


            /*  
            DisplayPlc = new DisplayPlc.DisplayPlc(Datenstruktur, ConfigPlc, BeschriftungenPlc);
              
            TestAutomat = new TestAutomat.TestAutomat(Datenstruktur, DisplayPlc.EventBeschriftungAktualisieren, BeschriftungenPlc, PlcDaemon.Plc);
            TestAutomat.SetTestConfig("./ConfigTests/");
            TestAutomat.TabItemFuellen(TabItemAutomatischerSoftwareTest, DisplayPlc);
            */
        }

        private void PolygonAufPressed(object sender, System.Windows.Input.MouseButtonEventArgs e) => _viewModel.Paternosterlager.S1 = true;
        private void PolygonAufReleased(object sender, System.Windows.Input.MouseButtonEventArgs e) => _viewModel.Paternosterlager.S1 = false;
        private void PolygonAbPressed(object sender, System.Windows.Input.MouseButtonEventArgs e) => _viewModel.Paternosterlager.S2 = true;
        private void PolygonAbReleased(object sender, System.Windows.Input.MouseButtonEventArgs e) => _viewModel.Paternosterlager.S2 = false;
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e) => FensterAktiv = false;
    }
}