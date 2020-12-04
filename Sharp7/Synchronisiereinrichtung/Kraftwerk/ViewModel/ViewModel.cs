namespace Synchronisiereinrichtung.Kraftwerk.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public Model.Kraftwerk Kraftwerk { get; }
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            Kraftwerk = new Model.Kraftwerk();
            ViAnzeige = new VisuAnzeigen(mainWindow, Kraftwerk);
        }



        private ICommand _btnReset;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnReset => _btnReset ??= new RelayCommand(p => Kraftwerk.Reset(), p => true);

        private ICommand _btnSchalterQ1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnSchalterQ1 =>
            _btnSchalterQ1 ??= new RelayCommand(p => Kraftwerk.Synchronisieren(), p => true);

        private ICommand _btnSchalterStart;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnSchalterStart =>
            _btnSchalterStart ??= new RelayCommand(p => Kraftwerk.Starten(), p => true);

        private ICommand _btnSchalterStop;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnSchalterStop => _btnSchalterStop ??= new RelayCommand(p => Kraftwerk.Stoppen(), p => true);
    }
}