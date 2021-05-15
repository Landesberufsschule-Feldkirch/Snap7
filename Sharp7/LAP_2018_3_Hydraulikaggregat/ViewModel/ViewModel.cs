namespace LAP_2018_3_Hydraulikaggregat.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public Model.Hydraulikaggregat Hydraulikaggregat { get; }
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            Hydraulikaggregat = new Model.Hydraulikaggregat();
            ViAnzeige = new VisuAnzeigen(mainWindow, Hydraulikaggregat);
        }


        private ICommand _btnNachfuellen;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnNachfuellen => _btnNachfuellen ??= new RelayCommand(_ => Hydraulikaggregat.BtnNachfuellen(), _ => true);

        private ICommand _btnF1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnF1 => _btnF1 ??= new RelayCommand(_ => Hydraulikaggregat.BtnF1(), _ => true);

        private ICommand _btnQ1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnQ1 => _btnQ1 ??= new RelayCommand(_ => ViAnzeige.BtnQ1(), _ => true);

        private ICommand _btnQ2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnQ2 => _btnQ2 ??= new RelayCommand(_ => ViAnzeige.BtnQ2(), _ => true);

        private ICommand _btnQ3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnQ3 => _btnQ3 ??= new RelayCommand(_ => ViAnzeige.BtnQ3(), _ => true);

        private ICommand _btnQ1Q3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnQ1Q3 => _btnQ1Q3 ??= new RelayCommand(_ => ViAnzeige.BtnQ1Q3(), _ => true);

        private ICommand _btnS1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS1 => _btnS1 ??= new RelayCommand(_ => ViAnzeige.BtnS1(), _ => true);

        private ICommand _btnS2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS2 => _btnS2 ??= new RelayCommand(_ => ViAnzeige.BtnS2(), _ => true);

        private ICommand _btnS3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS3 => _btnS3 ??= new RelayCommand(_ => ViAnzeige.BtnS3(), _ => true);



        private ICommand _erweiterungOelkuehler;
        public ICommand ErweiterungOelkuehler => _erweiterungOelkuehler ??= new RelayCommand(_ => ViAnzeige.ErweiterungOelkuehler(), _ => true);
        
        private ICommand _erweiterungZylinder;
        public ICommand ErweiterungZylinder => _erweiterungZylinder ??= new RelayCommand(_ => ViAnzeige.ErweiterungZylinder(), _ => true);

        private ICommand _erweiterungOelfilter;
        public ICommand ErweiterungOelfilter => _erweiterungOelfilter ??= new RelayCommand(_ => ViAnzeige.ErweiterungOelfilter(), _ => true);
    }
}