namespace Synchronisiereinrichtung.kraftwerk.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        private readonly Model.Kraftwerk _kraftwerk;
        public Schreiber Schreiber { get; set; }
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            _kraftwerk = new Model.Kraftwerk();
            Schreiber = new Schreiber(_kraftwerk);
            ViAnzeige = new VisuAnzeigen(mainWindow, _kraftwerk);
        }

        public Model.Kraftwerk Kraftwerk => _kraftwerk;

        #region BtnReset

        private ICommand _btnReset;

        public ICommand BtnReset => _btnReset ?? (_btnReset = new RelayCommand(p => _kraftwerk.Reset(), p => true));

        #endregion BtnReset

        #region BtnSchalterQ1

        private ICommand _btnSchalterQ1;

        public ICommand BtnSchalterQ1 =>
            _btnSchalterQ1 ??
            (_btnSchalterQ1 = new RelayCommand(p => _kraftwerk.Synchronisieren(), p => true));

        #endregion BtnSchalterQ1

        #region BtnSchalterStart

        private ICommand _btnSchalterStart;

        public ICommand BtnSchalterStart =>
            _btnSchalterStart ??
            (_btnSchalterStart = new RelayCommand(p => _kraftwerk.Starten(), p => true));

        #endregion BtnSchalterStart

        #region BtnSchalterStop

        private ICommand _btnSchalterStop;

        public ICommand BtnSchalterStop => _btnSchalterStop ?? (_btnSchalterStop = new RelayCommand(p => _kraftwerk.Stoppen(), p => true));

        #endregion BtnSchalterStop
    }
}