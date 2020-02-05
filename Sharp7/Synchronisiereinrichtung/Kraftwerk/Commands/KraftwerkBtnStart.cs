namespace Synchronisiereinrichtung.Kraftwerk.Command
{
    using Synchronisiereinrichtung.Kraftwerk.ViewModel;
    using System.Windows.Input;

    public class KraftwerkBtnStart : ICommand
    {
        private readonly KraftwerkViewModel viewModel;

        public KraftwerkBtnStart(KraftwerkViewModel vm)
        {
            viewModel = vm;
        }

        #region iCommand Members

        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return viewModel.CanUpdateStart;
        }

        public void Execute(object parameter)
        {
            viewModel.SchalterStart();
        }

        #endregion iCommand Members
    }
}