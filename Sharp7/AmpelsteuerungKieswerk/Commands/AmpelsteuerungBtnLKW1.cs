namespace AmpelsteuerungKieswerk.Commands
{
    using AmpelsteuerungKieswerk.ViewModel;
    using System.Windows.Input;

    class AmpelsteuerungBtnLkw1 : ICommand
    {
        private readonly AmpelsteuerungKieswerkViewModel viewModel;

        public AmpelsteuerungBtnLkw1(AmpelsteuerungKieswerkViewModel vm)
        {
            viewModel = vm;
        }

        #region ICommand Members
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) { return viewModel.CanButtonLkw1; }
        public void Execute(object parameter) { viewModel.TasterLkw1(); }
        #endregion

    }
}
