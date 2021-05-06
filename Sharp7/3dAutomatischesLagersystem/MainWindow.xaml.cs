using AutomatischesLagersystem.DreiD;
using Kommunikation;
using System;
using System.Windows;

namespace AutomatischesLagersystem
{
    public partial class MainWindow
    {
        public IPlc Plc { get; set; }
        public DreiDElemente[] BediengeraetStartpositionen { get; set; }
        public DreiDElemente[] KistenStartPositionen { get; set; }
        public DreiDKisten[] KistenAktuellePositionen { get; set; }
        public SetManual.SetManualWindow SetManualWindow { get; set; }
        public bool KisteLiegtAufDemRegalbediengeraet { get; set; }
        public Model.RegalBedienGeraet RegalBedienGeraet { get; set; }
        public bool DebugWindowAktiv { get; set; }
        public bool FensterAktiv { get; set; }
        public DreiDErstellen DreiD { get; set; }
        public int[] DreiDModelleIds { get; set; }
        public string VersionInfoLokal { get; set; }
        public string VersionNummer { get; set; }

        public Datenstruktur Datenstruktur { get; set; }

        public DatenRangieren DatenRangieren { get; set; }
        private readonly ViewModel.ViewModel _viewModel;
        private const int AnzByteDigInput = 2;
        private const int AnzByteDigOutput = 2;
        private const int AnzByteAnalogInput = 2;
        private const int AnzByteAnalogOutput = 2;

        public MainWindow()
        {
            //BetriebsartProjekt = S71200.BetriebsartProjekt.LaborPlatte;

            const string versionText = "3D Automatisches Lagersystem";
            VersionNummer = "V2.0";
            VersionInfoLokal = versionText + " " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput);



            FensterAktiv = true;
            KisteLiegtAufDemRegalbediengeraet = false;
            BediengeraetStartpositionen = new DreiDElemente[4];
            KistenStartPositionen = new DreiDElemente[100];
            KistenAktuellePositionen = new DreiDKisten[100];
            DreiDModelleIds = new int[ViewModel.IdEintraege.AnzahlEintraege];

            RegalBedienGeraet = new Model.RegalBedienGeraet();

            _viewModel = new ViewModel.ViewModel(this);
            DatenRangieren = new DatenRangieren(this, _viewModel);

            InitializeComponent();

            DataContext = _viewModel;

            var befehlszeile = Environment.GetCommandLineArgs();
            var plcType = befehlszeile[1].Substring(5);
            if (plcType == "CX9020") Plc = new Cx9020(Datenstruktur, DatenRangieren.Rangieren);
            else Plc = new S71200(Datenstruktur, DatenRangieren.Rangieren);

            DatenRangieren.ReferenzUebergeben(Plc);

            DreiD = new DreiDErstellen(this);

            btnDebugWindow.Visibility = System.Diagnostics.Debugger.IsAttached ? Visibility.Visible : Visibility.Hidden;
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            SetManualWindow = new SetManual.SetManualWindow(_viewModel);
            SetManualWindow.Show();
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e) => FensterAktiv = false;
    }
}