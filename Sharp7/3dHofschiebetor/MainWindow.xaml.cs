using _3dHofschiebetor.DreiD;
using Kommunikation;
using System;
using System.Windows;


namespace _3dHofschiebetor
{
    public partial class MainWindow
    {
        public IPlc Plc { get; set; }
        public DreiDElemente[] HofDreiDElementes { get; set; }
        public bool DebugWindowAktiv { get; set; }
        public IPlc S71200 { get; set; }
        public bool FensterAktiv { get; set; }
        public string VersionInfoLokal { get; set; }
        public string VersionNummer { get; set; }

        public SetManual.SetManualWindow SetManualWindow { get; set; }

        public Datenstruktur Datenstruktur { get; set; }

        public DatenRangieren DatenRangieren { get; set; }

        public DreiDErstellen DreiD { get; set; }

        private readonly ViewModel.ViewModel _viewModel;

        private const int AnzByteDigInput = 2;
        private const int AnzByteDigOutput = 1;
        private const int AnzByteAnalogInput = 0;
        private const int AnzByteAnalogOutput = 0;

        public MainWindow()
        {
            const string versionText = "3D Hofschiebetor";
            VersionNummer = "V2.0";

            VersionInfoLokal = versionText + " " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput);

            _viewModel = new ViewModel.ViewModel(this);
            DatenRangieren = new DatenRangieren(this, _viewModel);

            InitializeComponent();

            DataContext = _viewModel;

            var befehlszeile = Environment.GetCommandLineArgs();
            if (befehlszeile.Length == 2 && befehlszeile[1].Contains("CX9020"))
            {
                Plc = new Cx9020(Datenstruktur, DatenRangieren.Rangieren);
            }
            else
            {
                Plc = new S71200(Datenstruktur, DatenRangieren.Rangieren);
            }

            DatenRangieren.ReferenzUebergeben(Plc);

            DreiD = new DreiDErstellen(this);

            //BtnDebugWindow.Visibility = System.Diagnostics.Debugger.IsAttached ? Visibility.Visible : Visibility.Hidden;
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