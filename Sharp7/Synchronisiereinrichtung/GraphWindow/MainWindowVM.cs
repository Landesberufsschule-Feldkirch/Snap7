using RealTimeGraphX.DataPoints;
using RealTimeGraphX.WPF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Synchronisiereinrichtung
{
    public class MainWindowVM
    {
        private readonly MainWindow mainWindow;

        public WpfGraphController<TimeSpanDataPoint, DoubleDataPoint> MultiController { get; set; }

        public MainWindowVM(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;

            MultiController = new WpfGraphController<TimeSpanDataPoint, DoubleDataPoint>();
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
                Name = "Phasenlage",
                Stroke = Colors.Crimson
            });

            Stopwatch watch = new Stopwatch();
            watch.Start();

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    List<DoubleDataPoint> yy = new List<DoubleDataPoint>()
                    {
                        mainWindow.Y,
                        mainWindow.Ie,
                        mainWindow.Y, // mainWindow.n,
                       mainWindow.Y, // mainWindow.fGenerator,
                       mainWindow.Y, // mainWindow.UGenerator,
                       mainWindow.Y, // mainWindow.Phasenlage,
                    };

                    var x = watch.Elapsed;

                    List<TimeSpanDataPoint> xx = new List<TimeSpanDataPoint>()
                    {
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
