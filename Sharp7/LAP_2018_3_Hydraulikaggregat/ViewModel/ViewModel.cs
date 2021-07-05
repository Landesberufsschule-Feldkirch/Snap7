namespace LAP_2018_3_Hydraulikaggregat.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public Model.Hydraulikaggregat Hydraulikaggregat { get; }
        public VisuAnzeigen ViAnz { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            Hydraulikaggregat = new Model.Hydraulikaggregat(mainWindow, this);
            ViAnz = new VisuAnzeigen(mainWindow, Hydraulikaggregat);
        }


        private ICommand _btnNachfuellen;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnNachfuellen => _btnNachfuellen ??= new RelayCommand(_ => Hydraulikaggregat.BtnNachfuellen(), _ => true);

        private ICommand _btnUeberdruck;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnUeberdruck => _btnUeberdruck ??= new RelayCommand(_ => Hydraulikaggregat.BtnUeberdruck(), _ => true);

        private ICommand _btnF1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnF1 => _btnF1 ??= new RelayCommand(_ => Hydraulikaggregat.BtnF1(), _ => true);

        private ICommand _btnQ1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnQ1 => _btnQ1 ??= new RelayCommand(_ => ViAnz.BtnQ1(), _ => true);

        private ICommand _btnQ2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnQ2 => _btnQ2 ??= new RelayCommand(_ => ViAnz.BtnQ2(), _ => true);

        private ICommand _btnQ3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnQ3 => _btnQ3 ??= new RelayCommand(_ => ViAnz.BtnQ3(), _ => true);

        private ICommand _btnQ1Q3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnQ1Q3 => _btnQ1Q3 ??= new RelayCommand(_ => ViAnz.BtnQ1Q3(), _ => true);

        private ICommand _btnB4;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnB4 => _btnB4 ??= new RelayCommand(_ => ViAnz.BtnB4(), _ => true);

        private ICommand _btnB5;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnB5 => _btnB5 ??= new RelayCommand(_ => ViAnz.BtnB5(), _ => true);

        private ICommand _btnS1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS1 => _btnS1 ??= new RelayCommand(_ => ViAnz.BtnS1(), _ => true);

        private ICommand _btnS2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS2 => _btnS2 ??= new RelayCommand(_ => ViAnz.BtnS2(), _ => true);

        private ICommand _btnS3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS3 => _btnS3 ??= new RelayCommand(_ => ViAnz.BtnS3(), _ => true);

        private ICommand _btnS4;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS4 => _btnS4 ??= new RelayCommand(_ => ViAnz.BtnS4(), _ => true);


        private ICommand _erweiterungOelkuehler;
        // ReSharper disable once UnusedMember.Global
        public ICommand ErweiterungOelkuehler => _erweiterungOelkuehler ??= new RelayCommand(_ => Hydraulikaggregat.CheckErweiterungOelkuehler(), _ => true);

        private ICommand _erweiterungZylinder;
        // ReSharper disable once UnusedMember.Global
        public ICommand ErweiterungZylinder => _erweiterungZylinder ??= new RelayCommand(_ => Hydraulikaggregat.CheckErweiterungZylinder(), _ => true);

        private ICommand _erweiterungOelfilter;
        // ReSharper disable once UnusedMember.Global
        public ICommand ErweiterungOelfilter => _erweiterungOelfilter ??= new RelayCommand(_ => Hydraulikaggregat.CheckErweiterungOelfilter(), _ => true);
    }
}