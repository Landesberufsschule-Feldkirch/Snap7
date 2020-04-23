namespace WordClock.ViewModel
{
    using System.Windows.Input;
    using WordClock.Commands;

    public class ViewModel
    {
        public readonly Model.Zeiten zeiten;
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            zeiten = new Model.Zeiten();
            ViAnzeige = new VisuAnzeigen(mainWindow, zeiten);
        }

        public Model.Zeiten Zeiten { get { return zeiten; } }

        #region BtnSetCurrentTime

        private ICommand _btnSetCurrentTime;

        public ICommand BtnSetCurrentTime
        {
            get
            {
                if (_btnSetCurrentTime == null)
                {
                    _btnSetCurrentTime = new RelayCommand(p => zeiten.SetCurrentTime(), p => true);
                }
                return _btnSetCurrentTime;
            }
        }

        #endregion BtnSetCurrentTime
    }
}