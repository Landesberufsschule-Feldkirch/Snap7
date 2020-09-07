namespace Parkhaus.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        private readonly Model.Parkhaus _parkhaus;
        public Model.Parkhaus Parkhaus => _parkhaus;
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            _parkhaus = new Model.Parkhaus();
            ViAnzeige = new VisuAnzeigen(mainWindow, _parkhaus);
        }


        private ICommand _btnAuto;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnBuchstabe => _btnAuto ?? (_btnAuto = new RelayCommand(ViAnzeige.Auto));
    }
}