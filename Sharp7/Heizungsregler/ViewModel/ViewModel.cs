namespace Heizungsregler.ViewModel
{
    using Heizungsregler.Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        private readonly Model.Heizungsregler heizungsregler;
        public Heizungsregler.ViewModel.Schreiber Schreiber { get; set; }
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            heizungsregler = new Model.Heizungsregler(mainWindow);
            Schreiber = new Heizungsregler.ViewModel.Schreiber(heizungsregler);
            ViAnzeige = new VisuAnzeigen(mainWindow, heizungsregler);
        }

        public Model.Heizungsregler Heizungsregler => heizungsregler;

        #region BtnReset

        private ICommand _btnReset;

        public ICommand BtnReset => _btnReset ?? (_btnReset = new RelayCommand(p => heizungsregler.Reset(), p => true));

        #endregion BtnReset
    }
}