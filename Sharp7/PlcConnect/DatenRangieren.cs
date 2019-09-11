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
                AusgaengeSchreiben(ButtonStart.IsPressed, DigInput, 0, 0x01);
                AusgaengeSchreiben(ButtonStop.IsPressed, DigInput, 0, 0x02);

                ButtonRahmenAendern(ButtonStart, DigInput, 0, 0x01, 5, 1);
                ButtonRahmenAendern(ButtonStop, DigInput, 0, 0x02, 5, 1);

                KreisFarbeUmschalten(KreisBetrieb, DigOutput, 0, 0x01, Colors.Green, Colors.White);
                KreisFarbeUmschalten(KreisStoerung, DigOutput, 0, 0x02, Colors.Red, Colors.White);

            });

        }


        void AusgaengeSchreiben(bool Bedingung, byte[] Ausgaenge, byte Byte, UInt16 BitMuster)
        {
            byte BitEin = (byte)(BitMuster & 0xFF);
            byte BitAus = (byte)(~BitMuster & 0xFF);

            if (Bedingung) Ausgaenge[Byte] |= BitEin;
            else Ausgaenge[Byte] &= BitAus;
        }

        void KreisFarbeUmschalten(Ellipse ellipse, byte[] Eingaenge, byte Byte, byte BitMuster, Color FarbeEin, Color FarbeAus)
        {
            if ((Eingaenge[Byte] & BitMuster) == BitMuster) ellipse.Fill = new SolidColorBrush(FarbeEin);
            else ellipse.Fill = new SolidColorBrush(FarbeAus);
        }

        void ButtonRahmenAendern(Button button, byte[] Ausgaenge, byte Byte, byte BitMuster, double StaerkeEin, double StaerkeAus)
        {
            if ((Ausgaenge[Byte] & BitMuster) == BitMuster)
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
