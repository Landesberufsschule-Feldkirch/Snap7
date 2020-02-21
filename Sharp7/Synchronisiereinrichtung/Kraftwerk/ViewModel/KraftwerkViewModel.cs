namespace Synchronisiereinrichtung.Kraftwerk.ViewModel
{
    using RealTimeGraphX.DataPoints;
    using RealTimeGraphX.WPF;
    using Synchronisiereinrichtung.Kraftwerk.Commands;
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
        private readonly Model.Kraftwerk kraftwerk;

        public KraftwerkViewModel(MainWindow mainWindow)
        {
            kraftwerk = new Model.Kraftwerk(mainWindow);
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
                        Kraftwerk.ViSoll.ManualVentilstellung,
                        Kraftwerk.ViSoll.ManualErregerstrom,
                        Kraftwerk.Generator_n,
                        Kraftwerk.Generator_f,
                        Kraftwerk.Generator_U,
                        Kraftwerk.SpannungsdifferenzGeneratorNetz,
                        Kraftwerk.Generator_P
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

        public Model.Kraftwerk Kraftwerk { get { return kraftwerk; } }


        #region BtnSchalterReset
        private ICommand _btnSchalterReset;
        public ICommand BtnSchalterReset
        {
            get
            {
                if (_btnSchalterReset == null)
                {
                    _btnSchalterReset = new RelayCommand(p => kraftwerk.Reset(), p => true);
                }
                return _btnSchalterReset;
            }
        }
        #endregion

        #region BtnSchalterQ1
        private ICommand _btnSchalterQ1;
        public ICommand BtnSchalterQ1
        {
            get
            {
                if (_btnSchalterQ1 == null)
                {
                    _btnSchalterQ1 = new RelayCommand(p => kraftwerk.Synchronisieren(), p => true);
                }
                return _btnSchalterQ1;
            }
        }
        #endregion

        #region BtnSchalterStart
        private ICommand _btnSchalterStart;
        public ICommand BtnSchalterStart
        {
            get
            {
                if (_btnSchalterStart == null)
                {
                    _btnSchalterStart = new RelayCommand(p => kraftwerk.Starten(), p => true);
                }
                return _btnSchalterStart;
            }
        }
        #endregion

        #region BtnSchalterStop
        private ICommand _btnSchalterStop;
        public ICommand BtnSchalterStop
        {
            get
            {
                if (_btnSchalterStop == null)
                {
                    _btnSchalterStop = new RelayCommand(p => kraftwerk.Stoppen(), p => true);
                }
                return _btnSchalterStop;
            }
        }
        #endregion

    }
}