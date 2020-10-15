﻿using Kommunikation;
using System.Text;

namespace LAP_2010_1_Kompressoranlage
{
    public partial class MainWindow
    {
        public IPlc Plc { get; set; }
        public string VersionInfo { get; set; }
        public string VersionNummer { get; set; }
        public Datenstruktur Datenstruktur { get; set; }

        private const int AnzByteDigInput = 1;
        private const int AnzByteDigOutput = 1;
        private const int AnzByteAnalogInput = 0;
        private const int AnzByteAnalogOutput = 0;

        public MainWindow()
        {
            const string versionText = "LAP 2010/1 Kompressoranlage";
            VersionNummer = "V2.0";

            VersionInfo = versionText + " - " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput)
            {
                VersionInput = Encoding.ASCII.GetBytes(VersionInfo)
            };


            var viewModel = new ViewModel.ViewModel(this);
            var datenRangieren = new DatenRangieren(viewModel);

            InitializeComponent();
            DataContext = viewModel;

            Plc = new S71200(Datenstruktur, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }
    }
}