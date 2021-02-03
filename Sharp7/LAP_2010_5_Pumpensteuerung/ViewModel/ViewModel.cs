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
        public ICommand BtnTasterHand => _btnTasterHand ??= new RelayCommand(_ => Pumpensteuerung.TasterHand(), _ => true);

        private ICommand _btnTasterAus;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterAus => _btnTasterAus ??= new RelayCommand(_ => Pumpensteuerung.TasterAus(), _ => true);

        private ICommand _btnTasterAutomatik;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterAutomatik => _btnTasterAutomatik ??= new RelayCommand(_ => Pumpensteuerung.TasterAutomatik(), _ => true);

        private ICommand _btnS3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS3 => _btnS3 ??= new RelayCommand(_ => ViAnzeige.SetS3(), _ => true);

        private ICommand _btnThermorelaisF1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnThermorelaisF1 => _btnThermorelaisF1 ??= new RelayCommand(_ => Pumpensteuerung.ThermorelaisF1(), _ => true);

        private ICommand _btnVentilY1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnVentilY1 => _btnVentilY1 ??= new RelayCommand(_ => Pumpensteuerung.VentilY1(), _ => true);

        private ICommand _btnSetManualQ1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnSetManualQ1 => _btnSetManualQ1 ??= new RelayCommand(_ => Pumpensteuerung.SetManualQ1(), _ => true);
    }
}