namespace PaternosterLager.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using System.Windows.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {

        private readonly PaternosterLager.Model.Paternosterlager paternosterlager;
        private readonly MainWindow mainWindow;


        private const double geschwindigkeit = 0.1;

        public VisuAnzeigen(MainWindow mw, PaternosterLager.Model.Paternosterlager pa)
        {
            mainWindow = mw;
            paternosterlager = pa;

            ClickModeBtnAuf = "Press";
            ClickModeBtnAb = "Press";

            SpsStatus = "-";
            SpsColor = "LightBlue";


            AlleKettengliedRegale = new ObservableCollection<KettengliedRegal>();
            for (var i = 0; i < 20; i++) AlleKettengliedRegale.Add(new KettengliedRegal(i));

            Thread t = new Thread(VisuAnzeigenTask);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            //System.Threading.Tasks.Task.Run(() => VisuAnzeigenTask());
        }
        private void VisuAnzeigenTask()
        {
            bool lagerGezeichnet = false;

            while (true)
            {
                if (mainWindow.S7_1200 != null)
                {
                    if (mainWindow.S7_1200.GetSpsError()) SpsColor = "Red"; else SpsColor = "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }


                if (mainWindow.ZeichenFlaeche != null && !lagerGezeichnet)
                {
                    lagerGezeichnet = true;
                    LagerHinzufuegen();
                }

                Thread.Sleep(10);
            }
        }

        public void LagerHinzufuegen()
        {

            foreach (var kettengliedRegal in AlleKettengliedRegale)
            {
                var myPath = new Path
                {
                    Fill = Brushes.LemonChiffon,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    Data = kettengliedRegal.GetKettengliedRegal()
                };

                Canvas.SetLeft(myPath, kettengliedRegal.GetPosX());
                Canvas.SetTop(myPath, kettengliedRegal.GetPosY());

                mainWindow.ZeichenFlaeche.Children.Add(myPath);
            }
        }

        public void SetGeschwindigkeit(double geschwindigkeit)
        {
            foreach (var kettengliedRegal in paternosterlager.AlleKettengliedRegale) kettengliedRegal.SetGeschwindigkeit(geschwindigkeit);
        }


        internal void TasterAuf()
        {
            paternosterlager.RichtungAuf = ClickModeButtonAuf();
            SetGeschwindigkeit(geschwindigkeit);
        }
        internal void TasterAb()
        {
            paternosterlager.RichtungAb = ClickModeButtonAb();
            SetGeschwindigkeit(-geschwindigkeit);
        }






        #region SPS Status und Farbe
        private string _spsStatus;
        public string SpsStatus
        {
            get { return _spsStatus; }
            set
            {
                _spsStatus = value;
                OnPropertyChanged(nameof(SpsStatus));
            }
        }



        private string _spsColor;
        public string SpsColor
        {
            get { return _spsColor; }
            set
            {
                _spsColor = value;
                OnPropertyChanged(nameof(SpsColor));
            }
        }
        #endregion



        #region KettengliederRegale

        private ObservableCollection<KettengliedRegal> _alleKettengliedRegale;
        public ObservableCollection<KettengliedRegal> AlleKettengliedRegale
        {
            get { return _alleKettengliedRegale; }
            set
            {
                _alleKettengliedRegale = value;
                OnPropertyChanged(nameof(AlleKettengliedRegale));
            }
        }

        #endregion







        #region ClickModeBtnAuf
        public bool ClickModeButtonAuf()
        {
            if (ClickModeBtnAuf == "Press")
            {
                ClickModeBtnAuf = "Release";
                return true;
            }
            else
            {
                ClickModeBtnAuf = "Press";
            }
            return false;
        }

        private string _clickModeBtnAuf;
        public string ClickModeBtnAuf
        {
            get { return _clickModeBtnAuf; }
            set
            {
                _clickModeBtnAuf = value;
                OnPropertyChanged(nameof(ClickModeBtnAuf));
            }
        }
        #endregion

        #region ClickModeBtnAb
        public bool ClickModeButtonAb()
        {
            if (ClickModeBtnAb == "Press")
            {
                ClickModeBtnAb = "Release";
                return true;
            }
            else
            {
                ClickModeBtnAb = "Press";
            }
            return false;
        }

        private string _clickModeBtnAb;
        public string ClickModeBtnAb
        {
            get { return _clickModeBtnAb; }
            set
            {
                _clickModeBtnAb = value;
                OnPropertyChanged(nameof(ClickModeBtnAb));
            }
        }
        #endregion




        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion iNotifyPeropertyChanged Members

    }
}
