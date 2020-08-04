namespace StiegenhausBeleuchtung.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public readonly Model.StiegenhausBeleuchtung stiegenhausBeleuchtung;
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            stiegenhausBeleuchtung = new Model.StiegenhausBeleuchtung();
            ViAnzeige = new VisuAnzeigen(mainWindow, stiegenhausBeleuchtung);
            stiegenhausBeleuchtung.ProblemLoesen(ViAnzeige);
        }

        public Model.StiegenhausBeleuchtung StiegenhausBeleuchtung => stiegenhausBeleuchtung;

        #region BtnStart

        private ICommand _btnStart;

        public ICommand BtnStart => _btnStart ?? (_btnStart = new RelayCommand(stiegenhausBeleuchtung.BtnStart));

        #endregion BtnStart

        #region BtnBewegungsmelder

        private ICommand _btnBewegungsmelder;

        public ICommand BtnBewegungsmelder => _btnBewegungsmelder ?? (_btnBewegungsmelder = new RelayCommand(ViAnzeige.BtnBewegungsmelder));

        #endregion BtnBewegungsmelder
    }
}