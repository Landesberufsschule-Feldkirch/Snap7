using Kommunikation;
using System;

namespace ConfigPlc
{
    public class Plc
    {
        public enum PlcConfig
        {
            Di,
            Da,
            Ai,
            Aa
        }

        public GetConfig GetConfig { get; set; }
        public Datenstruktur Datenstruktur { get; set; }

        public Plc(Datenstruktur datenstruktur, string pfad)
        {
            Datenstruktur = datenstruktur;

            GetConfig = new GetConfig();

            GetConfig.SetDiConfig($"{pfad}/DI.json");
            GetConfig.SetDaConfig($"{pfad}/DA.json");
            GetConfig.SetAiConfig($"{pfad}/AI.json");
            GetConfig.SetAaConfig($"{pfad}/AA.json");
        }
    }
}