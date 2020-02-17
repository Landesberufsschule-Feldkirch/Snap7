namespace Nadeltelegraph.Commands
{
    using Nadeltelegraph.ViewModel;
    using System.Windows.Input;

    class NadeltelegraphBuchstabeP : ICommand
    {
        private readonly NadeltelegraphViewModel viewModel;

        public NadeltelegraphBuchstabeP(NadeltelegraphViewModel vm)
        {
            viewModel = vm;
        }

        #region ICommand Members
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) { return viewModel.CanBuchstabeP; }
        public void Execute(object parameter) { viewModel.BuchstabeP(); }
        #endregion
    }
}