using System;
using System.IO;
using TestAutomat.AutoTester.Config;
using TestAutomat.PlcDisplay.Config;

namespace TestAutomat.AutoTester.Model
{
    public class AutoTester
    {
        private readonly ViewModel.AutoTesterViewModel _autoTesterViewModel;

        public GetTestConfig GetTestConfig { get; set; }
        public GetPlcConfig GetPlcConfig { get; set; }

        public AutoTester(ViewModel.AutoTesterViewModel autoTesterViewModel, FileSystemInfo aktuellesProjekt)
        {
            _autoTesterViewModel = autoTesterViewModel;

            GetTestConfig = new GetTestConfig(aktuellesProjekt);
            GetPlcConfig = new GetPlcConfig(aktuellesProjekt);
            GetTestConfig.KonfigurationTesten();

            System.Threading.Tasks.Task.Run(TestRunnerTask);
        }
        private void TestRunnerTask()
        {
            foreach (var einzelneZeile in GetTestConfig.TestConfig.AutomatischeSoftwareTests)
            {
                switch (einzelneZeile.Befehl)
                {
                    case TestBefehle.Init: AlleTestBefehle.TestBefehlInit(einzelneZeile); break;
                    case TestBefehle.EingaengeTesten: AlleTestBefehle.TestBefehlEingaengeTesten(einzelneZeile); break;
                    case TestBefehle.Pause: AlleTestBefehle.TestBefehlPause(einzelneZeile); break;

                    case TestBefehle.Default: break;

                    default: throw new ArgumentOutOfRangeException();
                }


                ZeileAusfuehren(einzelneZeile);
            }

            // ReSharper disable once FunctionNeverReturns
        }
        private void ZeileAusfuehren(TestsEinstellungen testZeile)
        {
            _autoTesterViewModel.AutoTesterAnzeige.AddEinzelneZeileAnzeigen(
                new TestAusgabe(testZeile.LaufendeNr,
                    (int)testZeile.EingaengeBitmuster.GetDec(),
                    (int)testZeile.AusgaengeBitmuster.GetDec(),
                    testZeile.Kommentar));
        }
    }
}