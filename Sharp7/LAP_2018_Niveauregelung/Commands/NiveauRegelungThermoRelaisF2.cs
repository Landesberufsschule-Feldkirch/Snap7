namespace LAP_2018_Niveauregelung.Commands
{
    using LAP_2018_Niveauregelung.ViewModel;
    using System.Windows.Input;

    class NiveauRegelungThermorelaisF2 : ICommand
    {

        private readonly NiveauRegelungViewModel viewModel;


        public NiveauRegelungThermorelaisF2(NiveauRegelungViewModel vm)
        {
            viewModel = vm;
        }

        #region ICommand Members
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) { return viewModel.CanThermorelaisF2; }
        public void Execute(object parameter) { viewModel.ThermorelaisF2(); }
        #endregion

    }
}
