using Sharp7;
using System.Threading;

namespace Synchronisiereinrichtung
{
    public class DatenRangieren
    {
        public bool Q1 { get; set; }
        public bool Q1alt { get; set; }
        public bool S1 { get; set; }
        public bool S2 { get; set; }

        public int Y { get; set; }
        public int Ie { get; set; }

        public short n { get; set; }
        public short fGenerator { get; set; }
        public short fNetz { get; set; }
        public short UGenerator { get; set; }
        public short UNetz { get; set; }
        public short PNetz { get; set; }
        public short UDiff { get; set; }
        public short ph { get; set; }

        public double Drehzahl { get; set; }
        public double FrequenzGenerator { get; set; }
        public double FrequenzNetz { get; set; }
        public double SpannungGenerator { get; set; }
        public double SpannungNetz { get; set; }
        public double SpannungDifferenz { get; set; }
        public double LeistungNetz { get; set; }
        public double LeistungGenerator { get; set; }
        public double SpannungsUnterschiedSynchronisieren { get; set; }
        public double FrequenzDifferenz { get; set; }
        public double Phasenlage { get; set; }

        public bool MaschineTot { get; set; }
      
        enum BytePosition
        {
            Byte_0 = 0
        }
        enum AnzahlByte
        {
            Byte_1 = 1
        }
        enum BitPosAusgang
        {
            Q1 = 0
        }
        enum BitPosEingang
        {
            S1 = 0,
            S2
        }

        public void DatenRangieren_Task()
        {
            while (TaskAktiv && FensterAktiv)
            {
                if ((Client != null) && TaskAktiv && !DebugWindowAktiv)
                {
                    S7.SetIntAt(AnalogInput, 0, n);
                    S7.SetIntAt(AnalogInput, 2, UNetz);
                    S7.SetIntAt(AnalogInput, 4, fNetz);
                    S7.SetIntAt(AnalogInput, 6, UGenerator);
                    S7.SetIntAt(AnalogInput, 8, fGenerator);
                    S7.SetIntAt(AnalogInput, 10, PNetz);
                    S7.SetIntAt(AnalogInput, 12, UDiff);
                    S7.SetIntAt(AnalogInput, 14, ph);

                    Client.DBWrite((int)Datenbausteine.DigIn, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_1, DigInput);
                    Client.DBRead((int)Datenbausteine.DigOut, (int)BytePosition.Byte_0, (int)AnzahlByte.Byte_1, DigOutput);

                    Y = S7.GetIntAt(AnalogOutput, 0);
                    Ie = S7.GetIntAt(AnalogOutput, 2);

                    S7.SetBitAt(ref DigInput, (int)BytePosition.Byte_0, (int)BitPosEingang.S1, S1);
                    S7.SetBitAt(ref DigInput, (int)BytePosition.Byte_0, (int)BitPosEingang.S2, S2);
                    Q1 = S7.GetBitAt(DigOutput, (int)BytePosition.Byte_0, (int)BitPosAusgang.Q1);
                }

                Thread.Sleep(100);
            }
        }

        public void RangierenInput(byte[] digInput, byte[] anInput)
        {
            // Daten lesen        
        }
        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
        {
            // es werden keine Werte von der SPS geschrieben
        }

        public DatenRangieren(Logikfunktionen logikfunktionen)
        {
            this.logikfunktionen = logikfunktionen;
        }

    }
}
