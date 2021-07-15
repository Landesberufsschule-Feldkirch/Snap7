namespace StiegenhausBeleuchtung.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public Model.StiegenhausBeleuchtung StiegenhausBeleuchtung { get; }
        public VisuAnzeigen ViAnz { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            StiegenhausBeleuchtung = new Model.StiegenhausBeleuchtung();
            ViAnz = new VisuAnzeigen(mainWindow, StiegenhausBeleuchtung);
            StiegenhausBeleuchtung.ProblemLoesen(ViAnz);
        }

        private ICommand _btnTaster;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnz.Taster);
    }
}