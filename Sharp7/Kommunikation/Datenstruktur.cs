﻿using System;

namespace Kommunikation
{
    public enum BetriebsartTestablauf
    {
        Automatik = 0,
        Einzelschritt
    }
    public enum BetriebsartProjekt
    {
        LaborPlatte = 0,
        Simulation,
        AutomatischerSoftwareTest
    }
    public class Datenstruktur
    {
        public BetriebsartProjekt BetriebsartProjekt;
        public BetriebsartTestablauf BetriebsartTestablauf;
        public bool NaechstenSchrittGehen;
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
        public int DiagrammZeitbereich { get; set; }

        public Datenstruktur()
        {
            Init();
        }
        public Datenstruktur(int byteDigitalInput, int byteDigitalOutput, int byteAnalogInput, int byteAnalogOutput)
        {
            Init();

            AnzahlByteDigitalInput = byteDigitalInput;
            AnzahlByteDigitalOutput = byteDigitalOutput;
            AnzahlByteAnalogInput = byteAnalogInput;
            AnzahlByteAnalogOutput = byteAnalogOutput;
        }
        private void Init()
        {
            Array.Clear(BefehleSps, 0, BefehleSps.Length);
            Array.Clear(VersionInputSps, 0, VersionInputSps.Length);
            Array.Clear(DigInput, 0, DigInput.Length);
            Array.Clear(DigOutput, 0, DigOutput.Length);
            Array.Clear(AnalogInput, 0, AnalogInput.Length);
            Array.Clear(AnalogOutput, 0, AnalogOutput.Length);

            DiagrammZeitbereich = 20;
            BetriebsartTestablauf = BetriebsartTestablauf.Automatik;
            BetriebsartProjekt = BetriebsartProjekt.LaborPlatte;
            NaechstenSchrittGehen = false;
        }

        public void SetDigInput(int anzByteDigInput) => AnzahlByteDigitalInput = anzByteDigInput;
        public void SetDigOutput(int anzByteDigOutput) => AnzahlByteDigitalOutput = anzByteDigOutput;
        public void SetAnalogInput(int anzByteAnalogInput) => AnzahlByteAnalogInput = anzByteAnalogInput;
        public void SetAnalogOutput(int anzByteAnalogOutput) => AnzahlByteAnalogOutput = anzByteAnalogOutput;
    }
}