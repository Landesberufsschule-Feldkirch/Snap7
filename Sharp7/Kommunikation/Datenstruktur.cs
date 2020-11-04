using System;

namespace Kommunikation
{
    public class Datenstruktur
    {
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

        public Datenstruktur(int byteDigitalInput, int byteDigitalOutput, int byteAnalogInput, int byteAnalogOutput)
        {
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
    }
}