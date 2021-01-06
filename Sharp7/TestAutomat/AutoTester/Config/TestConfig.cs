using Newtonsoft.Json;
using PlcDatenTypen;
using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace TestAutomat.AutoTester.Config
{
    public enum TestBefehle
    {
        // ReSharper disable UnusedMember.Global
        Default = 0,
        Init,
        EingaengeTesten,
        Pause
        // ReSharper restore UnusedMember.Global
    }

    public class TestConfig
    {
        public ObservableCollection<TestsEinstellungen> AutomatischeSoftwareTests { get; set; } = new();
    }

    public class TestsEinstellungen
    {
        public TestsEinstellungen()
        {
            LaufendeNr = 0;
            EingaengeBitmuster = new PlcUint("0");
            AusgaengeBitmuster = new PlcUint("0");
            AusgaengeBitmaske = new PlcUint("0");
            Befehl = TestBefehle.Default;
            Dauer = new PlcZeitDauer("0");
            BefehlZusatz1 = "";
            BefehlZusatz2 = "";
            Kommentar = "";
        }

        public int LaufendeNr { get; set; }
        public PlcUint EingaengeBitmuster { get; set; }
        public PlcUint AusgaengeBitmuster { get; set; }
        public PlcUint AusgaengeBitmaske { get; set; }
        public TestBefehle Befehl { get; set; }
        public PlcZeitDauer Dauer { get; set; }
        public string BefehlZusatz1 { get; set; }
        public string BefehlZusatz2 { get; set; }
        public string Kommentar { get; set; }
    }

    internal class TestBefehleConverter : JsonConverter<TestBefehle>
    {
        public override TestBefehle ReadJson(JsonReader reader, Type objectType, TestBefehle existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) return default;
            var token = reader.Value as string ?? reader.Value.ToString();
            var stripped = Regex.Replace(token!, @"<[^>]+>", string.Empty);
            return Enum.TryParse<TestBefehle>(stripped, out var result) ? result : default;
        }
        public override void WriteJson(JsonWriter writer, TestBefehle value, JsonSerializer serializer) => writer.WriteValue(value.ToString());
    }

    internal class PlcUintConverter : JsonConverter<PlcUint>
    {
        public override PlcUint ReadJson(JsonReader reader, Type objectType, PlcUint existingValue, bool hasExistingValue, JsonSerializer serializer) => reader.Value == null ? default : new PlcUint(reader.Value.ToString());
        public override void WriteJson(JsonWriter writer, PlcUint value, JsonSerializer serializer) => throw new NotImplementedException();
    }

    internal class PlcZeitDauerConverter : JsonConverter<PlcZeitDauer>
    {
        public override PlcZeitDauer ReadJson(JsonReader reader, Type objectType, PlcZeitDauer existingValue, bool hasExistingValue, JsonSerializer serializer) => reader.Value == null ? default : new PlcZeitDauer(reader.Value.ToString());
        public override void WriteJson(JsonWriter writer, PlcZeitDauer value, JsonSerializer serializer) => throw new NotImplementedException();
    }
}