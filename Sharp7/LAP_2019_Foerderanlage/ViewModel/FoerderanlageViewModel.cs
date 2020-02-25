namespace LAP_2019_Foerderanlage.ViewModel
{
    using System.Windows.Input;
    using LAP_2019_Foerderanlage.Commands;

    public class FoerderanlageViewModel
    {

        public VisuAnzeigen ViAnzeige { get; set; }
        public readonly Model.Foerderanlage foerderanlage;

        public FoerderanlageViewModel(MainWindow mainWindow)
        {
            foerderanlage = new Model.Foerderanlage();
            ViAnzeige = new VisuAnzeigen(mainWindow, foerderanlage);
        }

        public Model.Foerderanlage Foerderanlage { get { return foerderanlage; } }


        #region BtnWagenNachLinks
        private ICommand _btnWagenNachLinks;
        public ICommand BtnWagenNachLinks
        {
            get
            {
                if (_btnWagenNachLinks == null)
                {
                    _btnWagenNachLinks = new RelayCommand(p => foerderanlage.WagenNachLinks(), p => true);
                }
                return _btnWagenNachLinks;
            }
        }
        #endregion

        #region BtnWagenNachRechts
        private ICommand _btnWagenNachRechts;
        public ICommand BtnWagenNachRechts
        {
            get
            {
                if (_btnWagenNachRechts == null)
                {
                    _btnWagenNachRechts = new RelayCommand(p => foerderanlage.WagenNachRechts(), p => true);
                }
                return _btnWagenNachRechts;
            }
        }
        #endregion

    }
}
