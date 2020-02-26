namespace LAP_2019_Foerderanlage.ViewModel
{
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;
    using Utilities;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private const int MaterialSiloHoehe = 8 * 35;

        private readonly MainWindow mainWindow;
        public readonly Model.Foerderanlage foerderanlage;


        public VisuAnzeigen(MainWindow mw, Model.Foerderanlage fa)
        {
            mainWindow = mw;
            foerderanlage = fa;

            SpsStatus = "-";
            SpsColor = "LightBlue";

            SelectedIndex = 0; // Automatikbetrieb

            Margin1 = new System.Windows.Thickness(0, MaterialSiloHoehe * 0.1, 0, 0);


            ClickModeBtnS0 = "Press";
            ClickModeBtnS1 = "Press";
            ClickModeBtnS9 = "Press";
            ClickModeBtnS10 = "Press";
            ClickModeBtnS11 = "Press";
            ClickModeBtnS12 = "Press";

            ClickModeBtnM1_RL = "Press";
            ClickModeBtnM1_LL = "Press";
            ClickModeBtnM2 = "Press";
            ClickModeBtnY1 = "Press";
            ClickModeBtnM1_LL_Y1 = "Press";

            VisibilityBtnSetManual = "Visible";

            VisibilityM1Ein = "Hidden";
            VisibilityM2Ein = "Hidden";

            VisibilityY1Ein = "Hidden";
            VisibilityY1Aus = "Visible";

            VisibilityS7Ein = "Visible";
            VisibilityS7Aus = "Hidden";

            VisibilityS8Ein = "Visible";
            VisibilityS8Aus = "Hidden";

            VisibilityMaterialOben = "Hidden";
            VisibilityMaterialUnten = "Hidden";

            VisibilityPfeilLinkslauf = "Hidden";
            VisibilityPfeilRechtslauf = "Hidden";


            ColorF4 = "LawnGreen";
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

            System.Threading.Tasks.Task.Run(() => VisuAnzeigenTask());
        }



        private void VisuAnzeigenTask()
        {
            while (true)
            {
                FuellstandSilo(foerderanlage.Silo.GetFuellstand());

                SichtbarkeitBtnSetManual(System.Diagnostics.Debugger.IsAttached);

                SichtbarkeitPfeilLinkslauf(foerderanlage.Q4_LL);
                SichtbarkeitPfeilRechtslauf(foerderanlage.Q3_RL);

                SichtbarkeitM1(foerderanlage.Q3_RL || foerderanlage.Q4_LL);
                SichtbarkeitM2(foerderanlage.XFU);

                SichtbarkeitY1(foerderanlage.Y1);

                SichtbarkeitS7(foerderanlage.Wagen.IstWagenRechts());
                SichtbarkeitS8(foerderanlage.Wagen.IstWagenVoll());

                SichtbarkeitMaterialOben(foerderanlage.Silo.GetFuellstand() > 0.01);
                SichtbarkeitMaterialUnten((foerderanlage.Silo.GetFuellstand() > 0.01) && (foerderanlage.Y1));

                FarbeF4(foerderanlage.F4);
                FarbeP1(foerderanlage.P1);
                FarbeP2(foerderanlage.P2);
                FarbeS2(foerderanlage.S2);


                PositionWagenBeschriftung(foerderanlage.Wagen.GetPosition());
                PositionWagen(foerderanlage.Wagen.GetPosition());
                PositionWagenInhalt(foerderanlage.Wagen.GetPosition(), foerderanlage.Wagen.GetFuellstand());
                WagenFuellstand = foerderanlage.Wagen.GetFuellstand();

                if (mainWindow.AnimationGestartet)
                {
                    if (foerderanlage.XFU) mainWindow.Controller.Play(); else mainWindow.Controller.Pause();
                }

                if (mainWindow.S7_1200 != null)
                {
                    if (mainWindow.S7_1200.GetSpsError()) SpsColor = "Red"; else SpsColor = "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }


                Thread.Sleep(10);
            }
        }

        internal void SetS0() { foerderanlage.S12 = ClickModeButtonS0(); }
        internal void SetS1() { foerderanlage.S12 = ClickModeButtonS1(); }
        internal void SetS9() { foerderanlage.S12 = ClickModeButtonS9(); }
        internal void SetS10() { foerderanlage.S12 = ClickModeButtonS10(); }
        internal void SetS11() { foerderanlage.S12 = ClickModeButtonS11(); }
        internal void SetS12() { foerderanlage.S12 = ClickModeButtonS12(); }


        internal void SetManualM1_RL() { foerderanlage.Manual_M1_RL = ClickModeButtonM1_RL(); }
        internal void SetManualM1_LL() { foerderanlage.Manual_M1_LL = ClickModeButtonM1_LL(); }
        internal void SetManualM1_LL_Y1()
        {
            bool M1_LL_Y1 = ClickModeButtonM1_LL_Y1();
            foerderanlage.Manual_M1_LL = M1_LL_Y1;
            foerderanlage.Manual_Y1 = M1_LL_Y1;
        }
        internal void SetManualM2() { foerderanlage.Manual_M2 = ClickModeButtonM2(); }
        internal void SetManualY1() { foerderanlage.Manual_Y1 = ClickModeButtonY1(); }



        #region SPS Status und Farbe
        private string _spsStatus;
        public string SpsStatus
        {
            get { return _spsStatus; }
            set
            {
                _spsStatus = value;
                OnPropertyChanged(nameof(SpsStatus));
            }
        }

        private string _spsColor;
        public string SpsColor
        {
            get { return _spsColor; }
            set
            {
                _spsColor = value;
                OnPropertyChanged(nameof(SpsColor));
            }
        }
        #endregion


        #region SelectedIndex

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
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

        #endregion



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
            get { return _clickModeBtnS0; }
            set
            {
                _clickModeBtnS0 = value;
                OnPropertyChanged(nameof(ClickModeBtnS0));
            }
        }
        #endregion

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
            get { return _clickModeBtnS1; }
            set
            {
                _clickModeBtnS1 = value;
                OnPropertyChanged(nameof(ClickModeBtnS1));
            }
        }
        #endregion

        #region ClickModeBtnS9
        public bool ClickModeButtonS9()
        {
            if (ClickModeBtnS9 == "Press")
            {
                ClickModeBtnS9 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnS9 = "Press";
            }
            return false;
        }

        private string _clickModeBtnS9;
        public string ClickModeBtnS9
        {
            get { return _clickModeBtnS9; }
            set
            {
                _clickModeBtnS9 = value;
                OnPropertyChanged(nameof(ClickModeBtnS9));
            }
        }
        #endregion

        #region ClickModeBtnS10
        public bool ClickModeButtonS10()
        {
            if (ClickModeBtnS10 == "Press")
            {
                ClickModeBtnS10 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnS10 = "Press";
            }
            return false;
        }

        private string _clickModeBtnS10;
        public string ClickModeBtnS10
        {
            get { return _clickModeBtnS10; }
            set
            {
                _clickModeBtnS10 = value;
                OnPropertyChanged(nameof(ClickModeBtnS10));
            }
        }
        #endregion

        #region ClickModeBtnS11
        public bool ClickModeButtonS11()
        {
            if (ClickModeBtnS11 == "Press")
            {
                ClickModeBtnS11 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnS11 = "Press";
            }
            return false;
        }

        private string _clickModeBtnS11;
        public string ClickModeBtnS11
        {
            get { return _clickModeBtnS11; }
            set
            {
                _clickModeBtnS11 = value;
                OnPropertyChanged(nameof(ClickModeBtnS11));
            }
        }
        #endregion

        #region ClickModeBtnS12
        public bool ClickModeButtonS12()
        {
            if (ClickModeBtnS12 == "Press")
            {
                ClickModeBtnS12 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnS12 = "Press";
            }
            return false;
        }

        private string _clickModeBtnS12;
        public string ClickModeBtnS12
        {
            get { return _clickModeBtnS12; }
            set
            {
                _clickModeBtnS12 = value;
                OnPropertyChanged(nameof(ClickModeBtnS12));
            }
        }
        #endregion


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
            get { return _clickModeBtnM1_RL; }
            set
            {
                _clickModeBtnM1_RL = value;
                OnPropertyChanged(nameof(ClickModeBtnM1_RL));
            }
        }
        #endregion

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
            get { return _clickModeBtnM1_LL; }
            set
            {
                _clickModeBtnM1_LL = value;
                OnPropertyChanged(nameof(ClickModeBtnM1_LL));
            }
        }
        #endregion

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
            get { return _clickModeBtnM2; }
            set
            {
                _clickModeBtnM2 = value;
                OnPropertyChanged(nameof(ClickModeBtnM2));
            }
        }
        #endregion

        #region ClickModeBtnY1
        public bool ClickModeButtonY1()
        {
            if (ClickModeBtnY1 == "Press")
            {
                ClickModeBtnY1 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnY1 = "Press";
            }
            return false;
        }

        private string _clickModeBtnY1;
        public string ClickModeBtnY1
        {
            get { return _clickModeBtnY1; }
            set
            {
                _clickModeBtnY1 = value;
                OnPropertyChanged(nameof(ClickModeBtnY1));
            }
        }
        #endregion

        #region ClickModeBtnM1_LL_Y1
        public bool ClickModeButtonM1_LL_Y1()
        {
            if (ClickModeBtnM1_LL_Y1 == "Press")
            {
                ClickModeBtnM1_LL_Y1 = "Release";
                return true;
            }
            else
            {
                ClickModeBtnM1_LL_Y1 = "Press";
            }
            return false;
        }

        private string _clickModeBtnM1_LL_Y1;
        public string ClickModeBtnM1_LL_Y1
        {
            get { return _clickModeBtnM1_LL_Y1; }
            set
            {
                _clickModeBtnM1_LL_Y1 = value;
                OnPropertyChanged(nameof(ClickModeBtnM1_LL_Y1));
            }
        }
        #endregion


        #region FuellstandSilo
        public void FuellstandSilo(double pegel)
        {
            Margin1 = new System.Windows.Thickness(0, MaterialSiloHoehe * (1 - pegel), 0, 0);
        }

        private Thickness _margin1;
        public Thickness Margin1
        {
            get { return _margin1; }
            set
            {
                _margin1 = value;
                OnPropertyChanged(nameof(Margin1));
            }
        }
        #endregion




        #region Sichtbarkeit BtnSetManual
        public void SichtbarkeitBtnSetManual(bool val)
        {
            if (val)
            {
                VisibilityBtnSetManual = "Visible";
            }
            else
            {
                VisibilityBtnSetManual = "Hidden";
            }
        }

        private string _visibilityBtnSetManual;
        public string VisibilityBtnSetManual
        {
            get { return _visibilityBtnSetManual; }
            set
            {
                _visibilityBtnSetManual = value;
                OnPropertyChanged(nameof(VisibilityBtnSetManual));
            }
        }
        #endregion

        #region Sichtbarkeit PfeilLinkslauf
        public void SichtbarkeitPfeilLinkslauf(bool val)
        {
            if (val)
            {
                VisibilityPfeilLinkslauf = "Visible";
            }
            else
            {
                VisibilityPfeilLinkslauf = "Hidden";
            }
        }

        private string _visibilityPfeilLinkslauf;
        public string VisibilityPfeilLinkslauf
        {
            get { return _visibilityPfeilLinkslauf; }
            set
            {
                _visibilityPfeilLinkslauf = value;
                OnPropertyChanged(nameof(VisibilityPfeilLinkslauf));
            }
        }
        #endregion

        #region Sichtbarkeit PfeilRechtslauf
        public void SichtbarkeitPfeilRechtslauf(bool val)
        {
            if (val)
            {
                VisibilityPfeilRechtslauf = "Visible";
            }
            else
            {
                VisibilityPfeilRechtslauf = "Hidden";
            }
        }

        private string _visibilityPfeilRechtslauf;
        public string VisibilityPfeilRechtslauf
        {
            get { return _visibilityPfeilRechtslauf; }
            set
            {
                _visibilityPfeilRechtslauf = value;
                OnPropertyChanged(nameof(VisibilityPfeilRechtslauf));
            }
        }
        #endregion

        #region Sichtbarkeit M1
        public void SichtbarkeitM1(bool val)
        {
            if (val)
            {
                VisibilityM1Ein = "Visible";
            }
            else
            {
                VisibilityM1Ein = "Hidden";
            }
        }

        private string _visibilityM1Ein;
        public string VisibilityM1Ein
        {
            get { return _visibilityM1Ein; }
            set
            {
                _visibilityM1Ein = value;
                OnPropertyChanged(nameof(VisibilityM1Ein));
            }
        }
        #endregion

        #region Sichtbarkeit M2
        public void SichtbarkeitM2(bool val)
        {
            if (val)
            {
                VisibilityM2Ein = "Visible";
            }
            else
            {
                VisibilityM2Ein = "Hidden";
            }
        }

        private string _visibilityM2Ein;
        public string VisibilityM2Ein
        {
            get { return _visibilityM2Ein; }
            set
            {
                _visibilityM2Ein = value;
                OnPropertyChanged(nameof(VisibilityM2Ein));
            }
        }
        #endregion

        #region Sichtbarkeit Y1
        public void SichtbarkeitY1(bool val)
        {
            if (val)
            {
                VisibilityY1Ein = "Visible";
                VisibilityY1Aus = "Hidden";
            }
            else
            {
                VisibilityY1Ein = "Hidden";
                VisibilityY1Aus = "Visible";
            }
        }

        private string _visibilityY1Ein;
        public string VisibilityY1Ein
        {
            get { return _visibilityY1Ein; }
            set
            {
                _visibilityY1Ein = value;
                OnPropertyChanged(nameof(VisibilityY1Ein));
            }
        }

        private string _visibilityY1Aus;

        public string VisibilityY1Aus
        {
            get { return _visibilityY1Aus; }
            set
            {
                _visibilityY1Aus = value;
                OnPropertyChanged(nameof(VisibilityY1Aus));
            }
        }
        #endregion

        #region Sichtbarkeit S7
        public void SichtbarkeitS7(bool val)
        {
            if (val)
            {
                VisibilityS7Ein = "Visible";
                VisibilityS7Aus = "Hidden";
            }
            else
            {
                VisibilityS7Ein = "Hidden";
                VisibilityS7Aus = "Visible";
            }
        }

        private string _visibilityS7Ein;
        public string VisibilityS7Ein
        {
            get { return _visibilityS7Ein; }
            set
            {
                _visibilityS7Ein = value;
                OnPropertyChanged(nameof(VisibilityS7Ein));
            }
        }

        private string _visibilityS7Aus;

        public string VisibilityS7Aus
        {
            get { return _visibilityS7Aus; }
            set
            {
                _visibilityS7Aus = value;
                OnPropertyChanged(nameof(VisibilityS7Aus));
            }
        }
        #endregion

        #region Sichtbarkeit S8
        public void SichtbarkeitS8(bool val)
        {
            if (val)
            {
                VisibilityS8Ein = "Visible";
                VisibilityS8Aus = "Hidden";
            }
            else
            {
                VisibilityS8Ein = "Hidden";
                VisibilityS8Aus = "Visible";
            }
        }

        private string _visibilityS8Ein;
        public string VisibilityS8Ein
        {
            get { return _visibilityS8Ein; }
            set
            {
                _visibilityS8Ein = value;
                OnPropertyChanged(nameof(VisibilityS8Ein));
            }
        }

        private string _visibilityS8Aus;

        public string VisibilityS8Aus
        {
            get { return _visibilityS8Aus; }
            set
            {
                _visibilityS8Aus = value;
                OnPropertyChanged(nameof(VisibilityS8Aus));
            }
        }
        #endregion

        #region Sichtbarkeit MaterialOben
        public void SichtbarkeitMaterialOben(bool val)
        {
            if (val)
            {
                VisibilityMaterialOben = "Visible";
            }
            else
            {
                VisibilityMaterialOben = "Hidden";
            }
        }



        private string _visibilityMaterialOben;

        public string VisibilityMaterialOben
        {
            get { return _visibilityMaterialOben; }
            set
            {
                _visibilityMaterialOben = value;
                OnPropertyChanged(nameof(VisibilityMaterialOben));
            }
        }
        #endregion

        #region Sichtbarkeit MaterialUnten
        public void SichtbarkeitMaterialUnten(bool val)
        {
            if (val)
            {
                VisibilityMaterialUnten = "Visible";
            }
            else
            {
                VisibilityMaterialUnten = "Hidden";
            }
        }



        private string _visibilityMaterialUnten;

        public string VisibilityMaterialUnten
        {
            get { return _visibilityMaterialUnten; }
            set
            {
                _visibilityMaterialUnten = value;
                OnPropertyChanged(nameof(VisibilityMaterialUnten));
            }
        }
        #endregion



        #region Color F4
        public void FarbeF4(bool val)
        {
            if (val) ColorF4 = "LawnGreen"; else ColorF4 = "Red";
        }

        private string _colorF4;
        public string ColorF4
        {
            get { return _colorF4; }
            set
            {
                _colorF4 = value;
                OnPropertyChanged(nameof(ColorF4));
            }
        }
        #endregion

        #region Color P1
        public void FarbeP1(bool val)
        {
            if (val) ColorP1 = "LawnGreen"; else ColorP1 = "White";
        }

        private string _colorP1;
        public string ColorP1
        {
            get { return _colorP1; }
            set
            {
                _colorP1 = value;
                OnPropertyChanged(nameof(ColorP1));
            }
        }
        #endregion

        #region Color P2
        public void FarbeP2(bool val)
        {
            if (val) ColorP2 = "Red"; else ColorP2 = "White";
        }

        private string _colorP2;
        public string ColorP2
        {
            get { return _colorP2; }
            set
            {
                _colorP2 = value;
                OnPropertyChanged(nameof(ColorP2));
            }
        }
        #endregion

        #region Color S2
        public void FarbeS2(bool val)
        {
            if (val) ColorS2 = "LawnGreen"; else ColorS2 = "Red";
        }

        private string _colorS2;
        public string ColorS2
        {
            get { return _colorS2; }
            set
            {
                _colorS2 = value;
                OnPropertyChanged(nameof(ColorS2));
            }
        }
        #endregion




        #region PositionWagenBeschriftung
        public void PositionWagenBeschriftung(Punkt pos)
        {
            PosWagenBeschriftungLeft = pos.X + 74;
            PosWagenBeschriftungTop = pos.Y + 106;
        }

        private double _posWagenBeschriftungLeft;
        public double PosWagenBeschriftungLeft
        {
            get { return _posWagenBeschriftungLeft; }
            set
            {
                _posWagenBeschriftungLeft = value;
                OnPropertyChanged(nameof(PosWagenBeschriftungLeft));
            }
        }
        private double _posWagenBeschriftungTop;
        public double PosWagenBeschriftungTop
        {
            get { return _posWagenBeschriftungTop; }
            set
            {
                _posWagenBeschriftungTop = value;
                OnPropertyChanged(nameof(PosWagenBeschriftungTop));
            }
        }
        #endregion

        #region PositionWagen
        public void PositionWagen(Punkt pos)
        {
            PosWagenLeft = pos.X;
            PosWagenTop = pos.Y;
        }

        private double _posWagenLeft;
        public double PosWagenLeft
        {
            get { return _posWagenLeft; }
            set
            {
                _posWagenLeft = value;
                OnPropertyChanged(nameof(PosWagenLeft));
            }
        }
        private double _posWagenTop;
        public double PosWagenTop
        {
            get { return _posWagenTop; }
            set
            {
                _posWagenTop = value;
                OnPropertyChanged(nameof(PosWagenTop));
            }
        }
        #endregion

        #region PositionWagenInhalt
        public void PositionWagenInhalt(Punkt pos, double fuellstand)
        {
            PosWagenInhaltLeft = pos.X + 12;
            PosWagenInhaltTop = pos.Y + 10 + 88 - fuellstand;
        }

        private double _posWagenInhaltLeft;
        public double PosWagenInhaltLeft
        {
            get { return _posWagenInhaltLeft; }
            set
            {
                _posWagenInhaltLeft = value;
                OnPropertyChanged(nameof(PosWagenInhaltLeft));
            }
        }
        private double _posWagenInhaltTop;
        public double PosWagenInhaltTop
        {
            get { return _posWagenInhaltTop; }
            set
            {
                _posWagenInhaltTop = value;
                OnPropertyChanged(nameof(PosWagenInhaltTop));
            }
        }
        #endregion

        #region WagenFuellstand
        private double _wagenFuellstand;
        public double WagenFuellstand
        {
            get { return _wagenFuellstand; }
            set
            {
                _wagenFuellstand = value;
                OnPropertyChanged(nameof(WagenFuellstand));
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
