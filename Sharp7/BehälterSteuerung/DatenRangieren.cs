using System;
using System.Threading.Tasks;

namespace BehälterSteuerung
{
    public partial class MainWindow
    {
        bool Ventil_Q1 = false;
        bool Ventil_Q2 = false;
        bool Ventil_Q3 = false;
        bool Ventil_Q4 = false;
        bool Ventil_Q5 = false;
        bool Ventil_Q6 = false;

        bool Leuchte_P1 = false;

        bool Pegel_B1 = false;
        bool Pegel_B2 = false;
        bool Pegel_B3 = false;
        bool Pegel_B4 = false;
        bool Pegel_B5 = false;
        bool Pegel_B6 = false;

        double Pegel_1 = 0.7;
        double Pegel_2 = 0.5;
        double Pegel_3 = 0.3;

        public const int BitPos_Q1 = 0x0001;
        public const int BitPos_Q3 = 0x0002;
        public const int BitPos_Q5 = 0x0004;
        public const int BitPos_P1 = 0x0008;

        public const int BitPos_B1 = 0x0001;
        public const int BitPos_B2 = 0x0002;
        public const int BitPos_B3 = 0x0004;
        public const int BitPos_B4 = 0x0008;
        public const int BitPos_B5 = 0x0010;
        public const int BitPos_B6 = 0x0020;

        public void DatenRangieren_Task()
        {
            while (TaskAktiv && FensterAktiv)
            {
                BitmusterSchreiben(Pegel_B1, DigInput, Startbyte_0, BitPos_B1);
                BitmusterSchreiben(Pegel_B2, DigInput, Startbyte_0, BitPos_B2);
                BitmusterSchreiben(Pegel_B3, DigInput, Startbyte_0, BitPos_B3);
                BitmusterSchreiben(Pegel_B4, DigInput, Startbyte_0, BitPos_B4);
                BitmusterSchreiben(Pegel_B5, DigInput, Startbyte_0, BitPos_B5);
                BitmusterSchreiben(Pegel_B6, DigInput, Startbyte_0, BitPos_B6);

                if ((Client != null) && TaskAktiv)
                {
                    Client.DBWrite(DB_DigInput, Startbyte_0, AnzahlByte_1, DigInput);
                    Client.DBRead(DB_DigOutput, Startbyte_0, AnzahlByte_1, DigOutput);
                }

                if (BitmusterTesten(DigOutput, Startbyte_0, BitPos_Q1)) Ventil_Q1 = true; else Ventil_Q1 = false;
                if (BitmusterTesten(DigOutput, Startbyte_0, BitPos_Q3)) Ventil_Q3 = true; else Ventil_Q3 = false;
                if (BitmusterTesten(DigOutput, Startbyte_0, BitPos_Q5)) Ventil_Q5 = true; else Ventil_Q5 = false;
                if (BitmusterTesten(DigOutput, Startbyte_0, BitPos_P1)) Leuchte_P1 = true; else Leuchte_P1 = false;

                Task.Delay(100);
            }
        }

        public bool BitmusterTesten(byte[] ByteArray, byte ByteNummer, UInt16 BitMuster)
        {
            if ((ByteArray[ByteNummer] & BitMuster) == BitMuster) return true;
            else return false;
        }

        void BitmusterSchreiben(bool Bedingung, byte[] ByteArray, byte ByteNummer, UInt16 BitMuster)
        {
            byte BitEin = (byte)(BitMuster & 0xFF);
            byte BitAus = (byte)(~BitMuster & 0xFF);

            if (Bedingung) ByteArray[ByteNummer] |= BitEin;
            else ByteArray[ByteNummer] &= BitAus;
        }
        
    }
}
