namespace Nadeltelegraph.Commands
{
    using Nadeltelegraph.ViewModel;
    using System.Windows.Input;

    class NadeltelegraphBuchstabeF : ICommand
    {
        private readonly NadeltelegraphViewModel viewModel;

        public NadeltelegraphBuchstabeF(NadeltelegraphViewModel vm)
        {
            viewModel = vm;
        }

        #region ICommand Members
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) { return viewModel.CanBuchstabeF; }
        public void Execute(object parameter) { viewModel.BuchstabeF(); }
        #endregion
    }
}