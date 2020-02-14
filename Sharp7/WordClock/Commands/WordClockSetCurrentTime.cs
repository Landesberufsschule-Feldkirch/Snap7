namespace WordClock.Commands
{
    using System.Windows.Input;
    using WordClock.ViewModel;

    class WordClockSetCurrentTime : ICommand
    {

        private readonly WordClockViewModel viewModel;

        public WordClockSetCurrentTime(WordClockViewModel vm)
        {
            viewModel = vm;
        }

        #region ICommand Members
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) { return viewModel.CanSetCurrentTime; }
        public void Execute(object parameter) { viewModel.SetCurrentTime(); }
        #endregion

    }
}
