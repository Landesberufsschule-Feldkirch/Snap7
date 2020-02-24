namespace Nadeltelegraph.ViewModel
{
    using Nadeltelegraph.Commands;
    using System.Windows.Input;

    public class NadeltelegraphViewModel
    {
        public readonly Model.Nadeltelegraph nadeltelegraph;
        public VisuAnzeigen ViAnzeige { get; set; }
        public NadeltelegraphViewModel(MainWindow mainWindow)
        {
            nadeltelegraph = new Model.Nadeltelegraph(mainWindow);
            ViAnzeige = new VisuAnzeigen(mainWindow, nadeltelegraph);
        }

        public Model.Nadeltelegraph Nadeltelegraph { get { return nadeltelegraph; } }



        #region BtnBuchstabeA
        private ICommand _btnBuchstabeA;
        public ICommand BtnBuchstabeA
        {
            get
            {
                if (_btnBuchstabeA == null)
                {
                    _btnBuchstabeA = new RelayCommand(p => nadeltelegraph.BuchstabeA(), p => true);
                }
                return _btnBuchstabeA;
            }
        }
        #endregion

        #region BtnBuchstabeB
        private ICommand _btnBuchstabeB;
        public ICommand BtnBuchstabeB
        {
            get
            {
                if (_btnBuchstabeB == null)
                {
                    _btnBuchstabeB = new RelayCommand(p => nadeltelegraph.BuchstabeB(), p => true);
                }
                return _btnBuchstabeB;
            }
        }
        #endregion

        #region BtnBuchstabeD
        private ICommand _btnBuchstabeD;
        public ICommand BtnBuchstabeD
        {
            get
            {
                if (_btnBuchstabeD == null)
                {
                    _btnBuchstabeD = new RelayCommand(p => nadeltelegraph.BuchstabeD(), p => true);
                }
                return _btnBuchstabeD;
            }
        }
        #endregion

        #region BtnBuchstabeE
        private ICommand _btnBuchstabeE;
        public ICommand BtnBuchstabeE
        {
            get
            {
                if (_btnBuchstabeE == null)
                {
                    _btnBuchstabeE = new RelayCommand(p => nadeltelegraph.BuchstabeE(), p => true);
                }
                return _btnBuchstabeE;
            }
        }
        #endregion

        #region BtnBuchstabeF
        private ICommand _btnBuchstabeF;
        public ICommand BtnBuchstabeF
        {
            get
            {
                if (_btnBuchstabeF == null)
                {
                    _btnBuchstabeF = new RelayCommand(p => nadeltelegraph.BuchstabeF(), p => true);
                }
                return _btnBuchstabeF;
            }
        }
        #endregion

        #region BtnBuchstabeG
        private ICommand _btnBuchstabeG;
        public ICommand BtnBuchstabeG
        {
            get
            {
                if (_btnBuchstabeG == null)
                {
                    _btnBuchstabeG = new RelayCommand(p => nadeltelegraph.BuchstabeG(), p => true);
                }
                return _btnBuchstabeG;
            }
        }
        #endregion

        #region BtnBuchstabeH
        private ICommand _btnBuchstabeH;
        public ICommand BtnBuchstabeH
        {
            get
            {
                if (_btnBuchstabeH == null)
                {
                    _btnBuchstabeH = new RelayCommand(p => nadeltelegraph.BuchstabeH(), p => true);
                }
                return _btnBuchstabeH;
            }
        }
        #endregion

        #region BtnBuchstabeI
        private ICommand _btnBuchstabeI;
        public ICommand BtnBuchstabeI
        {
            get
            {
                if (_btnBuchstabeI == null)
                {
                    _btnBuchstabeI = new RelayCommand(p => nadeltelegraph.BuchstabeI(), p => true);
                }
                return _btnBuchstabeI;
            }
        }
        #endregion

        #region BtnBuchstabeK
        private ICommand _btnBuchstabeK;
        public ICommand BtnBuchstabeK
        {
            get
            {
                if (_btnBuchstabeK == null)
                {
                    _btnBuchstabeK = new RelayCommand(p => nadeltelegraph.BuchstabeK(), p => true);
                }
                return _btnBuchstabeK;
            }
        }
        #endregion

        #region BtnBuchstabeL
        private ICommand _btnBuchstabeL;
        public ICommand BtnBuchstabeL
        {
            get
            {
                if (_btnBuchstabeL == null)
                {
                    _btnBuchstabeL = new RelayCommand(p => nadeltelegraph.BuchstabeL(), p => true);
                }
                return _btnBuchstabeL;
            }
        }
        #endregion

        #region BtnBuchstabeM
        private ICommand _btnBuchstabeM;
        public ICommand BtnBuchstabeM
        {
            get
            {
                if (_btnBuchstabeM == null)
                {
                    _btnBuchstabeM = new RelayCommand(p => nadeltelegraph.BuchstabeM(), p => true);
                }
                return _btnBuchstabeM;
            }
        }
        #endregion

        #region BtnBuchstabeN
        private ICommand _btnBuchstabeN;
        public ICommand BtnBuchstabeN
        {
            get
            {
                if (_btnBuchstabeN == null)
                {
                    _btnBuchstabeN = new RelayCommand(p => nadeltelegraph.BuchstabeN(), p => true);
                }
                return _btnBuchstabeN;
            }
        }
        #endregion

        #region BtnBuchstabeO
        private ICommand _btnBuchstabeO;
        public ICommand BtnBuchstabeO
        {
            get
            {
                if (_btnBuchstabeO == null)
                {
                    _btnBuchstabeO = new RelayCommand(p => nadeltelegraph.BuchstabeO(), p => true);
                }
                return _btnBuchstabeO;
            }
        }
        #endregion

        #region BtnBuchstabeP
        private ICommand _btnBuchstabeP;
        public ICommand BtnBuchstabeP
        {
            get
            {
                if (_btnBuchstabeP == null)
                {
                    _btnBuchstabeP = new RelayCommand(p => nadeltelegraph.BuchstabeP(), p => true);
                }
                return _btnBuchstabeP;
            }
        }
        #endregion

        #region BtnBuchstabeR
        private ICommand _btnBuchstabeR;
        public ICommand BtnBuchstabeR
        {
            get
            {
                if (_btnBuchstabeR == null)
                {
                    _btnBuchstabeR = new RelayCommand(p => nadeltelegraph.BuchstabeR(), p => true);
                }
                return _btnBuchstabeR;
            }
        }
        #endregion

        #region BtnBuchstabeS
        private ICommand _btnBuchstabeS;
        public ICommand BtnBuchstabeS
        {
            get
            {
                if (_btnBuchstabeS == null)
                {
                    _btnBuchstabeS = new RelayCommand(p => nadeltelegraph.BuchstabeS(), p => true);
                }
                return _btnBuchstabeS;
            }
        }
        #endregion

        #region BtnBuchstabeT
        private ICommand _btnBuchstabeT;
        public ICommand BtnBuchstabeT
        {
            get
            {
                if (_btnBuchstabeT == null)
                {
                    _btnBuchstabeT = new RelayCommand(p => nadeltelegraph.BuchstabeT(), p => true);
                }
                return _btnBuchstabeT;
            }
        }
        #endregion

        #region BtnBuchstabeV
        private ICommand _btnBuchstabeV;
        public ICommand BtnBuchstabeV
        {
            get
            {
                if (_btnBuchstabeV == null)
                {
                    _btnBuchstabeV = new RelayCommand(p => nadeltelegraph.BuchstabeV(), p => true);
                }
                return _btnBuchstabeV;
            }
        }
        #endregion

        #region BtnBuchstabeW
        private ICommand _btnBuchstabeW;
        public ICommand BtnBuchstabeW
        {
            get
            {
                if (_btnBuchstabeW == null)
                {
                    _btnBuchstabeW = new RelayCommand(p => nadeltelegraph.BuchstabeW(), p => true);
                }
                return _btnBuchstabeW;
            }
        }
        #endregion

        #region BtnBuchstabeY
        private ICommand _btnBuchstabeY;
        public ICommand BtnBuchstabeY
        {
            get
            {
                if (_btnBuchstabeY == null)
                {
                    _btnBuchstabeY = new RelayCommand(p => nadeltelegraph.BuchstabeY(), p => true);
                }
                return _btnBuchstabeY;
            }
        }
        #endregion

    }
}