namespace Heizungsregler.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public VisuAnzeigen ViAnz { get; set; }

        private readonly MainWindow _mainWindow;

        public ViewModel(MainWindow mw)
        {
            _mainWindow = mw;

            ViAnz = new VisuAnzeigen(_mainWindow);
        }


        private ICommand _btnReset;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnReset => _btnReset ??= new RelayCommand(_ => _mainWindow.WohnHaus.Reset(), _ => true);
    }
}