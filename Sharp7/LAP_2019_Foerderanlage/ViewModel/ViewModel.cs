namespace LAP_2019_Foerderanlage.ViewModel
{
    using LAP_2019_Foerderanlage.Commands;
    using System.Windows.Input;

    public class ViewModel
    {

        public VisuAnzeigen ViAnzeige { get; set; }
        public readonly Model.Foerderanlage foerderanlage;

        public ViewModel(MainWindow mainWindow)
        {
            foerderanlage = new Model.Foerderanlage(mainWindow);
            ViAnzeige = new VisuAnzeigen(mainWindow, foerderanlage);
        }

        public Model.Foerderanlage Foerderanlage { get { return foerderanlage; } }


        #region BtnF4
        private ICommand _btnF4;
        public ICommand BtnF4
        {
            get
            {
                if (_btnF4 == null)
                {
                    _btnF4 = new RelayCommand(p => foerderanlage.BtnF4(), p => true);
                }
                return _btnF4;
            }
        }
        #endregion

        #region BtnS0
        private ICommand _btnS0;
        public ICommand BtnS0
        {
            get
            {
                if (_btnS0 == null)
                {
                    _btnS0 = new RelayCommand(p => ViAnzeige.SetS0(), p => true);
                }
                return _btnS0;
            }
        }
        #endregion

        #region BtnS1
        private ICommand _btnS1;
        public ICommand BtnS1
        {
            get
            {
                if (_btnS1 == null)
                {
                    _btnS1 = new RelayCommand(p => ViAnzeige.SetS1(), p => true);
                }
                return _btnS1;
            }
        }
        #endregion

        #region BtnS2
        private ICommand _btnS2;
        public ICommand BtnS2
        {
            get
            {
                if (_btnS2 == null)
                {
                    _btnS2 = new RelayCommand(p => foerderanlage.BtnS2(), p => true);
                }
                return _btnS2;
            }
        }
        #endregion

        #region BtnS9
        private ICommand _btnS9;
        public ICommand BtnS9
        {
            get
            {
                if (_btnS9 == null)
                {
                    _btnS9 = new RelayCommand(p => ViAnzeige.SetS9(), p => true);
                }
                return _btnS9;
            }
        }
        #endregion

        #region BtnS10
        private ICommand _btnS10;
        public ICommand BtnS10
        {
            get
            {
                if (_btnS10 == null)
                {
                    _btnS10 = new RelayCommand(p => ViAnzeige.SetS10(), p => true);
                }
                return _btnS10;
            }
        }
        #endregion

        #region BtnS11
        private ICommand _btnS11;
        public ICommand BtnS11
        {
            get
            {
                if (_btnS11 == null)
                {
                    _btnS11 = new RelayCommand(p => ViAnzeige.SetS11(), p => true);
                }
                return _btnS11;
            }
        }
        #endregion

        #region BtnS12
        private ICommand _btnS12;
        public ICommand BtnS12
        {
            get
            {
                if (_btnS12 == null)
                {
                    _btnS12 = new RelayCommand(p => ViAnzeige.SetS12(), p => true);
                }
                return _btnS12;
            }
        }
        #endregion

        #region BtnWagenNachLinks
        private ICommand _btnWagenNachLinks;
        public ICommand BtnWagenNachLinks
        {
            get
            {
                if (_btnWagenNachLinks == null)
                {
                    _btnWagenNachLinks = new RelayCommand(p => foerderanlage.WagenNachLinks(), p => true);
                }
                return _btnWagenNachLinks;
            }
        }
        #endregion

        #region BtnWagenNachRechts
        private ICommand _btnWagenNachRechts;
        public ICommand BtnWagenNachRechts
        {
            get
            {
                if (_btnWagenNachRechts == null)
                {
                    _btnWagenNachRechts = new RelayCommand(p => foerderanlage.WagenNachRechts(), p => true);
                }
                return _btnWagenNachRechts;
            }
        }
        #endregion

        #region BtnM1_RL
        private ICommand _btnM1_RL;
        public ICommand BtnM1_RL
        {
            get
            {
                if (_btnM1_RL == null)
                {
                    _btnM1_RL = new RelayCommand(p => ViAnzeige.SetManualM1_RL(), p => true);
                }
                return _btnM1_RL;
            }
        }
        #endregion

        #region BtnM1_LL
        private ICommand _btnM1_LL;
        public ICommand BtnM1_LL
        {
            get
            {
                if (_btnM1_LL == null)
                {
                    _btnM1_LL = new RelayCommand(p => ViAnzeige.SetManualM1_LL(), p => true);
                }
                return _btnM1_LL;
            }
        }
        #endregion

        #region BtnM2
        private ICommand _btnM2;
        public ICommand BtnM2
        {
            get
            {
                if (_btnM2 == null)
                {
                    _btnM2 = new RelayCommand(p => ViAnzeige.SetManualM2(), p => true);
                }
                return _btnM2;
            }
        }
        #endregion

        #region BtnY1
        private ICommand _btnY1;
        public ICommand BtnY1
        {
            get
            {
                if (_btnY1 == null)
                {
                    _btnY1 = new RelayCommand(p => ViAnzeige.SetManualY1(), p => true);
                }
                return _btnY1;
            }
        }
        #endregion

        #region BtnM1_LL_Y1
        private ICommand _btnM1_LL_Y1;
        public ICommand BtnM1_LL_Y1
        {
            get
            {
                if (_btnM1_LL_Y1 == null)
                {
                    _btnM1_LL_Y1 = new RelayCommand(p => ViAnzeige.SetManualM1_LL_Y1(), p => true);
                }
                return _btnM1_LL_Y1;
            }
        }
        #endregion

    }
}
