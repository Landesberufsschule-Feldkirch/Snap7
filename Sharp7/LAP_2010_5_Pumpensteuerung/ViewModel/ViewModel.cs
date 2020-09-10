namespace LAP_2010_5_Pumpensteuerung.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        private readonly Model.Pumpensteuerung _pumpensteuerung;
        public Model.Pumpensteuerung Pumpensteuerung => _pumpensteuerung;
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            _pumpensteuerung = new Model.Pumpensteuerung();
            ViAnzeige = new VisuAnzeigen(mainWindow, _pumpensteuerung);
        }


        private ICommand _btnTasterHand;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterHand =>
            _btnTasterHand ??= new RelayCommand(p => _pumpensteuerung.TasterHand(), p => true);

        private ICommand _btnTasterAus;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterAus => _btnTasterAus ??= new RelayCommand(p => _pumpensteuerung.TasterAus(), p => true);

        private ICommand _btnTasterAutomatik;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterAutomatik =>
            _btnTasterAutomatik ??= new RelayCommand(p => _pumpensteuerung.TasterAutomatik(), p => true);

        private ICommand _btnS3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS3 => _btnS3 ??= new RelayCommand(p => ViAnzeige.SetS3(), p => true);

        private ICommand _btnThermorelaisF1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnThermorelaisF1 =>
            _btnThermorelaisF1 ??= new RelayCommand(p => _pumpensteuerung.ThermorelaisF1(), p => true);

        private ICommand _btnVentilY1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnVentilY1 => _btnVentilY1 ??= new RelayCommand(p => _pumpensteuerung.VentilY1(), p => true);

        private ICommand _btnSetManualQ1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnSetManualQ1 =>
            _btnSetManualQ1 ??= new RelayCommand(p => _pumpensteuerung.SetManualQ1(), p => true);
    }
}