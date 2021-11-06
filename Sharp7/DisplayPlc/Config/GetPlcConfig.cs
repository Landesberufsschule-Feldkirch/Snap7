using System;
using System.IO;
using System.Windows;

namespace DisplayPlc.Config
{
    public class GetPlcConfig
    {
        public PlcBelegung PlcBelegung { get; set; }

        public GetPlcConfig(FileSystemInfo aktuellesProjekt) => SetEaBelegung(@$"{aktuellesProjekt.FullName}\testEaBelegung.json");
        private void SetEaBelegung(string pfad)
        {
            try
            {
                PlcBelegung = Newtonsoft.Json.JsonConvert.DeserializeObject<PlcBelegung>(File.ReadAllText(pfad));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Datei nicht gefunden:" + pfad + " --> " + ex);
            }
        }
    }
}