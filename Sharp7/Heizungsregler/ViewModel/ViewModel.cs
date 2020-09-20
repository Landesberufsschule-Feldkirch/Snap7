namespace Heizungsregler.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public Schreiber Schreiber { get; set; }
        public VisuAnzeigen ViAnzeige { get; set; }
        private MainWindow _mainWindow;
        public ViewModel(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;

               Schreiber = new Schreiber(_mainWindow);
            ViAnzeige = new VisuAnzeigen(_mainWindow);
        }


        private ICommand _btnReset;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnReset => _btnReset ??= new RelayCommand(p => _mainWindow.Heizungsregler.Reset(), p => true);
    }
}