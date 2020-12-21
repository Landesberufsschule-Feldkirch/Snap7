using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using TestAutomat.Model;

namespace TestAutomat.AutoTester
{
    public class AutoTester
    {
        private ObservableCollection<DirectoryInfo> alleTestOrdner;
        private AutoTester _autoTester;
        private OrdnerLesen _alleOrdnerLesen;

        public GetConfig GetConfig { get; set; }

       
        public AutoTester(FileSystemInfo aktuellesProjekt)
        {
            GetConfig = new GetConfig(aktuellesProjekt);

            System.Threading.Tasks.Task.Run(TestRunnerTask);
        }

        public AutoTester(OrdnerLesen alleOrdnerLesen, AutoTester autoTester)
        {
            _alleOrdnerLesen = alleOrdnerLesen;
            _autoTester = autoTester;
        }

        private static void TestRunnerTask()
        {
            while (true)
            {
                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}