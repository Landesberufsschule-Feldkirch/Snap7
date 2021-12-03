using BeschriftungPlc;
using Kommunikation;
using System.Windows;
using System.Windows.Controls;
using DigitalerZwillingMitAutoTests;


namespace Kata
{
    public partial class MainWindow
    {

        public DtAutoTests DtAutoTests;
        public string VersionInfoLokal { get; set; }
        public string VersionNummer { get; set; }
        public DisplayPlc.DisplayPlc DisplayPlc { get; set; }
        public DatenRangieren DatenRangieren { get; set; }



        public MainWindow()
        {
            const string versionText = "Kata";
            const string versionNummer = "V2.0";

            const int anzByteDigInput = 1;
            const int anzByteDigOutput = 1;
            const int anzByteAnalogInput = 0;
            const int anzByteAnalogOutput = 0;

            VersionInfoLokal = versionText + " " + versionNummer;


            var viewModel = new ViewModel.ViewModel(this);
            InitializeComponent();
            DataContext = viewModel;
            

            DtAutoTests = new DtAutoTests();

            DtAutoTests.SetTabItemAutoTests(TabItemAutomatischerSoftwareTest);
            DtAutoTests.SetConfigPlc("./ConfigPlc");
            DtAutoTests.SetConfigTests("./ConfigTests/");
            DtAutoTests.SetInputOutput(anzByteDigInput, anzByteDigOutput, anzByteAnalogInput, anzByteAnalogOutput);
          //  DtAutoTests.SetDatenRangierenCallback(DatenRangieren.Rangieren);

            DtAutoTests.Starten();

         //   DatenRangieren.ReferenzUebergeben(DtAutoTests.PlcDaemon.Plc);

 DatenRangieren = new DatenRangieren();
            DatenRangieren.SetReferenzModel(viewModel.Kata);


            //   Datenstruktur = new Datenstruktur(anzByteDigInput, anzByteDigOutput, anzByteAnalogInput, anzByteAnalogOutput);
            //  ConfigPlc = new ConfigPlc.Plc("./ConfigPlc");
            //  BeschriftungenPlc = new BeschriftungenPlc();
            //  PlcDaemon = new PlcDaemon(Datenstruktur, DatenRangieren.Rangieren);
            //DisplayPlc = new DisplayPlc.DisplayPlc(Datenstruktur, ConfigPlc, BeschriftungenPlc);

            //   TestAutomat = new TestAutomat.TestAutomat(Datenstruktur, DisplayPlc.EventBeschriftungAktualisieren, BeschriftungenPlc, PlcDaemon.Plc);
            //   TestAutomat.SetTestConfig("./ConfigTests/");
            //    TestAutomat.TabItemFuellen(TabItemAutomatischerSoftwareTest, DisplayPlc);


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
                case 0: DtAutoTests.Datenstruktur.BetriebsartProjekt = BetriebsartProjekt.Simulation; break;
                case 1: DtAutoTests.Datenstruktur.BetriebsartProjekt = BetriebsartProjekt.AutomatischerSoftwareTest; break;
            }

            DtAutoTests?.DisplayPlc.SetBetriebsartProjekt(DtAutoTests.Datenstruktur);
        }
        private void PlcButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (DtAutoTests.DisplayPlc.FensterAktiv) DtAutoTests.DisplayPlc.Schliessen();
            else DtAutoTests.DisplayPlc.Oeffnen();
        }
        private void Schliessen()
        {
            DtAutoTests.DisplayPlc.TaskBeenden();
            DtAutoTests.TestAutomat.TaskBeenden();
            Application.Current.Shutdown();
        }
    }
}