namespace LAP_2018_3_Hydraulikaggregat.ViewModel
{
    using LAP_2018_3_Hydraulikaggregat.Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public VisuAnzeigen ViAnzeige { get; set; }
        public readonly LAP_2018_3_Hydraulikaggregat.Model.Hydraulikaggregat hydraulikaggregat;

        public ViewModel(MainWindow mainWindow)
        {
            hydraulikaggregat = new LAP_2018_3_Hydraulikaggregat.Model.Hydraulikaggregat();
            ViAnzeige = new VisuAnzeigen(mainWindow, hydraulikaggregat);
        }

        public LAP_2018_3_Hydraulikaggregat.Model.Hydraulikaggregat Hydraulikaggregat { get { return hydraulikaggregat; } }

        #region BtnNachfuellen

        private ICommand _btnNachfuellen;

        public ICommand BtnNachfuellen
        {
            get
            {
                if (_btnNachfuellen == null)
                {
                    _btnNachfuellen = new RelayCommand(p => hydraulikaggregat.BtnNachfuellen(), p => true);
                }
                return _btnNachfuellen;
            }
        }

        #endregion BtnNachfuellen

        #region BtnF1

        private ICommand _btnF1;

        public ICommand BtnF1
        {
            get
            {
                if (_btnF1 == null)
                {
                    _btnF1 = new RelayCommand(p => hydraulikaggregat.BtnF1(), p => true);
                }
                return _btnF1;
            }
        }

        #endregion BtnF1

        #region BtnQ1

        private ICommand _btnQ1;

        public ICommand BtnQ1
        {
            get
            {
                if (_btnQ1 == null)
                {
                    _btnQ1 = new RelayCommand(p => ViAnzeige.BtnQ1(), p => true);
                }
                return _btnQ1;
            }
        }

        #endregion BtnQ1

        #region BtnQ2

        private ICommand _btnQ2;

        public ICommand BtnQ2
        {
            get
            {
                if (_btnQ2 == null)
                {
                    _btnQ2 = new RelayCommand(p => ViAnzeige.BtnQ2(), p => true);
                }
                return _btnQ2;
            }
        }

        #endregion BtnQ2

        #region BtnQ3

        private ICommand _btnQ3;

        public ICommand BtnQ3
        {
            get
            {
                if (_btnQ3 == null)
                {
                    _btnQ3 = new RelayCommand(p => ViAnzeige.BtnQ3(), p => true);
                }
                return _btnQ3;
            }
        }

        #endregion BtnQ3

        #region BtnQ1_Q3

        private ICommand _btnQ1_Q3;

        public ICommand BtnQ1_Q3
        {
            get
            {
                if (_btnQ1_Q3 == null)
                {
                    _btnQ1_Q3 = new RelayCommand(p => ViAnzeige.BtnQ1_Q3(), p => true);
                }
                return _btnQ1_Q3;
            }
        }

        #endregion BtnQ1_Q3

        #region BtnS1

        private ICommand _btnS1;

        public ICommand BtnS1
        {
            get
            {
                if (_btnS1 == null)
                {
                    _btnS1 = new RelayCommand(p => ViAnzeige.BtnS1(), p => true);
                }
                return _btnS1;
            }
        }

        #endregion BtnS1

        #region BtnS2

        private ICommand _btnS2;

        public ICommand BtnS2
        {
            get
            {
                if (_btnS2 == null)
                {
                    _btnS2 = new RelayCommand(p => ViAnzeige.BtnS2(), p => true);
                }
                return _btnS2;
            }
        }

        #endregion BtnS2

        #region BtnS3

        private ICommand _btnS3;

        public ICommand BtnS3
        {
            get
            {
                if (_btnS3 == null)
                {
                    _btnS3 = new RelayCommand(p => ViAnzeige.BtnS3(), p => true);
                }
                return _btnS3;
            }
        }

        #endregion BtnS3
    }
}