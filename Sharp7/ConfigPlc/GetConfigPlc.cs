using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;

namespace ConfigPlc
{
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
    public class GetConfigPlc
    {
        public Di DiConfig { get; set; }
        public Da DaConfig { get; set; }
        public Ai AiConfig { get; set; }
        public Aa AaConfig { get; set; }

        internal void SetDiConfig(string pfad)
        {
            try
            {
                DiConfig = JsonConvert.DeserializeObject<Di>(File.ReadAllText(pfad));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Datei nicht gefunden:" + pfad + " --> " + ex);
            }
        }
        internal void SetDaConfig(string pfad)
        {
            try
            {
                DaConfig = JsonConvert.DeserializeObject<Da>(File.ReadAllText(pfad));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Datei nicht gefunden:" + pfad + " --> " + ex);
            }
        }
        internal void SetAiConfig(string pfad)
        {
            try
            {
                AiConfig = JsonConvert.DeserializeObject<Ai>(File.ReadAllText(pfad));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Datei nicht gefunden:" + pfad + " --> " + ex);
            }
        }
        internal void SetAaConfig(string pfad)
        {
            try
            {
                AaConfig = JsonConvert.DeserializeObject<Aa>(File.ReadAllText(pfad));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Datei nicht gefunden:" + pfad + " --> " + ex);
            }
        }
    }
}