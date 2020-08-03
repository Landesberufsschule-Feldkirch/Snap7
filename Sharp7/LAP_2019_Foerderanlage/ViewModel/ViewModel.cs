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

        public Model.Foerderanlage Foerderanlage => foerderanlage;

        #region BtnF1

        private ICommand _btnF1;

        public ICommand BtnF1
        {
            get
            {
                if (_btnF1 == null)
                {
                    _btnF1 = new RelayCommand(p => foerderanlage.BtnF1(), p => true);
                }
                return _btnF1;
            }
        }

        #endregion BtnF1

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

        #endregion BtnS0

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

        #endregion BtnS1

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

        #endregion BtnS2

        #region BtnS5

        private ICommand _btnS5;

        public ICommand BtnS5
        {
            get
            {
                if (_btnS5 == null)
                {
                    _btnS5 = new RelayCommand(p => ViAnzeige.SetS5(), p => true);
                }
                return _btnS5;
            }
        }

        #endregion BtnS5

        #region BtnS6

        private ICommand _btnS6;

        public ICommand BtnS6
        {
            get
            {
                if (_btnS6 == null)
                {
                    _btnS6 = new RelayCommand(p => ViAnzeige.SetS6(), p => true);
                }
                return _btnS6;
            }
        }

        #endregion BtnS6

        #region BtnS7

        private ICommand _btnS7;

        public ICommand BtnS7
        {
            get
            {
                if (_btnS7 == null)
                {
                    _btnS7 = new RelayCommand(p => ViAnzeige.SetS7(), p => true);
                }
                return _btnS7;
            }
        }

        #endregion BtnS7

        #region BtnS8

        private ICommand _btnS8;

        public ICommand BtnS8
        {
            get
            {
                if (_btnS8 == null)
                {
                    _btnS8 = new RelayCommand(p => ViAnzeige.SetS8(), p => true);
                }
                return _btnS8;
            }
        }

        #endregion BtnS8

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

        #endregion BtnWagenNachLinks

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

        #endregion BtnWagenNachRechts

        #region BtnNachuellen

        private ICommand _btnNachuellen;

        public ICommand BtnNachuellen
        {
            get
            {
                if (_btnNachuellen == null)
                {
                    _btnNachuellen = new RelayCommand(p => foerderanlage.Nachfuellen(), p => true);
                }
                return _btnNachuellen;
            }
        }

        #endregion BtnNachuellen

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

        #endregion BtnM1_RL

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

        #endregion BtnM1_LL

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

        #endregion BtnM2

        #region BtnK1

        private ICommand _btnK1;

        public ICommand BtnK1
        {
            get
            {
                if (_btnK1 == null)
                {
                    _btnK1 = new RelayCommand(p => ViAnzeige.SetManualK1(), p => true);
                }
                return _btnK1;
            }
        }

        #endregion BtnK1

        #region BtnM1_LL_K1

        private ICommand _btnM1_LL_K1;

        public ICommand BtnM1_LL_K1
        {
            get
            {
                if (_btnM1_LL_K1 == null)
                {
                    _btnM1_LL_K1 = new RelayCommand(p => ViAnzeige.SetManualM1_LL_K1(), p => true);
                }
                return _btnM1_LL_K1;
            }
        }

        #endregion BtnM1_LL_K1
    }
}