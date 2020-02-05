namespace Synchronisiereinrichtung.Kraftwerk.Command
{
    using Synchronisiereinrichtung.Kraftwerk.ViewModel;
    using System.Windows.Input;

    public class KraftwerkBtnManualReset : ICommand
    {
        private readonly KraftwerkViewModel viewModel;

        public KraftwerkBtnManualReset(KraftwerkViewModel vm)
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
            return viewModel.CanUpdateReset;
        }

        public void Execute(object parameter)
        {
            viewModel.SchalterReset();
        }

        #endregion iCommand Members
    }
}