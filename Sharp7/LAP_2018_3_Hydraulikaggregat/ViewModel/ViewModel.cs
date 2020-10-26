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
        public ICommand BtnNachfuellen =>
            _btnNachfuellen ??= new RelayCommand(p => Hydraulikaggregat.BtnNachfuellen(), p => true);

        private ICommand _btnF1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnF1 => _btnF1 ??= new RelayCommand(p => Hydraulikaggregat.BtnF1(), p => true);

        private ICommand _btnQ1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnQ1 => _btnQ1 ??= new RelayCommand(p => ViAnzeige.BtnQ1(), p => true);

        private ICommand _btnQ2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnQ2 => _btnQ2 ??= new RelayCommand(p => ViAnzeige.BtnQ2(), p => true);

        private ICommand _btnQ3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnQ3 => _btnQ3 ??= new RelayCommand(p => ViAnzeige.BtnQ3(), p => true);

        private ICommand _btnQ1Q3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnQ1Q3 => _btnQ1Q3 ??= new RelayCommand(p => ViAnzeige.BtnQ1Q3(), p => true);

        private ICommand _btnS1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS1 => _btnS1 ??= new RelayCommand(p => ViAnzeige.BtnS1(), p => true);

        private ICommand _btnS2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS2 => _btnS2 ??= new RelayCommand(p => ViAnzeige.BtnS2(), p => true);

        private ICommand _btnS3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS3 => _btnS3 ??= new RelayCommand(p => ViAnzeige.BtnS3(), p => true);
    }
}