namespace LAP_2018_Abfuellanlage.Commands
{

    using LAP_2018_Abfuellanlage.ViewModel;
    using System.Windows.Input;

    class AbfuellanlageSetManualK1 : ICommand
    {

        private readonly AbfuellanlageViewModel viewModel;


        public AbfuellanlageSetManualK1(AbfuellanlageViewModel vm)
        {
            viewModel = vm;
        }

        #region ICommand Members
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) { return viewModel.CanSetManualK1; }
        public void Execute(object parameter) { viewModel.TasterK1(); }
        #endregion

    }
}