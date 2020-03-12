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

        public Model.Pumpensteuerung Pumpensteuerung { get { return pumpensteuerung; } }


        #region BtnTasterHand
        private ICommand _btnTasterHand;
        public ICommand BtnTasterHand
        {
            get
            {
                if (_btnTasterHand == null)
                {
                    _btnTasterHand = new RelayCommand(p => pumpensteuerung.TasterHand(), p => true);
                }
                return _btnTasterHand;
            }
        }
        #endregion

        #region BtnTasterAus
        private ICommand _btnTasterAus;
        public ICommand BtnTasterAus
        {
            get
            {
                if (_btnTasterAus == null)
                {
                    _btnTasterAus = new RelayCommand(p => pumpensteuerung.TasterAus(), p => true);
                }
                return _btnTasterAus;
            }
        }
        #endregion

        #region BtnTasterAutomatik
        private ICommand _btnTasterAutomatik;
        public ICommand BtnTasterAutomatik
        {
            get
            {
                if (_btnTasterAutomatik == null)
                {
                    _btnTasterAutomatik = new RelayCommand(p => pumpensteuerung.TasterAutomatik(), p => true);
                }
                return _btnTasterAutomatik;
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
