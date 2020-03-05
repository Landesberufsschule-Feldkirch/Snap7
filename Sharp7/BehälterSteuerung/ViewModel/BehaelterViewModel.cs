namespace BehaelterSteuerung.ViewModel
{
    using BehaelterSteuerung.Commands;
    using System.Windows.Input;

    public class BehaelterViewModel
    {
        public readonly Model.BehaelterSteuerung alleBehaelter;
        public VisuAnzeigen ViAnzeige { get; set; }

        public BehaelterViewModel(MainWindow mainWindow)
        {
            alleBehaelter = new Model.BehaelterSteuerung();
            ViAnzeige = new VisuAnzeigen(mainWindow, alleBehaelter);        
        }

        public Model.BehaelterSteuerung AlleBehaelter { get { return alleBehaelter; } }




        #region BtnVentilQ2
        private ICommand _btnVentilQ2;
        public ICommand BtnVentilQ2
        {
            get
            {
                if (_btnVentilQ2 == null)
                {
                    _btnVentilQ2 = new RelayCommand(p => alleBehaelter.VentilQ2(), p => true);
                }
                return _btnVentilQ2;
            }
        }
        #endregion

        #region BtnVentilQ4
        private ICommand _btnVentilQ4;
        public ICommand BtnVentilQ4
        {
            get
            {
                if (_btnVentilQ4 == null)
                {
                    _btnVentilQ4 = new RelayCommand(p => alleBehaelter.VentilQ4(), p => true);
                }
                return _btnVentilQ4;
            }
        }
        #endregion

        #region BtnVentilQ6
        private ICommand _btnVentilQ6;
        public ICommand BtnVentilQ6
        {
            get
            {
                if (_btnVentilQ6 == null)
                {
                    _btnVentilQ6 = new RelayCommand(p => alleBehaelter.VentilQ6(), p => true);
                }
                return _btnVentilQ6;
            }
        }
        #endregion

        #region BtnVentilQ8
        private ICommand _btnVentilQ8;
        public ICommand BtnVentilQ8
        {
            get
            {
                if (_btnVentilQ8 == null)
                {
                    _btnVentilQ8 = new RelayCommand(p => alleBehaelter.VentilQ8(), p => true);
                }
                return _btnVentilQ8;
            }
        }
        #endregion




        #region BtnAutomatik1234
        private ICommand _btnAutomatik1234;
        public ICommand BtnAutomatik1234
        {
            get
            {
                if (_btnAutomatik1234 == null)
                {
                    _btnAutomatik1234 = new RelayCommand(p => alleBehaelter.Automatik1234(), p => true);
                }
                return _btnAutomatik1234;
            }
        }
        #endregion

        #region BtnAutomatik1324
        private ICommand _btnAutomatik1324;
        public ICommand BtnAutomatik1324
        {
            get
            {
                if (_btnAutomatik1324 == null)
                {
                    _btnAutomatik1324 = new RelayCommand(p => alleBehaelter.Automatik1324(), p => true);
                }
                return _btnAutomatik1324;
            }
        }
        #endregion

        #region BtnAutomatik1432
        private ICommand _btnAutomatik1432;
        public ICommand BtnAutomatik1432
        {
            get
            {
                if (_btnAutomatik1432 == null)
                {
                    _btnAutomatik1432 = new RelayCommand(p => alleBehaelter.Automatik1432(), p => true);
                }
                return _btnAutomatik1432;
            }
        }
        #endregion

        #region BtnAutomatik4321
        private ICommand _btnAutomatik4321;
        public ICommand BtnAutomatik4321
        {
            get
            {
                if (_btnAutomatik4321 == null)
                {
                    _btnAutomatik4321 = new RelayCommand(p => alleBehaelter.Automatik4321(), p => true);
                }
                return _btnAutomatik4321;
            }
        }
        #endregion
    }
}
