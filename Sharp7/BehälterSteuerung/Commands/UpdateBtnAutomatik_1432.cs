namespace BehaelterSteuerung.Command
{
    using BehaelterSteuerung.ViewModel;
    using System.Windows.Input;
    class BehaltersteuerungUpdateAutomatik1432 : ICommand
    {
        private readonly BehaelterViewModel _ViewModel;
        public BehaltersteuerungUpdateAutomatik1432(BehaelterViewModel vm)
        {
            _ViewModel = vm;
        }

        #region ICommand Members
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _ViewModel.CanUpdateAutomatik1432;
        }

        public void Execute(object parameter)
        {
            _ViewModel.Automatik1432();
        }
        #endregion
    }
}
