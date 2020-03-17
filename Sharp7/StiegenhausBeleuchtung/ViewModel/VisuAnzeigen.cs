namespace StiegenhausBeleuchtung.ViewModel
{
    using System.ComponentModel;
    using System.Threading;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.StiegenhausBeleuchtung stiegenhausBeleuchtung;
        private readonly MainWindow mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.StiegenhausBeleuchtung stiegenhaus)
        {
            mainWindow = mw;
            stiegenhausBeleuchtung = stiegenhaus;


            ReiseStart = "sadf:-";
            ReiseZiel = "sadf:-";

            Color_P01 = "Yellow";
            Color_P02 = "Yellow";
            Color_P03 = "Yellow";
            Color_P11 = "Yellow";
            Color_P12 = "Yellow";
            Color_P13 = "Yellow";
            Color_P21 = "Yellow";
            Color_P22 = "Yellow";
            Color_P23 = "Yellow";
            Color_P31 = "Yellow";
            Color_P32 = "Yellow";
            Color_P33 = "Yellow";
            Color_P41 = "Yellow";
            Color_P42 = "Yellow";
            Color_P43 = "Yellow";


            ClickModeBtn_B01 = "Press";
            ClickModeBtn_B02 = "Press";
            ClickModeBtn_B03 = "Press";
            ClickModeBtn_B11 = "Press";
            ClickModeBtn_B12 = "Press";
            ClickModeBtn_B13 = "Press";
            ClickModeBtn_B21 = "Press";
            ClickModeBtn_B22 = "Press";
            ClickModeBtn_B23 = "Press";
            ClickModeBtn_B31 = "Press";
            ClickModeBtn_B32 = "Press";
            ClickModeBtn_B33 = "Press";
            ClickModeBtn_B41 = "Press";
            ClickModeBtn_B42 = "Press";
            ClickModeBtn_B43 = "Press";


            Visibility_B01_Aus = "Hidden";
            Visibility_B01_Ein = "Visible";
            Visibility_B02_Aus = "Hidden";
            Visibility_B02_Ein = "Visible";
            Visibility_B03_Aus = "Hidden";
            Visibility_B03_Ein = "Visible";
            Visibility_B11_Aus = "Hidden";
            Visibility_B11_Ein = "Visible";
            Visibility_B12_Aus = "Hidden";
            Visibility_B12_Ein = "Visible";
            Visibility_B13_Aus = "Hidden";
            Visibility_B13_Ein = "Visible";
            Visibility_B21_Aus = "Hidden";
            Visibility_B21_Ein = "Visible";
            Visibility_B22_Aus = "Hidden";
            Visibility_B22_Ein = "Visible";
            Visibility_B23_Aus = "Hidden";
            Visibility_B23_Ein = "Visible";
            Visibility_B31_Aus = "Hidden";
            Visibility_B31_Ein = "Visible";
            Visibility_B32_Aus = "Hidden";
            Visibility_B32_Ein = "Visible";
            Visibility_B33_Aus = "Hidden";
            Visibility_B33_Ein = "Visible";
            Visibility_B41_Aus = "Hidden";
            Visibility_B41_Ein = "Visible";
            Visibility_B42_Aus = "Hidden";
            Visibility_B42_Ein = "Visible";
            Visibility_B43_Aus = "Hidden";
            Visibility_B43_Ein = "Visible";
            

            SpsStatus = "-";
            SpsColor = "LightBlue";

            System.Threading.Tasks.Task.Run(() => VisuAnzeigenTask());
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                Farbe_P01(stiegenhausBeleuchtung.P_01);
                Farbe_P02(stiegenhausBeleuchtung.P_02);
                Farbe_P03(stiegenhausBeleuchtung.P_03);
                Farbe_P11(stiegenhausBeleuchtung.P_11);
                Farbe_P12(stiegenhausBeleuchtung.P_12);
                Farbe_P13(stiegenhausBeleuchtung.P_13);
                Farbe_P21(stiegenhausBeleuchtung.P_21);
                Farbe_P22(stiegenhausBeleuchtung.P_22);
                Farbe_P23(stiegenhausBeleuchtung.P_23);
                Farbe_P31(stiegenhausBeleuchtung.P_31);
                Farbe_P32(stiegenhausBeleuchtung.P_32);
                Farbe_P33(stiegenhausBeleuchtung.P_33);
                Farbe_P41(stiegenhausBeleuchtung.P_41);
                Farbe_P42(stiegenhausBeleuchtung.P_42);
                Farbe_P43(stiegenhausBeleuchtung.P_43);

                VisibilityBewegungsmelderB01(stiegenhausBeleuchtung.B_01);
                VisibilityBewegungsmelderB02(stiegenhausBeleuchtung.B_02);
                VisibilityBewegungsmelderB03(stiegenhausBeleuchtung.B_03);
                VisibilityBewegungsmelderB11(stiegenhausBeleuchtung.B_11);
                VisibilityBewegungsmelderB12(stiegenhausBeleuchtung.B_12);
                VisibilityBewegungsmelderB13(stiegenhausBeleuchtung.B_13);
                VisibilityBewegungsmelderB21(stiegenhausBeleuchtung.B_21);
                VisibilityBewegungsmelderB22(stiegenhausBeleuchtung.B_22);
                VisibilityBewegungsmelderB23(stiegenhausBeleuchtung.B_23);
                VisibilityBewegungsmelderB31(stiegenhausBeleuchtung.B_31);
                VisibilityBewegungsmelderB32(stiegenhausBeleuchtung.B_32);
                VisibilityBewegungsmelderB33(stiegenhausBeleuchtung.B_33);
                VisibilityBewegungsmelderB41(stiegenhausBeleuchtung.B_41);
                VisibilityBewegungsmelderB42(stiegenhausBeleuchtung.B_42);
                VisibilityBewegungsmelderB43(stiegenhausBeleuchtung.B_43);


                if (mainWindow.S7_1200 != null)
                {
                    if (mainWindow.S7_1200.GetSpsError()) SpsColor = "Red"; else SpsColor = "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
        }


        internal void Btn_B01() { stiegenhausBeleuchtung.B_01 = ClickModeButton_B01(); }
        internal void Btn_B02() { stiegenhausBeleuchtung.B_02 = ClickModeButton_B02(); }
        internal void Btn_B03() { stiegenhausBeleuchtung.B_03 = ClickModeButton_B03(); }
        internal void Btn_B11() { stiegenhausBeleuchtung.B_11 = ClickModeButton_B11(); }
        internal void Btn_B12() { stiegenhausBeleuchtung.B_12 = ClickModeButton_B12(); }
        internal void Btn_B13() { stiegenhausBeleuchtung.B_13 = ClickModeButton_B13(); }
        internal void Btn_B21() { stiegenhausBeleuchtung.B_21 = ClickModeButton_B21(); }
        internal void Btn_B22() { stiegenhausBeleuchtung.B_22 = ClickModeButton_B22(); }
        internal void Btn_B23() { stiegenhausBeleuchtung.B_23 = ClickModeButton_B23(); }
        internal void Btn_B31() { stiegenhausBeleuchtung.B_31 = ClickModeButton_B31(); }
        internal void Btn_B32() { stiegenhausBeleuchtung.B_32 = ClickModeButton_B32(); }
        internal void Btn_B33() { stiegenhausBeleuchtung.B_33 = ClickModeButton_B33(); }
        internal void Btn_B41() { stiegenhausBeleuchtung.B_41 = ClickModeButton_B41(); }
        internal void Btn_B42() { stiegenhausBeleuchtung.B_42 = ClickModeButton_B42(); }
        internal void Btn_B43() { stiegenhausBeleuchtung.B_43 = ClickModeButton_B43(); }


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



        #region ReiseStart
        private string _reiseStart;
        public string ReiseStart
        {
            get { return _reiseStart; }
            set
            {
                var v = value.Split(':');
                _reiseStart = v[1].Trim();
                OnPropertyChanged(nameof(ReiseStart));
            }
        }
        #endregion

        #region ReiseZiel
        private string _reiseZiel;
        public string ReiseZiel
        {
            get { return _reiseZiel; }
            set
            {
                var v = value.Split(':');
                _reiseZiel = v[1].Trim();
                OnPropertyChanged(nameof(ReiseZiel));
            }
        }
        #endregion



        #region Color P01
        public void Farbe_P01(bool val)
        {
            if (val) Color_P01 = "Yellow"; else Color_P01 = "White";
        }

        private string _color_P01;
        public string Color_P01
        {
            get { return _color_P01; }
            set
            {
                _color_P01 = value;
                OnPropertyChanged(nameof(Color_P01));
            }
        }
        #endregion

        #region Color P02
        public void Farbe_P02(bool val)
        {
            if (val) Color_P02 = "Yellow"; else Color_P02 = "White";
        }

        private string _color_P02;
        public string Color_P02
        {
            get { return _color_P02; }
            set
            {
                _color_P02 = value;
                OnPropertyChanged(nameof(Color_P02));
            }
        }
        #endregion

        #region Color P03
        public void Farbe_P03(bool val)
        {
            if (val) Color_P03 = "Yellow"; else Color_P03 = "White";
        }

        private string _color_P03;
        public string Color_P03
        {
            get { return _color_P03; }
            set
            {
                _color_P03 = value;
                OnPropertyChanged(nameof(Color_P03));
            }
        }
        #endregion

        #region Color P11
        public void Farbe_P11(bool val)
        {
            if (val) Color_P11 = "Yellow"; else Color_P11 = "White";
        }

        private string _color_P11;
        public string Color_P11
        {
            get { return _color_P11; }
            set
            {
                _color_P11 = value;
                OnPropertyChanged(nameof(Color_P11));
            }
        }
        #endregion

        #region Color P12
        public void Farbe_P12(bool val)
        {
            if (val) Color_P12 = "Yellow"; else Color_P12 = "White";
        }

        private string _color_P12;
        public string Color_P12
        {
            get { return _color_P12; }
            set
            {
                _color_P12 = value;
                OnPropertyChanged(nameof(Color_P12));
            }
        }
        #endregion

        #region Color P13
        public void Farbe_P13(bool val)
        {
            if (val) Color_P13 = "Yellow"; else Color_P13 = "White";
        }

        private string _color_P13;
        public string Color_P13
        {
            get { return _color_P13; }
            set
            {
                _color_P13 = value;
                OnPropertyChanged(nameof(Color_P13));
            }
        }
        #endregion

        #region Color P21
        public void Farbe_P21(bool val)
        {
            if (val) Color_P21 = "Yellow"; else Color_P21 = "White";
        }

        private string _color_P21;
        public string Color_P21
        {
            get { return _color_P21; }
            set
            {
                _color_P21 = value;
                OnPropertyChanged(nameof(Color_P21));
            }
        }
        #endregion

        #region Color P22
        public void Farbe_P22(bool val)
        {
            if (val) Color_P22 = "Yellow"; else Color_P22 = "White";
        }

        private string _color_P22;
        public string Color_P22
        {
            get { return _color_P22; }
            set
            {
                _color_P22 = value;
                OnPropertyChanged(nameof(Color_P22));
            }
        }
        #endregion

        #region Color P23
        public void Farbe_P23(bool val)
        {
            if (val) Color_P23 = "Yellow"; else Color_P23 = "White";
        }

        private string _color_P23;
        public string Color_P23
        {
            get { return _color_P23; }
            set
            {
                _color_P23 = value;
                OnPropertyChanged(nameof(Color_P23));
            }
        }
        #endregion

        #region Color P31
        public void Farbe_P31(bool val)
        {
            if (val) Color_P31 = "Yellow"; else Color_P31 = "White";
        }

        private string _color_P31;
        public string Color_P31
        {
            get { return _color_P31; }
            set
            {
                _color_P31 = value;
                OnPropertyChanged(nameof(Color_P31));
            }
        }
        #endregion

        #region Color P32
        public void Farbe_P32(bool val)
        {
            if (val) Color_P32 = "Yellow"; else Color_P32 = "White";
        }

        private string _color_P32;
        public string Color_P32
        {
            get { return _color_P32; }
            set
            {
                _color_P32 = value;
                OnPropertyChanged(nameof(Color_P32));
            }
        }
        #endregion

        #region Color P33
        public void Farbe_P33(bool val)
        {
            if (val) Color_P33 = "Yellow"; else Color_P33 = "White";
        }

        private string _color_P33;
        public string Color_P33
        {
            get { return _color_P33; }
            set
            {
                _color_P33 = value;
                OnPropertyChanged(nameof(Color_P33));
            }
        }
        #endregion

        #region Color P41
        public void Farbe_P41(bool val)
        {
            if (val) Color_P41 = "Yellow"; else Color_P41 = "White";
        }

        private string _color_P41;
        public string Color_P41
        {
            get { return _color_P41; }
            set
            {
                _color_P41 = value;
                OnPropertyChanged(nameof(Color_P41));
            }
        }
        #endregion

        #region Color P42
        public void Farbe_P42(bool val)
        {
            if (val) Color_P42 = "Yellow"; else Color_P42 = "White";
        }

        private string _color_P42;
        public string Color_P42
        {
            get { return _color_P42; }
            set
            {
                _color_P42 = value;
                OnPropertyChanged(nameof(Color_P42));
            }
        }
        #endregion

        #region Color P43
        public void Farbe_P43(bool val)
        {
            if (val) Color_P43 = "Yellow"; else Color_P43 = "White";
        }

        private string _color_P43;
        public string Color_P43
        {
            get { return _color_P43; }
            set
            {
                _color_P43 = value;
                OnPropertyChanged(nameof(Color_P43));
            }
        }
        #endregion


        #region ClickModeBtn_B01
        public bool ClickModeButton_B01()
        {
            if (ClickModeBtn_B01 == "Press")
            {
                ClickModeBtn_B01 = "Release";
                return true;
            }
            else
            {
                ClickModeBtn_B01 = "Press";
            }
            return false;
        }

        private string _clickModeBtn_B01;
        public string ClickModeBtn_B01
        {
            get { return _clickModeBtn_B01; }
            set
            {
                _clickModeBtn_B01 = value;
                OnPropertyChanged(nameof(ClickModeBtn_B01));
            }
        }
        #endregion

        #region ClickModeBtn_B02
        public bool ClickModeButton_B02()
        {
            if (ClickModeBtn_B02 == "Press")
            {
                ClickModeBtn_B02 = "Release";
                return true;
            }
            else
            {
                ClickModeBtn_B02 = "Press";
            }
            return false;
        }

        private string _clickModeBtn_B02;
        public string ClickModeBtn_B02
        {
            get { return _clickModeBtn_B02; }
            set
            {
                _clickModeBtn_B02 = value;
                OnPropertyChanged(nameof(ClickModeBtn_B02));
            }
        }
        #endregion

        #region ClickModeBtn_B03
        public bool ClickModeButton_B03()
        {
            if (ClickModeBtn_B03 == "Press")
            {
                ClickModeBtn_B03 = "Release";
                return true;
            }
            else
            {
                ClickModeBtn_B03 = "Press";
            }
            return false;
        }

        private string _clickModeBtn_B03;
        public string ClickModeBtn_B03
        {
            get { return _clickModeBtn_B03; }
            set
            {
                _clickModeBtn_B03 = value;
                OnPropertyChanged(nameof(ClickModeBtn_B03));
            }
        }
        #endregion

        #region ClickModeBtn_B11
        public bool ClickModeButton_B11()
        {
            if (ClickModeBtn_B11 == "Press")
            {
                ClickModeBtn_B11 = "Release";
                return true;
            }
            else
            {
                ClickModeBtn_B11 = "Press";
            }
            return false;
        }

        private string _clickModeBtn_B11;
        public string ClickModeBtn_B11
        {
            get { return _clickModeBtn_B11; }
            set
            {
                _clickModeBtn_B11 = value;
                OnPropertyChanged(nameof(ClickModeBtn_B11));
            }
        }
        #endregion

        #region ClickModeBtn_B12
        public bool ClickModeButton_B12()
        {
            if (ClickModeBtn_B12 == "Press")
            {
                ClickModeBtn_B12 = "Release";
                return true;
            }
            else
            {
                ClickModeBtn_B12 = "Press";
            }
            return false;
        }

        private string _clickModeBtn_B12;
        public string ClickModeBtn_B12
        {
            get { return _clickModeBtn_B12; }
            set
            {
                _clickModeBtn_B12 = value;
                OnPropertyChanged(nameof(ClickModeBtn_B12));
            }
        }
        #endregion

        #region ClickModeBtn_B13
        public bool ClickModeButton_B13()
        {
            if (ClickModeBtn_B13 == "Press")
            {
                ClickModeBtn_B13 = "Release";
                return true;
            }
            else
            {
                ClickModeBtn_B13 = "Press";
            }
            return false;
        }

        private string _clickModeBtn_B13;
        public string ClickModeBtn_B13
        {
            get { return _clickModeBtn_B13; }
            set
            {
                _clickModeBtn_B13 = value;
                OnPropertyChanged(nameof(ClickModeBtn_B13));
            }
        }
        #endregion

        #region ClickModeBtn_B21
        public bool ClickModeButton_B21()
        {
            if (ClickModeBtn_B21 == "Press")
            {
                ClickModeBtn_B21 = "Release";
                return true;
            }
            else
            {
                ClickModeBtn_B21 = "Press";
            }
            return false;
        }

        private string _clickModeBtn_B21;
        public string ClickModeBtn_B21
        {
            get { return _clickModeBtn_B21; }
            set
            {
                _clickModeBtn_B21 = value;
                OnPropertyChanged(nameof(ClickModeBtn_B21));
            }
        }
        #endregion

        #region ClickModeBtn_B22
        public bool ClickModeButton_B22()
        {
            if (ClickModeBtn_B22 == "Press")
            {
                ClickModeBtn_B22 = "Release";
                return true;
            }
            else
            {
                ClickModeBtn_B22 = "Press";
            }
            return false;
        }

        private string _clickModeBtn_B22;
        public string ClickModeBtn_B22
        {
            get { return _clickModeBtn_B22; }
            set
            {
                _clickModeBtn_B22 = value;
                OnPropertyChanged(nameof(ClickModeBtn_B22));
            }
        }
        #endregion

        #region ClickModeBtn_B23
        public bool ClickModeButton_B23()
        {
            if (ClickModeBtn_B23 == "Press")
            {
                ClickModeBtn_B23 = "Release";
                return true;
            }
            else
            {
                ClickModeBtn_B23 = "Press";
            }
            return false;
        }

        private string _clickModeBtn_B23;
        public string ClickModeBtn_B23
        {
            get { return _clickModeBtn_B23; }
            set
            {
                _clickModeBtn_B23 = value;
                OnPropertyChanged(nameof(ClickModeBtn_B23));
            }
        }
        #endregion

        #region ClickModeBtn_B31
        public bool ClickModeButton_B31()
        {
            if (ClickModeBtn_B31 == "Press")
            {
                ClickModeBtn_B31 = "Release";
                return true;
            }
            else
            {
                ClickModeBtn_B31 = "Press";
            }
            return false;
        }

        private string _clickModeBtn_B31;
        public string ClickModeBtn_B31
        {
            get { return _clickModeBtn_B31; }
            set
            {
                _clickModeBtn_B31 = value;
                OnPropertyChanged(nameof(ClickModeBtn_B31));
            }
        }
        #endregion

        #region ClickModeBtn_B32
        public bool ClickModeButton_B32()
        {
            if (ClickModeBtn_B32 == "Press")
            {
                ClickModeBtn_B32 = "Release";
                return true;
            }
            else
            {
                ClickModeBtn_B32 = "Press";
            }
            return false;
        }

        private string _clickModeBtn_B32;
        public string ClickModeBtn_B32
        {
            get { return _clickModeBtn_B32; }
            set
            {
                _clickModeBtn_B32 = value;
                OnPropertyChanged(nameof(ClickModeBtn_B32));
            }
        }
        #endregion

        #region ClickModeBtn_B33
        public bool ClickModeButton_B33()
        {
            if (ClickModeBtn_B33 == "Press")
            {
                ClickModeBtn_B33 = "Release";
                return true;
            }
            else
            {
                ClickModeBtn_B33 = "Press";
            }
            return false;
        }

        private string _clickModeBtn_B33;
        public string ClickModeBtn_B33
        {
            get { return _clickModeBtn_B33; }
            set
            {
                _clickModeBtn_B33 = value;
                OnPropertyChanged(nameof(ClickModeBtn_B33));
            }
        }
        #endregion

        #region ClickModeBtn_B41
        public bool ClickModeButton_B41()
        {
            if (ClickModeBtn_B41 == "Press")
            {
                ClickModeBtn_B41 = "Release";
                return true;
            }
            else
            {
                ClickModeBtn_B41 = "Press";
            }
            return false;
        }

        private string _clickModeBtn_B41;
        public string ClickModeBtn_B41
        {
            get { return _clickModeBtn_B41; }
            set
            {
                _clickModeBtn_B41 = value;
                OnPropertyChanged(nameof(ClickModeBtn_B41));
            }
        }
        #endregion

        #region ClickModeBtn_B42
        public bool ClickModeButton_B42()
        {
            if (ClickModeBtn_B42 == "Press")
            {
                ClickModeBtn_B42 = "Release";
                return true;
            }
            else
            {
                ClickModeBtn_B42 = "Press";
            }
            return false;
        }

        private string _clickModeBtn_B42;
        public string ClickModeBtn_B42
        {
            get { return _clickModeBtn_B42; }
            set
            {
                _clickModeBtn_B42 = value;
                OnPropertyChanged(nameof(ClickModeBtn_B42));
            }
        }
        #endregion

        #region ClickModeBtn_B43
        public bool ClickModeButton_B43()
        {
            if (ClickModeBtn_B43 == "Press")
            {
                ClickModeBtn_B43 = "Release";
                return true;
            }
            else
            {
                ClickModeBtn_B43 = "Press";
            }
            return false;
        }

        private string _clickModeBtn_B43;
        public string ClickModeBtn_B43
        {
            get { return _clickModeBtn_B43; }
            set
            {
                _clickModeBtn_B43 = value;
                OnPropertyChanged(nameof(ClickModeBtn_B43));
            }
        }
        #endregion
                          


        #region Visibility Bewegungsmelder B01
        public void VisibilityBewegungsmelderB01(bool val)
        {
            if (val)
            {
                Visibility_B01_Ein = "Visible";
                Visibility_B01_Aus = "Hidden";
            }
            else
            {
                Visibility_B01_Ein = "Hidden";
                Visibility_B01_Aus = "Visible";
            }
        }

        private string _visibility_B01_Ein;
        public string Visibility_B01_Ein
        {
            get { return _visibility_B01_Ein; }
            set
            {
                _visibility_B01_Ein = value;
                OnPropertyChanged(nameof(Visibility_B01_Ein));
            }
        }

        private string _visibility_B01_Aus;
        public string Visibility_B01_Aus
        {
            get { return _visibility_B01_Aus; }
            set
            {
                _visibility_B01_Aus = value;
                OnPropertyChanged(nameof(Visibility_B01_Aus));
            }
        }
        #endregion

        #region Visibility Bewegungsmelder B02
        public void VisibilityBewegungsmelderB02(bool val)
        {
            if (val)
            {
                Visibility_B02_Ein = "Visible";
                Visibility_B02_Aus = "Hidden";
            }
            else
            {
                Visibility_B02_Ein = "Hidden";
                Visibility_B02_Aus = "Visible";
            }
        }

        private string _visibility_B02_Ein;
        public string Visibility_B02_Ein
        {
            get { return _visibility_B02_Ein; }
            set
            {
                _visibility_B02_Ein = value;
                OnPropertyChanged(nameof(Visibility_B02_Ein));
            }
        }

        private string _visibility_B02_Aus;
        public string Visibility_B02_Aus
        {
            get { return _visibility_B02_Aus; }
            set
            {
                _visibility_B02_Aus = value;
                OnPropertyChanged(nameof(Visibility_B02_Aus));
            }
        }
        #endregion

        #region Visibility Bewegungsmelder B03
        public void VisibilityBewegungsmelderB03(bool val)
        {
            if (val)
            {
                Visibility_B03_Ein = "Visible";
                Visibility_B03_Aus = "Hidden";
            }
            else
            {
                Visibility_B03_Ein = "Hidden";
                Visibility_B03_Aus = "Visible";
            }
        }

        private string _visibility_B03_Ein;
        public string Visibility_B03_Ein
        {
            get { return _visibility_B03_Ein; }
            set
            {
                _visibility_B03_Ein = value;
                OnPropertyChanged(nameof(Visibility_B03_Ein));
            }
        }

        private string _visibility_B03_Aus;
        public string Visibility_B03_Aus
        {
            get { return _visibility_B03_Aus; }
            set
            {
                _visibility_B03_Aus = value;
                OnPropertyChanged(nameof(Visibility_B03_Aus));
            }
        }
        #endregion

        #region Visibility Bewegungsmelder B11
        public void VisibilityBewegungsmelderB11(bool val)
        {
            if (val)
            {
                Visibility_B11_Ein = "Visible";
                Visibility_B11_Aus = "Hidden";
            }
            else
            {
                Visibility_B11_Ein = "Hidden";
                Visibility_B11_Aus = "Visible";
            }
        }

        private string _visibility_B11_Ein;
        public string Visibility_B11_Ein
        {
            get { return _visibility_B11_Ein; }
            set
            {
                _visibility_B11_Ein = value;
                OnPropertyChanged(nameof(Visibility_B11_Ein));
            }
        }

        private string _visibility_B11_Aus;
        public string Visibility_B11_Aus
        {
            get { return _visibility_B11_Aus; }
            set
            {
                _visibility_B11_Aus = value;
                OnPropertyChanged(nameof(Visibility_B11_Aus));
            }
        }
        #endregion

        #region Visibility Bewegungsmelder B12
        public void VisibilityBewegungsmelderB12(bool val)
        {
            if (val)
            {
                Visibility_B12_Ein = "Visible";
                Visibility_B12_Aus = "Hidden";
            }
            else
            {
                Visibility_B12_Ein = "Hidden";
                Visibility_B12_Aus = "Visible";
            }
        }

        private string _visibility_B12_Ein;
        public string Visibility_B12_Ein
        {
            get { return _visibility_B12_Ein; }
            set
            {
                _visibility_B12_Ein = value;
                OnPropertyChanged(nameof(Visibility_B12_Ein));
            }
        }

        private string _visibility_B12_Aus;
        public string Visibility_B12_Aus
        {
            get { return _visibility_B12_Aus; }
            set
            {
                _visibility_B12_Aus = value;
                OnPropertyChanged(nameof(Visibility_B12_Aus));
            }
        }
        #endregion

        #region Visibility Bewegungsmelder B13
        public void VisibilityBewegungsmelderB13(bool val)
        {
            if (val)
            {
                Visibility_B13_Ein = "Visible";
                Visibility_B13_Aus = "Hidden";
            }
            else
            {
                Visibility_B13_Ein = "Hidden";
                Visibility_B13_Aus = "Visible";
            }
        }

        private string _visibility_B13_Ein;
        public string Visibility_B13_Ein
        {
            get { return _visibility_B13_Ein; }
            set
            {
                _visibility_B13_Ein = value;
                OnPropertyChanged(nameof(Visibility_B13_Ein));
            }
        }

        private string _visibility_B13_Aus;
        public string Visibility_B13_Aus
        {
            get { return _visibility_B13_Aus; }
            set
            {
                _visibility_B13_Aus = value;
                OnPropertyChanged(nameof(Visibility_B13_Aus));
            }
        }
        #endregion

        #region Visibility Bewegungsmelder B21
        public void VisibilityBewegungsmelderB21(bool val)
        {
            if (val)
            {
                Visibility_B21_Ein = "Visible";
                Visibility_B21_Aus = "Hidden";
            }
            else
            {
                Visibility_B21_Ein = "Hidden";
                Visibility_B21_Aus = "Visible";
            }
        }

        private string _visibility_B21_Ein;
        public string Visibility_B21_Ein
        {
            get { return _visibility_B21_Ein; }
            set
            {
                _visibility_B21_Ein = value;
                OnPropertyChanged(nameof(Visibility_B21_Ein));
            }
        }

        private string _visibility_B21_Aus;
        public string Visibility_B21_Aus
        {
            get { return _visibility_B21_Aus; }
            set
            {
                _visibility_B21_Aus = value;
                OnPropertyChanged(nameof(Visibility_B21_Aus));
            }
        }
        #endregion

        #region Visibility Bewegungsmelder B22
        public void VisibilityBewegungsmelderB22(bool val)
        {
            if (val)
            {
                Visibility_B22_Ein = "Visible";
                Visibility_B22_Aus = "Hidden";
            }
            else
            {
                Visibility_B22_Ein = "Hidden";
                Visibility_B22_Aus = "Visible";
            }
        }

        private string _visibility_B22_Ein;
        public string Visibility_B22_Ein
        {
            get { return _visibility_B22_Ein; }
            set
            {
                _visibility_B22_Ein = value;
                OnPropertyChanged(nameof(Visibility_B22_Ein));
            }
        }

        private string _visibility_B22_Aus;
        public string Visibility_B22_Aus
        {
            get { return _visibility_B22_Aus; }
            set
            {
                _visibility_B22_Aus = value;
                OnPropertyChanged(nameof(Visibility_B22_Aus));
            }
        }
        #endregion

        #region Visibility Bewegungsmelder B23
        public void VisibilityBewegungsmelderB23(bool val)
        {
            if (val)
            {
                Visibility_B23_Ein = "Visible";
                Visibility_B23_Aus = "Hidden";
            }
            else
            {
                Visibility_B23_Ein = "Hidden";
                Visibility_B23_Aus = "Visible";
            }
        }

        private string _visibility_B23_Ein;
        public string Visibility_B23_Ein
        {
            get { return _visibility_B23_Ein; }
            set
            {
                _visibility_B23_Ein = value;
                OnPropertyChanged(nameof(Visibility_B23_Ein));
            }
        }

        private string _visibility_B23_Aus;
        public string Visibility_B23_Aus
        {
            get { return _visibility_B23_Aus; }
            set
            {
                _visibility_B23_Aus = value;
                OnPropertyChanged(nameof(Visibility_B23_Aus));
            }
        }
        #endregion

        #region Visibility Bewegungsmelder B31
        public void VisibilityBewegungsmelderB31(bool val)
        {
            if (val)
            {
                Visibility_B31_Ein = "Visible";
                Visibility_B31_Aus = "Hidden";
            }
            else
            {
                Visibility_B31_Ein = "Hidden";
                Visibility_B31_Aus = "Visible";
            }
        }

        private string _visibility_B31_Ein;
        public string Visibility_B31_Ein
        {
            get { return _visibility_B31_Ein; }
            set
            {
                _visibility_B31_Ein = value;
                OnPropertyChanged(nameof(Visibility_B31_Ein));
            }
        }

        private string _visibility_B31_Aus;
        public string Visibility_B31_Aus
        {
            get { return _visibility_B31_Aus; }
            set
            {
                _visibility_B31_Aus = value;
                OnPropertyChanged(nameof(Visibility_B31_Aus));
            }
        }
        #endregion

        #region Visibility Bewegungsmelder B32
        public void VisibilityBewegungsmelderB32(bool val)
        {
            if (val)
            {
                Visibility_B32_Ein = "Visible";
                Visibility_B32_Aus = "Hidden";
            }
            else
            {
                Visibility_B32_Ein = "Hidden";
                Visibility_B32_Aus = "Visible";
            }
        }

        private string _visibility_B32_Ein;
        public string Visibility_B32_Ein
        {
            get { return _visibility_B32_Ein; }
            set
            {
                _visibility_B32_Ein = value;
                OnPropertyChanged(nameof(Visibility_B32_Ein));
            }
        }

        private string _visibility_B32_Aus;
        public string Visibility_B32_Aus
        {
            get { return _visibility_B32_Aus; }
            set
            {
                _visibility_B32_Aus = value;
                OnPropertyChanged(nameof(Visibility_B32_Aus));
            }
        }
        #endregion

        #region Visibility Bewegungsmelder B33
        public void VisibilityBewegungsmelderB33(bool val)
        {
            if (val)
            {
                Visibility_B33_Ein = "Visible";
                Visibility_B33_Aus = "Hidden";
            }
            else
            {
                Visibility_B33_Ein = "Hidden";
                Visibility_B33_Aus = "Visible";
            }
        }

        private string _visibility_B33_Ein;
        public string Visibility_B33_Ein
        {
            get { return _visibility_B33_Ein; }
            set
            {
                _visibility_B33_Ein = value;
                OnPropertyChanged(nameof(Visibility_B33_Ein));
            }
        }

        private string _visibility_B33_Aus;
        public string Visibility_B33_Aus
        {
            get { return _visibility_B33_Aus; }
            set
            {
                _visibility_B33_Aus = value;
                OnPropertyChanged(nameof(Visibility_B33_Aus));
            }
        }
        #endregion

        #region Visibility Bewegungsmelder B41
        public void VisibilityBewegungsmelderB41(bool val)
        {
            if (val)
            {
                Visibility_B41_Ein = "Visible";
                Visibility_B41_Aus = "Hidden";
            }
            else
            {
                Visibility_B41_Ein = "Hidden";
                Visibility_B41_Aus = "Visible";
            }
        }

        private string _visibility_B41_Ein;
        public string Visibility_B41_Ein
        {
            get { return _visibility_B41_Ein; }
            set
            {
                _visibility_B41_Ein = value;
                OnPropertyChanged(nameof(Visibility_B41_Ein));
            }
        }

        private string _visibility_B41_Aus;
        public string Visibility_B41_Aus
        {
            get { return _visibility_B41_Aus; }
            set
            {
                _visibility_B41_Aus = value;
                OnPropertyChanged(nameof(Visibility_B41_Aus));
            }
        }
        #endregion

        #region Visibility Bewegungsmelder B42
        public void VisibilityBewegungsmelderB42(bool val)
        {
            if (val)
            {
                Visibility_B42_Ein = "Visible";
                Visibility_B42_Aus = "Hidden";
            }
            else
            {
                Visibility_B42_Ein = "Hidden";
                Visibility_B42_Aus = "Visible";
            }
        }

        private string _visibility_B42_Ein;
        public string Visibility_B42_Ein
        {
            get { return _visibility_B42_Ein; }
            set
            {
                _visibility_B42_Ein = value;
                OnPropertyChanged(nameof(Visibility_B42_Ein));
            }
        }

        private string _visibility_B42_Aus;
        public string Visibility_B42_Aus
        {
            get { return _visibility_B42_Aus; }
            set
            {
                _visibility_B42_Aus = value;
                OnPropertyChanged(nameof(Visibility_B42_Aus));
            }
        }
        #endregion

        #region Visibility Bewegungsmelder B43
        public void VisibilityBewegungsmelderB43(bool val)
        {
            if (val)
            {
                Visibility_B43_Ein = "Visible";
                Visibility_B43_Aus = "Hidden";
            }
            else
            {
                Visibility_B43_Ein = "Hidden";
                Visibility_B43_Aus = "Visible";
            }
        }

        private string _visibility_B43_Ein;
        public string Visibility_B43_Ein
        {
            get { return _visibility_B43_Ein; }
            set
            {
                _visibility_B43_Ein = value;
                OnPropertyChanged(nameof(Visibility_B43_Ein));
            }
        }

        private string _visibility_B43_Aus;
        public string Visibility_B43_Aus
        {
            get { return _visibility_B43_Aus; }
            set
            {
                _visibility_B43_Aus = value;
                OnPropertyChanged(nameof(Visibility_B43_Aus));
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