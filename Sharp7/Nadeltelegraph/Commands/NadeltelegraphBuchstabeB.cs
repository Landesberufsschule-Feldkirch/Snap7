namespace Nadeltelegraph.Commands
{
    using Nadeltelegraph.ViewModel;
    using System.Windows.Input;

    class NadeltelegraphBuchstabeB : ICommand
    {
        private readonly NadeltelegraphViewModel viewModel;

        public NadeltelegraphBuchstabeB(NadeltelegraphViewModel vm)
        {
            viewModel = vm;
        }

        #region ICommand Members
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) { return viewModel.CanBuchstabeB; }
        public void Execute(object parameter) { viewModel.BuchstabeB(); }
        #endregion
    }
}