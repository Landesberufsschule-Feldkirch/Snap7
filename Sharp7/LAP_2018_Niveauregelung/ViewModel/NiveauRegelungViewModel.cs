namespace LAP_2018_4_Niveauregelung.ViewModel
{
    using LAP_2018_4_Niveauregelung.Commands;
    using System.Windows.Input;

    public class NiveauRegelungViewModel
    {

        public readonly Model.NiveauRegelung niveauRegelung;
        public VisuAnzeigen ViAnzeige { get; set; }

        public NiveauRegelungViewModel(MainWindow mainWindow)
        {
            niveauRegelung = new Model.NiveauRegelung();
            ViAnzeige = new VisuAnzeigen(mainWindow, niveauRegelung);
        }

        public Model.NiveauRegelung NiveauRegelung { get { return niveauRegelung; } }



        #region BtnTasterS1
        private ICommand _btnTasterS1;
        public ICommand BtnTasterS1
        {
            get
            {
                if (_btnTasterS1 == null)
                {
                    _btnTasterS1 = new RelayCommand(p => ViAnzeige.TasterS1(), p => true);
                }
                return _btnTasterS1;
            }
        }
        #endregion

        #region BtnTasterS2
        private ICommand _btnTasterS2;
        public ICommand BtnTasterS2
        {
            get
            {
                if (_btnTasterS2 == null)
                {
                    _btnTasterS2 = new RelayCommand(p => ViAnzeige.TasterS2(), p => true);
                }
                return _btnTasterS2;
            }
        }
        #endregion

        #region BtnTasterS3
        private ICommand _btnTasterS3;
        public ICommand BtnTasterS3
        {
            get
            {
                if (_btnTasterS3 == null)
                {
                    _btnTasterS3 = new RelayCommand(p => ViAnzeige.TasterS3(), p => true);
                }
                return _btnTasterS3;
            }
        }
        #endregion

        #region BtnThermorelaisF1
        private ICommand _btnThermorelaisF1;
        public ICommand BtnThermorelaisF1
        {
            get
            {
                if (_btnThermorelaisF1 == null)
                {
                    _btnThermorelaisF1 = new RelayCommand(p => niveauRegelung.ThermorelaisF1(), p => true);
                }
                return _btnThermorelaisF1;
            }
        }
        #endregion

        #region BtnThermorelaisF2
        private ICommand _btnThermorelaisF2;
        public ICommand BtnThermorelaisF2
        {
            get
            {
                if (_btnThermorelaisF2 == null)
                {
                    _btnThermorelaisF2 = new RelayCommand(p => niveauRegelung.ThermorelaisF2(), p => true);
                }
                return _btnThermorelaisF2;
            }
        }
        #endregion

        #region BtnVentilY1
        private ICommand _btnVentilY1;
        public ICommand BtnVentilY1
        {
            get
            {
                if (_btnVentilY1 == null)
                {
                    _btnVentilY1 = new RelayCommand(p => niveauRegelung.VentilY1(), p => true);
                }
                return _btnVentilY1;
            }
        }
        #endregion

        #region BtnSetManualM1
        private ICommand _btnSetManualM1;
        public ICommand BtnSetManualM1
        {
            get
            {
                if (_btnSetManualM1 == null)
                {
                    _btnSetManualM1 = new RelayCommand(p => niveauRegelung.SetManualM1(), p => true);
                }
                return _btnSetManualM1;
            }
        }
        #endregion

        #region BtnSetManualM2
        private ICommand _btnSetManualM2;
        public ICommand BtnSetManualM2
        {
            get
            {
                if (_btnSetManualM2 == null)
                {
                    _btnSetManualM2 = new RelayCommand(p => niveauRegelung.SetManualM2(), p => true);
                }
                return _btnSetManualM2;
            }
        }
        #endregion
    }
}
