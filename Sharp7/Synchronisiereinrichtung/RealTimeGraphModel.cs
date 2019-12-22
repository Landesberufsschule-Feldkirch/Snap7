using RealTimeGraphX.DataPoints;
using RealTimeGraphX.Renderers;
using RealTimeGraphX.WPF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Synchronisiereinrichtung
{
    public class ViewModel
    {
        //Graph controller with timespan as X axis and double as Y.
        public WpfGraphController<TimeSpanDataPoint, DoubleDataPoint> Controller { get; set; }

        public ViewModel()
        {
            Controller = new WpfGraphController<TimeSpanDataPoint, DoubleDataPoint>();
            Controller.Renderer = new ScrollingLineRenderer<WpfGraphDataSeries>();
            Controller.DataSeriesCollection.Add(new WpfGraphDataSeries()
            {
                Name = "Series Name",
                Stroke = Colors.Red,
            });

            //We will attach the surface using WPF binding...
            //Controller.Surface = null;
        }

        private void Start()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    //Get the current elapsed time and mouse position.
                    var y = System.Windows.Forms.Cursor.Position.Y;
                    var x = watch.Elapsed;

                    //Push the x,y to the controller.
                    Controller.PushData(x, y);

                    Thread.Sleep(30);
                }
            });
            thread.Start();
        }
    }
}
