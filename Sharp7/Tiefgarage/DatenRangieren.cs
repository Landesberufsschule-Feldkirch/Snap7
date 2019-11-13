using System;
using System.Threading.Tasks;

namespace Tiefgarage
{
    public partial class MainWindow
    {
        public const int BitPos_B1 = 0x0001;
        public const int BitPos_B2 = 0x0002;

        public bool Pegel_B1 = true;
        public bool Pegel_B2 = true;

        public void DatenRangieren_Task()
        {
            while (TaskAktiv && FensterAktiv)
            {
                BitmusterSchreiben(Pegel_B1, DigInput, Startbyte_0, BitPos_B1);
                BitmusterSchreiben(Pegel_B2, DigInput, Startbyte_0, BitPos_B2);

                if ((Client != null) && TaskAktiv)
                {
                    Client.DBWrite(DB_DigInput, Startbyte_0, AnzahlByte_1, DigInput);
                    Client.DBRead(DB_DigOutput, Startbyte_0, AnzahlByte_2, DigOutput);
                }

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
