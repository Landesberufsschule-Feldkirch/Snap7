namespace LAP_2018_2_Abfuellanlage.ViewModel
{
    using Commands;
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

        // ReSharper disable once UnusedMember.Global
        public Model.Abfuellanlage Abfuellanlage => abfuellAnlage;

        #region BtnReset

        private ICommand _btnReset;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnReset => _btnReset ?? (_btnReset = new RelayCommand(p => abfuellAnlage.AllesReset(), p => true));

        #endregion BtnReset

        #region BtnNachfuellen

        private ICommand _btnNachfuellen;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnNachfuellen =>
            _btnNachfuellen ??
            (_btnNachfuellen = new RelayCommand(p => abfuellAnlage.TasterNachfuellen(), p => true));

        #endregion BtnNachfuellen

        #region BtnTasterF1

        private ICommand _btnTasterF1;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterF1 => _btnTasterF1 ?? (_btnTasterF1 = new RelayCommand(p => abfuellAnlage.TasterF1(), p => true));

        #endregion BtnTasterF1

        #region BtnTasterS1

        private ICommand _btnTasterS1;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS1 => _btnTasterS1 ?? (_btnTasterS1 = new RelayCommand(p => ViAnzeige.TasterS1(), p => true));

        #endregion BtnTasterS1

        #region BtnTasterS2

        private ICommand _btnTasterS2;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS2 => _btnTasterS2 ?? (_btnTasterS2 = new RelayCommand(p => ViAnzeige.TasterS2(), p => true));

        #endregion BtnTasterS2

        #region BtnTasterS3

        private ICommand _btnTasterS3;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS3 => _btnTasterS3 ?? (_btnTasterS3 = new RelayCommand(p => ViAnzeige.TasterS3(), p => true));

        #endregion BtnTasterS3

        #region BtnTasterS4

        private ICommand _btnTasterS4;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS4 => _btnTasterS4 ?? (_btnTasterS4 = new RelayCommand(p => ViAnzeige.TasterS4(), p => true));

        #endregion BtnTasterS4

        #region SetManualK1

        private ICommand _setManualK1;

        // ReSharper disable once UnusedMember.Global
        public ICommand SetManualK1 => _setManualK1 ?? (_setManualK1 = new RelayCommand(p => ViAnzeige.SetManualK1(), p => true));

        #endregion SetManualK1

        #region SetManualK2

        private ICommand _setManualK2;

        // ReSharper disable once UnusedMember.Global
        public ICommand SetManualK2 => _setManualK2 ?? (_setManualK2 = new RelayCommand(p => ViAnzeige.SetManualK2(), p => true));

        #endregion SetManualK2

        #region SetManualQ1

        private ICommand _setManualQ1;

        // ReSharper disable once UnusedMember.Global
        public ICommand SetManualQ1 => _setManualQ1 ?? (_setManualQ1 = new RelayCommand(p => ViAnzeige.SetManualQ1(), p => true));

        #endregion SetManualQ1
    }
}