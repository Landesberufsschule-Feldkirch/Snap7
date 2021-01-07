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
                    case TestBefehle.Init: AlleTestBefehle.TestBefehlInit(einzelneZeile, _autoTesterViewModel); break;
                    case TestBefehle.EingaengeTesten: AlleTestBefehle.TestBefehlEingaengeTesten(einzelneZeile, _autoTesterViewModel); break;
                    case TestBefehle.Pause: AlleTestBefehle.TestBefehlPause(einzelneZeile, _autoTesterViewModel); break;

                    case TestBefehle.Default: break;

                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }
      
    }
}