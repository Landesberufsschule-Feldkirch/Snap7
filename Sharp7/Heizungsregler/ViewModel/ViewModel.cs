namespace Heizungsregler.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        private readonly Model.Heizungsregler _heizungsregler;
        public Model.Heizungsregler Heizungsregler => _heizungsregler;
        public Schreiber Schreiber { get; set; }
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            _heizungsregler = new Model.Heizungsregler(mainWindow);
            Schreiber = new Schreiber(_heizungsregler);
            ViAnzeige = new VisuAnzeigen(mainWindow, _heizungsregler);
        }


        private ICommand _btnReset;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnReset => _btnReset ?? (_btnReset = new RelayCommand(p => _heizungsregler.Reset(), p => true));
    }
}