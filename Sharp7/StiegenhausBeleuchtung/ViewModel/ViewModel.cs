namespace StiegenhausBeleuchtung.ViewModel
{
    using StiegenhausBeleuchtung.Commands;
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

        public ICommand BtnStart
        {
            get
            {
                if (_btnStart == null)
                {
                    _btnStart = new RelayCommand(stiegenhausBeleuchtung.BtnStart);
                }
                return _btnStart;
            }
        }

        #endregion BtnStart

        #region BtnBewegungsmelder

        private ICommand _btnBewegungsmelder;

        public ICommand BtnBewegungsmelder
        {
            get
            {
                if (_btnBewegungsmelder == null)
                {
                    _btnBewegungsmelder = new RelayCommand(ViAnzeige.BtnBewegungsmelder);
                }
                return _btnBewegungsmelder;
            }
        }

        #endregion BtnBewegungsmelder
    }
}