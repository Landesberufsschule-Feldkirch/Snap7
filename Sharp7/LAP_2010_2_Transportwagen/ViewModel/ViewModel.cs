namespace LAP_2010_2_Transportwagen.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public Model.Transportwagen Transportwagen { get; }

        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            Transportwagen = new Model.Transportwagen();
            ViAnzeige = new VisuAnzeigen(mainWindow, Transportwagen);
        }

        private ICommand _btnTaster;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnzeige.Taster);

        private ICommand _btnSchalter;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnSchalter => _btnSchalter ??= new RelayCommand(ViAnzeige.Schalter);
    }
}