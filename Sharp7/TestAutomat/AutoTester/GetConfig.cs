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
    }
}