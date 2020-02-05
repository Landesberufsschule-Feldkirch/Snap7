namespace BehaelterSteuerung.Command
{
    using BehaelterSteuerung.ViewModel;
    using System.Windows.Input;
    class BehaltersteuerungBtnAutomatik1234 : ICommand
    {
        private readonly BehaelterViewModel viewModel;
        public BehaltersteuerungBtnAutomatik1234(BehaelterViewModel vm)
        {
            viewModel = vm;
        }

        #region ICommand Members
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) { return viewModel.CanUpdateAutomatik1234; }
        public void Execute(object parameter) { viewModel.Automatik1234(); }
        #endregion
    }
}
