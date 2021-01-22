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
        BitmusterTesten,
        Pause
        // ReSharper restore UnusedMember.Global
    }

    public enum TestErgebnis
    {
        // ReSharper disable UnusedMember.Global
        Aktiv = 0,
        Init,
        Erfolgreich,
        Timeout,
        Fehler
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
            Ergebnis = TestErgebnis.Erfolgreich;
            EingaengeBitmuster = new Uint("0");
            AusgaengeBitmuster = new Uint("0");
            AusgaengeBitmaske = new Uint("0");
            Befehl = TestBefehle.Default;
            Dauer = new ZeitDauer("0");
            BefehlZusatz1 = "";
            BefehlZusatz2 = "";
            Kommentar = "";
        }

        public int LaufendeNr { get; set; }
        public TestErgebnis Ergebnis { get; set; }
        public Uint EingaengeBitmuster { get; set; }
        public Uint AusgaengeBitmuster { get; set; }
        public Uint AusgaengeBitmaske { get; set; }
        public TestBefehle Befehl { get; set; }
        public ZeitDauer Dauer { get; set; }
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
    internal class PlcUintConverter : JsonConverter<Uint>
    {
        public override Uint ReadJson(JsonReader reader, Type objectType, Uint existingValue, bool hasExistingValue, JsonSerializer serializer) => reader.Value == null ? default : new Uint(reader.Value.ToString());
        public override void WriteJson(JsonWriter writer, Uint value, JsonSerializer serializer) => throw new NotImplementedException();
    }
    internal class ZeitDauerConverter : JsonConverter<ZeitDauer>
    {
        public override ZeitDauer ReadJson(JsonReader reader, Type objectType, ZeitDauer existingValue, bool hasExistingValue, JsonSerializer serializer) => reader.Value == null ? default : new ZeitDauer(reader.Value.ToString());
        public override void WriteJson(JsonWriter writer, ZeitDauer value, JsonSerializer serializer) => throw new NotImplementedException();
    }
}