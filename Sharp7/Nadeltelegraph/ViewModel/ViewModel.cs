namespace Nadeltelegraph.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public readonly Model.Nadeltelegraph nadeltelegraph;
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            nadeltelegraph = new Model.Nadeltelegraph();
            ViAnzeige = new VisuAnzeigen(mainWindow, nadeltelegraph);
        }

        // ReSharper disable once UnusedMember.Global
        public Model.Nadeltelegraph Nadeltelegraph => nadeltelegraph;

        #region BtnBuchstabe

        private ICommand _btnBuchstabe;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnBuchstabe => _btnBuchstabe ?? (_btnBuchstabe = new RelayCommand(ViAnzeige.Buchstabe));

        #endregion BtnBuchstabe
    }
}