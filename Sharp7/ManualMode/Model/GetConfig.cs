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
        SiemensAnalogwertProzent,
        SiemensAnalogwertPromille,
        BitmusterByte
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

        public override void WriteJson(JsonWriter writer, PlcEinUndAusgaengeTypen value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }

    public class GetConfig
    {
        public ConfigDi ConfigDi { get; set; }
        public ConfigDa ConfigDa { get; set; }
        public ConfigAi ConfigAi { get; set; }
        public ConfigAa ConfigAa { get; set; }

        public void SetDiConfig(string pfad) => ConfigDi = JsonConvert.DeserializeObject<ConfigDi>(File.ReadAllText(pfad), new MyEnumConverter());
        internal void SetDaConfig(string pfad) => ConfigDa = JsonConvert.DeserializeObject<ConfigDa>(File.ReadAllText(pfad), new MyEnumConverter());
        internal void SetAiConfig(string pfad) => ConfigAi = JsonConvert.DeserializeObject<ConfigAi>(File.ReadAllText(pfad), new MyEnumConverter());
        public void SetAaConfig(string pfad) => ConfigAa = JsonConvert.DeserializeObject<ConfigAa>(File.ReadAllText(pfad), new MyEnumConverter());
    }
}