namespace BehaelterSteuerung.Command
{
    using BehaelterSteuerung.ViewModel;
    using System.Windows.Input;
    class BehaltersteuerungBtnVentilQ2 : ICommand
    {
        private readonly BehaelterViewModel _ViewModel;
        public BehaltersteuerungBtnVentilQ2(BehaelterViewModel vm)
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
            return _ViewModel.CanUpdateVentilQ2;
        }

        public void Execute(object parameter)
        {
            _ViewModel.VentilQ2();
        }
        #endregion
    }
}
