namespace LAP_2010_5_Pumpensteuerung.ViewModel
{
    using LAP_2010_5_Pumpensteuerung.Commands;
    using System.Windows.Input;

    public class PumpensteuerungViewModel
    {

        public readonly Model.Pumpensteuerung pumpensteuerung;
        public VisuAnzeigen ViAnzeige { get; set; }

        public PumpensteuerungViewModel(MainWindow mainWindow)
        {
            pumpensteuerung = new Model.Pumpensteuerung();
            ViAnzeige = new VisuAnzeigen(mainWindow, pumpensteuerung);
        }

        public Model.Pumpensteuerung Pumpensteuerung{ get { return pumpensteuerung; } }






        #region BtnTasterS1
        private ICommand _btnTasterS1;
        public ICommand BtnTasterS1
        {
            get
            {
                if (_btnTasterS1 == null)
                {
                    _btnTasterS1 = new RelayCommand(p => pumpensteuerung.TasterS1(), p => true);
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
                    _btnTasterS2 = new RelayCommand(p => pumpensteuerung.TasterS2(), p => true);
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
                    _btnTasterS3 = new RelayCommand(p => pumpensteuerung.TasterS3(), p => true);
                }
                return _btnTasterS3;
            }
        }
        #endregion

        #region BtnS3
        private ICommand _btnS3;
        public ICommand BtnS3
        {
            get
            {
                if (_btnS3 == null)
                {
                    _btnS3 = new RelayCommand(p => ViAnzeige.SetS3(), p => true);
                }
                return _btnS3;
            }
        }
        #endregion

        #region BtnThermorelaisF5
        private ICommand _btnThermorelaisF5;
        public ICommand BtnThermorelaisF5
        {
            get
            {
                if (_btnThermorelaisF5 == null)
                {
                    _btnThermorelaisF5 = new RelayCommand(p => pumpensteuerung.ThermorelaisF5(), p => true);
                }
                return _btnThermorelaisF5;
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
                    _btnVentilY1 = new RelayCommand(p => pumpensteuerung.VentilY1(), p => true);
                }
                return _btnVentilY1;
            }
        }
        #endregion

        #region BtnSetManualK1
        private ICommand _btnSetManualK1;
        public ICommand BtnSetManualK1
        {
            get
            {
                if (_btnSetManualK1 == null)
                {
                    _btnSetManualK1 = new RelayCommand(p => pumpensteuerung.SetManualK1(), p => true);
                }
                return _btnSetManualK1;
            }
        }
        #endregion



    }
}
