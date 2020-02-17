namespace AmpelsteuerungKieswerk.Commands
{
    using AmpelsteuerungKieswerk.ViewModel;
    using System.Windows.Input;

    class AmpelsteuerungBtnLinksParken : ICommand
    {
        private readonly AmpelsteuerungKieswerkViewModel viewModel;

        public AmpelsteuerungBtnLinksParken(AmpelsteuerungKieswerkViewModel vm)
        {
            viewModel = vm;
        }

        #region ICommand Members
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) { return viewModel.CanButtonLinksParken; }
        public void Execute(object parameter) { viewModel.TasterLinksParken(); }
        #endregion

    }
}
