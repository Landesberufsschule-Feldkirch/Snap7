namespace Heizungsregler.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public Schreiber Schreiber { get; set; }
        public VisuAnzeigen ViAnzeige { get; set; }

        private readonly WohnHaus _wohnHaus;
        
        public ViewModel(MainWindow mw)
        {
            _wohnHaus = mw.WohnHaus;

            Schreiber = new Schreiber(_wohnHaus);
            ViAnzeige = new VisuAnzeigen(mw, _wohnHaus);
        }


        private ICommand _btnReset;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnReset => _btnReset ?? (_btnReset = new RelayCommand(p => _wohnHaus.Reset(), p => true));
    }
}