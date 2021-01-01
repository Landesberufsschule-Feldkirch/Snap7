using System.IO;
using TestAutomat.Model;

namespace TestAutomat.AutoTester
{
    public class AutoTester
    {
        private readonly ViewModel.ViewModel _viewModel;

        public GetConfig GetConfig { get; set; }


        public AutoTester(ViewModel.ViewModel viewModel, FileSystemInfo aktuellesProjekt)
        {
            _viewModel = viewModel;

            GetConfig = new GetConfig(aktuellesProjekt);
            GetConfig.KonfigurationTesten();


            System.Threading.Tasks.Task.Run(TestRunnerTask);
        }

        private void TestRunnerTask()
        {
            foreach (var einzelneZeile in GetConfig.TestConfig.AutomatischeSoftwareTests)
            {
                ZeileAusfuehren(einzelneZeile);
            }

            // ReSharper disable once FunctionNeverReturns
        }

        private void ZeileAusfuehren(TestsEinstellungen testZeile)
        {
            _viewModel.ViAnzeige.AddEinzelneZeileAnzeigen(
                new TestAusgabe(testZeile.LaufendeNr,
                    (int)testZeile.EingaengeBitmuster.GetDec(),
                    (int)testZeile.AusgaengeBitmuster.GetDec(),
                    testZeile.Kommentar));
        }
    }
}