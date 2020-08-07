namespace LAP_2018_4_Niveauregelung.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public readonly Model.NiveauRegelung niveauRegelung;
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            niveauRegelung = new Model.NiveauRegelung();
            ViAnzeige = new VisuAnzeigen(mainWindow, niveauRegelung);
        }

        // ReSharper disable once UnusedMember.Global
        public Model.NiveauRegelung NiveauRegelung => niveauRegelung;

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

        #region BtnThermorelaisF1

        private ICommand _btnThermorelaisF1;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnThermorelaisF1 =>
            _btnThermorelaisF1 ??
            (_btnThermorelaisF1 = new RelayCommand(p => niveauRegelung.ThermorelaisF1(), p => true));

        #endregion BtnThermorelaisF1

        #region BtnThermorelaisF2

        private ICommand _btnThermorelaisF2;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnThermorelaisF2 =>
            _btnThermorelaisF2 ??
            (_btnThermorelaisF2 = new RelayCommand(p => niveauRegelung.ThermorelaisF2(), p => true));

        #endregion BtnThermorelaisF2

        #region BtnVentilY1

        private ICommand _btnVentilY1;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnVentilY1 => _btnVentilY1 ?? (_btnVentilY1 = new RelayCommand(p => niveauRegelung.VentilY1(), p => true));

        #endregion BtnVentilY1

        #region BtnSetManualQ1

        private ICommand _btnSetManualQ1;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnSetManualQ1 =>
            _btnSetManualQ1 ??
            (_btnSetManualQ1 = new RelayCommand(p => niveauRegelung.SetManualQ1(), p => true));

        #endregion BtnSetManualQ1

        #region BtnSetManualQ2

        private ICommand _btnSetManualQ2;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnSetManualQ2 =>
            _btnSetManualQ2 ??
            (_btnSetManualQ2 = new RelayCommand(p => niveauRegelung.SetManualQ2(), p => true));

        #endregion BtnSetManualQ2
    }
}