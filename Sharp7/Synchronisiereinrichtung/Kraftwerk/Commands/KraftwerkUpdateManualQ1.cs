namespace Synchronisiereinrichtung.Kraftwerk.Command
{
    using System.Windows.Input;
    using Synchronisiereinrichtung.Kraftwerk.ViewModel;
    
    public class KraftwerkUpdateManualQ1 : ICommand
    {

        private readonly KraftwerkViewModel _ViewModel;

        public KraftwerkUpdateManualQ1(KraftwerkViewModel vm)
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
            return _ViewModel.CanUpdateQ1;
        }

        public void Execute(object parameter)
        {
            _ViewModel.SchalterQ1();
        }

        #endregion
    }
}
