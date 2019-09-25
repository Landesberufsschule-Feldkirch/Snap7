using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PlcConnect
{
    public partial class MainWindow
    {
        public void DatenRangieren()
        {
            this.Dispatcher.Invoke(() =>
            {
                BitmusterSchreiben(ButtonStart.IsPressed, DigInput, Startbyte_0, BitMuster_01);
                BitmusterSchreiben(ButtonStop.IsPressed, DigInput, Startbyte_0, BitMuster_02);

                ButtonRahmenAendern(BitmusterTesten(DigInput, Startbyte_0, BitMuster_01), ButtonStart, Rahmenbreite_5px, Rahmenbreite_1px);
                ButtonRahmenAendern(BitmusterTesten(DigInput, Startbyte_0, BitMuster_02), ButtonStop, Rahmenbreite_5px, Rahmenbreite_1px);

                KreisFarbeUmschalten(BitmusterTesten(DigOutput, Startbyte_0, BitMuster_01), KreisBetrieb, Colors.Green, Colors.White);
                KreisFarbeUmschalten(BitmusterTesten(DigOutput, Startbyte_0, BitMuster_02), KreisStoerung, Colors.Red, Colors.White);
            });
        }

        public bool BitmusterTesten(byte[] ByteArray, byte ByteNummer, UInt16 BitMuster)
        {
            if ((ByteArray[ByteNummer] & BitMuster) == BitMuster)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void BitmusterSchreiben(bool Bedingung, byte[] ByteArray, byte ByteNummer, UInt16 BitMuster)
        {
            byte BitEin = (byte)(BitMuster & 0xFF);
            byte BitAus = (byte)(~BitMuster & 0xFF);

            if (Bedingung)
            {
                ByteArray[ByteNummer] |= BitEin;
            }
            else
            {
                ByteArray[ByteNummer] &= BitAus;
            }
        }

        void KreisFarbeUmschalten(bool Wert, Ellipse ellipse, Color FarbeEin, Color FarbeAus)
        {
            if (Wert)
            {
                ellipse.Fill = new SolidColorBrush(FarbeEin);
            }
            else
            {
                ellipse.Fill = new SolidColorBrush(FarbeAus);
            }
        }

        void ButtonRahmenAendern(bool Wert, Button button, double StaerkeEin, double StaerkeAus)
        {
            if (Wert)
            {
                button.BorderThickness = new System.Windows.Thickness(StaerkeEin);
            }
            else
            {
                button.BorderThickness = new System.Windows.Thickness(StaerkeAus);
            }
        }

    }
}
