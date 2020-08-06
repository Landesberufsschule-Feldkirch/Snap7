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

        // ReSharper disable once UnusedMember.Global
        public Model.StiegenhausBeleuchtung StiegenhausBeleuchtung => stiegenhausBeleuchtung;

        #region BtnStart

        private ICommand _btnStart;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnStart => _btnStart ?? (_btnStart = new RelayCommand(stiegenhausBeleuchtung.BtnStart));

        #endregion BtnStart

        #region BtnBewegungsmelder

        private ICommand _btnBewegungsmelder;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnBewegungsmelder => _btnBewegungsmelder ?? (_btnBewegungsmelder = new RelayCommand(ViAnzeige.BtnBewegungsmelder));

        #endregion BtnBewegungsmelder
    }
}