namespace Nadeltelegraph.Commands
{
    using Nadeltelegraph.ViewModel;
    using System.Windows.Input;

    class NadeltelegraphBuchstabeT : ICommand
    {
        private readonly NadeltelegraphViewModel viewModel;

        public NadeltelegraphBuchstabeT(NadeltelegraphViewModel vm)
        {
            viewModel = vm;
        }

        #region ICommand Members
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) { return viewModel.CanBuchstabeT; }
        public void Execute(object parameter) { viewModel.BuchstabeT(); }
        #endregion
    }
}