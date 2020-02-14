namespace WordClock.Commands
{
    using System.Windows.Input;
    using WordClock.ViewModel;

    class WordClockSetSpeed : ICommand
    {

        private readonly WordClockViewModel viewModel;

        public WordClockSetSpeed(WordClockViewModel vm)
        {
            viewModel = vm;
        }

        #region ICommand Members
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) { return viewModel.CanSetSpeed; }
        public void Execute(object parameter) { viewModel.SetSpeed(); }
        #endregion

    }
}
