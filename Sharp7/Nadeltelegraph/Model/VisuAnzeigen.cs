namespace Nadeltelegraph.Model
{
    using System.ComponentModel;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        int WinkelNadel = 35;
        int BreiteBreit = 10;
        int BreiteSchmal = 1;
        public VisuAnzeigen()
        {
            SpsStatus = "-";
            SpsColor = "LightBlue";

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
        }

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

        private int _AngleNeedle1;
        public int AngleNeedle1
        {
            get { return _AngleNeedle1; }
            set
            {
                _AngleNeedle1 = value;
                OnPropertyChanged("AngleNeedle1");
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

        private int _AngleNeedle2;
        public int AngleNeedle2
        {
            get { return _AngleNeedle2; }
            set
            {
                _AngleNeedle2 = value;
                OnPropertyChanged("AngleNeedle2");
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

        private int _AngleNeedle3;
        public int AngleNeedle3
        {
            get { return _AngleNeedle3; }
            set
            {
                _AngleNeedle3 = value;
                OnPropertyChanged("AngleNeedle3");
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

        private int _AngleNeedle4;
        public int AngleNeedle4
        {
            get { return _AngleNeedle4; }
            set
            {
                _AngleNeedle4 = value;
                OnPropertyChanged("AngleNeedle4");
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

        private int _AngleNeedle5;
        public int AngleNeedle5
        {
            get { return _AngleNeedle5; }
            set
            {
                _AngleNeedle5 = value;
                OnPropertyChanged("AngleNeedle5");
            }
        }
        #endregion

               


        #region Breite 1 UpRight
        public void Breite1UpRight(bool val)
        {
            if (val) Width1UpRight = BreiteBreit; else Width1UpRight = BreiteSchmal;
        }

        private int _Width1UpRight;
        public int Width1UpRight
        {
            get { return _Width1UpRight; }
            set
            {
                _Width1UpRight = value;
                OnPropertyChanged("Width1UpRight");
            }
        }
        #endregion

        #region Breite 2 UpRight
        public void Breite2UpRight(bool val)
        {
            if (val) Width2UpRight = BreiteBreit; else Width2UpRight = BreiteSchmal;
        }

        private int _Width2UpRight;
        public int Width2UpRight
        {
            get { return _Width2UpRight; }
            set
            {
                _Width2UpRight = value;
                OnPropertyChanged("Width2UpRight");
            }
        }
        #endregion

        #region Breite 3 UpRight
        public void Breite3UpRight(bool val)
        {
            if (val) Width3UpRight = BreiteBreit; else Width3UpRight = BreiteSchmal;
        }

        private int _Width3UpRight;
        public int Width3UpRight
        {
            get { return _Width3UpRight; }
            set
            {
                _Width3UpRight = value;
                OnPropertyChanged("Width3UpRight");
            }
        }
        #endregion

        #region Breite 4 UpRight
        public void Breite4UpRight(bool val)
        {
            if (val) Width4UpRight = BreiteBreit; else Width4UpRight = BreiteSchmal;
        }

        private int _Width4UpRight;
        public int Width4UpRight
        {
            get { return _Width4UpRight; }
            set
            {
                _Width4UpRight = value;
                OnPropertyChanged("Width4UpRight");
            }
        }
        #endregion


        #region Breite 2 UpLeft
        public void Breite2UpLeft(bool val)
        {
            if (val) Width2UpLeft = BreiteBreit; else Width2UpLeft = BreiteSchmal;
        }

        private int _Width2UpLeft;
        public int Width2UpLeft
        {
            get { return _Width2UpLeft; }
            set
            {
                _Width2UpLeft = value;
                OnPropertyChanged("Width2UpLeft");
            }
        }
        #endregion

        #region Breite 3 UpLeft
        public void Breite3UpLeft(bool val)
        {
            if (val) Width3UpLeft = BreiteBreit; else Width3UpLeft = BreiteSchmal;
        }

        private int _Width3UpLeft;
        public int Width3UpLeft
        {
            get { return _Width3UpLeft; }
            set
            {
                _Width3UpLeft = value;
                OnPropertyChanged("Width3UpLeft");
            }
        }
        #endregion

        #region Breite 4 UpLeft
        public void Breite4UpLeft(bool val)
        {
            if (val) Width4UpLeft = BreiteBreit; else Width4UpLeft = BreiteSchmal;
        }

        private int _Width4UpLeft;
        public int Width4UpLeft
        {
            get { return _Width4UpLeft; }
            set
            {
                _Width4UpLeft = value;
                OnPropertyChanged("Width4UpLeft");
            }
        }
        #endregion

        #region Breite 5 UpLeft
        public void Breite5UpLeft(bool val)
        {
            if (val) Width5UpLeft = BreiteBreit; else Width5UpLeft = BreiteSchmal;
        }

        private int _Width5UpLeft;
        public int Width5UpLeft
        {
            get { return _Width5UpLeft; }
            set
            {
                _Width5UpLeft = value;
                OnPropertyChanged("Width5UpLeft");
            }
        }
        #endregion



        #region Breite 1 DownRight
        public void Breite1DownRight(bool val)
        {
            if (val) Width1DownRight = BreiteBreit; else Width1DownRight = BreiteSchmal;
        }

        private int _Width1DownRight;
        public int Width1DownRight
        {
            get { return _Width1DownRight; }
            set
            {
                _Width1DownRight = value;
                OnPropertyChanged("Width1DownRight");
            }
        }
        #endregion

        #region Breite 2 DownRight
        public void Breite2DownRight(bool val)
        {
            if (val) Width2DownRight = BreiteBreit; else Width2DownRight = BreiteSchmal;
        }

        private int _Width2DownRight;
        public int Width2DownRight
        {
            get { return _Width2DownRight; }
            set
            {
                _Width2DownRight = value;
                OnPropertyChanged("Width2DownRight");
            }
        }
        #endregion

        #region Breite 3 DownRight
        public void Breite3DownRight(bool val)
        {
            if (val) Width3DownRight = BreiteBreit; else Width3DownRight = BreiteSchmal;
        }

        private int _Width3DownRight;
        public int Width3DownRight
        {
            get { return _Width3DownRight; }
            set
            {
                _Width3DownRight = value;
                OnPropertyChanged("Width3DownRight");
            }
        }
        #endregion

        #region Breite 4 DownRight
        public void Breite4DownRight(bool val)
        {
            if (val) Width4DownRight = BreiteBreit; else Width4DownRight = BreiteSchmal;
        }

        private int _Width4DownRight;
        public int Width4DownRight
        {
            get { return _Width4DownRight; }
            set
            {
                _Width4DownRight = value;
                OnPropertyChanged("Width4DownRight");
            }
        }
        #endregion



        #region Breite 2 DownLeft
        public void Breite2DownLeft(bool val)
        {
            if (val) Width2DownLeft = BreiteBreit; else Width2DownLeft = BreiteSchmal;
        }

        private int _Width2DownLeft;
        public int Width2DownLeft
        {
            get { return _Width2DownLeft; }
            set
            {
                _Width2DownLeft = value;
                OnPropertyChanged("Width2DownLeft");
            }
        }
        #endregion

        #region Breite 3 DownLeft
        public void Breite3DownLeft(bool val)
        {
            if (val) Width3DownLeft = BreiteBreit; else Width3DownLeft = BreiteSchmal;
        }

        private int _Width3DownLeft;
        public int Width3DownLeft
        {
            get { return _Width3DownLeft; }
            set
            {
                _Width3DownLeft = value;
                OnPropertyChanged("Width3DownLeft");
            }
        }
        #endregion

        #region Breite 4 DownLeft
        public void Breite4DownLeft(bool val)
        {
            if (val) Width4DownLeft = BreiteBreit; else Width4DownLeft = BreiteSchmal;
        }

        private int _Width4DownLeft;
        public int Width4DownLeft
        {
            get { return _Width4DownLeft; }
            set
            {
                _Width4DownLeft = value;
                OnPropertyChanged("Width4DownLeft");
            }
        }
        #endregion

        #region Breite 5 DownLeft
        public void Breite5DownLeft(bool val)
        {
            if (val) Width5DownLeft = BreiteBreit; else Width5DownLeft = BreiteSchmal;
        }

        private int _Width5DownLeft;
        public int Width5DownLeft
        {
            get { return _Width5DownLeft; }
            set
            {
                _Width5DownLeft = value;
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