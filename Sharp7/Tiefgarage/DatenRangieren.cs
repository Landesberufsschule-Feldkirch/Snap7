using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tiefgarage
{    public partial class MainWindow
    {
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
        
        void EinAusgabeFelderInitialisieren()
        {
            foreach (byte b in DigInput) { DigInput[b] = 0; }
            foreach (byte b in DigOutput) { DigOutput[b] = 0; }
        }

    }
}
