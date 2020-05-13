namespace AutomatischesLagersystem.ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading;
    using System.Windows.Media.Media3D;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.AutomatischesLagersystem automatischesLagersystem;
        private readonly MainWindow mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.AutomatischesLagersystem al)
        {
            mainWindow = mw;
            automatischesLagersystem = al;

            for (int i = 0; i < 100; i++) ClickModeBtn.Add("Press");

            IstPosition = "00";
            SollPosition = "00";

            XPosSlider = 0;
            YPosSlider = 0;
            ZPosSlider = 0;

            VisibilityB1Ein = "hidden";
            VisibilityB1Aus = "visible";

            VisibilityB2Ein = "Visible";
            VisibilityB2Aus = "Hidden";

            SpsStatus = "-";
            SpsColor = "LightBlue";

            System.Threading.Tasks.Task.Run(() => VisuAnzeigenTask());
        }

        private void VisuAnzeigenTask()
        {
            int i = -1000;

            while (true)
            {
                if (mainWindow.viewPort3d != null)
                {
                    if (i < 1000) i++; else i = -1000;
                    mainWindow.Dispatcher.Invoke(() =>
                   {
                       if (mainWindow.FensterAktiv)
                       {
                           //mainWindow.viewPort3d.Children[3].Transform = new TranslateTransform3D(XSliderPosition(), YSliderPosition(), ZSliderPosition());

                           foreach (var model in mainWindow.viewPort3d.Children)
                           {
                               i++;
                           }
                       }
                   });
                }



                if (mainWindow.S7_1200 != null)
                {
                    if (mainWindow.S7_1200.GetSpsError()) SpsColor = "Red"; else SpsColor = "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }

                Thread.Sleep(100);
            }
        }

        internal void Buchstabe(object buchstabe)
        {
            if (buchstabe is string ascii)
            {
                var asciiCode = ascii[0];
                if (ClickModeButton(asciiCode)) automatischesLagersystem.Zeichen = asciiCode; else automatischesLagersystem.Zeichen = ' ';
            }
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

        #endregion SPS Status und Farbe

        #region Sichtbarkeit B1

        public void SichtbarkeitB1(bool val)
        {
            if (val)
            {
                VisibilityB1Ein = "visible";
                VisibilityB1Aus = "hidden";
            }
            else
            {
                VisibilityB1Ein = "hidden";
                VisibilityB1Aus = "visible";
            }
        }

        private string _VisibilityB1Ein;

        public string VisibilityB1Ein
        {
            get { return _VisibilityB1Ein; }
            set
            {
                _VisibilityB1Ein = value;
                OnPropertyChanged(nameof(VisibilityB1Ein));
            }
        }

        private string _VisibilityB1Aus;

        public string VisibilityB1Aus
        {
            get { return _VisibilityB1Aus; }
            set
            {
                _VisibilityB1Aus = value;
                OnPropertyChanged(nameof(VisibilityB1Aus));
            }
        }

        #endregion Sichtbarkeit B1

        #region Sichtbarkeit B2

        public void SichtbarkeitB2(bool val)
        {
            if (val)
            {
                VisibilityB2Ein = "Visible";
                VisibilityB2Aus = "Hidden";
            }
            else
            {
                VisibilityB2Ein = "Hidden";
                VisibilityB2Aus = "Visible";
            }
        }

        private string _VisibilityB2Ein;

        public string VisibilityB2Ein
        {
            get { return _VisibilityB2Ein; }
            set
            {
                _VisibilityB2Ein = value;
                OnPropertyChanged(nameof(VisibilityB2Ein));
            }
        }

        private string _VisibilityB2Aus;

        public string VisibilityB2Aus
        {
            get { return _VisibilityB2Aus; }
            set
            {
                _VisibilityB2Aus = value;
                OnPropertyChanged(nameof(VisibilityB2Aus));
            }
        }

        #endregion Sichtbarkeit B2


        #region ClickModeAlleButtons

        public bool ClickModeButton(int AsciiCode)
        {
            if (ClickModeBtn[AsciiCode] == "Press")
            {
                ClickModeBtn[AsciiCode] = "Release";
                return true;
            }
            else
            {
                ClickModeBtn[AsciiCode] = "Press";
            }
            return false;
        }

        private ObservableCollection<string> _clickModeBtn = new ObservableCollection<string>();

        public ObservableCollection<string> ClickModeBtn
        {
            get { return _clickModeBtn; }
            set
            {
                _clickModeBtn = value;
                OnPropertyChanged(nameof(ClickModeBtn));
            }
        }

        #endregion ClickModeAlleButtons


        #region IstPosition

        private string _istPosition;

        public string IstPosition
        {
            get { return _istPosition; }
            set
            {
                _istPosition = value;
                OnPropertyChanged(nameof(IstPosition));
            }
        }

        #endregion IstPosition

        #region SollPosition

        private string _sollPosition;

        public string SollPosition
        {
            get { return _sollPosition; }
            set
            {
                _sollPosition = value;
                OnPropertyChanged(nameof(SollPosition));
            }
        }

        #endregion SollPosition


        #region xPosSlider

        public double XSliderPosition() => XPosSlider;

        private double _xSliderPosition;

        public double XPosSlider
        {
            get { return _xSliderPosition; }
            set
            {
                _xSliderPosition = value;
                OnPropertyChanged(nameof(XPosSlider));
            }
        }

        #endregion xPosSlider

        #region yPosSlider

        public double YSliderPosition() => YPosSlider;

        private double _ySliderPosition;

        public double YPosSlider
        {
            get { return _ySliderPosition; }
            set
            {
                _ySliderPosition = value;
                OnPropertyChanged(nameof(YPosSlider));
            }
        }

        #endregion yPosSlider

        #region zPosSlider

        public double ZSliderPosition() => ZPosSlider;

        private double _zSliderPosition;

        public double ZPosSlider
        {
            get { return _zSliderPosition; }
            set
            {
                _zSliderPosition = value;
                OnPropertyChanged(nameof(ZPosSlider));
            }
        }

        #endregion zPosSlider




        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion iNotifyPeropertyChanged Members
    }
}