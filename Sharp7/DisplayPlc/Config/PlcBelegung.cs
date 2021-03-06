﻿using Newtonsoft.Json;
using PlcDatenTypen;
using System;

namespace DisplayPlc.Config
{
    public class PlcBelegung
    {
        public Uint Eingaenge { get; set; }
        public Uint Ausgaenge { get; set; }

        // ReSharper disable once UnusedMember.Global
        internal class MyUintConverter : JsonConverter<Uint>
        {
            public override Uint ReadJson(JsonReader reader, Type objectType, Uint existingValue, bool hasExistingValue, JsonSerializer serializer) => reader.Value == null ? default : new Uint(reader.Value.ToString());
            public override void WriteJson(JsonWriter writer, Uint value, JsonSerializer serializer) => throw new NotImplementedException();
        }
    }
}