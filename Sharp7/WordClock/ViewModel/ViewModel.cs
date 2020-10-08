// ReSharper disable UnusedMember.Global
namespace WordClock.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        private readonly Model.Zeiten _zeiten;
        public Model.Zeiten Zeiten => _zeiten;
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            _zeiten = new Model.Zeiten();
            ViAnzeige = new VisuAnzeigen(mainWindow, _zeiten);
        }


        private ICommand _btnSetCurrentTime;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnSetCurrentTime =>
            _btnSetCurrentTime ??= new RelayCommand(p => _zeiten.SetCurrentTime(), p => true);
    }
}