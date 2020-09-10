namespace LAP_2018_2_Abfuellanlage.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        private readonly Model.Abfuellanlage _abfuellAnlage;
        public Model.Abfuellanlage Abfuellanlage => _abfuellAnlage;
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            _abfuellAnlage = new Model.Abfuellanlage();
            ViAnzeige = new VisuAnzeigen(mainWindow, _abfuellAnlage);
        }


        private ICommand _btnReset;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnReset => _btnReset ??= new RelayCommand(p => _abfuellAnlage.AllesReset(), p => true);

        private ICommand _btnNachfuellen;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnNachfuellen =>
            _btnNachfuellen ??= new RelayCommand(p => _abfuellAnlage.TasterNachfuellen(), p => true);

        private ICommand _btnTasterF1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterF1 => _btnTasterF1 ??= new RelayCommand(p => _abfuellAnlage.TasterF1(), p => true);

        private ICommand _btnTasterS1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS1 => _btnTasterS1 ??= new RelayCommand(p => ViAnzeige.TasterS1(), p => true);

        private ICommand _btnTasterS2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS2 => _btnTasterS2 ??= new RelayCommand(p => ViAnzeige.TasterS2(), p => true);

        private ICommand _btnTasterS3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS3 => _btnTasterS3 ??= new RelayCommand(p => ViAnzeige.TasterS3(), p => true);

        private ICommand _btnTasterS4;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS4 => _btnTasterS4 ??= new RelayCommand(p => ViAnzeige.TasterS4(), p => true);

        private ICommand _setManualK1;
        // ReSharper disable once UnusedMember.Global
        public ICommand SetManualK1 => _setManualK1 ??= new RelayCommand(p => ViAnzeige.SetManualK1(), p => true);

        private ICommand _setManualK2;
        // ReSharper disable once UnusedMember.Global
        public ICommand SetManualK2 => _setManualK2 ??= new RelayCommand(p => ViAnzeige.SetManualK2(), p => true);

        private ICommand _setManualQ1;
        // ReSharper disable once UnusedMember.Global
        public ICommand SetManualQ1 => _setManualQ1 ??= new RelayCommand(p => ViAnzeige.SetManualQ1(), p => true);
    }
}