namespace Synchronisiereinrichtung.Kraftwerk.Command
{
    using Synchronisiereinrichtung.Kraftwerk.ViewModel;
    using System.Windows.Input;

    public class KraftwerkBtnStop : ICommand
    {
        private readonly KraftwerkViewModel viewModel;

        public KraftwerkBtnStop(KraftwerkViewModel vm)
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
            return viewModel.CanUpdateStop;
        }

        public void Execute(object parameter)
        {
            viewModel.SchalterStop();
        }

        #endregion iCommand Members
    }
}