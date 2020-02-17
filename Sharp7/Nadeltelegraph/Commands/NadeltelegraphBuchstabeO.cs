namespace Nadeltelegraph.Commands
{
    using Nadeltelegraph.ViewModel;
    using System.Windows.Input;

    class NadeltelegraphBuchstabeO : ICommand
    {
        private readonly NadeltelegraphViewModel viewModel;

        public NadeltelegraphBuchstabeO(NadeltelegraphViewModel vm)
        {
            viewModel = vm;
        }

        #region ICommand Members
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) { return viewModel.CanBuchstabeO; }
        public void Execute(object parameter) { viewModel.BuchstabeO(); }
        #endregion
    }
}