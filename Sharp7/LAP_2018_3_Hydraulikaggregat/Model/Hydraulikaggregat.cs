using System.Diagnostics;
using System.Threading;
using System.Windows;

namespace LAP_2018_3_Hydraulikaggregat.Model
{
    public class Hydraulikaggregat
    {
        public bool B1 { get; set; }    // Sensor Ölstand
        public bool B2 { get; set; }    // Sensor Druck erreicht
        public bool B3 { get; set; }    // Sensor Überdruck
        public bool B4 { get; set; }    // Temperatur Ölbehälter
        public bool B5 { get; set; }    // Druckschalter "Ölfilter"  
        public bool F1 { get; set; }    // Motorstörung
        public bool K1 { get; set; }    // Ventil "Zylinder ausfahren"  
        public bool K2 { get; set; }    // Ventil "Zylinder einfahren" 
        public bool P1 { get; set; }    // Meldeleuchte Störung Motor
        public bool P2 { get; set; }    // Meldeleuchte Überdruck
        public bool P3 { get; set; }    // Meldeleuchte Druck erreicht
        public bool P4 { get; set; }    // Meldeleuchte Ölstand min.
        public bool P5 { get; set; }    // Meldeleuchte "Lüfter Ölkühler"  
        public bool P6 { get; set; }    // Meldeleuchte "Ölfilter wechseln" 
        public bool Q1 { get; set; }    // Netzschütz
        public bool Q2 { get; set; }    // Sternschütz
        public bool Q3 { get; set; }    // Dreieckschütz
        public bool Q4 { get; set; }    //Lüfter(Ölkühler)
        public bool S1 { get; set; }    // Taster Start
        public bool S2 { get; set; }    // Taster Stop
        public bool S3 { get; set; }    // Taster Quittieren
        public bool S4 { get; set; }    //Taster "Zylinder"  

        public double Druck { get; set; }
        public double Pegel { get; set; }
        public Stopwatch Stopwatch { get; set; }

        private const double DruckVerlust = 0.998;
        private const double DruckAnstieg = 0.04;
        private const double PegelVerlust = 0.999;
        private const int ZeitBisStoerung = 10000;

        private readonly MainWindow _mainWindow;
        private readonly ViewModel.ViewModel _viewModel;

        public Hydraulikaggregat(MainWindow mainWindow, ViewModel.ViewModel viewModel)
        {
            _mainWindow = mainWindow;
            _viewModel = viewModel;

            Druck = 0;
            Pegel = 0.8;
            B3 = true;
            F1 = true;
            S2 = true;

            Stopwatch = new Stopwatch();
            Stopwatch.Restart();

            System.Threading.Tasks.Task.Run(HydraulikaggregatTask);
        }

        private void HydraulikaggregatTask()
        {
            while (true)
            {
                if (Q1 && Q3)
                {
                    Pegel *= PegelVerlust;
                    Druck += DruckAnstieg;
                    Stopwatch.Start();
                }
                else Stopwatch.Stop();

                Druck *= DruckVerlust;

                if (Druck > 10) Druck = 10;

                if (B2)
                {
                    if (Druck > 8) B2 = false;
                }
                else
                {
                    if (Druck < 7) B2 = true;
                }

                B1 = Pegel > 0.45;

                B3 = Stopwatch.ElapsedMilliseconds <= ZeitBisStoerung;


                ErweiterungOelkuehler();
                ErweiterungZylinder();
                ErweiterungOelfilter();

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }


        internal void ErweiterungOelkuehler()
        {
            //if ((bool)_mainWindow.ChkOelkuehler.IsChecked)
        }

        internal void ErweiterungZylinder()
        {
            // if ((bool)_mainWindow.ChZylinder.IsChecked)
        }

        internal void ErweiterungOelfilter()
        {
            // if ((bool)_mainWindow.ChkOelfilter.IsChecked) 
        }
        internal void CheckErweiterungOelkuehler()
        {
            if (_mainWindow.ChkOelkuehler.IsChecked != null && (bool)_mainWindow.ChkOelkuehler.IsChecked) _viewModel.ViAnzeige.OelkuehlerAbgedeckt = Visibility.Hidden; else _viewModel.ViAnzeige.OelkuehlerAbgedeckt = Visibility.Visible;
        }
        internal void CheckErweiterungZylinder()
        {
            if (_mainWindow.ChZylinder.IsChecked != null && (bool)_mainWindow.ChZylinder.IsChecked) _viewModel.ViAnzeige.ZylinderAbgedeckt = Visibility.Hidden; else _viewModel.ViAnzeige.ZylinderAbgedeckt = Visibility.Visible;
        }
        internal void CheckErweiterungOelfilter()
        {
            if (_mainWindow.ChkOelfilter.IsChecked != null && (bool)_mainWindow.ChkOelfilter.IsChecked) _viewModel.ViAnzeige.OelfilterAbgedeckt = Visibility.Hidden; else _viewModel.ViAnzeige.OelfilterAbgedeckt = Visibility.Visible;
        }

        internal void BtnF1() => F1 = !F1;

        internal void BtnNachfuellen() => Pegel = 1;
    }
}