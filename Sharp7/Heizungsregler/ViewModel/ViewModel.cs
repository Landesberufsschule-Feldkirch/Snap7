namespace Heizungsregler.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public VisuAnzeigen ViAnzeige { get; set; }

        private readonly MainWindow _mainWindow;

        public ViewModel(MainWindow mw)
        {
            _mainWindow = mw;

            ViAnzeige = new VisuAnzeigen(_mainWindow);
        }


        private ICommand _btnReset;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnReset => _btnReset ??= new RelayCommand(p => _mainWindow.WohnHaus.Reset(), p => true);
    }
}