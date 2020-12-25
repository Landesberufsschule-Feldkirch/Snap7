using System;
using System.IO;

namespace TestAutomat.AutoTester
{
    public class GetConfig
    {
        public ConfigTests ConfigTests { get; set; }
 
        public GetConfig(FileSystemInfo aktuellesProjekt)
        {
            SetTestsConfig(aktuellesProjekt.FullName + "/test.json");
        }

        public void SetTestsConfig(string pfad) => ConfigTests = Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigTests>(File.ReadAllText(pfad));

        public void KonfigurationTesten()
        {
            var laufendeNr = 0;
            
            foreach (var testZeile in ConfigTests.AutomatischeSoftwareTests)
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