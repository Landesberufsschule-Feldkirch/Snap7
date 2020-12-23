using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using ManualMode.Model;

namespace ManualMode
{
    public static partial class FensterFunktionen
    {
        internal static void SliderZeichnen(int x, int y, int par, string bez, double min, double max, double tick, ManualMode manualMode, Grid grid)
        {
            var schiebeRegler = new Slider
            {
                Padding = new Thickness(5, 0, 0, 0),
                Background = Brushes.Yellow,
                Minimum = min,
                Maximum = max,
                IsSnapToTickEnabled = true,
                TickFrequency = tick,
                Height = 18,
                VerticalAlignment = VerticalAlignment.Top,
                Name = $"{bez}_{par}",
                Tag = manualMode
            };

            schiebeRegler.ValueChanged += (sender, _) =>
            {

                if (sender is not Slider) return;
                var localSender = (Slider)sender;
                if (localSender.Tag is not ManualMode manMode) return;
                var textBoxNamensTeile = localSender.Name.Split("_");
                var id = short.Parse(textBoxNamensTeile[1]);

                switch (textBoxNamensTeile[0])
                {
                    case "Aa":
                        var startByte = manMode.GetConfig.AaConfig.AnalogeAusgaenge[id].StartByte;
                        const double siemensAnalogSkalierung = 27648;

                        switch (manMode.GetConfig.AaConfig.AnalogeAusgaenge[id].Type)
                        {
                            case PlcEinUndAusgaengeTypen.Ascii: break;
                            case PlcEinUndAusgaengeTypen.BitmusterByte: break;
                            case PlcEinUndAusgaengeTypen.Default: break;
                            case PlcEinUndAusgaengeTypen.SiemensAnalogwertProzent: break;
                            case PlcEinUndAusgaengeTypen.SiemensAnalogwertPromille: break;
                            case PlcEinUndAusgaengeTypen.SiemensAnalogwertSchieberegler:
                                var siemens = (short)(localSender.Value * siemensAnalogSkalierung / manMode.GetConfig.AaConfig.AnalogeAusgaenge[id].MaximalWert);
                                var einzelneBytes = BitConverter.GetBytes(siemens);
                                manMode.Datenstruktur.AnalogOutput[startByte] = einzelneBytes[1];//Siemens --> BigEndian
                                manMode.Datenstruktur.AnalogOutput[startByte + 1] = einzelneBytes[0];
                                break;
                            default: throw new ArgumentOutOfRangeException();
                        }
                        break;
                }

            };
            Grid.SetColumn(schiebeRegler, x);
            Grid.SetRow(schiebeRegler, y);
            grid.Children.Add(schiebeRegler);
        }


        internal static void SliderWertZeichnen(int x, int y, int par, string bez, DependencyProperty visibilityProperty, ManualMode manualMode, Grid grid)
        {
            var kommentar = new TextBlock
            {
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Green),
                Padding = new Thickness(10, 5, 5, 5),
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            kommentar.SetBinding(TextBlock.TextProperty, new Binding($"ManVisuAnzeigen.Content{bez} [{ par }]"));
            kommentar.SetBinding(visibilityProperty, new Binding($"ManVisuAnzeigen.Visibility{bez} [{ par }]"));

            Grid.SetColumn(kommentar, x);
            Grid.SetRow(kommentar, y);
            grid.Children.Add(kommentar);
        }

    }
}