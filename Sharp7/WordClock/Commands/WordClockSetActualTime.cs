namespace WordClock.Commands
{
    using System.Windows.Input;
    using WordClock.ViewModel;

    class WordClockSetActualTime : ICommand
    {

        private readonly WordClockViewModel viewModel;

        public WordClockSetActualTime(WordClockViewModel vm)
        {
            viewModel = vm;
        }

        #region ICommand Members
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) { return viewModel.CanSetActualTime; }
        public void Execute(object parameter) { viewModel.SetActualTime(); }
        #endregion

    }
}
