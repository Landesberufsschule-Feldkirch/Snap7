namespace LAP_2010_2_Transportwagen.ViewModel
{
    using LAP_2010_2_Transportwagen.Commands;
    using System.Windows.Input;

    public class TransportwagenViewModel
    {

        public VisuAnzeigen ViAnzeige { get; set; }
        public readonly LAP_2010_2_Transportwagen.Model.Transportwagen transportwagen;

        public TransportwagenViewModel(MainWindow mainWindow)
        {
            transportwagen = new Model.Transportwagen();
            ViAnzeige = new VisuAnzeigen(mainWindow, transportwagen);
        }

        public Model.Transportwagen Transportwagen { get { return transportwagen; } }


        #region SetManualK1
        private ICommand _setManualK1;
        public ICommand SetManualK1
        {
            get
            {
                if (_setManualK1 == null)
                {
                    _setManualK1 = new RelayCommand(p => ViAnzeige.SetManualK1(), p => true);
                }
                return _setManualK1;
            }
        }
        #endregion

        #region SetManualK2
        private ICommand _setManualK2;
        public ICommand SetManualK2
        {
            get
            {
                if (_setManualK2 == null)
                {
                    _setManualK2 = new RelayCommand(p => ViAnzeige.SetManualK2(), p => true);
                }
                return _setManualK2;
            }
        }
        #endregion

        #region BtnS1
        private ICommand _btnS1;
        public ICommand BtnS1
        {
            get
            {
                if (_btnS1 == null)
                {
                    _btnS1 = new RelayCommand(p => ViAnzeige.SetS1(), p => true);
                }
                return _btnS1;
            }
        }
        #endregion

        #region BtnS2
        private ICommand _btnS2;
        public ICommand BtnS2
        {
            get
            {
                if (_btnS2 == null)
                {
                    _btnS2 = new RelayCommand(p => transportwagen.SetS2(), p => true);
                }
                return _btnS2;
            }
        }
        #endregion

        #region BtnS3
        private ICommand _btnS3;
        public ICommand BtnS3
        {
            get
            {
                if (_btnS3 == null)
                {
                    _btnS3 = new RelayCommand(p => ViAnzeige.SetS3(), p => true);
                }
                return _btnS3;
            }
        }
        #endregion

        #region BtnF3
        private ICommand _btnF3;
        public ICommand BtnF3
        {
            get
            {
                if (_btnF3 == null)
                {
                    _btnF3 = new RelayCommand(p => transportwagen.SetF3(), p => true);
                }
                return _btnF3;
            }
        }
        #endregion


    }
}
