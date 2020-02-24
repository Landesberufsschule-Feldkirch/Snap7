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
        private readonly Synchronisiereinrichtung.kraftwerk.Model.Kraftwerk kraftwerk;

        public Schreiber(Synchronisiereinrichtung.kraftwerk.Model.Kraftwerk kw)
        {
            kraftwerk = kw;
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
                    List<DoubleDataPoint> yy = new List<DoubleDataPoint>()
                    {
                        kraftwerk.ManualVentilstellung,
                        kraftwerk.ManualErregerstrom,
                        kraftwerk.Generator_n,
                        kraftwerk.Generator_f,
                        kraftwerk.Generator_U,
                        kraftwerk.SpannungsdifferenzGeneratorNetz,
                        kraftwerk.Generator_P
                    };

                    var x = watch.Elapsed;

                    List<TimeSpanDataPoint> xx = new List<TimeSpanDataPoint>()
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
