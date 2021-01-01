using System;
using Newtonsoft.Json;
using PlcUint;

namespace TestAutomat.AutoTester
{
    public class TestEaBelegung
    {
        public Uint Eingaenge { get; set; }
        public Uint Ausgaenge { get; set; }

        public bool GetEingangAktiv(int i) => Eingaenge.GetBitGesetzt(i);
        public bool GetAusgangAktiv(int i) => Ausgaenge.GetBitGesetzt(i);

        internal class MyUintConverter : JsonConverter<Uint>
        {
            public override Uint ReadJson(JsonReader reader, Type objectType, Uint existingValue, bool hasExistingValue, JsonSerializer serializer) => reader.Value == null ? default : new Uint(reader.Value.ToString());
            public override void WriteJson(JsonWriter writer, Uint value, JsonSerializer serializer) => throw new NotImplementedException();
        }
    }
}