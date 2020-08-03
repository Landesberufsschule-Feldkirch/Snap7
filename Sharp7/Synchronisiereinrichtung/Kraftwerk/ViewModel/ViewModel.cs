namespace Synchronisiereinrichtung.kraftwerk.ViewModel
{
    using Synchronisiereinrichtung.kraftwerk.Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        private readonly Model.Kraftwerk kraftwerk;
        public kraftwerk.ViewModel.Schreiber Schreiber { get; set; }
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            kraftwerk = new Model.Kraftwerk();
            Schreiber = new kraftwerk.ViewModel.Schreiber(kraftwerk);
            ViAnzeige = new VisuAnzeigen(mainWindow, kraftwerk);
        }

        public Model.Kraftwerk Kraftwerk => kraftwerk;

        #region BtnReset

        private ICommand _btnReset;

        public ICommand BtnReset
        {
            get
            {
                if (_btnReset == null)
                {
                    _btnReset = new RelayCommand(p => kraftwerk.Reset(), p => true);
                }
                return _btnReset;
            }
        }

        #endregion BtnReset

        #region BtnSchalterQ1

        private ICommand _btnSchalterQ1;

        public ICommand BtnSchalterQ1
        {
            get
            {
                if (_btnSchalterQ1 == null)
                {
                    _btnSchalterQ1 = new RelayCommand(p => kraftwerk.Synchronisieren(), p => true);
                }
                return _btnSchalterQ1;
            }
        }

        #endregion BtnSchalterQ1

        #region BtnSchalterStart

        private ICommand _btnSchalterStart;

        public ICommand BtnSchalterStart
        {
            get
            {
                if (_btnSchalterStart == null)
                {
                    _btnSchalterStart = new RelayCommand(p => kraftwerk.Starten(), p => true);
                }
                return _btnSchalterStart;
            }
        }

        #endregion BtnSchalterStart

        #region BtnSchalterStop

        private ICommand _btnSchalterStop;

        public ICommand BtnSchalterStop
        {
            get
            {
                if (_btnSchalterStop == null)
                {
                    _btnSchalterStop = new RelayCommand(p => kraftwerk.Stoppen(), p => true);
                }
                return _btnSchalterStop;
            }
        }

        #endregion BtnSchalterStop
    }
}