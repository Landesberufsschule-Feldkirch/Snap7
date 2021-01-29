using SoftCircuits.Silk;
using System.IO;
using Kommunikation;
using TestAutomat.PlcDisplay.Config;

namespace TestAutomat.AutoTester.Model
{
    public class AutoTester
    {

        public enum TestErgebnis
        {
            // ReSharper disable UnusedMember.Global
            Aktiv = 0,
            Init,
            Erfolgreich,
            Timeout,
            Fehler
            // ReSharper restore UnusedMember.Global
        }

        public GetPlcConfig GetPlcConfig { get; set; }
        public AutoTesterWindow AutoTesterWindow { get; set; }
        public Datenstruktur Datenstruktur { get; set; }

        private readonly bool _compilerlaufErfolgreich;
        private readonly CompiledProgram _compiledProgram;

        public AutoTester(AutoTesterWindow autoTesterWindow, FileSystemInfo aktuellesProjekt, Datenstruktur datenstruktur)
        {
            AutoTesterWindow = autoTesterWindow;
            Datenstruktur = datenstruktur;
            GetPlcConfig = new GetPlcConfig(aktuellesProjekt);

            Silk.Silk.ReferenzenUebergeben(autoTesterWindow);
            (_compilerlaufErfolgreich, _compiledProgram) = Silk.Silk.Compile(aktuellesProjekt + "\\testSource.ssc");

            if (_compilerlaufErfolgreich) System.Threading.Tasks.Task.Run(TestRunnerTask);
        }
        private void TestRunnerTask()
        {
            if (_compilerlaufErfolgreich) Silk.Silk.RunProgram(_compiledProgram);
        }
    }
}