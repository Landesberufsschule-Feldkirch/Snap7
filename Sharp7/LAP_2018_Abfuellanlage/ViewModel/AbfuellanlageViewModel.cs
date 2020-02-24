namespace LAP_2018_Abfuellanlage.ViewModel
{
    using LAP_2018_Abfuellanlage.Commands;
    using System.Windows.Input;

    public class AbfuellanlageViewModel
    {

        public readonly Model.AlleFlaschen alleFlaschen;
        public VisuAnzeigen ViAnzeige { get; set; }

        public AbfuellanlageViewModel(MainWindow mainWindow)
        {
            alleFlaschen = new Model.AlleFlaschen(mainWindow);
            ViAnzeige = new VisuAnzeigen(mainWindow, alleFlaschen);
        }

        public Model.AlleFlaschen AlleFlaschen { get { return alleFlaschen; } }


        #region BtnNachfuellen
        private ICommand _btnNachfuellen;
        public ICommand BtnNachfuellen
        {
            get
            {
                if (_btnNachfuellen == null)
                {
                    _btnNachfuellen = new RelayCommand(p => alleFlaschen.TasterNachfuellen(), p => true);
                }
                return _btnNachfuellen;
            }
        }
        #endregion

        #region BtnTasterF5
        private ICommand _btnTasterF5;
        public ICommand BtnTasterF5
        {
            get
            {
                if (_btnTasterF5 == null)
                {
                    _btnTasterF5 = new RelayCommand(p => alleFlaschen.TasterF5(), p => true);
                }
                return _btnTasterF5;
            }
        }
        #endregion

        #region BtnTasterS1
        private ICommand _btnTasterS1;
        public ICommand BtnTasterS1
        {
            get
            {
                if (_btnTasterS1 == null)
                {
                    _btnTasterS1 = new RelayCommand(p => alleFlaschen.TasterS1(), p => true);
                }
                return _btnTasterS1;
            }
        }
        #endregion

        #region BtnTasterS2
        private ICommand _btnTasterS2;
        public ICommand BtnTasterS2
        {
            get
            {
                if (_btnTasterS2 == null)
                {
                    _btnTasterS2 = new RelayCommand(p => alleFlaschen.TasterS2(), p => true);
                }
                return _btnTasterS2;
            }
        }
        #endregion

        #region BtnTasterS3
        private ICommand _btnTasterS3;
        public ICommand BtnTasterS3
        {
            get
            {
                if (_btnTasterS3 == null)
                {
                    _btnTasterS3 = new RelayCommand(p => alleFlaschen.TasterS3(), p => true);
                }
                return _btnTasterS3;
            }
        }
        #endregion

        #region BtnTasterS4
        private ICommand _btnTasterS4;
        public ICommand BtnTasterS4
        {
            get
            {
                if (_btnTasterS4 == null)
                {
                    _btnTasterS4 = new RelayCommand(p => alleFlaschen.TasterS4(), p => true);
                }
                return _btnTasterS4;
            }
        }
        #endregion

        #region SetManualK1
        private ICommand _setManualK1;
        public ICommand SetManualK1
        {
            get
            {
                if (_setManualK1 == null)
                {
                    _setManualK1 = new RelayCommand(p => alleFlaschen.SetManualK1(), p => true);
                }
                return _setManualK1;
            }
        }
        #endregion
        
        #region SetManualK2
        private ICommand _setManualK2;
        public ICommand SetManualK2
        {
            get
            {
                if (_setManualK2 == null)
                {
                    _setManualK2 = new RelayCommand(p => alleFlaschen.SetManualK2(), p => true);
                }
                return _setManualK2;
            }
        }
        #endregion

        #region SetManualM1
        private ICommand _setManualM1;
        public ICommand SetManualM1
        {
            get
            {
                if (_setManualM1 == null)
                {
                    _setManualM1 = new RelayCommand(p => alleFlaschen.SetManualM1(), p => true);
                }
                return _setManualM1;
            }
        }
        #endregion

    }
}
