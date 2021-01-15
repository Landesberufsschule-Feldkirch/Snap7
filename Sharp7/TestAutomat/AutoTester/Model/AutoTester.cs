using System;
using System.Collections.Generic;
using System.IO;
using TestAutomat.AutoTester.Config;
using TestAutomat.PlcDisplay.Config;

namespace TestAutomat.AutoTester.Model
{
    public class AutoTester
    {
        public GetTestConfig GetTestConfig { get; set; }
        public GetPlcConfig GetPlcConfig { get; set; }
        
        private readonly List<TestAusgabe> _ergebnisListe;

        public AutoTester( FileSystemInfo aktuellesProjekt)
        {
            _ergebnisListe = new List<TestAusgabe>();

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
                    case TestBefehle.Init: AddTestErgebnis(AlleTestBefehle.TestBefehlInit(einzelneZeile)); break;
                    case TestBefehle.EingaengeTesten: AddTestErgebnis(AlleTestBefehle.TestBefehlEingaengeTesten(einzelneZeile)); break;
                    case TestBefehle.Pause: AddTestErgebnis(AlleTestBefehle.TestBefehlPause(einzelneZeile)); break;

                    case TestBefehle.Default: break;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }
        private void AddTestErgebnis(TestAusgabe testErgebnis) => _ergebnisListe.Add(testErgebnis);
        public int GetAnzahlErgebnisse() => _ergebnisListe.Count;
        internal TestAusgabe GetTestErgebniss()
        {
            var ergebnis = _ergebnisListe[0];
            _ergebnisListe.RemoveAt(0);
            return ergebnis;
        }
    }
}