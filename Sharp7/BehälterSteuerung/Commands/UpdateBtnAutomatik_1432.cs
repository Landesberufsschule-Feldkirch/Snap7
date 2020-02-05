namespace BehaelterSteuerung.Command
{
    using BehaelterSteuerung.ViewModel;
    using System.Windows.Input;
    class BehaltersteuerungBtnAutomatik1432 : ICommand
    {
        private readonly BehaelterViewModel viewModel;
        public BehaltersteuerungBtnAutomatik1432(BehaelterViewModel vm)
        {
            viewModel = vm;
        }

        #region ICommand Members
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) { return viewModel.CanUpdateAutomatik1432; }
        public void Execute(object parameter) { viewModel.Automatik1432(); }
        #endregion
    }
}
