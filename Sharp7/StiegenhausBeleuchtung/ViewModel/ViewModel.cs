namespace StiegenhausBeleuchtung.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public Model.StiegenhausBeleuchtung StiegenhausBeleuchtung { get; }
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            StiegenhausBeleuchtung = new Model.StiegenhausBeleuchtung();
            ViAnzeige = new VisuAnzeigen(mainWindow, StiegenhausBeleuchtung);
            StiegenhausBeleuchtung.ProblemLoesen(ViAnzeige);
        }

        private ICommand _btnStart;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnStart => _btnStart ??= new RelayCommand(StiegenhausBeleuchtung.BtnStart);

        private ICommand _btnBewegungsmelder;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnBewegungsmelder => _btnBewegungsmelder ??= new RelayCommand(ViAnzeige.BtnBewegungsmelder);
    }
}