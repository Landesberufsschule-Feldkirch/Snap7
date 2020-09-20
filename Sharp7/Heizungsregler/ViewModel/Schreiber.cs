using RealTimeGraphX.DataPoints;
using RealTimeGraphX.WPF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Heizungsregler.ViewModel
{
    public class Schreiber
    {
        public WpfGraphController<TimeSpanDataPoint, DoubleDataPoint> MultiController { get; set; }
        private readonly MainWindow _mainWindow;

        public Schreiber(MainWindow mw)
        {
            _mainWindow = mw;
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
                        1,2,3,4,5,6,7
                        /*
                        kraftwerk.ManualVentilstellung,
                        kraftwerk.ManualErregerstrom,
                        kraftwerk.Generator_n,
                        kraftwerk.Generator_f,
                        kraftwerk.Generator_U,
                        kraftwerk.SpannungsdifferenzGeneratorNetz,
                        kraftwerk.Generator_P
                        */
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
                // ReSharper disable once FunctionNeverReturns
            });
        }
    }
}