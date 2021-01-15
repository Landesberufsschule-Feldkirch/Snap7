namespace LAP_2018_2_Abfuellanlage.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public Model.Abfuellanlage Abfuellanlage { get; }
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            Abfuellanlage = new Model.Abfuellanlage();
            ViAnzeige = new VisuAnzeigen(mainWindow, Abfuellanlage);
        }


        private ICommand _btnReset;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnReset => _btnReset ??= new RelayCommand(_ => Abfuellanlage.AllesReset(), _ => true);

        private ICommand _btnNachfuellen;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnNachfuellen =>
            _btnNachfuellen ??= new RelayCommand(_ => Abfuellanlage.TasterNachfuellen(), _ => true);

        private ICommand _btnTasterF1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterF1 => _btnTasterF1 ??= new RelayCommand(_ => Abfuellanlage.TasterF1(), _ => true);

        private ICommand _btnTasterS1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS1 => _btnTasterS1 ??= new RelayCommand(_ => ViAnzeige.TasterS1(), _ => true);

        private ICommand _btnTasterS2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS2 => _btnTasterS2 ??= new RelayCommand(_ => ViAnzeige.TasterS2(), _ => true);

        private ICommand _btnTasterS3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS3 => _btnTasterS3 ??= new RelayCommand(_ => ViAnzeige.TasterS3(), _ => true);

        private ICommand _btnTasterS4;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS4 => _btnTasterS4 ??= new RelayCommand(_ => ViAnzeige.TasterS4(), _ => true);

        private ICommand _setManualK1;
        // ReSharper disable once UnusedMember.Global
        public ICommand SetManualK1 => _setManualK1 ??= new RelayCommand(_ => ViAnzeige.SetManualK1(), _ => true);

        private ICommand _setManualK2;
        // ReSharper disable once UnusedMember.Global
        public ICommand SetManualK2 => _setManualK2 ??= new RelayCommand(_ => ViAnzeige.SetManualK2(), _ => true);

        private ICommand _setManualQ1;
        // ReSharper disable once UnusedMember.Global
        public ICommand SetManualQ1 => _setManualQ1 ??= new RelayCommand(_ => ViAnzeige.SetManualQ1(), _ => true);
    }
}