using Synchronisiereinrichtung.Kraftwerk.ViewModels;
using System;
using System.Windows.Input;

namespace Synchronisiereinrichtung.Kraftwerk.Commands
{
  internal  class KraftwerkUpdateCommand : ICommand
    {
        public KraftwerkUpdateCommand(KraftwerkViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private KraftwerkViewModel _ViewModel;


        #region iCommand Members

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _ViewModel.CanUpdate;
        }

        public void Execute(object parameter)
        {
            _ViewModel.SaveChanges();
        }

        #endregion
    }
}
