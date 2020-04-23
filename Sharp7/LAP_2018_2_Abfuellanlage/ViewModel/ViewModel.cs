namespace LAP_2018_2_Abfuellanlage.ViewModel
{
    using LAP_2018_2_Abfuellanlage.Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public readonly Model.Abfuellanlage abfuellAnlage;
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            abfuellAnlage = new Model.Abfuellanlage();
            ViAnzeige = new VisuAnzeigen(mainWindow, abfuellAnlage);
        }

        public Model.Abfuellanlage Abfuellanlage { get { return abfuellAnlage; } }

        #region BtnReset

        private ICommand _btnReset;

        public ICommand BtnReset
        {
            get
            {
                if (_btnReset == null)
                {
                    _btnReset = new RelayCommand(p => abfuellAnlage.AllesReset(), p => true);
                }
                return _btnReset;
            }
        }

        #endregion BtnReset

        #region BtnNachfuellen

        private ICommand _btnNachfuellen;

        public ICommand BtnNachfuellen
        {
            get
            {
                if (_btnNachfuellen == null)
                {
                    _btnNachfuellen = new RelayCommand(p => abfuellAnlage.TasterNachfuellen(), p => true);
                }
                return _btnNachfuellen;
            }
        }

        #endregion BtnNachfuellen

        #region BtnTasterF1

        private ICommand _btnTasterF1;

        public ICommand BtnTasterF1
        {
            get
            {
                if (_btnTasterF1 == null)
                {
                    _btnTasterF1 = new RelayCommand(p => abfuellAnlage.TasterF1(), p => true);
                }
                return _btnTasterF1;
            }
        }

        #endregion BtnTasterF1

        #region BtnTasterS1

        private ICommand _btnTasterS1;

        public ICommand BtnTasterS1
        {
            get
            {
                if (_btnTasterS1 == null)
                {
                    _btnTasterS1 = new RelayCommand(p => ViAnzeige.TasterS1(), p => true);
                }
                return _btnTasterS1;
            }
        }

        #endregion BtnTasterS1

        #region BtnTasterS2

        private ICommand _btnTasterS2;

        public ICommand BtnTasterS2
        {
            get
            {
                if (_btnTasterS2 == null)
                {
                    _btnTasterS2 = new RelayCommand(p => ViAnzeige.TasterS2(), p => true);
                }
                return _btnTasterS2;
            }
        }

        #endregion BtnTasterS2

        #region BtnTasterS3

        private ICommand _btnTasterS3;

        public ICommand BtnTasterS3
        {
            get
            {
                if (_btnTasterS3 == null)
                {
                    _btnTasterS3 = new RelayCommand(p => ViAnzeige.TasterS3(), p => true);
                }
                return _btnTasterS3;
            }
        }

        #endregion BtnTasterS3

        #region BtnTasterS4

        private ICommand _btnTasterS4;

        public ICommand BtnTasterS4
        {
            get
            {
                if (_btnTasterS4 == null)
                {
                    _btnTasterS4 = new RelayCommand(p => ViAnzeige.TasterS4(), p => true);
                }
                return _btnTasterS4;
            }
        }

        #endregion BtnTasterS4

        #region SetManualK1

        private ICommand _setManualK1;

        public ICommand SetManualK1
        {
            get
            {
                if (_setManualK1 == null)
                {
                    _setManualK1 = new RelayCommand(p => ViAnzeige.SetManualK1(), p => true);
                }
                return _setManualK1;
            }
        }

        #endregion SetManualK1

        #region SetManualK2

        private ICommand _setManualK2;

        public ICommand SetManualK2
        {
            get
            {
                if (_setManualK2 == null)
                {
                    _setManualK2 = new RelayCommand(p => ViAnzeige.SetManualK2(), p => true);
                }
                return _setManualK2;
            }
        }

        #endregion SetManualK2

        #region SetManualQ1

        private ICommand _setManualQ1;

        public ICommand SetManualQ1
        {
            get
            {
                if (_setManualQ1 == null)
                {
                    _setManualQ1 = new RelayCommand(p => ViAnzeige.SetManualQ1(), p => true);
                }
                return _setManualQ1;
            }
        }

        #endregion SetManualQ1
    }
}