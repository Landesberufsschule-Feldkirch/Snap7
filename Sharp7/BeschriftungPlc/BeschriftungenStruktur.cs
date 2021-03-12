using System.IO;
using Newtonsoft.Json;

namespace BeschriftungPlc
{
    public class BeschriftungenStruktur
    {
        public Da DaBeschriftungen { get; set; }
        public Di DiBeschriftungen { get; set; }

        internal void PlcDaBeschriftungen(string pfad) => DaBeschriftungen = JsonConvert.DeserializeObject<Da>(File.ReadAllText(pfad));
        public void PlcDiBeschriftungen(string pfad) => DiBeschriftungen = JsonConvert.DeserializeObject<Di>(File.ReadAllText(pfad));

    }
}