namespace WordClock.ViewModel
{
    using System.Windows.Input;
    using WordClock.Commands;

    public class WordClockViewModel
    {
        public readonly Model.Zeiten zeiten;

        public WordClockViewModel(MainWindow mainWindow)
        {
            zeiten = new Model.Zeiten(mainWindow);
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
        #endregion
    }
}
