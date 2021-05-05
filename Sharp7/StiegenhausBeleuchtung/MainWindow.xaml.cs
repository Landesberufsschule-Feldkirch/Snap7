using System;
using Kommunikation;
using System.Text;

namespace StiegenhausBeleuchtung
{
    public partial class MainWindow
    {
       public IPlc Plc { get; set; }
        public string VersionInfoLokal { get; set; }
        public string VersionNummer { get; set; }
        public Datenstruktur Datenstruktur { get; set; }

        private const int AnzByteDigInput = 1;
        private const int AnzByteDigOutput = 1;
        private const int AnzByteAnalogInput = 0;
        private const int AnzByteAnalogOutput = 0;

        public MainWindow()
        {
            const string versionText = "Stiegenhausbeleuchtung";
            VersionNummer = "V2.0";
            VersionInfoLokal = versionText + " " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput)
            {
                VersionInputSps = Encoding.ASCII.GetBytes(VersionInfoLokal)
            };
            var viewModel = new ViewModel.ViewModel(this);
            var datenRangieren = new DatenRangieren(viewModel);

            InitializeComponent();
            DataContext = viewModel;

            var befehlszeile = Environment.GetCommandLineArgs();
            var plcType = befehlszeile[1][5..];
            if (plcType == "CX9020") Plc = new Cx9020(Datenstruktur, datenRangieren.Rangieren);
            else Plc = new S71200(Datenstruktur, datenRangieren.Rangieren);

            datenRangieren.ReferenzUebergeben(Plc);
        }
    }
}