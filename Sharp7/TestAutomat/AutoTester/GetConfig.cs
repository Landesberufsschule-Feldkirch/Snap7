using System;
using System.IO;

namespace TestAutomat.AutoTester
{
    public class GetConfig
    {

        public TestConfig TestConfig { get; set; }
        public TestEaBelegung TestEaBelegung { get; set; }

        public GetConfig(FileSystemInfo aktuellesProjekt)
        {
            SetTestConfig(aktuellesProjekt.FullName + "\\testConfig.json");
            SetEaBelegung(aktuellesProjekt.FullName + "\\testEaBelegung.json");
        }

        public void SetTestConfig(string pfad) => TestConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<TestConfig>(File.ReadAllText(pfad), new MyUintConverter());
        private void SetEaBelegung(string pfad) => TestEaBelegung = Newtonsoft.Json.JsonConvert.DeserializeObject<TestEaBelegung>(File.ReadAllText(pfad), new MyUintConverter());
        
        public void KonfigurationTesten()
        {
            var laufendeNr = 0;

            foreach (var testZeile in TestConfig.AutomatischeSoftwareTests)
            {
                if (testZeile.LaufendeNr == laufendeNr)
                {
                    laufendeNr++;
                }
                else throw new ArgumentException($"Konfiguration Zeile {testZeile.LaufendeNr}");


            }
        }
    }
}