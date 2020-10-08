namespace StiegenhausBeleuchtung.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        private readonly Model.StiegenhausBeleuchtung _stiegenhausBeleuchtung;
        public Model.StiegenhausBeleuchtung StiegenhausBeleuchtung => _stiegenhausBeleuchtung;
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            _stiegenhausBeleuchtung = new Model.StiegenhausBeleuchtung();
            ViAnzeige = new VisuAnzeigen(mainWindow, _stiegenhausBeleuchtung);
            _stiegenhausBeleuchtung.ProblemLoesen(ViAnzeige);
        }



        private ICommand _btnStart;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnStart => _btnStart ??= new RelayCommand(_stiegenhausBeleuchtung.BtnStart);

        private ICommand _btnBewegungsmelder;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnBewegungsmelder => _btnBewegungsmelder ??= new RelayCommand(ViAnzeige.BtnBewegungsmelder);
    }
}