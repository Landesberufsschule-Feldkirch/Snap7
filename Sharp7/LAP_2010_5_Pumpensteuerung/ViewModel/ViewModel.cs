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

        public ICommand BtnTasterHand =>
            _btnTasterHand ??
            (_btnTasterHand = new RelayCommand(p => pumpensteuerung.TasterHand(), p => true));

        #endregion BtnTasterHand

        #region BtnTasterAus

        private ICommand _btnTasterAus;

        public ICommand BtnTasterAus => _btnTasterAus ?? (_btnTasterAus = new RelayCommand(p => pumpensteuerung.TasterAus(), p => true));

        #endregion BtnTasterAus

        #region BtnTasterAutomatik

        private ICommand _btnTasterAutomatik;

        public ICommand BtnTasterAutomatik =>
            _btnTasterAutomatik ?? (_btnTasterAutomatik =
                new RelayCommand(p => pumpensteuerung.TasterAutomatik(), p => true));

        #endregion BtnTasterAutomatik

        #region BtnS3

        private ICommand _btnS3;

        public ICommand BtnS3 => _btnS3 ?? (_btnS3 = new RelayCommand(p => ViAnzeige.SetS3(), p => true));

        #endregion BtnS3

        #region BtnThermorelaisF1

        private ICommand _btnThermorelaisF1;

        public ICommand BtnThermorelaisF1 =>
            _btnThermorelaisF1 ??
            (_btnThermorelaisF1 = new RelayCommand(p => pumpensteuerung.ThermorelaisF1(), p => true));

        #endregion BtnThermorelaisF1

        #region BtnVentilY1

        private ICommand _btnVentilY1;

        public ICommand BtnVentilY1 => _btnVentilY1 ?? (_btnVentilY1 = new RelayCommand(p => pumpensteuerung.VentilY1(), p => true));

        #endregion BtnVentilY1

        #region BtnSetManualQ1

        private ICommand _btnSetManualQ1;

        public ICommand BtnSetManualQ1 =>
            _btnSetManualQ1 ??
            (_btnSetManualQ1 = new RelayCommand(p => pumpensteuerung.SetManualQ1(), p => true));

        #endregion BtnSetManualQ1
    }
}