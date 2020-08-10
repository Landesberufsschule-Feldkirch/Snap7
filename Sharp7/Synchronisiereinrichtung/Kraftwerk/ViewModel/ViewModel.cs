namespace Synchronisiereinrichtung.kraftwerk.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        private readonly Model.Kraftwerk _kraftwerk;
        public Model.Kraftwerk Kraftwerk => _kraftwerk;
        public Schreiber Schreiber { get; set; }
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            _kraftwerk = new Model.Kraftwerk();
            Schreiber = new Schreiber(_kraftwerk);
            ViAnzeige = new VisuAnzeigen(mainWindow, _kraftwerk);
        }



        private ICommand _btnReset;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnReset => _btnReset ?? (_btnReset = new RelayCommand(p => _kraftwerk.Reset(), p => true));

        private ICommand _btnSchalterQ1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnSchalterQ1 =>
            _btnSchalterQ1 ??
            (_btnSchalterQ1 = new RelayCommand(p => _kraftwerk.Synchronisieren(), p => true));

        private ICommand _btnSchalterStart;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnSchalterStart =>
            _btnSchalterStart ??
            (_btnSchalterStart = new RelayCommand(p => _kraftwerk.Starten(), p => true));

        private ICommand _btnSchalterStop;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnSchalterStop => _btnSchalterStop ?? (_btnSchalterStop = new RelayCommand(p => _kraftwerk.Stoppen(), p => true));
    }
}