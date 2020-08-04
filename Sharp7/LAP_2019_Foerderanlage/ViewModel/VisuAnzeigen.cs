namespace LAP_2019_Foerderanlage.ViewModel
{
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;
    using Utilities;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private const int materialSiloHoehe = 8 * 35;
        private readonly MainWindow mainWindow;
        public readonly Model.Foerderanlage foerderanlage;

        public VisuAnzeigen(MainWindow mw, Model.Foerderanlage fa)
        {
            mainWindow = mw;
            foerderanlage = fa;

            VersionNr = "V0.0";
            SpsVersionsInfoSichtbar = "hidden";
            SpsVersionLokal = "fehlt";
            SpsVersionEntfernt = "fehlt";
            SpsStatus = "x";
            SpsColor = "LightBlue";

            SelectedIndex = 0; // Automatikbetrieb

            Margin1 = new System.Windows.Thickness(0, materialSiloHoehe * 0.1, 0, 0);

            ClickModeBtnS0 = "Press";
            ClickModeBtnS1 = "Press";
            ClickModeBtnS5 = "Press";
            ClickModeBtnS6 = "Press";
            ClickModeBtnS7 = "Press";
            ClickModeBtnS8 = "Press";

            ClickModeBtnM1_RL = "Press";
            ClickModeBtnM1_LL = "Press";
            ClickModeBtnM2 = "Press";
            ClickModeBtnK1 = "Press";
            ClickModeBtnM1_LL_K1 = "Press";

            VisibilityBtnSetManual = "Visible";

            VisibilityM1Ein = "Hidden";
            VisibilityM2Ein = "Hidden";

            VisibilityK1Ein = "Hidden";
            VisibilityK1Aus = "Visible";

            VisibilityB1Ein = "Visible";
            VisibilityB1Aus = "Hidden";

            VisibilityB2Ein = "Visible";
            VisibilityB2Aus = "Hidden";

            VisibilityMaterialOben = "Hidden";
            VisibilityMaterialUnten = "Hidden";

            VisibilityPfeilLinkslauf = "Hidden";
            VisibilityPfeilRechtslauf = "Hidden";

            VisibilityKurzschluss = "Hidden";

            ColorF1 = "LawnGreen";
            ColorP1 = "LawnGreen";
            ColorP2 = "LawnGreen";
            ColorS2 = "LawnGreen";

            PosWagenBeschriftungLeft = 74;
            PosWagenBeschriftungTop = 106;

            PosWagenLeft = 0;
            PosWagenTop = 0;

            PosWagenInhaltLeft = 12;
            PosWagenInhaltTop = 10;

            WagenFuellstand = 88;

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                FuellstandSilo(foerderanlage.Silo.GetFuellstand());

                SichtbarkeitBtnSetManual(System.Diagnostics.Debugger.IsAttached);

                SichtbarkeitPfeilLinkslauf(foerderanlage.Q2);
                SichtbarkeitPfeilRechtslauf(foerderanlage.Q1);

                SichtbarkeitM1(foerderanlage.Q1 || foerderanlage.Q2);
                SichtbarkeitM2(foerderanlage.T1);

                SichtbarkeitK1(foerderanlage.K1);

                SichtbarkeitB1(foerderanlage.Wagen.IstWagenRechts());
                SichtbarkeitB2(foerderanlage.Wagen.IstWagenVoll());

                SichtbarkeitMaterialOben(foerderanlage.Silo.GetFuellstand() > 0.01);
                SichtbarkeitMaterialUnten((foerderanlage.Silo.GetFuellstand() > 0.01) && (foerderanlage.K1));

                if (foerderanlage.Q1 && foerderanlage.Q2) VisibilityKurzschluss = "Visible"; else VisibilityKurzschluss = "Hidden";

                FarbeF1(foerderanlage.F1);
                FarbeP1(foerderanlage.P1);
                FarbeP2(foerderanlage.P2);
                FarbeS2(foerderanlage.S2);

                PositionWagenBeschriftung(foerderanlage.Wagen.GetPosition());
                PositionWagen(foerderanlage.Wagen.GetPosition());
                PositionWagenInhalt(foerderanlage.Wagen.GetPosition(), foerderanlage.Wagen.GetFuellstand());
                WagenFuellstand = foerderanlage.Wagen.GetFuellstand();

                if (mainWindow.AnimationGestartet)
                {
                    if (foerderanlage.T1) mainWindow.Controller.Play(); else mainWindow.Controller.Pause();
                }

                if (mainWindow.S7_1200 != null)
                {
                    SpsVersionLokal = mainWindow.VersionInfo;
                    SpsVersionEntfernt = mainWindow.S7_1200.GetVersion();                  
                    if (SpsVersionLokal == SpsVersionEntfernt) SpsVersionsInfoSichtbar = "hidden"; else SpsVersionsInfoSichtbar = "visible";

                    SpsColor = mainWindow.S7_1200.GetSpsError() ? "Red" : "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
        }

        internal void SetS0() => foerderanlage.S8 = ClickModeButtonS0();

        internal void SetS1() => foerderanlage.S8 = ClickModeButtonS1();

        internal void SetS5() => foerderanlage.S8 = ClickModeButtonS5();

        internal void SetS6() => foerderanlage.S8 = ClickModeButtonS6();

        internal void SetS7() => foerderanlage.S8 = ClickModeButtonS7();

        internal void SetS8() => foerderanlage.S8 = ClickModeButtonS8();

        internal void SetManualM1_RL() => foerderanlage.Manual_M1_RL = ClickModeButtonM1_RL();

        internal void SetManualM1_LL() => foerderanlage.Manual_M1_LL = ClickModeButtonM1_LL();

        internal void SetManualM1_LL_K1()
        {
            bool M1_LL_K1 = ClickModeButtonM1_LL_K1();
            foerderanlage.Manual_M1_LL = M1_LL_K1;
            foerderanlage.Manual_K1 = M1_LL_K1;
        }

        internal void SetManualM2() => foerderanlage.Manual_M2 = ClickModeButtonM2();

        internal void SetManualK1() => foerderanlage.Manual_K1 = ClickModeButtonK1();

        #region SPS Version, Status und Farbe

        private string _versionNr;
        public string VersionNr
        {
            get => _versionNr;
            set
            {
                _versionNr = value;
                OnPropertyChanged(nameof(VersionNr));
            }
        }

        private string _SpsVersionLokal;
        public string SpsVersionLokal
        {
            get => _SpsVersionLokal;
            set
            {
                _SpsVersionLokal = value;
                OnPropertyChanged(nameof(SpsVersionLokal));
            }
        }

        private string _SpsVersionEntfernt;
        public string SpsVersionEntfernt
        {
            get => _SpsVersionEntfernt;
            set
            {
                _SpsVersionEntfernt = value;
                OnPropertyChanged(nameof(SpsVersionEntfernt));
            }
        }

        private string _spsVersionsInfoSichtbar;
        public string SpsVersionsInfoSichtbar
        {
            get => _spsVersionsInfoSichtbar;
            set
            {
                _spsVersionsInfoSichtbar = value;
                OnPropertyChanged(nameof(SpsVersionsInfoSichtbar));
            }
        }

        private string _spsStatus;

        public string SpsStatus
        {
            get => _spsStatus;
            set
            {
                _spsStatus = value;
                OnPropertyChanged(nameof(SpsStatus));
            }
        }

        private string _spsColor;

        public string SpsColor
        {
            get => _spsColor;
            set
            {
                _spsColor = value;
                OnPropertyChanged(nameof(SpsColor));
            }
        }

        #endregion SPS Versionsinfo, Status und Farbe

        #region SelectedIndex

        private int _selectedIndex;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));

                if (value == 0)
                {
                    foerderanlage.S5 = true;    // Automatikbetrieb
                    foerderanlage.S6 = false;    // Handbetrieb
                }
                else
                {
                    foerderanlage.S5 = false;    // Automatikbetrieb
                    foerderanlage.S6 = true;    // Handbetrieb
                }
            }
        }

        #endregion SelectedIndex

        #region ClickModeBtnS0

        public bool ClickModeButtonS0()
        {
            if (ClickModeBtnS0 == "Press")
            {
                ClickModeBtnS0 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnS0 = "Press";
            }
            return false;
        }

        private string _clickModeBtnS0;

        public string ClickModeBtnS0
        {
            get => _clickModeBtnS0;
            set
            {
                _clickModeBtnS0 = value;
                OnPropertyChanged(nameof(ClickModeBtnS0));
            }
        }

        #endregion ClickModeBtnS0

        #region ClickModeBtnS1

        public bool ClickModeButtonS1()
        {
            if (ClickModeBtnS1 == "Press")
            {
                ClickModeBtnS1 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnS1 = "Press";
            }
            return false;
        }

        private string _clickModeBtnS1;

        public string ClickModeBtnS1
        {
            get => _clickModeBtnS1;
            set
            {
                _clickModeBtnS1 = value;
                OnPropertyChanged(nameof(ClickModeBtnS1));
            }
        }

        #endregion ClickModeBtnS1

        #region ClickModeBtnS5

        public bool ClickModeButtonS5()
        {
            if (ClickModeBtnS5 == "Press")
            {
                ClickModeBtnS5 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnS5 = "Press";
            }
            return false;
        }

        private string _clickModeBtnS5;

        public string ClickModeBtnS5
        {
            get => _clickModeBtnS5;
            set
            {
                _clickModeBtnS5 = value;
                OnPropertyChanged(nameof(ClickModeBtnS5));
            }
        }

        #endregion ClickModeBtnS5

        #region ClickModeBtnS6

        public bool ClickModeButtonS6()
        {
            if (ClickModeBtnS6 == "Press")
            {
                ClickModeBtnS6 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnS6 = "Press";
            }
            return false;
        }

        private string _clickModeBtnS6;

        public string ClickModeBtnS6
        {
            get => _clickModeBtnS6;
            set
            {
                _clickModeBtnS6 = value;
                OnPropertyChanged(nameof(ClickModeBtnS6));
            }
        }

        #endregion ClickModeBtnS6

        #region ClickModeBtnS7

        public bool ClickModeButtonS7()
        {
            if (ClickModeBtnS7 == "Press")
            {
                ClickModeBtnS7 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnS7 = "Press";
            }
            return false;
        }

        private string _clickModeBtnS7;

        public string ClickModeBtnS7
        {
            get => _clickModeBtnS7;
            set
            {
                _clickModeBtnS7 = value;
                OnPropertyChanged(nameof(ClickModeBtnS7));
            }
        }

        #endregion ClickModeBtnS7

        #region ClickModeBtnS8

        public bool ClickModeButtonS8()
        {
            if (ClickModeBtnS8 == "Press")
            {
                ClickModeBtnS8 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnS8 = "Press";
            }
            return false;
        }

        private string _clickModeBtnS8;

        public string ClickModeBtnS8
        {
            get => _clickModeBtnS8;
            set
            {
                _clickModeBtnS8 = value;
                OnPropertyChanged(nameof(ClickModeBtnS8));
            }
        }

        #endregion ClickModeBtnS8

        #region ClickModeBtnM1_RL

        public bool ClickModeButtonM1_RL()
        {
            if (ClickModeBtnM1_RL == "Press")
            {
                ClickModeBtnM1_RL = "Release";
                return true;
            }
            else
            {
                ClickModeBtnM1_RL = "Press";
            }
            return false;
        }

        private string _clickModeBtnM1_RL;

        public string ClickModeBtnM1_RL
        {
            get => _clickModeBtnM1_RL;
            set
            {
                _clickModeBtnM1_RL = value;
                OnPropertyChanged(nameof(ClickModeBtnM1_RL));
            }
        }

        #endregion ClickModeBtnM1_RL

        #region ClickModeBtnM1_LL

        public bool ClickModeButtonM1_LL()
        {
            if (ClickModeBtnM1_LL == "Press")
            {
                ClickModeBtnM1_LL = "Release";
                return true;
            }
            else
            {
                ClickModeBtnM1_LL = "Press";
            }
            return false;
        }

        private string _clickModeBtnM1_LL;

        public string ClickModeBtnM1_LL
        {
            get => _clickModeBtnM1_LL;
            set
            {
                _clickModeBtnM1_LL = value;
                OnPropertyChanged(nameof(ClickModeBtnM1_LL));
            }
        }

        #endregion ClickModeBtnM1_LL

        #region ClickModeBtnM2

        public bool ClickModeButtonM2()
        {
            if (ClickModeBtnM2 == "Press")
            {
                ClickModeBtnM2 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnM2 = "Press";
            }
            return false;
        }

        private string _clickModeBtnM2;

        public string ClickModeBtnM2
        {
            get => _clickModeBtnM2;
            set
            {
                _clickModeBtnM2 = value;
                OnPropertyChanged(nameof(ClickModeBtnM2));
            }
        }

        #endregion ClickModeBtnM2

        #region ClickModeBtnK1

        public bool ClickModeButtonK1()
        {
            if (ClickModeBtnK1 == "Press")
            {
                ClickModeBtnK1 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnK1 = "Press";
            }
            return false;
        }

        private string _clickModeBtnK1;

        public string ClickModeBtnK1
        {
            get => _clickModeBtnK1;
            set
            {
                _clickModeBtnK1 = value;
                OnPropertyChanged(nameof(ClickModeBtnK1));
            }
        }

        #endregion ClickModeBtnK1

        #region ClickModeBtnM1_LL_K1

        public bool ClickModeButtonM1_LL_K1()
        {
            if (ClickModeBtnM1_LL_K1 == "Press")
            {
                ClickModeBtnM1_LL_K1 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnM1_LL_K1 = "Press";
            }
            return false;
        }

        private string _clickModeBtnM1_LL_K1;

        public string ClickModeBtnM1_LL_K1
        {
            get => _clickModeBtnM1_LL_K1;
            set
            {
                _clickModeBtnM1_LL_K1 = value;
                OnPropertyChanged(nameof(ClickModeBtnM1_LL_K1));
            }
        }

        #endregion ClickModeBtnM1_LL_K1

        #region FuellstandSilo

        public void FuellstandSilo(double pegel)
        {
            Margin1 = new System.Windows.Thickness(0, materialSiloHoehe * (1 - pegel), 0, 0);
        }

        private Thickness _margin1;

        public Thickness Margin1
        {
            get => _margin1;
            set
            {
                _margin1 = value;
                OnPropertyChanged(nameof(Margin1));
            }
        }

        #endregion FuellstandSilo

        #region Sichtbarkeit BtnSetManual

        public void SichtbarkeitBtnSetManual(bool val)
        {
            if (val) VisibilityBtnSetManual = "Visible"; else VisibilityBtnSetManual = "Hidden";
        }

        private string _visibilityBtnSetManual;

        public string VisibilityBtnSetManual
        {
            get => _visibilityBtnSetManual;
            set
            {
                _visibilityBtnSetManual = value;
                OnPropertyChanged(nameof(VisibilityBtnSetManual));
            }
        }

        #endregion Sichtbarkeit BtnSetManual

        #region Sichtbarkeit PfeilLinkslauf

        public void SichtbarkeitPfeilLinkslauf(bool val)
        {
            if (val) VisibilityPfeilLinkslauf = "Visible"; else VisibilityPfeilLinkslauf = "Hidden";
        }

        private string _visibilityPfeilLinkslauf;

        public string VisibilityPfeilLinkslauf
        {
            get => _visibilityPfeilLinkslauf;
            set
            {
                _visibilityPfeilLinkslauf = value;
                OnPropertyChanged(nameof(VisibilityPfeilLinkslauf));
            }
        }

        #endregion Sichtbarkeit PfeilLinkslauf

        #region Sichtbarkeit PfeilRechtslauf

        public void SichtbarkeitPfeilRechtslauf(bool val)
        {
            if (val) VisibilityPfeilRechtslauf = "Visible"; else VisibilityPfeilRechtslauf = "Hidden";
        }

        private string _visibilityPfeilRechtslauf;

        public string VisibilityPfeilRechtslauf
        {
            get => _visibilityPfeilRechtslauf;
            set
            {
                _visibilityPfeilRechtslauf = value;
                OnPropertyChanged(nameof(VisibilityPfeilRechtslauf));
            }
        }

        #endregion Sichtbarkeit PfeilRechtslauf

        #region Sichtbarkeit M1

        public void SichtbarkeitM1(bool val)
        {
            if (val) VisibilityM1Ein = "Visible"; else VisibilityM1Ein = "Hidden";
        }

        private string _visibilityM1Ein;

        public string VisibilityM1Ein
        {
            get => _visibilityM1Ein;
            set
            {
                _visibilityM1Ein = value;
                OnPropertyChanged(nameof(VisibilityM1Ein));
            }
        }

        #endregion Sichtbarkeit M1

        #region Sichtbarkeit M2

        public void SichtbarkeitM2(bool val)
        {
            if (val) VisibilityM2Ein = "Visible"; else VisibilityM2Ein = "Hidden";
        }

        private string _visibilityM2Ein;

        public string VisibilityM2Ein
        {
            get => _visibilityM2Ein;
            set
            {
                _visibilityM2Ein = value;
                OnPropertyChanged(nameof(VisibilityM2Ein));
            }
        }

        #endregion Sichtbarkeit M2

        #region Sichtbarkeit K1

        public void SichtbarkeitK1(bool val)
        {
            if (val)
            {
                VisibilityK1Ein = "Visible";
                VisibilityK1Aus = "Hidden";
            }
            else
            {
                VisibilityK1Ein = "Hidden";
                VisibilityK1Aus = "Visible";
            }
        }

        private string _visibilityK1Ein;

        public string VisibilityK1Ein
        {
            get => _visibilityK1Ein;
            set
            {
                _visibilityK1Ein = value;
                OnPropertyChanged(nameof(VisibilityK1Ein));
            }
        }

        private string _visibilityK1Aus;

        public string VisibilityK1Aus
        {
            get => _visibilityK1Aus;
            set
            {
                _visibilityK1Aus = value;
                OnPropertyChanged(nameof(VisibilityK1Aus));
            }
        }

        #endregion Sichtbarkeit K1

        #region Sichtbarkeit B1

        public void SichtbarkeitB1(bool val)
        {
            if (val)
            {
                VisibilityB1Ein = "Visible";
                VisibilityB1Aus = "Hidden";
            }
            else
            {
                VisibilityB1Ein = "Hidden";
                VisibilityB1Aus = "Visible";
            }
        }

        private string _visibilityB1Ein;

        public string VisibilityB1Ein
        {
            get => _visibilityB1Ein;
            set
            {
                _visibilityB1Ein = value;
                OnPropertyChanged(nameof(VisibilityB1Ein));
            }
        }

        private string _visibilityB1Aus;

        public string VisibilityB1Aus
        {
            get => _visibilityB1Aus;
            set
            {
                _visibilityB1Aus = value;
                OnPropertyChanged(nameof(VisibilityB1Aus));
            }
        }

        #endregion Sichtbarkeit B1

        #region Sichtbarkeit B2

        public void SichtbarkeitB2(bool val)
        {
            if (val)
            {
                VisibilityB2Ein = "Visible";
                VisibilityB2Aus = "Hidden";
            }
            else
            {
                VisibilityB2Ein = "Hidden";
                VisibilityB2Aus = "Visible";
            }
        }

        private string _visibilityB2Ein;

        public string VisibilityB2Ein
        {
            get => _visibilityB2Ein;
            set
            {
                _visibilityB2Ein = value;
                OnPropertyChanged(nameof(VisibilityB2Ein));
            }
        }

        private string _visibilityB2Aus;

        public string VisibilityB2Aus
        {
            get => _visibilityB2Aus;
            set
            {
                _visibilityB2Aus = value;
                OnPropertyChanged(nameof(VisibilityB2Aus));
            }
        }

        #endregion Sichtbarkeit B2

        #region Sichtbarkeit MaterialOben

        public void SichtbarkeitMaterialOben(bool val)
        {
            if (val) VisibilityMaterialOben = "Visible"; else VisibilityMaterialOben = "Hidden";
        }

        private string _visibilityMaterialOben;

        public string VisibilityMaterialOben
        {
            get => _visibilityMaterialOben;
            set
            {
                _visibilityMaterialOben = value;
                OnPropertyChanged(nameof(VisibilityMaterialOben));
            }
        }

        #endregion Sichtbarkeit MaterialOben

        #region Sichtbarkeit MaterialUnten

        public void SichtbarkeitMaterialUnten(bool val)
        {
            if (val) VisibilityMaterialUnten = "Visible"; else VisibilityMaterialUnten = "Hidden";
        }

        private string _visibilityMaterialUnten;

        public string VisibilityMaterialUnten
        {
            get => _visibilityMaterialUnten;
            set
            {
                _visibilityMaterialUnten = value;
                OnPropertyChanged(nameof(VisibilityMaterialUnten));
            }
        }

        #endregion Sichtbarkeit MaterialUnten

        #region VisibilityKurzschluss

        private string _visibilityKurzschluss;

        public string VisibilityKurzschluss
        {
            get => _visibilityKurzschluss;
            set
            {
                _visibilityKurzschluss = value;
                OnPropertyChanged(nameof(VisibilityKurzschluss));
            }
        }

        #endregion VisibilityKurzschluss

        #region Color F1

        public void FarbeF1(bool val)
        {
            if (val) ColorF1 = "LawnGreen"; else ColorF1 = "Red";
        }

        private string _colorF1;

        public string ColorF1
        {
            get => _colorF1;
            set
            {
                _colorF1 = value;
                OnPropertyChanged(nameof(ColorF1));
            }
        }

        #endregion Color F1

        #region Color P1

        public void FarbeP1(bool val)
        {
            if (val) ColorP1 = "LawnGreen"; else ColorP1 = "White";
        }

        private string _colorP1;

        public string ColorP1
        {
            get => _colorP1;
            set
            {
                _colorP1 = value;
                OnPropertyChanged(nameof(ColorP1));
            }
        }

        #endregion Color P1

        #region Color P2

        public void FarbeP2(bool val)
        {
            if (val) ColorP2 = "Red"; else ColorP2 = "White";
        }

        private string _colorP2;

        public string ColorP2
        {
            get => _colorP2;
            set
            {
                _colorP2 = value;
                OnPropertyChanged(nameof(ColorP2));
            }
        }

        #endregion Color P2

        #region Color S2

        public void FarbeS2(bool val)
        {
            if (val) ColorS2 = "LawnGreen"; else ColorS2 = "Red";
        }

        private string _colorS2;

        public string ColorS2
        {
            get => _colorS2;
            set
            {
                _colorS2 = value;
                OnPropertyChanged(nameof(ColorS2));
            }
        }

        #endregion Color S2

        #region PositionWagenBeschriftung

        public void PositionWagenBeschriftung(Punkt pos)
        {
            PosWagenBeschriftungLeft = pos.X + 74;
            PosWagenBeschriftungTop = pos.Y + 106;
        }

        private double _posWagenBeschriftungLeft;

        public double PosWagenBeschriftungLeft
        {
            get => _posWagenBeschriftungLeft;
            set
            {
                _posWagenBeschriftungLeft = value;
                OnPropertyChanged(nameof(PosWagenBeschriftungLeft));
            }
        }

        private double _posWagenBeschriftungTop;

        public double PosWagenBeschriftungTop
        {
            get => _posWagenBeschriftungTop;
            set
            {
                _posWagenBeschriftungTop = value;
                OnPropertyChanged(nameof(PosWagenBeschriftungTop));
            }
        }

        #endregion PositionWagenBeschriftung

        #region PositionWagen

        public void PositionWagen(Punkt pos)
        {
            PosWagenLeft = pos.X;
            PosWagenTop = pos.Y;
        }

        private double _posWagenLeft;

        public double PosWagenLeft
        {
            get => _posWagenLeft;
            set
            {
                _posWagenLeft = value;
                OnPropertyChanged(nameof(PosWagenLeft));
            }
        }

        private double _posWagenTop;

        public double PosWagenTop
        {
            get => _posWagenTop;
            set
            {
                _posWagenTop = value;
                OnPropertyChanged(nameof(PosWagenTop));
            }
        }

        #endregion PositionWagen

        #region PositionWagenInhalt

        public void PositionWagenInhalt(Punkt pos, double fuellstand)
        {
            PosWagenInhaltLeft = pos.X + 12;
            PosWagenInhaltTop = pos.Y + 10 + 88 - fuellstand;
        }

        private double _posWagenInhaltLeft;

        public double PosWagenInhaltLeft
        {
            get => _posWagenInhaltLeft;
            set
            {
                _posWagenInhaltLeft = value;
                OnPropertyChanged(nameof(PosWagenInhaltLeft));
            }
        }

        private double _posWagenInhaltTop;

        public double PosWagenInhaltTop
        {
            get => _posWagenInhaltTop;
            set
            {
                _posWagenInhaltTop = value;
                OnPropertyChanged(nameof(PosWagenInhaltTop));
            }
        }

        #endregion PositionWagenInhalt

        #region WagenFuellstand

        private double _wagenFuellstand;

        public double WagenFuellstand
        {
            get => _wagenFuellstand;
            set
            {
                _wagenFuellstand = value;
                OnPropertyChanged(nameof(WagenFuellstand));
            }
        }

        #endregion WagenFuellstand

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}