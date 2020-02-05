namespace Synchronisiereinrichtung.Kraftwerk.Command
{
    using Synchronisiereinrichtung.Kraftwerk.ViewModel;
    using System.Windows.Input;

    public class KraftwerkBtnManualQ1 : ICommand
    {
        private readonly KraftwerkViewModel viewModel;

        public KraftwerkBtnManualQ1(KraftwerkViewModel vm)
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
            return viewModel.CanUpdateQ1;
        }

        public void Execute(object parameter)
        {
            viewModel.SchalterQ1();
        }

        #endregion iCommand Members
    }
}