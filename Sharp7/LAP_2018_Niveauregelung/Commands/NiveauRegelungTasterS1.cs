namespace LAP_2018_Niveauregelung.Commands
{
    using LAP_2018_Niveauregelung.ViewModel;
    using System.Windows.Input;

    class NiveauRegelungTasterS1 : ICommand
    {

        private readonly NiveauRegelungViewModel viewModel;


        public NiveauRegelungTasterS1(NiveauRegelungViewModel vm)
        {
            viewModel = vm;
        }

        #region ICommand Members
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) { return viewModel.CanButtonS1; }
        public void Execute(object parameter) { viewModel.TasterS1(); }
        #endregion

    }
}
