namespace LAP_2018_4_Niveauregelung.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        private readonly Model.NiveauRegelung _niveauRegelung;
        public Model.NiveauRegelung NiveauRegelung => _niveauRegelung;
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            _niveauRegelung = new Model.NiveauRegelung();
            ViAnzeige = new VisuAnzeigen(mainWindow, _niveauRegelung);
        }



        private ICommand _btnTasterS1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS1 => _btnTasterS1 ??= new RelayCommand(p => ViAnzeige.TasterS1(), p => true);

        private ICommand _btnTasterS2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS2 => _btnTasterS2 ??= new RelayCommand(p => ViAnzeige.TasterS2(), p => true);

        private ICommand _btnTasterS3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS3 => _btnTasterS3 ??= new RelayCommand(p => ViAnzeige.TasterS3(), p => true);

        private ICommand _btnThermorelaisF1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnThermorelaisF1 =>
            _btnThermorelaisF1 ??= new RelayCommand(p => _niveauRegelung.ThermorelaisF1(), p => true);

        private ICommand _btnThermorelaisF2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnThermorelaisF2 =>
            _btnThermorelaisF2 ??= new RelayCommand(p => _niveauRegelung.ThermorelaisF2(), p => true);

        private ICommand _btnVentilY1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnVentilY1 => _btnVentilY1 ??= new RelayCommand(p => _niveauRegelung.VentilY1(), p => true);

        private ICommand _btnSetManualQ1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnSetManualQ1 =>
            _btnSetManualQ1 ??= new RelayCommand(p => _niveauRegelung.SetManualQ1(), p => true);

        private ICommand _btnSetManualQ2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnSetManualQ2 =>
            _btnSetManualQ2 ??= new RelayCommand(p => _niveauRegelung.SetManualQ2(), p => true);
    }
}