using AutomatischesLagersystem.DreiD;
using Kommunikation;
using System.Windows;

namespace AutomatischesLagersystem
{
    public partial class MainWindow : Window
    {
        public DreiDElemente[] BediengeraetStartpositionen { get; set; }
        public DreiDElemente[] KistenStartPositionen { get; set; }
        public DreiDKisten[] KistenAktuellePositionen { get; set; }
        public SetManual.SetManualWindow SetManualWindow { get; set; }
        public bool KisteLiegtAufDemRegalbediengeraet { get; set; }
        public Model.RegalBedienGeraet RegalBedienGeraet { get; set; }
        public bool DebugWindowAktiv { get; set; }
        public S7_1200 S7_1200 { get; set; }
        public bool FensterAktiv { get; set; }
        public DreiDErstellen DreiD { get; set; }
        public int[] DreiDModelleIds { get; set; }


        private readonly DatenRangieren datenRangieren;
        private readonly ViewModel.ViewModel viewModel;


        public MainWindow()
        {
            FensterAktiv = true;
            KisteLiegtAufDemRegalbediengeraet = false;
            BediengeraetStartpositionen = new DreiDElemente[4];
            KistenStartPositionen = new DreiDElemente[100];
            KistenAktuellePositionen = new DreiDKisten[100];
            DreiDModelleIds = new int[ViewModel.VisuAnzeigen.IdEintraege.AnzahlEintraege];

            RegalBedienGeraet = new AutomatischesLagersystem.Model.RegalBedienGeraet();

            viewModel = new ViewModel.ViewModel(this);
            datenRangieren = new DatenRangieren(this, viewModel);

            InitializeComponent();

            DataContext = viewModel;
            S7_1200 = new S7_1200(2, 2, 2, 2, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            DreiD = new DreiDErstellen(this);
        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            SetManualWindow = new SetManual.SetManualWindow(viewModel);
            SetManualWindow.Show();
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e) => FensterAktiv = false;
    }
}
