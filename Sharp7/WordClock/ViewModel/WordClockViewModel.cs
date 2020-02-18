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
            BtnSetCurrentTime = new WordClockSetCurrentTime(this);
        }

        public Model.Zeiten Zeiten { get { return zeiten; } }


        public bool CanSetCurrentTime { get { return true; } }

        public ICommand BtnSetCurrentTime { get; private set; }

        public void SetCurrentTime() { zeiten.SetCurrentTime(); }
    }
}
