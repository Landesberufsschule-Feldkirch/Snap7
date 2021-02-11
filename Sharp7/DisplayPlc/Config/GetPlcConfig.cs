using System.IO;

namespace DisplayPlc.Config
{
    public class GetPlcConfig
    {
        public PlcBelegung PlcBelegung { get; set; }

        public GetPlcConfig(FileSystemInfo aktuellesProjekt) => SetEaBelegung(aktuellesProjekt.FullName + "\\testEaBelegung.json");
        private void SetEaBelegung(string pfad) => PlcBelegung = Newtonsoft.Json.JsonConvert.DeserializeObject<PlcBelegung>(File.ReadAllText(pfad), new PlcBelegung.MyUintConverter());
    }
}