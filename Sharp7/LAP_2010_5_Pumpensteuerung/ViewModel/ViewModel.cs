namespace LAP_2010_5_Pumpensteuerung.ViewModel
{
    using LAP_2010_5_Pumpensteuerung.Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public readonly Model.Pumpensteuerung pumpensteuerung;
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            pumpensteuerung = new Model.Pumpensteuerung();
            ViAnzeige = new VisuAnzeigen(mainWindow, pumpensteuerung);
        }

        public Model.Pumpensteuerung Pumpensteuerung => pumpensteuerung;

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

        #endregion BtnTasterHand

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

        #endregion BtnTasterAus

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

        #endregion BtnTasterAutomatik

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

        #endregion BtnS3

        #region BtnThermorelaisF1

        private ICommand _btnThermorelaisF1;

        public ICommand BtnThermorelaisF1
        {
            get
            {
                if (_btnThermorelaisF1 == null)
                {
                    _btnThermorelaisF1 = new RelayCommand(p => pumpensteuerung.ThermorelaisF1(), p => true);
                }
                return _btnThermorelaisF1;
            }
        }

        #endregion BtnThermorelaisF1

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

        #endregion BtnVentilY1

        #region BtnSetManualQ1

        private ICommand _btnSetManualQ1;

        public ICommand BtnSetManualQ1
        {
            get
            {
                if (_btnSetManualQ1 == null)
                {
                    _btnSetManualQ1 = new RelayCommand(p => pumpensteuerung.SetManualQ1(), p => true);
                }
                return _btnSetManualQ1;
            }
        }

        #endregion BtnSetManualQ1
    }
}