namespace LAP_2010_2_Transportwagen.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public VisuAnzeigen ViAnzeige { get; set; }
        public readonly Model.Transportwagen transportwagen;

        public ViewModel(MainWindow mainWindow)
        {
            transportwagen = new Model.Transportwagen();
            ViAnzeige = new VisuAnzeigen(mainWindow, transportwagen);
        }

        // ReSharper disable once UnusedMember.Global
        public Model.Transportwagen Transportwagen => transportwagen;

        #region SetManualQ1

        private ICommand _setManualQ1;

        // ReSharper disable once UnusedMember.Global
        public ICommand SetManualQ1 => _setManualQ1 ?? (_setManualQ1 = new RelayCommand(p => ViAnzeige.SetManualQ1(), p => true));

        #endregion SetManualQ1

        #region SetManualQ2

        private ICommand _setManualQ2;

        // ReSharper disable once UnusedMember.Global
        public ICommand SetManualQ2 => _setManualQ2 ?? (_setManualQ2 = new RelayCommand(p => ViAnzeige.SetManualQ2(), p => true));

        #endregion SetManualQ2

        #region BtnS1

        private ICommand _btnS1;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS1 => _btnS1 ?? (_btnS1 = new RelayCommand(p => ViAnzeige.SetS1(), p => true));

        #endregion BtnS1

        #region BtnS2

        private ICommand _btnS2;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS2 => _btnS2 ?? (_btnS2 = new RelayCommand(p => transportwagen.SetS2(), p => true));

        #endregion BtnS2

        #region BtnS3

        private ICommand _btnS3;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS3 => _btnS3 ?? (_btnS3 = new RelayCommand(p => ViAnzeige.SetS3(), p => true));

        #endregion BtnS3

        #region BtnF1

        private ICommand _btnF1;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnF1 => _btnF1 ?? (_btnF1 = new RelayCommand(p => transportwagen.SetF1(), p => true));

        #endregion BtnF1
    }
}