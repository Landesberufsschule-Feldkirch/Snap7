namespace StiegenhausBeleuchtung.ViewModel
{
    using StiegenhausBeleuchtung.Commands;
    using System.Windows.Input;

    public class StiegenhausBeleuchtungViewModel
    {

        public readonly Model.StiegenhausBeleuchtung stiegenhausBeleuchtung;
        public VisuAnzeigen ViAnzeige { get; set; }
        public StiegenhausBeleuchtungViewModel(MainWindow mainWindow)
        {
            stiegenhausBeleuchtung = new Model.StiegenhausBeleuchtung();
            ViAnzeige = new VisuAnzeigen(mainWindow, stiegenhausBeleuchtung);
            stiegenhausBeleuchtung.ProblemLoesen(ViAnzeige);
        }

        public Model.StiegenhausBeleuchtung StiegenhausBeleuchtung { get { return stiegenhausBeleuchtung; } }


        #region BtnStart
        private ICommand _btnStart;
        public ICommand BtnStart
        {
            get
            {
                if (_btnStart == null)
                {
                    _btnStart = new RelayCommand(stiegenhausBeleuchtung.BtnStart);
                }
                return _btnStart;
            }
        }
        #endregion



        #region Btn_B01
        private ICommand _btn_B01;
        public ICommand Btn_B01
        {
            get
            {
                if (_btn_B01 == null)
                {
                    _btn_B01 = new RelayCommand(p => ViAnzeige.Btn_B01(), p => true);
                }
                return _btn_B01;
            }
        }
        #endregion

        #region Btn_B02
        private ICommand _btn_B02;
        public ICommand Btn_B02
        {
            get
            {
                if (_btn_B02 == null)
                {
                    _btn_B02 = new RelayCommand(p => ViAnzeige.Btn_B02(), p => true);
                }
                return _btn_B02;
            }
        }
        #endregion

        #region Btn_B03
        private ICommand _btn_B03;
        public ICommand Btn_B03
        {
            get
            {
                if (_btn_B03 == null)
                {
                    _btn_B03 = new RelayCommand(p => ViAnzeige.Btn_B03(), p => true);
                }
                return _btn_B03;
            }
        }
        #endregion

        #region Btn_B11
        private ICommand _btn_B11;
        public ICommand Btn_B11
        {
            get
            {
                if (_btn_B11 == null)
                {
                    _btn_B11 = new RelayCommand(p => ViAnzeige.Btn_B11(), p => true);
                }
                return _btn_B11;
            }
        }
        #endregion

        #region Btn_B12
        private ICommand _btn_B12;
        public ICommand Btn_B12
        {
            get
            {
                if (_btn_B12 == null)
                {
                    _btn_B12 = new RelayCommand(p => ViAnzeige.Btn_B12(), p => true);
                }
                return _btn_B12;
            }
        }
        #endregion

        #region Btn_B13
        private ICommand _btn_B13;
        public ICommand Btn_B13
        {
            get
            {
                if (_btn_B13 == null)
                {
                    _btn_B13 = new RelayCommand(p => ViAnzeige.Btn_B13(), p => true);
                }
                return _btn_B13;
            }
        }
        #endregion

        #region Btn_B21
        private ICommand _btn_B21;
        public ICommand Btn_B21
        {
            get
            {
                if (_btn_B21 == null)
                {
                    _btn_B21 = new RelayCommand(p => ViAnzeige.Btn_B21(), p => true);
                }
                return _btn_B21;
            }
        }
        #endregion

        #region Btn_B22
        private ICommand _btn_B22;
        public ICommand Btn_B22
        {
            get
            {
                if (_btn_B22 == null)
                {
                    _btn_B22 = new RelayCommand(p => ViAnzeige.Btn_B22(), p => true);
                }
                return _btn_B22;
            }
        }
        #endregion

        #region Btn_B23
        private ICommand _btn_B23;
        public ICommand Btn_B23
        {
            get
            {
                if (_btn_B23 == null)
                {
                    _btn_B23 = new RelayCommand(p => ViAnzeige.Btn_B23(), p => true);
                }
                return _btn_B23;
            }
        }
        #endregion

        #region Btn_B31
        private ICommand _btn_B31;
        public ICommand Btn_B31
        {
            get
            {
                if (_btn_B31 == null)
                {
                    _btn_B31 = new RelayCommand(p => ViAnzeige.Btn_B31(), p => true);
                }
                return _btn_B31;
            }
        }
        #endregion

        #region Btn_B32
        private ICommand _btn_B32;
        public ICommand Btn_B32
        {
            get
            {
                if (_btn_B32 == null)
                {
                    _btn_B32 = new RelayCommand(p => ViAnzeige.Btn_B32(), p => true);
                }
                return _btn_B32;
            }
        }
        #endregion

        #region Btn_B33
        private ICommand _btn_B33;
        public ICommand Btn_B33
        {
            get
            {
                if (_btn_B33 == null)
                {
                    _btn_B33 = new RelayCommand(p => ViAnzeige.Btn_B33(), p => true);
                }
                return _btn_B33;
            }
        }
        #endregion

        #region Btn_B41
        private ICommand _btn_B41;
        public ICommand Btn_B41
        {
            get
            {
                if (_btn_B41 == null)
                {
                    _btn_B41 = new RelayCommand(p => ViAnzeige.Btn_B41(), p => true);
                }
                return _btn_B41;
            }
        }
        #endregion

        #region Btn_B42
        private ICommand _btn_B42;
        public ICommand Btn_B42
        {
            get
            {
                if (_btn_B42 == null)
                {
                    _btn_B42 = new RelayCommand(p => ViAnzeige.Btn_B42(), p => true);
                }
                return _btn_B42;
            }
        }
        #endregion

        #region Btn_B43
        private ICommand _btn_B43;
        public ICommand Btn_B43
        {
            get
            {
                if (_btn_B43 == null)
                {
                    _btn_B43 = new RelayCommand(p => ViAnzeige.Btn_B43(), p => true);
                }
                return _btn_B43;
            }
        }
        #endregion





    }
}
