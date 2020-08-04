using RealTimeGraphX.DataPoints;
using RealTimeGraphX.WPF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Synchronisiereinrichtung.kraftwerk.ViewModel
{
    public class Schreiber
    {
        public WpfGraphController<TimeSpanDataPoint, DoubleDataPoint> MultiController { get; set; }
        private readonly Model.Kraftwerk _kraftwerk;

        public Schreiber(Model.Kraftwerk kw)
        {
            _kraftwerk = kw;
            MultiController = new WpfGraphController<TimeSpanDataPoint, DoubleDataPoint>();
            MulticontrollerFuellen();
        }

        private void MulticontrollerFuellen()
        {
            MultiController.Range.MinimumY = 0;
            MultiController.Range.MaximumY = 1080;
            MultiController.Range.MaximumX = TimeSpan.FromSeconds(30);
            MultiController.Range.AutoY = true;

            MultiController.DataSeriesCollection.Add(new WpfGraphDataSeries()
            {
                Name = "Ventilöffnung Y",
                Stroke = Colors.Red,
            });

            MultiController.DataSeriesCollection.Add(new WpfGraphDataSeries()
            {
                Name = "Erregerstrom IE",
                Stroke = Colors.Green,
            });

            MultiController.DataSeriesCollection.Add(new WpfGraphDataSeries()
            {
                Name = "Drehzahl n",
                Stroke = Colors.Blue,
            });

            MultiController.DataSeriesCollection.Add(new WpfGraphDataSeries()
            {
                Name = "Frequenz f",
                Stroke = Colors.Yellow,
            });

            MultiController.DataSeriesCollection.Add(new WpfGraphDataSeries()
            {
                Name = "Generatorspannung UG",
                Stroke = Colors.Gray,
            });

            MultiController.DataSeriesCollection.Add(new WpfGraphDataSeries()
            {
                Name = "Spannungsdifferenz Ud",
                Stroke = Colors.YellowGreen,
            });

            MultiController.DataSeriesCollection.Add(new WpfGraphDataSeries()
            {
                Name = "Leistung P",
                Stroke = Colors.AliceBlue,
            });

            Stopwatch watch = new Stopwatch();
            watch.Start();

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    var yy = new List<DoubleDataPoint>()
                    {
                        _kraftwerk.ManualVentilstellung,
                        _kraftwerk.ManualErregerstrom,
                        _kraftwerk.GeneratorN,
                        _kraftwerk.GeneratorF,
                        _kraftwerk.GeneratorU,
                        _kraftwerk.SpannungsdifferenzGeneratorNetz,
                        _kraftwerk.GeneratorP
                    };

                    var x = watch.Elapsed;

                    var xx = new List<TimeSpanDataPoint>()
                    {
                        x,
                        x,
                        x,
                        x,
                        x,
                        x,
                        x
                    };

                    MultiController.PushData(xx, yy);

                    Thread.Sleep(50);
                }
            });
        }
    }
}