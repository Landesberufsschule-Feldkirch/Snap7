namespace LAP_2018_3_Hydraulikaggregat.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public VisuAnzeigen ViAnzeige { get; set; }
        public readonly Model.Hydraulikaggregat hydraulikaggregat;

        public ViewModel(MainWindow mainWindow)
        {
            hydraulikaggregat = new Model.Hydraulikaggregat();
            ViAnzeige = new VisuAnzeigen(mainWindow, hydraulikaggregat);
        }

        // ReSharper disable once UnusedMember.Global
        public Model.Hydraulikaggregat Hydraulikaggregat => hydraulikaggregat;

        #region BtnNachfuellen

        private ICommand _btnNachfuellen;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnNachfuellen =>
            _btnNachfuellen ??
            (_btnNachfuellen = new RelayCommand(p => hydraulikaggregat.BtnNachfuellen(), p => true));

        #endregion BtnNachfuellen

        #region BtnF1

        private ICommand _btnF1;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnF1 => _btnF1 ?? (_btnF1 = new RelayCommand(p => hydraulikaggregat.BtnF1(), p => true));

        #endregion BtnF1

        #region BtnQ1

        private ICommand _btnQ1;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnQ1 => _btnQ1 ?? (_btnQ1 = new RelayCommand(p => ViAnzeige.BtnQ1(), p => true));

        #endregion BtnQ1

        #region BtnQ2

        private ICommand _btnQ2;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnQ2 => _btnQ2 ?? (_btnQ2 = new RelayCommand(p => ViAnzeige.BtnQ2(), p => true));

        #endregion BtnQ2

        #region BtnQ3

        private ICommand _btnQ3;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnQ3 => _btnQ3 ?? (_btnQ3 = new RelayCommand(p => ViAnzeige.BtnQ3(), p => true));

        #endregion BtnQ3

        #region BtnQ1Q3

        private ICommand _btnQ1Q3;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnQ1Q3 => _btnQ1Q3 ?? (_btnQ1Q3 = new RelayCommand(p => ViAnzeige.BtnQ1Q3(), p => true));

        #endregion BtnQ1Q3

        #region BtnS1

        private ICommand _btnS1;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS1 => _btnS1 ?? (_btnS1 = new RelayCommand(p => ViAnzeige.BtnS1(), p => true));

        #endregion BtnS1

        #region BtnS2

        private ICommand _btnS2;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS2 => _btnS2 ?? (_btnS2 = new RelayCommand(p => ViAnzeige.BtnS2(), p => true));

        #endregion BtnS2

        #region BtnS3

        private ICommand _btnS3;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS3 => _btnS3 ?? (_btnS3 = new RelayCommand(p => ViAnzeige.BtnS3(), p => true));

        #endregion BtnS3
    }
}