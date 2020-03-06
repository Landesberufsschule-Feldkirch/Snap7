namespace LAP_2010_4_Abfuellanlage.ViewModel
{
    using BehaelterSteuerung.Commands;
    using System.Windows.Input;

    public class AbfuellanlageViewModel
    {

        public VisuAnzeigen ViAnzeige { get; set; }
        public readonly LAP_2010_4_Abfuellanlage.Model.AbfuellAnlage abfuellAnlage;

        public AbfuellanlageViewModel(MainWindow mainWindow)
        {
            abfuellAnlage = new Model.AbfuellAnlage();
            ViAnzeige = new VisuAnzeigen(mainWindow, abfuellAnlage);
        }

        public Model.AbfuellAnlage AbfuellAnlage { get { return abfuellAnlage; } }

        #region BtnK1
        private ICommand _btnK1;
        public ICommand BtnK1
        {
            get
            {
                if (_btnK1 == null)
                {
                    _btnK1 = new RelayCommand(p => ViAnzeige.SetK1(), p => true);
                }
                return _btnK1;
            }
        }
        #endregion

        #region BtnK2
        private ICommand _btnK2;
        public ICommand BtnK2
        {
            get
            {
                if (_btnK2 == null)
                {
                    _btnK2 = new RelayCommand(p => ViAnzeige.SetK2(), p => true);
                }
                return _btnK2;
            }
        }
        #endregion

        #region BtnK3
        private ICommand _btnK3;
        public ICommand BtnK3
        {
            get
            {
                if (_btnK3 == null)
                {
                    _btnK3 = new RelayCommand(p => ViAnzeige.SetK3(), p => true);
                }
                return _btnK3;
            }
        }
        #endregion



        #region BtnReset
        private ICommand _btnReset;
        public ICommand BtnReset
        {
            get
            {
                if (_btnReset == null)
                {
                    _btnReset = new RelayCommand(p => abfuellAnlage.AllesReset(), p => true);
                }
                return _btnReset;
            }
        }
        #endregion
        
        #region BtnNachuellen
        private ICommand _btnNachuellen;
        public ICommand BtnNachuellen
        {
            get
            {
                if (_btnNachuellen == null)
                {
                    _btnNachuellen = new RelayCommand(p => abfuellAnlage.Nachfuellen(), p => true);
                }
                return _btnNachuellen;
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
                    _btnS2 = new RelayCommand(p => ViAnzeige.BtnS2(), p => true);
                }
                return _btnS2;
            }
        }
        #endregion


    }
}
