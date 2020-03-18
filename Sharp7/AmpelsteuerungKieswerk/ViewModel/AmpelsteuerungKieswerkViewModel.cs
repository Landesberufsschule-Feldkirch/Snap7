namespace AmpelsteuerungKieswerk.ViewModel
{
    using AmpelsteuerungKieswerk.Commands;
    using System.Windows.Input;

    public class AmpelsteuerungKieswerkViewModel
    {

        public readonly Model.AlleLastKraftWagen alleLastKraftWagen;
        public VisuAnzeigen ViAnzeige { get; set; }
        public AmpelsteuerungKieswerkViewModel(MainWindow mainWindow)
        {
            alleLastKraftWagen = new Model.AlleLastKraftWagen();
            ViAnzeige = new VisuAnzeigen(mainWindow, alleLastKraftWagen);
        }

        public Model.AlleLastKraftWagen AlleLastKraftWagen { get { return alleLastKraftWagen; } }


        #region BtnLkw1
        private ICommand _btnLkw1;
        public ICommand BtnLkw1
        {
            get
            {
                if (_btnLkw1 == null)
                {
                    _btnLkw1 = new RelayCommand(p => alleLastKraftWagen.TasterLkw1(), p => true);
                }
                return _btnLkw1;
            }
        }
        #endregion

        #region BtnLkw2
        private ICommand _btnLkw2;
        public ICommand BtnLkw2
        {
            get
            {
                if (_btnLkw2 == null)
                {
                    _btnLkw2 = new RelayCommand(p => alleLastKraftWagen.TasterLkw2(), p => true);
                }
                return _btnLkw2;
            }
        }
        #endregion

        #region BtnLkw3
        private ICommand _btnLkw3;
        public ICommand BtnLkw3
        {
            get
            {
                if (_btnLkw3 == null)
                {
                    _btnLkw3 = new RelayCommand(p => alleLastKraftWagen.TasterLkw3(), p => true);
                }
                return _btnLkw3;
            }
        }
        #endregion

        #region BtnLkw4
        private ICommand _btnLkw4;
        public ICommand BtnLkw4
        {
            get
            {
                if (_btnLkw4 == null)
                {
                    _btnLkw4 = new RelayCommand(p => alleLastKraftWagen.TasterLkw4(), p => true);
                }
                return _btnLkw4;
            }
        }
        #endregion

        #region BtnLkw5
        private ICommand _btnLkw5;
        public ICommand BtnLkw5
        {
            get
            {
                if (_btnLkw5 == null)
                {
                    _btnLkw5 = new RelayCommand(p => alleLastKraftWagen.TasterLkw5(), p => true);
                }
                return _btnLkw5;
            }
        }
        #endregion

        #region BtnLinksParken
        private ICommand _btnLinksParken;
        public ICommand BtnLinksParken
        {
            get
            {
                if (_btnLinksParken == null)
                {
                    _btnLinksParken = new RelayCommand(p => alleLastKraftWagen.TasterLinksParken(), p => true);
                }
                return _btnLinksParken;
            }
        }
        #endregion

        #region BtnRechtsParken
        private ICommand _btnRechtsParken;
        public ICommand BtnRechtsParken
        {
            get
            {
                if (_btnRechtsParken == null)
                {
                    _btnRechtsParken = new RelayCommand(p => alleLastKraftWagen.TasterRechtsParken(), p => true);
                }
                return _btnRechtsParken;
            }
        }
        #endregion
    }
}
