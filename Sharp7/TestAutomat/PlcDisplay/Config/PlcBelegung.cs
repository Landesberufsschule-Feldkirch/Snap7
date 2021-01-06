using System;
using Newtonsoft.Json;
using PlcDatenTypen;

namespace TestAutomat.PlcDisplay.Config
{
    public class PlcBelegung
    {
        public PlcUint Eingaenge { get; set; }
        public PlcUint Ausgaenge { get; set; }

        public bool GetEingangAktiv(int i) => Eingaenge.GetBitGesetzt(i);
        public bool GetAusgangAktiv(int i) => Ausgaenge.GetBitGesetzt(i);

        // ReSharper disable once UnusedMember.Global
        internal class MyUintConverter : JsonConverter<PlcUint>
        {
            public override PlcUint ReadJson(JsonReader reader, Type objectType, PlcUint existingValue, bool hasExistingValue, JsonSerializer serializer) => reader.Value == null ? default : new PlcUint(reader.Value.ToString());
            public override void WriteJson(JsonWriter writer, PlcUint value, JsonSerializer serializer) => throw new NotImplementedException();
        }
    }
}