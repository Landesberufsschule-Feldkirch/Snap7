using Kommunikation;
using SoftCircuits.Silk;
using System.Diagnostics;
using System.IO;
using TestAutomat.PlcDisplay.Config;

namespace TestAutomat.AutoTester.Model
{
    public class AutoTester
    {
        public enum TestErgebnis
        {
            // ReSharper disable UnusedMember.Global
            CompilerStart = 0,
            CompilerErfolgreich,
            CompilerError,
            TestStart,
            TestEnde,
            Aktiv,
            Init,
            Erfolgreich,
            Timeout,
            Fehler,
            UnbekanntesErgebnis
            // ReSharper restore UnusedMember.Global
        }

        public GetPlcConfig GetPlcConfig { get; set; }
        public AutoTesterWindow AutoTesterWindow { get; set; }
        public Datenstruktur Datenstruktur { get; set; }
        public Stopwatch SilkStopwatch { get; set; }

        private readonly bool _compilerlaufErfolgreich;
        private readonly CompiledProgram _compiledProgram;

        public AutoTester(AutoTesterWindow autoTesterWindow, FileSystemInfo aktuellesProjekt, Datenstruktur datenstruktur)
        {
            Compiler compiler;
            SilkStopwatch = new Stopwatch();

            AutoTesterWindow = autoTesterWindow;
            Datenstruktur = datenstruktur;
            GetPlcConfig = new GetPlcConfig(aktuellesProjekt);

            Silk.Silk.ReferenzenUebergeben(autoTesterWindow, datenstruktur, SilkStopwatch);

            autoTesterWindow.UpdateDataGrid(new TestAusgabe(
                autoTesterWindow.DataGridId++,
                "0",
                TestErgebnis.CompilerStart,
                 "", "", ""));

            SilkStopwatch.Start();
            (_compilerlaufErfolgreich, compiler, _compiledProgram) = Silk.Silk.Compile(aktuellesProjekt + "\\testSource.ssc");

            if (_compilerlaufErfolgreich)
            {
                autoTesterWindow.UpdateDataGrid(new TestAusgabe(
                    autoTesterWindow.DataGridId++,
                    $"{SilkStopwatch.ElapsedMilliseconds}ms",
                    TestErgebnis.CompilerErfolgreich,
                     "", "", ""));

                System.Threading.Tasks.Task.Run(TestRunnerTask);
            }
            else
            {
                foreach (var error in compiler.Errors)
                {
                    autoTesterWindow.UpdateDataGrid(new TestAusgabe(
                        autoTesterWindow.DataGridId++,
                        $"{SilkStopwatch.ElapsedMilliseconds}ms",
                        TestErgebnis.CompilerError,
                        error.ToString(), "", ""));
                }
            }
        }
        private void TestRunnerTask()
        {
            SilkStopwatch.Restart();
            if (_compilerlaufErfolgreich) Silk.Silk.RunProgram(_compiledProgram);
        }
    }
}