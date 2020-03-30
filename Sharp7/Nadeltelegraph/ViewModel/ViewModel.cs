namespace Nadeltelegraph.ViewModel
{
    using Nadeltelegraph.Commands;
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

        public Model.Nadeltelegraph Nadeltelegraph { get { return nadeltelegraph; } }


        #region BtnBuchstabe
        private ICommand _btnBuchstabe;
        public ICommand BtnBuchstabe
        {
            get
            {
                if (_btnBuchstabe == null)
                {
                    _btnBuchstabe = new RelayCommand(ViAnzeige.Buchstabe);
                }
                return _btnBuchstabe;
            }
        }
        #endregion
    }
}