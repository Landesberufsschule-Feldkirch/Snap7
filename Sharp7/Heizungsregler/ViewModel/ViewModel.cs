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
            heizungsregler = new Model.Heizungsregler();
            Schreiber = new Heizungsregler.ViewModel.Schreiber(heizungsregler);
            ViAnzeige = new VisuAnzeigen(mainWindow, heizungsregler);
        }

        public Model.Heizungsregler Heizungsregler{ get { return heizungsregler; } }

        #region BtnReset

        private ICommand _btnReset;

        public ICommand BtnReset
        {
            get
            {
                if (_btnReset == null)
                {
                    _btnReset = new RelayCommand(p => heizungsregler.Reset(), p => true);
                }
                return _btnReset;
            }
        }

        #endregion


    }
}