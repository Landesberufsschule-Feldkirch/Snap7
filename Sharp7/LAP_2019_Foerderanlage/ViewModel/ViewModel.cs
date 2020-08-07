namespace LAP_2019_Foerderanlage.ViewModel
{
    using Commands;
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

        // ReSharper disable once UnusedMember.Global
        public Model.Foerderanlage Foerderanlage => foerderanlage;

        #region BtnF1

        private ICommand _btnF1;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnF1 => _btnF1 ?? (_btnF1 = new RelayCommand(p => foerderanlage.BtnF1(), p => true));

        #endregion BtnF1

        #region BtnS0

        private ICommand _btnS0;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS0 => _btnS0 ?? (_btnS0 = new RelayCommand(p => ViAnzeige.SetS0(), p => true));

        #endregion BtnS0

        #region BtnS1

        private ICommand _btnS1;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS1 => _btnS1 ?? (_btnS1 = new RelayCommand(p => ViAnzeige.SetS1(), p => true));

        #endregion BtnS1

        #region BtnS2

        private ICommand _btnS2;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS2 => _btnS2 ?? (_btnS2 = new RelayCommand(p => foerderanlage.BtnS2(), p => true));

        #endregion BtnS2

        #region BtnS5

        private ICommand _btnS5;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS5 => _btnS5 ?? (_btnS5 = new RelayCommand(p => ViAnzeige.SetS5(), p => true));

        #endregion BtnS5

        #region BtnS6

        private ICommand _btnS6;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS6 => _btnS6 ?? (_btnS6 = new RelayCommand(p => ViAnzeige.SetS6(), p => true));

        #endregion BtnS6

        #region BtnS7

        private ICommand _btnS7;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS7 => _btnS7 ?? (_btnS7 = new RelayCommand(p => ViAnzeige.SetS7(), p => true));

        #endregion BtnS7

        #region BtnS8

        private ICommand _btnS8;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS8 => _btnS8 ?? (_btnS8 = new RelayCommand(p => ViAnzeige.SetS8(), p => true));

        #endregion BtnS8

        #region BtnWagenNachLinks

        private ICommand _btnWagenNachLinks;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnWagenNachLinks =>
            _btnWagenNachLinks ??
            (_btnWagenNachLinks = new RelayCommand(p => foerderanlage.WagenNachLinks(), p => true));

        #endregion BtnWagenNachLinks

        #region BtnWagenNachRechts

        private ICommand _btnWagenNachRechts;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnWagenNachRechts =>
            _btnWagenNachRechts ?? (_btnWagenNachRechts =
                new RelayCommand(p => foerderanlage.WagenNachRechts(), p => true));

        #endregion BtnWagenNachRechts

        #region BtnNachuellen

        private ICommand _btnNachuellen;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnNachuellen =>
            _btnNachuellen ??
            (_btnNachuellen = new RelayCommand(p => foerderanlage.Nachfuellen(), p => true));

        #endregion BtnNachuellen

        #region BtnM1_RL

        private ICommand _btnM1Rl;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnM1Rl => _btnM1Rl ?? (_btnM1Rl = new RelayCommand(p => ViAnzeige.SetManualM1_RL(), p => true));

        #endregion BtnM1_RL

        #region BtnM1_LL

        private ICommand _btnM1Ll;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnM1Ll => _btnM1Ll ?? (_btnM1Ll = new RelayCommand(p => ViAnzeige.SetManualM1_LL(), p => true));

        #endregion BtnM1_LL

        #region BtnM2

        private ICommand _btnM2;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnM2 => _btnM2 ?? (_btnM2 = new RelayCommand(p => ViAnzeige.SetManualM2(), p => true));

        #endregion BtnM2

        #region BtnK1

        private ICommand _btnK1;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnK1 => _btnK1 ?? (_btnK1 = new RelayCommand(p => ViAnzeige.SetManualK1(), p => true));

        #endregion BtnK1

        #region BtnM1_LL_K1

        private ICommand _btnM1LlK1;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnM1LlK1 => _btnM1LlK1 ?? (_btnM1LlK1 = new RelayCommand(p => ViAnzeige.SetManualM1_LL_K1(), p => true));

        #endregion BtnM1_LL_K1
    }
}