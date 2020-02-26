namespace Nadeltelegraph.ViewModel
{
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        readonly int WinkelNadel = 35;
        readonly int BreiteBreit = 10;
        readonly int BreiteSchmal = 1;

        private readonly Model.Nadeltelegraph nadeltelegraph;
        private readonly MainWindow mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.Nadeltelegraph nt)
        {
            mainWindow = mw;
            nadeltelegraph = nt;

            SpsStatus = "-";
            SpsColor = "LightBlue";



            ClickModeBtnTasteA = "Press";
            ClickModeBtnTasteB = "Press";
            ClickModeBtnTasteD = "Press";
            ClickModeBtnTasteE = "Press";
            ClickModeBtnTasteF = "Press";
            ClickModeBtnTasteG = "Press";
            ClickModeBtnTasteH = "Press";
            ClickModeBtnTasteI = "Press";
            ClickModeBtnTasteK = "Press";
            ClickModeBtnTasteL = "Press";
            ClickModeBtnTasteM = "Press";
            ClickModeBtnTasteN = "Press";
            ClickModeBtnTasteO = "Press";
            ClickModeBtnTasteP = "Press";
            ClickModeBtnTasteR = "Press";
            ClickModeBtnTasteS = "Press";
            ClickModeBtnTasteT = "Press";
            ClickModeBtnTasteV = "Press";
            ClickModeBtnTasteW = "Press";
            ClickModeBtnTasteY = "Press";




            ColorP0 = "LightGray";

            AngleNeedle1 = 0;
            AngleNeedle2 = 0;
            AngleNeedle3 = 0;
            AngleNeedle4 = 0;
            AngleNeedle5 = 0;

            Width1UpRight = 1;
            Width2UpRight = 1;
            Width3UpRight = 1;
            Width4UpRight = 1;

            Width2UpLeft = 1;
            Width3UpLeft = 1;
            Width4UpLeft = 1;
            Width5UpLeft = 1;

            Width1DownRight = 1;
            Width2DownRight = 1;
            Width3DownRight = 1;
            Width4DownRight = 1;

            Width2DownLeft = 1;
            Width3DownLeft = 1;
            Width4DownLeft = 1;
            Width5DownLeft = 1;

            System.Threading.Tasks.Task.Run(() => VisuAnzeigenTask());
        }


        private void VisuAnzeigenTask()
        {
            while (true)
            {
                FarbeP0(nadeltelegraph.P0);

                Breite1UpRight(nadeltelegraph.P1R);
                Breite2UpRight(nadeltelegraph.P2R);
                Breite3UpRight(nadeltelegraph.P3R);
                Breite4UpRight(nadeltelegraph.P4R);

                Breite1DownRight(nadeltelegraph.P1L);
                Breite2DownRight(nadeltelegraph.P2L);
                Breite3DownRight(nadeltelegraph.P3L);
                Breite4DownRight(nadeltelegraph.P4L);

                Breite2UpLeft(nadeltelegraph.P2L);
                Breite3UpLeft(nadeltelegraph.P3L);
                Breite4UpLeft(nadeltelegraph.P4L);
                Breite5UpLeft(nadeltelegraph.P5L);

                Breite2DownLeft(nadeltelegraph.P2R);
                Breite3DownLeft(nadeltelegraph.P3R);
                Breite4DownLeft(nadeltelegraph.P4R);
                Breite5DownLeft(nadeltelegraph.P5R);

                WinkelNadel1(nadeltelegraph.P1R, nadeltelegraph.P1L);
                WinkelNadel2(nadeltelegraph.P2R, nadeltelegraph.P2L);
                WinkelNadel3(nadeltelegraph.P3R, nadeltelegraph.P3L);
                WinkelNadel4(nadeltelegraph.P4R, nadeltelegraph.P4L);
                WinkelNadel5(nadeltelegraph.P5R, nadeltelegraph.P5L);

                if (mainWindow.S7_1200 != null)
                {
                    if (mainWindow.S7_1200.GetSpsError()) SpsColor = "Red"; else SpsColor = "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }
                Thread.Sleep(10);
            }
        }

        internal void BuchstabeA() { if (ClickModeButtonTasteA()) nadeltelegraph.Zeichen = 'A'; else nadeltelegraph.Zeichen = ' '; }
        internal void BuchstabeB() { if (ClickModeButtonTasteB()) nadeltelegraph.Zeichen = 'B'; else nadeltelegraph.Zeichen = ' '; }
        internal void BuchstabeD() { if (ClickModeButtonTasteD()) nadeltelegraph.Zeichen = 'D'; else nadeltelegraph.Zeichen = ' '; }
        internal void BuchstabeE() { if (ClickModeButtonTasteE()) nadeltelegraph.Zeichen = 'E'; else nadeltelegraph.Zeichen = ' '; }
        internal void BuchstabeF() { if (ClickModeButtonTasteF()) nadeltelegraph.Zeichen = 'F'; else nadeltelegraph.Zeichen = ' '; }
        internal void BuchstabeG() { if (ClickModeButtonTasteG()) nadeltelegraph.Zeichen = 'G'; else nadeltelegraph.Zeichen = ' '; }
        internal void BuchstabeH() { if (ClickModeButtonTasteH()) nadeltelegraph.Zeichen = 'H'; else nadeltelegraph.Zeichen = ' '; }
        internal void BuchstabeI() { if (ClickModeButtonTasteI()) nadeltelegraph.Zeichen = 'I'; else nadeltelegraph.Zeichen = ' '; }
        internal void BuchstabeK() { if (ClickModeButtonTasteK()) nadeltelegraph.Zeichen = 'K'; else nadeltelegraph.Zeichen = ' '; }
        internal void BuchstabeL() { if (ClickModeButtonTasteL()) nadeltelegraph.Zeichen = 'L'; else nadeltelegraph.Zeichen = ' '; }
        internal void BuchstabeM() { if (ClickModeButtonTasteM()) nadeltelegraph.Zeichen = 'M'; else nadeltelegraph.Zeichen = ' '; }
        internal void BuchstabeN() { if (ClickModeButtonTasteN()) nadeltelegraph.Zeichen = 'N'; else nadeltelegraph.Zeichen = ' '; }
        internal void BuchstabeO() { if (ClickModeButtonTasteO()) nadeltelegraph.Zeichen = 'O'; else nadeltelegraph.Zeichen = ' '; }
        internal void BuchstabeP() { if (ClickModeButtonTasteP()) nadeltelegraph.Zeichen = 'P'; else nadeltelegraph.Zeichen = ' '; }
        internal void BuchstabeR() { if (ClickModeButtonTasteR()) nadeltelegraph.Zeichen = 'R'; else nadeltelegraph.Zeichen = ' '; }
        internal void BuchstabeS() { if (ClickModeButtonTasteS()) nadeltelegraph.Zeichen = 'S'; else nadeltelegraph.Zeichen = ' '; }
        internal void BuchstabeT() { if (ClickModeButtonTasteT()) nadeltelegraph.Zeichen = 'T'; else nadeltelegraph.Zeichen = ' '; }
        internal void BuchstabeV() { if (ClickModeButtonTasteV()) nadeltelegraph.Zeichen = 'V'; else nadeltelegraph.Zeichen = ' '; }
        internal void BuchstabeW() { if (ClickModeButtonTasteW()) nadeltelegraph.Zeichen = 'W'; else nadeltelegraph.Zeichen = ' '; }
        internal void BuchstabeY() { if (ClickModeButtonTasteY()) nadeltelegraph.Zeichen = 'Y'; else nadeltelegraph.Zeichen = ' '; }




        #region SPS Status und Farbe
        private string _spsStatus;
        public string SpsStatus
        {
            get { return _spsStatus; }
            set
            {
                _spsStatus = value;
                OnPropertyChanged("SpsStatus");
            }
        }

        private string _spsColor;
        public string SpsColor
        {
            get { return _spsColor; }
            set
            {
                _spsColor = value;
                OnPropertyChanged("SpsColor");
            }
        }
        #endregion




        #region ClickModeBtnTasteA
        public bool ClickModeButtonTasteA()
        {
            if (ClickModeBtnTasteA == "Press")
            {
                ClickModeBtnTasteA = "Release";
                return true;
            }
            else
            {
                ClickModeBtnTasteA = "Press";
            }
            return false;
        }

        private string _clickModeBtnTasteA;
        public string ClickModeBtnTasteA
        {
            get { return _clickModeBtnTasteA; }
            set
            {
                _clickModeBtnTasteA = value;
                OnPropertyChanged("ClickModeBtnTasteA");
            }
        }
        #endregion

        #region ClickModeBtnTasteB
        public bool ClickModeButtonTasteB()
        {
            if (ClickModeBtnTasteB == "Press")
            {
                ClickModeBtnTasteB = "Release";
                return true;
            }
            else
            {
                ClickModeBtnTasteB = "Press";
            }
            return false;
        }

        private string _clickModeBtnTasteB;
        public string ClickModeBtnTasteB
        {
            get { return _clickModeBtnTasteB; }
            set
            {
                _clickModeBtnTasteB = value;
                OnPropertyChanged("ClickModeBtnTasteB");
            }
        }
        #endregion

        #region ClickModeBtnTasteD
        public bool ClickModeButtonTasteD()
        {
            if (ClickModeBtnTasteD == "Press")
            {
                ClickModeBtnTasteD = "Release";
                return true;
            }
            else
            {
                ClickModeBtnTasteD = "Press";
            }
            return false;
        }

        private string _clickModeBtnTasteD;
        public string ClickModeBtnTasteD
        {
            get { return _clickModeBtnTasteD; }
            set
            {
                _clickModeBtnTasteD = value;
                OnPropertyChanged("ClickModeBtnTasteD");
            }
        }
        #endregion

        #region ClickModeBtnTasteE
        public bool ClickModeButtonTasteE()
        {
            if (ClickModeBtnTasteE == "Press")
            {
                ClickModeBtnTasteE = "Release";
                return true;
            }
            else
            {
                ClickModeBtnTasteE = "Press";
            }
            return false;
        }

        private string _clickModeBtnTasteE;
        public string ClickModeBtnTasteE
        {
            get { return _clickModeBtnTasteE; }
            set
            {
                _clickModeBtnTasteE = value;
                OnPropertyChanged("ClickModeBtnTasteE");
            }
        }
        #endregion

        #region ClickModeBtnTasteF
        public bool ClickModeButtonTasteF()
        {
            if (ClickModeBtnTasteF == "Press")
            {
                ClickModeBtnTasteF = "Release";
                return true;
            }
            else
            {
                ClickModeBtnTasteF = "Press";
            }
            return false;
        }

        private string _clickModeBtnTasteF;
        public string ClickModeBtnTasteF
        {
            get { return _clickModeBtnTasteF; }
            set
            {
                _clickModeBtnTasteF = value;
                OnPropertyChanged("ClickModeBtnTasteF");
            }
        }
        #endregion

        #region ClickModeBtnTasteG
        public bool ClickModeButtonTasteG()
        {
            if (ClickModeBtnTasteG == "Press")
            {
                ClickModeBtnTasteG = "Release";
                return true;
            }
            else
            {
                ClickModeBtnTasteG = "Press";
            }
            return false;
        }

        private string _clickModeBtnTasteG;
        public string ClickModeBtnTasteG
        {
            get { return _clickModeBtnTasteG; }
            set
            {
                _clickModeBtnTasteG = value;
                OnPropertyChanged("ClickModeBtnTasteG");
            }
        }
        #endregion

        #region ClickModeBtnTasteH
        public bool ClickModeButtonTasteH()
        {
            if (ClickModeBtnTasteH == "Press")
            {
                ClickModeBtnTasteH = "Release";
                return true;
            }
            else
            {
                ClickModeBtnTasteH = "Press";
            }
            return false;
        }

        private string _clickModeBtnTasteH;
        public string ClickModeBtnTasteH
        {
            get { return _clickModeBtnTasteH; }
            set
            {
                _clickModeBtnTasteH = value;
                OnPropertyChanged("ClickModeBtnTasteH");
            }
        }
        #endregion

        #region ClickModeBtnTasteI
        public bool ClickModeButtonTasteI()
        {
            if (ClickModeBtnTasteI == "Press")
            {
                ClickModeBtnTasteI = "Release";
                return true;
            }
            else
            {
                ClickModeBtnTasteI = "Press";
            }
            return false;
        }

        private string _clickModeBtnTasteI;
        public string ClickModeBtnTasteI
        {
            get { return _clickModeBtnTasteI; }
            set
            {
                _clickModeBtnTasteI = value;
                OnPropertyChanged("ClickModeBtnTasteI");
            }
        }
        #endregion

        #region ClickModeBtnTasteK
        public bool ClickModeButtonTasteK()
        {
            if (ClickModeBtnTasteK == "Press")
            {
                ClickModeBtnTasteK = "Release";
                return true;
            }
            else
            {
                ClickModeBtnTasteK = "Press";
            }
            return false;
        }

        private string _clickModeBtnTasteK;
        public string ClickModeBtnTasteK
        {
            get { return _clickModeBtnTasteK; }
            set
            {
                _clickModeBtnTasteK = value;
                OnPropertyChanged("ClickModeBtnTasteK");
            }
        }
        #endregion

        #region ClickModeBtnTasteL
        public bool ClickModeButtonTasteL()
        {
            if (ClickModeBtnTasteL == "Press")
            {
                ClickModeBtnTasteL = "Release";
                return true;
            }
            else
            {
                ClickModeBtnTasteL = "Press";
            }
            return false;
        }

        private string _clickModeBtnTasteL;
        public string ClickModeBtnTasteL
        {
            get { return _clickModeBtnTasteL; }
            set
            {
                _clickModeBtnTasteL = value;
                OnPropertyChanged("ClickModeBtnTasteL");
            }
        }
        #endregion

        #region ClickModeBtnTasteM
        public bool ClickModeButtonTasteM()
        {
            if (ClickModeBtnTasteM == "Press")
            {
                ClickModeBtnTasteM = "Release";
                return true;
            }
            else
            {
                ClickModeBtnTasteM = "Press";
            }
            return false;
        }

        private string _clickModeBtnTasteM;
        public string ClickModeBtnTasteM
        {
            get { return _clickModeBtnTasteM; }
            set
            {
                _clickModeBtnTasteM = value;
                OnPropertyChanged("ClickModeBtnTasteM");
            }
        }
        #endregion

        #region ClickModeBtnTasteN
        public bool ClickModeButtonTasteN()
        {
            if (ClickModeBtnTasteN == "Press")
            {
                ClickModeBtnTasteN = "Release";
                return true;
            }
            else
            {
                ClickModeBtnTasteN = "Press";
            }
            return false;
        }

        private string _clickModeBtnTasteN;
        public string ClickModeBtnTasteN
        {
            get { return _clickModeBtnTasteN; }
            set
            {
                _clickModeBtnTasteN = value;
                OnPropertyChanged("ClickModeBtnTasteN");
            }
        }
        #endregion

        #region ClickModeBtnTasteO
        public bool ClickModeButtonTasteO()
        {
            if (ClickModeBtnTasteO == "Press")
            {
                ClickModeBtnTasteO = "Release";
                return true;
            }
            else
            {
                ClickModeBtnTasteO = "Press";
            }
            return false;
        }

        private string _clickModeBtnTasteO;
        public string ClickModeBtnTasteO
        {
            get { return _clickModeBtnTasteO; }
            set
            {
                _clickModeBtnTasteO = value;
                OnPropertyChanged("ClickModeBtnTasteO");
            }
        }
        #endregion

        #region ClickModeBtnTasteP
        public bool ClickModeButtonTasteP()
        {
            if (ClickModeBtnTasteP == "Press")
            {
                ClickModeBtnTasteP = "Release";
                return true;
            }
            else
            {
                ClickModeBtnTasteP = "Press";
            }
            return false;
        }

        private string _clickModeBtnTasteP;
        public string ClickModeBtnTasteP
        {
            get { return _clickModeBtnTasteP; }
            set
            {
                _clickModeBtnTasteP = value;
                OnPropertyChanged("ClickModeBtnTasteP");
            }
        }
        #endregion

        #region ClickModeBtnTasteR
        public bool ClickModeButtonTasteR()
        {
            if (ClickModeBtnTasteR == "Press")
            {
                ClickModeBtnTasteR = "Release";
                return true;
            }
            else
            {
                ClickModeBtnTasteR = "Press";
            }
            return false;
        }

        private string _clickModeBtnTasteR;
        public string ClickModeBtnTasteR
        {
            get { return _clickModeBtnTasteR; }
            set
            {
                _clickModeBtnTasteR = value;
                OnPropertyChanged("ClickModeBtnTasteR");
            }
        }
        #endregion

        #region ClickModeBtnTasteS
        public bool ClickModeButtonTasteS()
        {
            if (ClickModeBtnTasteS == "Press")
            {
                ClickModeBtnTasteS = "Release";
                return true;
            }
            else
            {
                ClickModeBtnTasteS = "Press";
            }
            return false;
        }

        private string _clickModeBtnTasteS;
        public string ClickModeBtnTasteS
        {
            get { return _clickModeBtnTasteS; }
            set
            {
                _clickModeBtnTasteS = value;
                OnPropertyChanged("ClickModeBtnTasteS");
            }
        }
        #endregion

        #region ClickModeBtnTasteT
        public bool ClickModeButtonTasteT()
        {
            if (ClickModeBtnTasteT == "Press")
            {
                ClickModeBtnTasteT = "Release";
                return true;
            }
            else
            {
                ClickModeBtnTasteT = "Press";
            }
            return false;
        }

        private string _clickModeBtnTasteT;
        public string ClickModeBtnTasteT
        {
            get { return _clickModeBtnTasteT; }
            set
            {
                _clickModeBtnTasteT = value;
                OnPropertyChanged("ClickModeBtnTasteT");
            }
        }
        #endregion

        #region ClickModeBtnTasteV
        public bool ClickModeButtonTasteV()
        {
            if (ClickModeBtnTasteV == "Press")
            {
                ClickModeBtnTasteV = "Release";
                return true;
            }
            else
            {
                ClickModeBtnTasteV = "Press";
            }
            return false;
        }

        private string _clickModeBtnTasteV;
        public string ClickModeBtnTasteV
        {
            get { return _clickModeBtnTasteV; }
            set
            {
                _clickModeBtnTasteV = value;
                OnPropertyChanged("ClickModeBtnTasteV");
            }
        }
        #endregion

        #region ClickModeBtnTasteW
        public bool ClickModeButtonTasteW()
        {
            if (ClickModeBtnTasteW == "Press")
            {
                ClickModeBtnTasteW = "Release";
                return true;
            }
            else
            {
                ClickModeBtnTasteW = "Press";
            }
            return false;
        }

        private string _clickModeBtnTasteW;
        public string ClickModeBtnTasteW
        {
            get { return _clickModeBtnTasteW; }
            set
            {
                _clickModeBtnTasteW = value;
                OnPropertyChanged("ClickModeBtnTasteW");
            }
        }
        #endregion

        #region ClickModeBtnTasteY
        public bool ClickModeButtonTasteY()
        {
            if (ClickModeBtnTasteY == "Press")
            {
                ClickModeBtnTasteY = "Release";
                return true;
            }
            else
            {
                ClickModeBtnTasteY = "Press";
            }
            return false;
        }

        private string _clickModeBtnTasteY;
        public string ClickModeBtnTasteY
        {
            get { return _clickModeBtnTasteY; }
            set
            {
                _clickModeBtnTasteY = value;
                OnPropertyChanged("ClickModeBtnTasteY");
            }
        }
        #endregion






        #region Color P0
        public void FarbeP0(bool val)
        {
            if (val) ColorP0 = "Red"; else ColorP0 = "LightGray";
        }

        private string _colorP0;
        public string ColorP0
        {
            get { return _colorP0; }
            set
            {
                _colorP0 = value;
                OnPropertyChanged("ColorP0");
            }
        }
        #endregion



        #region Winkel Nadel 1
        public void WinkelNadel1(bool rechts, bool links)
        {
            AngleNeedle1 = 0;
            if (rechts) AngleNeedle1 = WinkelNadel;
            if (links) AngleNeedle1 = -WinkelNadel;
        }

        private int _angleNeedle1;
        public int AngleNeedle1
        {
            get { return _angleNeedle1; }
            set
            {
                _angleNeedle1 = value;
                OnPropertyChanged(nameof(AngleNeedle1));
            }
        }
        #endregion

        #region Winkel Nadel 2
        public void WinkelNadel2(bool rechts, bool links)
        {
            AngleNeedle2 = 0;
            if (rechts) AngleNeedle2 = WinkelNadel;
            if (links) AngleNeedle2 = -WinkelNadel;
        }

        private int _angleNeedle2;
        public int AngleNeedle2
        {
            get { return _angleNeedle2; }
            set
            {
                _angleNeedle2 = value;
                OnPropertyChanged(nameof(AngleNeedle2));
            }
        }
        #endregion

        #region Winkel Nadel 3
        public void WinkelNadel3(bool rechts, bool links)
        {
            AngleNeedle3 = 0;
            if (rechts) AngleNeedle3 = WinkelNadel;
            if (links) AngleNeedle3 = -WinkelNadel;
        }

        private int _angleNeedle3;
        public int AngleNeedle3
        {
            get { return _angleNeedle3; }
            set
            {
                _angleNeedle3 = value;
                OnPropertyChanged(nameof(AngleNeedle3));
            }
        }
        #endregion

        #region Winkel Nadel 4
        public void WinkelNadel4(bool rechts, bool links)
        {
            AngleNeedle4 = 0;
            if (rechts) AngleNeedle4 = WinkelNadel;
            if (links) AngleNeedle4 = -WinkelNadel;
        }

        private int _angleNeedle4;
        public int AngleNeedle4
        {
            get { return _angleNeedle4; }
            set
            {
                _angleNeedle4 = value;
                OnPropertyChanged(nameof(AngleNeedle4));
            }
        }
        #endregion

        #region Winkel Nadel 5
        public void WinkelNadel5(bool rechts, bool links)
        {
            AngleNeedle5 = 0;
            if (rechts) AngleNeedle5 = WinkelNadel;
            if (links) AngleNeedle5 = -WinkelNadel;
        }

        private int _angleNeedle5;
        public int AngleNeedle5
        {
            get { return _angleNeedle5; }
            set
            {
                _angleNeedle5 = value;
                OnPropertyChanged(nameof(AngleNeedle5));
            }
        }
        #endregion




        #region Breite 1 UpRight
        public void Breite1UpRight(bool val)
        {
            if (val) Width1UpRight = BreiteBreit; else Width1UpRight = BreiteSchmal;
        }

        private int _width1UpRight;
        public int Width1UpRight
        {
            get { return _width1UpRight; }
            set
            {
                _width1UpRight = value;
                OnPropertyChanged("Width1UpRight");
            }
        }
        #endregion

        #region Breite 2 UpRight
        public void Breite2UpRight(bool val)
        {
            if (val) Width2UpRight = BreiteBreit; else Width2UpRight = BreiteSchmal;
        }

        private int _width2UpRight;
        public int Width2UpRight
        {
            get { return _width2UpRight; }
            set
            {
                _width2UpRight = value;
                OnPropertyChanged("Width2UpRight");
            }
        }
        #endregion

        #region Breite 3 UpRight
        public void Breite3UpRight(bool val)
        {
            if (val) Width3UpRight = BreiteBreit; else Width3UpRight = BreiteSchmal;
        }

        private int _width3UpRight;
        public int Width3UpRight
        {
            get { return _width3UpRight; }
            set
            {
                _width3UpRight = value;
                OnPropertyChanged("Width3UpRight");
            }
        }
        #endregion

        #region Breite 4 UpRight
        public void Breite4UpRight(bool val)
        {
            if (val) Width4UpRight = BreiteBreit; else Width4UpRight = BreiteSchmal;
        }

        private int _width4UpRight;
        public int Width4UpRight
        {
            get { return _width4UpRight; }
            set
            {
                _width4UpRight = value;
                OnPropertyChanged("Width4UpRight");
            }
        }
        #endregion


        #region Breite 2 UpLeft
        public void Breite2UpLeft(bool val)
        {
            if (val) Width2UpLeft = BreiteBreit; else Width2UpLeft = BreiteSchmal;
        }

        private int _width2UpLeft;
        public int Width2UpLeft
        {
            get { return _width2UpLeft; }
            set
            {
                _width2UpLeft = value;
                OnPropertyChanged("Width2UpLeft");
            }
        }
        #endregion

        #region Breite 3 UpLeft
        public void Breite3UpLeft(bool val)
        {
            if (val) Width3UpLeft = BreiteBreit; else Width3UpLeft = BreiteSchmal;
        }

        private int _width3UpLeft;
        public int Width3UpLeft
        {
            get { return _width3UpLeft; }
            set
            {
                _width3UpLeft = value;
                OnPropertyChanged("Width3UpLeft");
            }
        }
        #endregion

        #region Breite 4 UpLeft
        public void Breite4UpLeft(bool val)
        {
            if (val) Width4UpLeft = BreiteBreit; else Width4UpLeft = BreiteSchmal;
        }

        private int _width4UpLeft;
        public int Width4UpLeft
        {
            get { return _width4UpLeft; }
            set
            {
                _width4UpLeft = value;
                OnPropertyChanged("Width4UpLeft");
            }
        }
        #endregion

        #region Breite 5 UpLeft
        public void Breite5UpLeft(bool val)
        {
            if (val) Width5UpLeft = BreiteBreit; else Width5UpLeft = BreiteSchmal;
        }

        private int _width5UpLeft;
        public int Width5UpLeft
        {
            get { return _width5UpLeft; }
            set
            {
                _width5UpLeft = value;
                OnPropertyChanged("Width5UpLeft");
            }
        }
        #endregion



        #region Breite 1 DownRight
        public void Breite1DownRight(bool val)
        {
            if (val) Width1DownRight = BreiteBreit; else Width1DownRight = BreiteSchmal;
        }

        private int _width1DownRight;
        public int Width1DownRight
        {
            get { return _width1DownRight; }
            set
            {
                _width1DownRight = value;
                OnPropertyChanged("Width1DownRight");
            }
        }
        #endregion

        #region Breite 2 DownRight
        public void Breite2DownRight(bool val)
        {
            if (val) Width2DownRight = BreiteBreit; else Width2DownRight = BreiteSchmal;
        }

        private int _width2DownRight;
        public int Width2DownRight
        {
            get { return _width2DownRight; }
            set
            {
                _width2DownRight = value;
                OnPropertyChanged("Width2DownRight");
            }
        }
        #endregion

        #region Breite 3 DownRight
        public void Breite3DownRight(bool val)
        {
            if (val) Width3DownRight = BreiteBreit; else Width3DownRight = BreiteSchmal;
        }

        private int _width3DownRight;
        public int Width3DownRight
        {
            get { return _width3DownRight; }
            set
            {
                _width3DownRight = value;
                OnPropertyChanged("Width3DownRight");
            }
        }
        #endregion

        #region Breite 4 DownRight
        public void Breite4DownRight(bool val)
        {
            if (val) Width4DownRight = BreiteBreit; else Width4DownRight = BreiteSchmal;
        }

        private int _width4DownRight;
        public int Width4DownRight
        {
            get { return _width4DownRight; }
            set
            {
                _width4DownRight = value;
                OnPropertyChanged("Width4DownRight");
            }
        }
        #endregion



        #region Breite 2 DownLeft
        public void Breite2DownLeft(bool val)
        {
            if (val) Width2DownLeft = BreiteBreit; else Width2DownLeft = BreiteSchmal;
        }

        private int _width2DownLeft;
        public int Width2DownLeft
        {
            get { return _width2DownLeft; }
            set
            {
                _width2DownLeft = value;
                OnPropertyChanged("Width2DownLeft");
            }
        }
        #endregion

        #region Breite 3 DownLeft
        public void Breite3DownLeft(bool val)
        {
            if (val) Width3DownLeft = BreiteBreit; else Width3DownLeft = BreiteSchmal;
        }

        private int _width3DownLeft;
        public int Width3DownLeft
        {
            get { return _width3DownLeft; }
            set
            {
                _width3DownLeft = value;
                OnPropertyChanged("Width3DownLeft");
            }
        }
        #endregion

        #region Breite 4 DownLeft
        public void Breite4DownLeft(bool val)
        {
            if (val) Width4DownLeft = BreiteBreit; else Width4DownLeft = BreiteSchmal;
        }

        private int _width4DownLeft;
        public int Width4DownLeft
        {
            get { return _width4DownLeft; }
            set
            {
                _width4DownLeft = value;
                OnPropertyChanged("Width4DownLeft");
            }
        }
        #endregion

        #region Breite 5 DownLeft
        public void Breite5DownLeft(bool val)
        {
            if (val) Width5DownLeft = BreiteBreit; else Width5DownLeft = BreiteSchmal;
        }

        private int _width5DownLeft;
        public int Width5DownLeft
        {
            get { return _width5DownLeft; }
            set
            {
                _width5DownLeft = value;
                OnPropertyChanged("Width5DownLeft");
            }
        }
        #endregion


        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion iNotifyPeropertyChanged Members
    }
}