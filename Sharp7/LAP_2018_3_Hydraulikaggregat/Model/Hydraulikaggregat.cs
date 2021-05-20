using System.Diagnostics;
using System.Threading;
using System.Windows;

namespace LAP_2018_3_Hydraulikaggregat.Model
{
    public class Hydraulikaggregat
    {
        public bool B1 { get; set; }    // Sensor Ölstand
        public bool B2 { get; set; }    // Sensor Druck erreicht
        public bool B3 { get; set; }    // Sensor Überdruck (Öffner)
        public bool B4 { get; set; }    // Temperatur Ölbehälter (Öffner)
        public bool B5 { get; set; }    // Druckschalter "Ölfilter" (Öffner) 
        public bool F1 { get; set; }    // Motorstörung (Öffner)
        public bool K1 { get; set; }    // Ventil "Zylinder ausfahren"  
        public bool K2 { get; set; }    // Ventil "Zylinder einfahren" 
        public bool P1 { get; set; }    // Meldeleuchte "Betriebsbereit" 
        public bool P2 { get; set; }    // Meldeleuchte Störung Motor
        public bool P3 { get; set; }    // Meldeleuchte Überdruck
        public bool P4 { get; set; }    // Meldeleuchte Druck erreicht
        public bool P5 { get; set; }    // Meldeleuchte Ölstand min.
        public bool P6 { get; set; }    // Meldeleuchte "Lüfter Ölkühler"  
        public bool P7 { get; set; }    // Meldeleuchte "Ölfilter wechseln" 
        public bool Q1 { get; set; }    // Netzschütz
        public bool Q2 { get; set; }    // Sternschütz
        public bool Q3 { get; set; }    // Dreieckschütz
        public bool Q4 { get; set; }    // Lüfter(Ölkühler)
        public bool S1 { get; set; }    // Taster Start
        public bool S2 { get; set; }    // Taster Stop
        public bool S3 { get; set; }    // Taster Quittieren
        public bool S4 { get; set; }    // Taster "Zylinder"  

        public double Druck { get; set; }
        public double Pegel { get; set; }
        public Stopwatch Stopwatch { get; set; }

        private const double DruckVerlust = 0.998;
        private const double DruckAnstieg = 0.04;
        private const double PegelVerlust = 0.999;

        private const double DruckMin = 7;
        private const double DruckMax = 8;
        private const double PegelMin = 0.45;

        private readonly MainWindow _mainWindow;
        private readonly ViewModel.ViewModel _viewModel;

        private bool ErweiterungOelkuehlerAktiv;
        private bool ErweiterungZylinderAktiv;
        private bool ErweiterungOelfilterAktiv;

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
                else
                {
                    Stopwatch.Stop();
                }

                Druck *= DruckVerlust;

                if (Druck > 10) Druck = 10;

                if (B2)
                {
                    if (Druck > DruckMax) B2 = true;
                }
                else
                {
                    if (Druck < DruckMin) B2 = false;
                }

                B1 = Pegel > PegelMin;

                ErweiterungOelkuehler();
                ErweiterungZylinder();
                ErweiterungOelfilter();

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }


        internal void ErweiterungOelkuehler()
        {
            if (ErweiterungOelkuehlerAktiv)
            {
                //
            }
            else
            {
                P6 = false;
            }
        }

        internal void ErweiterungZylinder()
        {
            if (ErweiterungZylinderAktiv)
            {
                //
            }
            else
            {
                K1 = false;
                K2 = false;
            }
        }

        internal void ErweiterungOelfilter()
        {
            if (ErweiterungOelfilterAktiv)
            {
                //
            }
            else
            {
                P7 = false;
            }
        }
        internal void CheckErweiterungOelkuehler()
        {
            ErweiterungOelkuehlerAktiv = _mainWindow.ChkOelkuehler.IsChecked != null && (bool)_mainWindow.ChkOelkuehler.IsChecked;
            _viewModel.ViAnzeige.OelkuehlerAbgedeckt = ErweiterungOelkuehlerAktiv ? Visibility.Hidden : Visibility.Visible;
        }
        internal void CheckErweiterungZylinder()
        {
            ErweiterungZylinderAktiv = _mainWindow.ChZylinder.IsChecked != null && (bool)_mainWindow.ChZylinder.IsChecked;
            _viewModel.ViAnzeige.ZylinderAbgedeckt = ErweiterungZylinderAktiv ? Visibility.Hidden : Visibility.Visible;

        }
        internal void CheckErweiterungOelfilter()
        {
            ErweiterungOelfilterAktiv = _mainWindow.ChkOelfilter.IsChecked != null && (bool)_mainWindow.ChkOelfilter.IsChecked;
            _viewModel.ViAnzeige.OelfilterAbgedeckt = ErweiterungOelfilterAktiv ? Visibility.Hidden : Visibility.Visible;
        }

        internal void BtnF1() => F1 = !F1;
        internal void BtnUeberdruck() => B3 = !B3;
        internal void BtnNachfuellen() => Pegel = 1;
    }
}