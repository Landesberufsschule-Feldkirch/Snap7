namespace BehaelterSteuerung.Command
{
    using BehaelterSteuerung.ViewModel;
    using System.Windows.Input;
    class BehaltersteuerungBtnVentilQ4 : ICommand
    {
        private readonly BehaelterViewModel viewModel;
        public BehaltersteuerungBtnVentilQ4(BehaelterViewModel vm)
        {
            viewModel = vm;
        }

        #region ICommand Members
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) { return viewModel.CanUpdateVentilQ4; }
        public void Execute(object parameter) { viewModel.VentilQ4(); }
        #endregion
    }
}
