namespace BehaelterSteuerung.Command
{
    using BehaelterSteuerung.ViewModel;
    using System.Windows.Input;
    class BehaltersteuerungUpdateAutomatik4321 : ICommand
    {
        private readonly BehaelterViewModel _ViewModel;
        public BehaltersteuerungUpdateAutomatik4321(BehaelterViewModel vm)
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
            return _ViewModel.CanUpdateAutomatik4321;
        }

        public void Execute(object parameter)
        {
            _ViewModel.Automatik4321();
        }
        #endregion
    }
}
