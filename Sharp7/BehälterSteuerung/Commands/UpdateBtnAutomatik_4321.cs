namespace BehaelterSteuerung.Command
{
    using BehaelterSteuerung.ViewModel;
    using System.Windows.Input;
    class BehaltersteuerungBtnAutomatik4321 : ICommand
    {
        private readonly BehaelterViewModel viewModel;
        public BehaltersteuerungBtnAutomatik4321(BehaelterViewModel vm)
        {
            viewModel = vm;
        }

        #region ICommand Members
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) { return viewModel.CanUpdateAutomatik4321; }
        public void Execute(object parameter) { viewModel.Automatik4321(); }
        #endregion
    }
}
