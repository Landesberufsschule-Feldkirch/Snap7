namespace Synchronisiereinrichtung.Kraftwerk.Command
{
    using Synchronisiereinrichtung.Kraftwerk.ViewModel;
    using System.Windows.Input;

    public class KraftwerkUpdateManualReset : ICommand
    {
        private readonly KraftwerkViewModel _ViewModel;

        public KraftwerkUpdateManualReset(KraftwerkViewModel vm)
        {
            _ViewModel = vm;
        }

        #region iCommand Members

        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _ViewModel.CanUpdateReset;
        }

        public void Execute(object parameter)
        {
            _ViewModel.SchalterReset();
        }

        #endregion iCommand Members
    }
}