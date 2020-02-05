namespace LAP_2018_Abfuellanlage.Commands
{

    using LAP_2018_Abfuellanlage.ViewModel;
    using System.Windows.Input;

    class AbfuellanlageBtnTasterS2 : ICommand
    {

        private readonly AbfuellanlageViewModel viewModel;


        public AbfuellanlageBtnTasterS2(AbfuellanlageViewModel vm)
        {
            viewModel = vm;
        }

        #region ICommand Members
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) { return viewModel.CanButtonS2; }
        public void Execute(object parameter) { viewModel.TasterS2(); }
        #endregion

    }
}
