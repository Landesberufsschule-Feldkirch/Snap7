using RealTimeGraphX.DataPoints;
using RealTimeGraphX.Renderers;
using RealTimeGraphX.WPF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Media;

namespace Synchronisiereinrichtung
{
    public class ViewModel
    {
        //Graph controller with timespan as X axis and double as Y.
        public WpfGraphController<TimeSpanDataPoint, DoubleDataPoint> Controller { get; set; }
        public WpfGraphController<TimeSpanDataPoint, DoubleDataPoint> MultiController { get; set; }

        public ViewModel()
        {
            Controller = new WpfGraphController<TimeSpanDataPoint, DoubleDataPoint>();
            Controller.Renderer = new ScrollingLineRenderer<WpfGraphDataSeries>();
            Controller.Range.AutoY = true;

            Controller.DataSeriesCollection.Add(new WpfGraphDataSeries()
            {
                Name = "Series Name",
                Stroke = Colors.Red,
                IsVisible=true,
            });

            MultiController = new WpfGraphController<TimeSpanDataPoint, DoubleDataPoint>();
            MultiController.Range.MinimumY = 0;
            MultiController.Range.MaximumY = 1080;
            MultiController.Range.MaximumX = TimeSpan.FromSeconds(10);
            MultiController.Range.AutoY = true;

            MultiController.DataSeriesCollection.Add(new WpfGraphDataSeries()
            {
                Name = "Series 1",
                Stroke = Colors.Red,
            });

            MultiController.DataSeriesCollection.Add(new WpfGraphDataSeries()
            {
                Name = "Series 2",
                Stroke = Colors.Green,
            });

            MultiController.DataSeriesCollection.Add(new WpfGraphDataSeries()
            {
                Name = "Series 3",
                Stroke = Colors.Blue,
            });

            MultiController.DataSeriesCollection.Add(new WpfGraphDataSeries()
            {
                Name = "Series 4",
                Stroke = Colors.Yellow,
            });

            MultiController.DataSeriesCollection.Add(new WpfGraphDataSeries()
            {
                Name = "Series 5",
                Stroke = Colors.Gray,
            });

            //We will attach the surface using WPF binding...
            //Controller.Surface = null;
        }

        public void Start()
        {
            Random random = new Random();


            Stopwatch watch = new Stopwatch();
            watch.Start();

            Thread thread = new Thread(() =>
            {

                while (true)
                {

                    var x = watch.Elapsed;
                    var y = random.Next(0, 100);


                    List<DoubleDataPoint> yy = new List<DoubleDataPoint>()
                    {
                        y,
                        y + 20,
                        y + 40,
                        y + 60,
                        y + 80,
                    };



                    List<TimeSpanDataPoint> xx = new List<TimeSpanDataPoint>()
                    {
                        x,
                        x,
                        x,
                        x,
                        x
                    };

                    Controller.PushData(x, y);
                    MultiController.PushData(xx, yy);

                    Thread.Sleep(30);
                }

            });
            thread.Start();
        }
    }
}
