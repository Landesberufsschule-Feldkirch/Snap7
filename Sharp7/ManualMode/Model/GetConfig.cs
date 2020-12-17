using Newtonsoft.Json;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ManualMode.Model
{
    [JsonConverter(typeof(MyEnumConverter))]
    public enum PlcEinUndAusgaengeTypen
    {
        Default,
        Ascii,
        BitmusterByte,
        SiemensAnalogwertProzent,
        SiemensAnalogwertPromille,
        SiemensAnalogwertSchieberegler
    }

    internal class MyEnumConverter : JsonConverter<PlcEinUndAusgaengeTypen>
    {
        public override PlcEinUndAusgaengeTypen ReadJson(JsonReader reader, Type objectType, PlcEinUndAusgaengeTypen existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) return default;
            var token = reader.Value as string ?? reader.Value.ToString();
            var stripped = Regex.Replace(token!, @"<[^>]+>", string.Empty);
            return Enum.TryParse<PlcEinUndAusgaengeTypen>(stripped, out var result) ? result : default;
        }
        public override void WriteJson(JsonWriter writer, PlcEinUndAusgaengeTypen value, JsonSerializer serializer) => writer.WriteValue(value.ToString());
    }

    public class GetConfig
    {
        public DiConfig DiConfig { get; set; }
        public DaConfig DaConfig { get; set; }
        public AiConfig AiConfig { get; set; }
        public AaConfig AaConfig { get; set; }

        public void SetDiConfig(string pfad) => DiConfig = JsonConvert.DeserializeObject<DiConfig>(File.ReadAllText(pfad), new MyEnumConverter());
        internal void SetDaConfig(string pfad) => DaConfig = JsonConvert.DeserializeObject<DaConfig>(File.ReadAllText(pfad), new MyEnumConverter());
        internal void SetAiConfig(string pfad) => AiConfig = JsonConvert.DeserializeObject<AiConfig>(File.ReadAllText(pfad), new MyEnumConverter());
        public void SetAaConfig(string pfad) => AaConfig = JsonConvert.DeserializeObject<AaConfig>(File.ReadAllText(pfad), new MyEnumConverter());
    }
}