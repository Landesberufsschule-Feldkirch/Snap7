namespace LAP_2019_Foerderanlage.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public Model.Foerderanlage Foerderanlage { get; }

        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            Foerderanlage = new Model.Foerderanlage(mainWindow);
            ViAnzeige = new VisuAnzeigen(mainWindow, Foerderanlage);
        }



        private ICommand _btnF1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnF1 => _btnF1 ??= new RelayCommand(_ => Foerderanlage.BtnF1(), _ => true);

        private ICommand _btnS0;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS0 => _btnS0 ??= new RelayCommand(_ => ViAnzeige.SetS0(), _ => true);

        private ICommand _btnS1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS1 => _btnS1 ??= new RelayCommand(_ => ViAnzeige.SetS1(), _ => true);

        private ICommand _btnS2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS2 => _btnS2 ??= new RelayCommand(_ => Foerderanlage.BtnS2(), _ => true);

        private ICommand _btnS5;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS5 => _btnS5 ??= new RelayCommand(_ => ViAnzeige.SetS5(), _ => true);

        private ICommand _btnS6;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS6 => _btnS6 ??= new RelayCommand(_ => ViAnzeige.SetS6(), _ => true);

        private ICommand _btnS7;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS7 => _btnS7 ??= new RelayCommand(_ => ViAnzeige.SetS7(), _ => true);

        private ICommand _btnS8;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS8 => _btnS8 ??= new RelayCommand(_ => ViAnzeige.SetS8(), _ => true);

        private ICommand _btnWagenNachLinks;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnWagenNachLinks => _btnWagenNachLinks ??= new RelayCommand(_ => Foerderanlage.WagenNachLinks(), _ => true);

        private ICommand _btnWagenNachRechts;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnWagenNachRechts => _btnWagenNachRechts ??= new RelayCommand(_ => Foerderanlage.WagenNachRechts(), _ => true);

        private ICommand _btnNachuellen;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnNachuellen => _btnNachuellen ??= new RelayCommand(_ => Foerderanlage.Nachfuellen(), _ => true);

        private ICommand _btnM1Rl;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnM1Rl => _btnM1Rl ??= new RelayCommand(_ => ViAnzeige.SetManualM1_RL(), _ => true);

        private ICommand _btnM1Ll;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnM1Ll => _btnM1Ll ??= new RelayCommand(_ => ViAnzeige.SetManualM1_LL(), _ => true);

        private ICommand _btnM2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnM2 => _btnM2 ??= new RelayCommand(_ => ViAnzeige.SetManualM2(), _ => true);

        private ICommand _btnK1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnK1 => _btnK1 ??= new RelayCommand(_ => ViAnzeige.SetManualK1(), _ => true);

        private ICommand _btnM1LlK1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnM1LlK1 => _btnM1LlK1 ??= new RelayCommand(_ => ViAnzeige.SetManualM1_LL_K1(), _ => true);
    }
}