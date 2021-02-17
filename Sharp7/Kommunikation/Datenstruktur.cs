using System;
using System.IO;

namespace Kommunikation
{
    public enum BetriebsartProjekt
    {
        LaborPlatte = 0,
        Simulation,
        AutomatischerSoftwareTest
    }
    public class Datenstruktur
    {
        private BetriebsartProjekt _betriebsartProjekt;
        public byte[] BefehleSps { get; set; } = new byte[1024];
        public byte[] VersionInputSps { get; set; } = new byte[1024];
        public byte[] DigInput { get; set; } = new byte[1024];
        public byte[] DigOutput { get; set; } = new byte[1024];
        public byte[] AnalogInput { get; set; } = new byte[1024];
        public byte[] AnalogOutput { get; set; } = new byte[1024];

        public int AnzahlByteDigitalInput { get; set; }
        public int AnzahlByteDigitalOutput { get; set; }
        public int AnzahlByteAnalogInput { get; set; }
        public int AnzahlByteAnalogOutput { get; set; }
        public string TestProjektOrdner { get; set; }

        public Datenstruktur(int byteDigitalInput, int byteDigitalOutput, int byteAnalogInput, int byteAnalogOutput)
        {
            _betriebsartProjekt = BetriebsartProjekt.LaborPlatte;

            AnzahlByteDigitalInput = byteDigitalInput;
            AnzahlByteDigitalOutput = byteDigitalOutput;
            AnzahlByteAnalogInput = byteAnalogInput;
            AnzahlByteAnalogOutput = byteAnalogOutput;

            Array.Clear(BefehleSps, 0, BefehleSps.Length);
            Array.Clear(VersionInputSps, 0, VersionInputSps.Length);
            Array.Clear(DigInput, 0, DigInput.Length);
            Array.Clear(DigOutput, 0, DigOutput.Length);
            Array.Clear(AnalogInput, 0, AnalogInput.Length);
            Array.Clear(AnalogOutput, 0, AnalogOutput.Length);
        }

        public void SetBetriebsartProjekt(BetriebsartProjekt betriebsart) => _betriebsartProjekt = betriebsart;
        public BetriebsartProjekt GetBetriebsartProjekt() => _betriebsartProjekt;
    }
}