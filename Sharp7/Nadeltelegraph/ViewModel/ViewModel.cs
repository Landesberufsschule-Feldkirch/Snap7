namespace Nadeltelegraph.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        private readonly Model.Nadeltelegraph _nadeltelegraph;
        public Model.Nadeltelegraph Nadeltelegraph => _nadeltelegraph;
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            _nadeltelegraph = new Model.Nadeltelegraph();
            ViAnzeige = new VisuAnzeigen(mainWindow, _nadeltelegraph);
        }


        private ICommand _btnBuchstabe;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnBuchstabe => _btnBuchstabe ??= new RelayCommand(ViAnzeige.Buchstabe);
    }
}