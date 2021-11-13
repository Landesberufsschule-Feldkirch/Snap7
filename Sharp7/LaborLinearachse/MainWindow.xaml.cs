using BeschriftungPlc;
using Kommunikation;
using System;
using System.Windows;
using System.Windows.Controls;

namespace LaborLinearachse
{
    public partial class MainWindow
    {
        public PlcDaemon PlcDaemon { get; set; }
        //public IPlc Plc { get; set; }
        public string VersionNummer { get; set; }
        public string VersionText { get; set; } = "Labor Linearachse";
        public ConfigPlc.Plc ConfigPlc { get; set; }
        public Datenstruktur Datenstruktur { get; set; }
        public TestAutomat.TestAutomat TestAutomat { get; set; }
        public DisplayPlc.DisplayPlc DisplayPlc { get; set; }
        public BeschriftungenPlc BeschriftungenPlc { get; set; }
        public DatenRangieren DatenRangieren { get; set; }

        private const int AnzByteDigInput = 2;
        private const int AnzByteDigOutput = 1;
        private const int AnzByteAnalogInput = 0;
        private const int AnzByteAnalogOutput = 0;

        public MainWindow()
        {

            VersionNummer = "V2.0";
                       

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput);
            ConfigPlc = new ConfigPlc.Plc("./ConfigPlc");
            BeschriftungenPlc = new BeschriftungenPlc();


            var viewModel = new ViewModel.ViewModel(this);
            InitializeComponent();
            DataContext = viewModel;

            DatenRangieren = new DatenRangieren(viewModel);

            PlcDaemon = new PlcDaemon(Datenstruktur, DatenRangieren.Rangieren);
            /*

            var befehlszeile = Environment.GetCommandLineArgs();
            Plc = befehlszeile.Length == 2 && befehlszeile[1].Contains("CX9020")
                ? new Cx9020(Datenstruktur, DatenRangieren.Rangieren)
                : new S71200(Datenstruktur, DatenRangieren.Rangieren);

Title = PlcDaemon.Plc.GetPlcBezeichnung() + ": " + versionText + " " + VersionNummer;
                    
 */
            DatenRangieren.ReferenzUebergeben(PlcDaemon.Plc);


            DisplayPlc = new DisplayPlc.DisplayPlc(Datenstruktur, ConfigPlc, BeschriftungenPlc);

            TestAutomat = new TestAutomat.TestAutomat(Datenstruktur, DisplayPlc.EventBeschriftungAktualisieren, BeschriftungenPlc, PlcDaemon.Plc);
            TestAutomat.SetTestConfig("./ConfigTests/");
            TestAutomat.TabItemFuellen(TabItemAutomatischerSoftwareTest, DisplayPlc);

            Closing += (_, e) =>
            {
                e.Cancel = true;
                Schliessen();
            };
        }
        private void BetriebsartProjektChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not TabControl tc) return;

            // ReSharper disable once ConvertSwitchStatementToSwitchExpression
            switch (tc.SelectedIndex)
            {
                case 0: Datenstruktur.BetriebsartProjekt = BetriebsartProjekt.LaborPlatte; break;
                case 1: Datenstruktur.BetriebsartProjekt = BetriebsartProjekt.Simulation; break;
                case 2: Datenstruktur.BetriebsartProjekt = BetriebsartProjekt.AutomatischerSoftwareTest; break;
            }

            DisplayPlc.SetBetriebsartProjekt(Datenstruktur);
        }
        private void PlcButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (DisplayPlc.FensterAktiv) DisplayPlc.Schliessen();
            else DisplayPlc.Oeffnen();
        }
        private void Schliessen()
        {
            DisplayPlc.TaskBeenden();
            TestAutomat.TaskBeenden();
            Application.Current.Shutdown();
        }
    }
}