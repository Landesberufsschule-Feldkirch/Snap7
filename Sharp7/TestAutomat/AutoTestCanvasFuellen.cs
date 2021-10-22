using BeschriftungPlc;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TestAutomat
{
    public partial class AutoTesterWindow
    {
        private const int ZeilenAbstand = 10;
        private const int SpaltenBreite = 300 - 15; // rechter Rand

        public void PlcBeschriftungAktualisieren(BeschriftungenPlc beschriftungenPlc)
        {
            BeschriftungDigitalInput(CanvasDigitalInput, beschriftungenPlc);
            BeschriftungDigitalOutput(CanvasDigitalOutputIst, beschriftungenPlc);
            BeschriftungDigitalOutput(CanvasDigitalOutputSoll, beschriftungenPlc);
        }
        private static void BeschriftungDigitalInput(Panel canvas, BeschriftungenPlc beschriftungenPlc)
        {
            if (canvas.Children.Count > 0) canvas.Children.Clear();

            foreach (var diDaten in beschriftungenPlc.BeschriftungenStruktur.DiBeschriftungen.DiBeschriftung)
            {
                var bitPos = diDaten.Bit + 8 * diDaten.Byte;
                var label = new Label
                {
                    Content = $"{bitPos:D2}: {diDaten.Bezeichnung}",
                    FontSize = 12,
                    FontFamily = new FontFamily("Courier New"),
                    Padding = new Thickness(0),
                    BorderThickness = new Thickness(0),
                    LayoutTransform = new RotateTransform(270)
                };

                Canvas.SetLeft(label, SpaltenBreite - ZeilenAbstand * (bitPos + bitPos / 4));
                Canvas.SetBottom(label, 5);
                _ = canvas.Children.Add(label);
            }
        }

        private static void BeschriftungDigitalOutput(Panel canvas, BeschriftungenPlc beschriftungenPlc)
        {
            if (canvas.Children.Count > 0) canvas.Children.Clear();

            foreach (var daDaten in beschriftungenPlc.BeschriftungenStruktur.DaBeschriftungen.DaBeschriftung)
            {
                var bitPos = daDaten.Bit + 8 * daDaten.Byte;
                var label = new Label
                {
                    Content = $"{bitPos:D2}: {daDaten.Bezeichnung}",
                    FontSize = 12,
                    FontFamily = new FontFamily("Courier New"),
                    Padding = new Thickness(0),
                    BorderThickness = new Thickness(0),
                    LayoutTransform = new RotateTransform(270)
                };

                Canvas.SetLeft(label, SpaltenBreite - ZeilenAbstand * (bitPos + bitPos / 4));
                Canvas.SetBottom(label, 5);
                _ = canvas.Children.Add(label);
            }
        }
    }
}