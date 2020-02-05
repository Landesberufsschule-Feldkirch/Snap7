namespace LAP_2018_Abfuellanlage.Commands
{

    using LAP_2018_Abfuellanlage.ViewModel;
    using System.Windows.Input;

    class AbfuellanlageBtnTasterS3 : ICommand
    {

        private readonly AbfuellanlageViewModel viewModel;


        public AbfuellanlageBtnTasterS3(AbfuellanlageViewModel vm)
        {
            viewModel = vm;
        }

        #region ICommand Members
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) { return viewModel.CanButtonS3; }
        public void Execute(object parameter) { viewModel.TasterS3(); }
        #endregion

    }
}
