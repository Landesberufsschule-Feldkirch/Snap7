﻿namespace PaternosterLager.ViewModel
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class KettengliedRegal
    {
        private double position;
        private readonly int id;

        private readonly double hoeheKettenglied;

        private const double hoehelagerkiste = 40;
        private const double breitelagerkisted = 80;

        private const double breiteKettenglied = 14;
        private const double durchmesserBolzen = 8;

        public KettengliedRegal(int id, double anzahlKisten)
        {
            this.id = id;

            var abstandLagerkisten = Model.PositionBestimmen.GetGesamtLaenge(durchmesserBolzen) / anzahlKisten;
            hoeheKettenglied = abstandLagerkisten / 3;

            position = id * abstandLagerkisten;
            SetGeschwindigkeit(0);
        }

        internal void Zeichnen(MainWindow mainWindow, double pos)
        {
            if (mainWindow.FensterAktiv)
            {
                mainWindow.ZeichenFlaeche.Children.Add(ZapfenZeichnen(pos + 0 * hoeheKettenglied, Brushes.Black));
                mainWindow.ZeichenFlaeche.Children.Add(ZapfenZeichnen(pos + 1 * hoeheKettenglied, Brushes.Red));
                mainWindow.ZeichenFlaeche.Children.Add(ZapfenZeichnen(pos + 2 * hoeheKettenglied, Brushes.Cyan));

                mainWindow.ZeichenFlaeche.Children.Add(KettengliedZeichnen(pos + 0 * hoeheKettenglied, 1, Brushes.Black));
                mainWindow.ZeichenFlaeche.Children.Add(KettengliedZeichnen(pos + 1 * hoeheKettenglied, 1, Brushes.Red));
                mainWindow.ZeichenFlaeche.Children.Add(KettengliedZeichnen(pos + 2 * hoeheKettenglied, 1, Brushes.Cyan));

                mainWindow.ZeichenFlaeche.Children.Add(LagerkisteZeichnen(pos + 1 * hoeheKettenglied, Brushes.Red));

                mainWindow.ZeichenFlaeche.Children.Add(BeschriftungZeichnen(pos + 1 * hoeheKettenglied, id));
            }
        }

        internal Ellipse ZapfenZeichnen(double offset, SolidColorBrush farbe)
        {
            var bolzen = new Ellipse
            {
                Width = durchmesserBolzen,
                Height = durchmesserBolzen,
                Fill = farbe
            };

            var (x, y, _) = Model.PositionBestimmen.ZapfenPositionBerechnen(position + offset, durchmesserBolzen);
            Canvas.SetLeft(bolzen, x - durchmesserBolzen / 2);
            Canvas.SetTop(bolzen, y - durchmesserBolzen / 2);

            return bolzen;
        }

        internal Rectangle KettengliedZeichnen(double offset, double staerke, SolidColorBrush farbe)
        {
            var kettenglied = new Rectangle
            {
                Width = breiteKettenglied,
                Height = hoeheKettenglied,
                Stroke = farbe,
                StrokeThickness = staerke
            };

            var (x, y, phi, positionUndRichtung) = Model.PositionBestimmen.KettengliedPositionBerechnen(position + offset, durchmesserBolzen, breiteKettenglied, hoeheKettenglied);

            switch (positionUndRichtung)
            {
                case Model.Zeichenbereich.links:
                    kettenglied.RenderTransformOrigin = new Point(0.5, 0.14);
                    kettenglied.RenderTransform = new RotateTransform(phi);
                    Canvas.SetLeft(kettenglied, x - breiteKettenglied / 2);
                    Canvas.SetTop(kettenglied, y + breiteKettenglied / 2 - hoeheKettenglied);
                    return kettenglied;

                default:
                    kettenglied.RenderTransformOrigin = new Point(0.5, 0.14);
                    kettenglied.RenderTransform = new RotateTransform(phi);
                    Canvas.SetLeft(kettenglied, x - breiteKettenglied / 2);
                    Canvas.SetTop(kettenglied, y - breiteKettenglied / 2);
                    return kettenglied;
            }
        }

        internal Rectangle LagerkisteZeichnen(double offset, SolidColorBrush farbe)
        {
            var lagerKiste = new Rectangle
            {
                Width = breitelagerkisted,
                Height = hoehelagerkiste,
                Stroke = farbe,
                StrokeThickness = 3
            };

            var (x, y, _) = Model.PositionBestimmen.ZapfenPositionBerechnen(position + offset, durchmesserBolzen);
            Canvas.SetLeft(lagerKiste, x - breitelagerkisted / 2);
            Canvas.SetTop(lagerKiste, y - durchmesserBolzen / 2);

            return lagerKiste;
        }

        internal Label BeschriftungZeichnen(double offset, int id)
        {
            var beschriftung = new Label
            {
                Content = id,
                FontSize = 14
            };

            var (x, y, _) = Model.PositionBestimmen.ZapfenPositionBerechnen(position + offset, durchmesserBolzen);
            Canvas.SetLeft(beschriftung, x + breitelagerkisted / 2);
            Canvas.SetTop(beschriftung, y);

            return beschriftung;
        }

        internal void SetGeschwindigkeit(double geschwindigkeit) => position += geschwindigkeit;
        internal double GetGesamtLaenge() => Model.PositionBestimmen.GetGesamtLaenge(durchmesserBolzen);
    }
}