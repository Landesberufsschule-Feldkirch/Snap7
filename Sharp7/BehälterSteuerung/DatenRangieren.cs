using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BehälterSteuerung
{
    public partial class MainWindow
    {
        public void DatenRangieren()
        {
            this.Dispatcher.Invoke(() =>
            {
 

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
