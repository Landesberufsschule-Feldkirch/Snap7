using System;
using System.IO;

namespace TestAutomat.AutoTester.Config
{
    public class GetTestConfig
    {
        public TestConfig TestConfig { get; set; }
     
        public GetTestConfig(FileSystemInfo aktuellesProjekt) => SetTestConfig(aktuellesProjekt.FullName + "\\testConfig.json");
        public void SetTestConfig(string pfad) => TestConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<TestConfig>(File.ReadAllText(pfad), new MyUintConverter(), new MyzeitDauerConverter());
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