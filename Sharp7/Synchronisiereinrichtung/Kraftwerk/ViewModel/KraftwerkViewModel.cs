namespace Synchronisiereinrichtung.Kraftwerk.ViewModel
{
    using RealTimeGraphX.WPF;
    using RealTimeGraphX.DataPoints;
    using Synchronisiereinrichtung.Kraftwerk.Command;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using System.Windows.Media;

    public class KraftwerkViewModel
    {
        public WpfGraphController<TimeSpanDataPoint, DoubleDataPoint> MultiController { get; set; }
        private readonly Model.Kraftwerk _Kraftwerk;

        public KraftwerkViewModel()
        {
            _Kraftwerk = new Model.Kraftwerk();
            UpdateSchalterReset = new KraftwerkUpdateManualReset(this);
            UpdateSchalterQ1 = new KraftwerkUpdateManualQ1(this);
            UpdateSchalterStart = new KraftwerkUpdateStart(this);
            UpdateSchalterStop = new KraftwerkUpdateStop(this);

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

            Stopwatch watch = new Stopwatch();
            watch.Start();

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    List<DoubleDataPoint> yy = new List<DoubleDataPoint>()
                    {
                        Kraftwerk.ViSoll.ManualVentilstellung,
                        Kraftwerk.ViSoll.ManualErregerstrom,
                        Kraftwerk.Generator_n,// mainWindow.n,
                       Kraftwerk.Generator_f, // mainWindow.fGenerator,
                       Kraftwerk.Generator_U // mainWindow.UGenerator,
                    };

                    var x = watch.Elapsed;

                    List<TimeSpanDataPoint> xx = new List<TimeSpanDataPoint>()
                    {
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

        public Model.Kraftwerk Kraftwerk
        {
            get { return _Kraftwerk; }
        }

        public bool CanUpdateQ1
        {
            get { return true; }
        }

        public bool CanUpdateReset
        {
            get { return true; }
        }

        public bool CanUpdateStart
        {
            get { return true; }
        }

        public bool CanUpdateStop
        {
            get { return true; }
        }

        public ICommand UpdateSchalterReset
        {
            get;
            private set;
        }

        public ICommand UpdateSchalterQ1
        {
            get;
            set;
        }

        public ICommand UpdateSchalterStart
        {
            get;
            private set;
        }

        public ICommand UpdateSchalterStop
        {
            get;
            set;
        }

        internal void SchalterQ1()
        {
            _Kraftwerk.Synchronisieren();
        }

        public void SchalterReset()
        {
            _Kraftwerk.Reset();
        }

        internal void SchalterStart()
        {
            _Kraftwerk.Starten();
        }

        public void SchalterStop()
        {
            _Kraftwerk.Stoppen();
        }
    }
}