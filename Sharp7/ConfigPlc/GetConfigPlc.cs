using Newtonsoft.Json;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ConfigPlc
{
    [JsonConverter(typeof(MyEnumConverter))]
    public enum PlcEinUndAusgaengeTypen
    {
        Default,
        // ReSharper disable UnusedMember.Global
        Ascii,
        BitmusterByte,
        SiemensAnalogwertProzent,
        SiemensAnalogwertPromille,
        SiemensAnalogwertSchieberegler
        // ReSharper restore UnusedMember.Global
    }

    internal class MyEnumConverter : JsonConverter<PlcEinUndAusgaengeTypen>
    {
        public override PlcEinUndAusgaengeTypen ReadJson(JsonReader reader, Type objectType, PlcEinUndAusgaengeTypen existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) return default;
            var token = reader.Value as string ?? reader.Value.ToString();
            var stripped = Regex.Replace(token!, "<[^>]+>", string.Empty);
            return Enum.TryParse<PlcEinUndAusgaengeTypen>(stripped, out var result) ? result : default;
        }
        public override void WriteJson(JsonWriter writer, PlcEinUndAusgaengeTypen value, JsonSerializer serializer) => writer.WriteValue(value.ToString());
    }

    public class GetConfigPlc
    {
        public Di DiConfig { get; set; }
        public Da DaConfig { get; set; }
        public Ai AiConfig { get; set; }
        public Aa AaConfig { get; set; }

        internal void SetDiConfig(string pfad) => DiConfig = JsonConvert.DeserializeObject<Di>(File.ReadAllText(pfad), new MyEnumConverter());
        internal void SetDaConfig(string pfad) => DaConfig = JsonConvert.DeserializeObject<Da>(File.ReadAllText(pfad), new MyEnumConverter());
        internal void SetAiConfig(string pfad) => AiConfig = JsonConvert.DeserializeObject<Ai>(File.ReadAllText(pfad), new MyEnumConverter());
        internal void SetAaConfig(string pfad) => AaConfig = JsonConvert.DeserializeObject<Aa>(File.ReadAllText(pfad), new MyEnumConverter());
    }
}