namespace LAP_2018_Abfuellanlage.Commands
{
    using LAP_2018_Abfuellanlage.ViewModel;
    using System.Windows.Input;

    class AbfuellanlageBtnNachfuellen : ICommand
    {
        private readonly AbfuellanlageViewModel viewModel;

        public AbfuellanlageBtnNachfuellen(AbfuellanlageViewModel vm)
        {
            viewModel = vm;
        }

        #region ICommand Members
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) { return viewModel.CanButtonNachfuellen; }
        public void Execute(object parameter) { viewModel.TasterNachfuellen(); }
        #endregion

    }
}
