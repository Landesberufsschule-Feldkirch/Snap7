namespace LAP_2018_2_Abfuellanlage.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public Model.Abfuellanlage Abfuellanlage { get; }
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            Abfuellanlage = new Model.Abfuellanlage();
            ViAnzeige = new VisuAnzeigen(mainWindow, Abfuellanlage);
        }

        private ICommand _btnTaster;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnzeige.Taster);

        private ICommand _btnSchalter;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnSchalter => _btnSchalter ??= new RelayCommand(ViAnzeige.Schalter);
    }
}