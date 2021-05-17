using Kommunikation;
using SoftCircuits.Silk;
using System.Diagnostics;
using System.IO;

namespace TestAutomat.Model
{
    public class AutoTester
    {
        public enum TestErgebnis
        {
            // ReSharper disable UnusedMember.Global
            Aktiv,
            AufBitmusterWarten,
            CompilerErfolgreich,
            CompilerError,
            Erfolgreich,
            Fehler,
            ImpulsWarZuKurz,
            ImpulsWarZuLang,
            Init,
            Kommentar,
            TestEnde,
            TestStart,
            Timeout,
            UnbekanntesErgebnis,
            Version,
            CompilerStart
            // ReSharper restore UnusedMember.Global
        }

        public AutoTesterWindow AutoTesterWindow { get; set; }
        public Datenstruktur Datenstruktur { get; set; }
        public Stopwatch SilkStopwatch { get; set; }

        private readonly bool _compilerlaufErfolgreich;
        private readonly CompiledProgram _compiledProgram;

        public AutoTester(AutoTesterWindow autoTesterWindow, FileSystemInfo aktuellesProjekt, Datenstruktur datenstruktur, IPlc plc)
        {
            Compiler compiler;
            SilkStopwatch = new Stopwatch();

            AutoTesterWindow = autoTesterWindow;
            Datenstruktur = datenstruktur;

            Silk.Silk.ReferenzenUebergeben(autoTesterWindow, datenstruktur, SilkStopwatch, plc);

            autoTesterWindow.UpdateDataGrid(new TestAusgabe(
                autoTesterWindow.DataGridId++,
                "0",
                TestErgebnis.CompilerStart,
                 " ",       // DigInput 
                 " ",       // DigOutput Soll
                 " ",       // DigOutput Ist
                 " "));

            SilkStopwatch.Start();
            (_compilerlaufErfolgreich, compiler, _compiledProgram) = Silk.Silk.Compile(aktuellesProjekt + "\\testSource.ssc");

            if (_compilerlaufErfolgreich)
            {
                autoTesterWindow.UpdateDataGrid(new TestAusgabe(
                    autoTesterWindow.DataGridId++,
                    $"{SilkStopwatch.ElapsedMilliseconds}ms",
                    TestErgebnis.CompilerErfolgreich,
                    " ",       // DigInput 
                    " ",       // DigOutput Soll
                    " ",       // DigOutput Ist
                    " "));

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
                        error.ToString(),   // DigInpt
                        " ",                // DigOutput Soll
                        " ",                // DigOutput Ist
                        " "));
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