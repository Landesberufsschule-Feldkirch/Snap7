namespace Nadeltelegraph.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public Model.Nadeltelegraph Nadeltelegraph { get; }

        public VisuAnzeigen ViAnz { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            Nadeltelegraph = new Model.Nadeltelegraph();
            ViAnz = new VisuAnzeigen(mainWindow, Nadeltelegraph);
        }


        private ICommand _btnBuchstabe;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnBuchstabe => _btnBuchstabe ??= new RelayCommand(ViAnz.Buchstabe);
    }
}