namespace LAP_2018_Niveauregelung.Commands
{
    using LAP_2018_Niveauregelung.ViewModel;
    using System.Windows.Input;

    class NiveauRegelungSetManualM2 : ICommand
    {

        private readonly NiveauRegelungViewModel viewModel;


        public NiveauRegelungSetManualM2(NiveauRegelungViewModel vm)
        {
            viewModel = vm;
        }

        #region ICommand Members
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) { return viewModel.CanSetManualM2; }
        public void Execute(object parameter) { viewModel.SetManualM2(); }
        #endregion

    }
}
