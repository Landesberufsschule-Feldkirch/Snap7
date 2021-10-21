using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BeschriftungPlc;

namespace TestAutomat
{
    public partial class AutoTesterWindow
    {
        private static void BeschriftungDigitalInput(Canvas canvas, BeschriftungenPlc beschriftungenPlc)
        {
            foreach (var diDaten in beschriftungenPlc.BeschriftungenStruktur.DiBeschriftungen.DiBeschriftung)
            {
                var label = new Label
                {
                    Content = diDaten.Bezeichnung,
                    FontSize = 16,
                    Padding = new Thickness(0),
                    BorderThickness = new Thickness(0),
                    LayoutTransform = new RotateTransform(270)
                };

                Canvas.SetLeft(label, 250 - 12 * (diDaten.Byte * 8 + diDaten.Bit));
                Canvas.SetBottom(label, 5);
                canvas.Children.Add(label);
            }
        }

        private static void BeschriftungDigitalOutputIst(Canvas canvas, BeschriftungenPlc beschriftungenPlc)
        {
            for (var i = 0; i < 10; i++)
            {
                var label = new Label
                {
                    Content = "Digital Output Ist " + i,
                    FontSize = 16,
                    Padding = new Thickness(0),
                    BorderThickness = new Thickness(0),
                    LayoutTransform = new RotateTransform(270)
                };

                Canvas.SetLeft(label, 10 * i);
                Canvas.SetBottom(label, 5);
                canvas.Children.Add(label);
            }
        }

        private static void BeschriftungDigitalOutputSoll(Canvas canvas, BeschriftungenPlc beschriftungenPlc)
        {
            for (var i = 0; i < 10; i++)
            {
                var label = new Label
                {
                    Content = "Digital Output Soll " + i,
                    FontSize = 16,
                    Padding = new Thickness(0),
                    BorderThickness = new Thickness(0),
                    LayoutTransform = new RotateTransform(270)
                };

                Canvas.SetLeft(label, 10 * i);
                Canvas.SetBottom(label, 5);
                canvas.Children.Add(label);
            }
        }
    }
}