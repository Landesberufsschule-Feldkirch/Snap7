using System;
using Kommunikation;
using System.Text;
using System.Windows;

namespace Parkhaus
{
    public partial class MainWindow
    {
       public IPlc Plc { get; set; }
        public string VersionInfoLokal { get; set; }
        public string VersionNummer { get; set; }
        public Datenstruktur Datenstruktur { get; set; }
        public ConfigPlc.Plc ConfigPlc { get; set; }

        public DatenRangieren DatenRangieren { get; set; }

        private const int AnzByteDigInput = 2;
        private const int AnzByteDigOutput = 2;
        private const int AnzByteAnalogInput = 2;
        private const int AnzByteAnalogOutput = 0;

        public MainWindow()
        {
            const string versionText = "Parkhaus";
            VersionNummer = "V2.0";
            VersionInfoLokal = versionText + " " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput)
            {
                VersionInputSps = Encoding.ASCII.GetBytes(VersionInfoLokal)
            };

            var viewModel = new ViewModel.ViewModel(this);
            DatenRangieren = new DatenRangieren(viewModel);

            InitializeComponent();
            DataContext = viewModel;

            var befehlszeile = Environment.GetCommandLineArgs();
            var plcType = befehlszeile[1][5..];
            if (plcType == "CX9020") Plc = new Cx9020(Datenstruktur, DatenRangieren.Rangieren);
            else Plc = new S71200(Datenstruktur, DatenRangieren.Rangieren);

            DatenRangieren.ReferenzUebergeben(Plc);

            ConfigPlc = new ConfigPlc.Plc("./ConfigPlc");
        }
    }
}
