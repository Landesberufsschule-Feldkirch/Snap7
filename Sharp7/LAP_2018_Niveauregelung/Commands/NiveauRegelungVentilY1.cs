namespace LAP_2018_Niveauregelung.Commands
{
    using LAP_2018_Niveauregelung.ViewModel;
    using System.Windows.Input;

    class NiveauRegelungVentilY1 : ICommand
    {

        private readonly NiveauRegelungViewModel viewModel;


        public NiveauRegelungVentilY1(NiveauRegelungViewModel vm)
        {
            viewModel = vm;
        }

        #region ICommand Members
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) { return viewModel.CanVentilY1; }
        public void Execute(object parameter) { viewModel.VentilY1(); }
        #endregion

    }
}
