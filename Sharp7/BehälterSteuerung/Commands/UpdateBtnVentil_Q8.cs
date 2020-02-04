namespace BehaelterSteuerung.Command
{
    using BehaelterSteuerung.ViewModel;
    using System.Windows.Input;
    class BehaltersteuerungBtnVentilQ8 : ICommand
    {
        private readonly BehaelterViewModel _ViewModel;
        public BehaltersteuerungBtnVentilQ8(BehaelterViewModel vm)
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
            return _ViewModel.CanUpdateVentilQ8;
        }

        public void Execute(object parameter)
        {
            _ViewModel.VentilQ8();
        }
        #endregion
    }
}
