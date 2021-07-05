namespace LAP_2018_4_Niveauregelung.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public Model.NiveauRegelung NiveauRegelung { get; }
        public VisuAnzeigen ViAnz { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            NiveauRegelung = new Model.NiveauRegelung();
            ViAnz = new VisuAnzeigen(mainWindow, NiveauRegelung);
        }



        private ICommand _btnTasterS1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS1 => _btnTasterS1 ??= new RelayCommand(_ => ViAnz.TasterS1(), _ => true);

        private ICommand _btnTasterS2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS2 => _btnTasterS2 ??= new RelayCommand(_ => ViAnz.TasterS2(), _ => true);

        private ICommand _btnTasterS3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS3 => _btnTasterS3 ??= new RelayCommand(_ => ViAnz.TasterS3(), _ => true);

        private ICommand _btnThermorelaisF1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnThermorelaisF1 => _btnThermorelaisF1 ??= new RelayCommand(_ => NiveauRegelung.ThermorelaisF1(), _ => true);

        private ICommand _btnThermorelaisF2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnThermorelaisF2 => _btnThermorelaisF2 ??= new RelayCommand(_ => NiveauRegelung.ThermorelaisF2(), _ => true);

        private ICommand _btnVentilY1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnVentilY1 => _btnVentilY1 ??= new RelayCommand(_ => NiveauRegelung.VentilY1(), _ => true);

        private ICommand _btnSetManualQ1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnSetManualQ1 => _btnSetManualQ1 ??= new RelayCommand(_ => NiveauRegelung.SetManualQ1(), _ => true);

        private ICommand _btnSetManualQ2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnSetManualQ2 => _btnSetManualQ2 ??= new RelayCommand(_ => NiveauRegelung.SetManualQ2(), _ => true);
    }
}