namespace LAP_2010_5_Pumpensteuerung.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public Model.Pumpensteuerung Pumpensteuerung { get; }
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            Pumpensteuerung = new Model.Pumpensteuerung();
            ViAnzeige = new VisuAnzeigen(mainWindow, Pumpensteuerung);
        }


        private ICommand _btnTasterHand;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterHand =>
            _btnTasterHand ??= new RelayCommand(p => Pumpensteuerung.TasterHand(), p => true);

        private ICommand _btnTasterAus;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterAus => _btnTasterAus ??= new RelayCommand(p => Pumpensteuerung.TasterAus(), p => true);

        private ICommand _btnTasterAutomatik;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterAutomatik =>
            _btnTasterAutomatik ??= new RelayCommand(p => Pumpensteuerung.TasterAutomatik(), p => true);

        private ICommand _btnS3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS3 => _btnS3 ??= new RelayCommand(p => ViAnzeige.SetS3(), p => true);

        private ICommand _btnThermorelaisF1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnThermorelaisF1 =>
            _btnThermorelaisF1 ??= new RelayCommand(p => Pumpensteuerung.ThermorelaisF1(), p => true);

        private ICommand _btnVentilY1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnVentilY1 => _btnVentilY1 ??= new RelayCommand(p => Pumpensteuerung.VentilY1(), p => true);

        private ICommand _btnSetManualQ1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnSetManualQ1 =>
            _btnSetManualQ1 ??= new RelayCommand(p => Pumpensteuerung.SetManualQ1(), p => true);
    }
}