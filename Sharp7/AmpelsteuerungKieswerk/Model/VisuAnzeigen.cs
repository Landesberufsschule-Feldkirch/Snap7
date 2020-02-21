namespace AmpelsteuerungKieswerk.Model
{
    using System.ComponentModel;
    using Utilities;
    using static AmpelsteuerungKieswerk.Model.LKW;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        public VisuAnzeigen()
        {
            SpsStatus = "-";
            SpsColor = "LightBlue";

            ColorB1 = "LightGray";
            ColorB2 = "LightGray";
            ColorB3 = "LightGray";
            ColorB4 = "LightGray";

            ColorLinksRot = "White";
            ColorLinksGelb = "White";
            ColorLinksGruen = "White";
            ColorRechtsRot = "White";
            ColorRechtsGelb = "White";
            ColorRechtsGruen = "White";

            PosLkw1Left = 10;
            PosLkw1Top = 10;
            PosLkw2Left = 10;
            PosLkw2Top = 50;
            PosLkw3Left = 10;
            PosLkw3Top = 80;
            PosLkw4Left = 10;
            PosLkw4Top = 100;
            PosLkw5Left = 10;
            PosLkw5Top = 130;

            DirectionLkw1 = 1;
            DirectionLkw2 = 1;
            DirectionLkw3 = 1;
            DirectionLkw4 = 1;
            DirectionLkw5 = 1;
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



        #region Color B1
        public void FarbeB1(bool val)
        {
            if (val) ColorB1 = "Red"; else ColorB1 = "LightGray";
        }

        private string _colorB1;
        public string ColorB1
        {
            get { return _colorB1; }
            set
            {
                _colorB1 = value;
                OnPropertyChanged("ColorB1");
            }
        }
        #endregion

        #region Color B2
        public void FarbeB2(bool val)
        {
            if (val) ColorB2 = "Red"; else ColorB2 = "LightGray";
        }

        private string _colorB2;
        public string ColorB2
        {
            get { return _colorB2; }
            set
            {
                _colorB2 = value;
                OnPropertyChanged("ColorB2");
            }
        }
        #endregion

        #region Color B3
        public void FarbeB3(bool val)
        {
            if (val) ColorB3 = "Red"; else ColorB3 = "LightGray";
        }

        private string _colorB3;
        public string ColorB3
        {
            get { return _colorB3; }
            set
            {
                _colorB3 = value;
                OnPropertyChanged("ColorB3");
            }
        }
        #endregion

        #region Color B4
        public void FarbeB4(bool val)
        {
            if (val) ColorB4 = "Red"; else ColorB4 = "LightGray";
        }

        private string _colorB4;
        public string ColorB4
        {
            get { return _colorB4; }
            set
            {
                _colorB4 = value;
                OnPropertyChanged("ColorB4");
            }
        }
        #endregion



        #region ColorLinksRot
        public void FarbeLinksRot(bool val)
        {
            if (val) ColorLinksRot = "Red"; else ColorLinksRot = "White";
        }

        private string _colorLinksRot;
        public string ColorLinksRot
        {
            get { return _colorLinksRot; }
            set
            {
                _colorLinksRot = value;
                OnPropertyChanged("ColorLinksRot");
            }
        }
        #endregion

        #region ColorLinksGelb
        public void FarbeLinksGelb(bool val)
        {
            if (val) ColorLinksGelb = "Yellow"; else ColorLinksGelb = "White";
        }

        private string _colorLinksGelb;
        public string ColorLinksGelb
        {
            get { return _colorLinksGelb; }
            set
            {
                _colorLinksGelb = value;
                OnPropertyChanged("ColorLinksGelb");
            }
        }
        #endregion

        #region ColorLinksGruen
        public void FarbeLinksGruen(bool val)
        {
            if (val) ColorLinksGruen = "Green"; else ColorLinksGruen = "White";
        }

        private string _colorLinksGruen;
        public string ColorLinksGruen
        {
            get { return _colorLinksGruen; }
            set
            {
                _colorLinksGruen = value;
                OnPropertyChanged("ColorLinksGruen");
            }
        }
        #endregion

        #region ColorRechtsRot
        public void FarbeRechtsRot(bool val)
        {
            if (val) ColorRechtsRot = "Red"; else ColorRechtsRot = "White";
        }

        private string _colorRechtsRot;
        public string ColorRechtsRot
        {
            get { return _colorRechtsRot; }
            set
            {
                _colorRechtsRot = value;
                OnPropertyChanged("ColorRechtsRot");
            }
        }
        #endregion

        #region ColorRechtsGelb
        public void FarbeRechtsGelb(bool val)
        {
            if (val) ColorRechtsGelb = "Yellow"; else ColorRechtsGelb = "White";
        }

        private string _colorRechtsGelb;
        public string ColorRechtsGelb
        {
            get { return _colorRechtsGelb; }
            set
            {
                _colorRechtsGelb = value;
                OnPropertyChanged("ColorRechtsGelb");
            }
        }
        #endregion

        #region ColorRechtsGruen
        public void FarbeRechtsGruen(bool val)
        {
            if (val) ColorRechtsGruen = "Green"; else ColorRechtsGruen = "White";
        }

        private string _colorRechtsGruen;
        public string ColorRechtsGruen
        {
            get { return _colorRechtsGruen; }
            set
            {
                _colorRechtsGruen = value;
                OnPropertyChanged("ColorRechtsGruen");
            }
        }
        #endregion



        #region RichtungLkw1
        public void RichtungLkw1(LkwRichtungen val)
        {
            if (val == LkwRichtungen.NachRechts) DirectionLkw1 = 1; else DirectionLkw1 = -1;
        }

        private int _directionLkw1;
        public int DirectionLkw1
        {
            get { return _directionLkw1; }
            set
            {
                _directionLkw1 = value;
                OnPropertyChanged("DirectionLkw1");
            }
        }
        #endregion

        #region RichtungLkw2
        public void RichtungLkw2(LkwRichtungen val)
        {
            if (val == LkwRichtungen.NachRechts) DirectionLkw2 = 1; else DirectionLkw2 = -1;
        }

        private int _directionLkw2;
        public int DirectionLkw2
        {
            get { return _directionLkw2; }
            set
            {
                _directionLkw2 = value;
                OnPropertyChanged("DirectionLkw2");
            }
        }
        #endregion

        #region RichtungLkw3
        public void RichtungLkw3(LkwRichtungen val)
        {
            if (val == LkwRichtungen.NachRechts) DirectionLkw3 = 1; else DirectionLkw3 = -1;
        }

        private int _directionLkw3;
        public int DirectionLkw3
        {
            get { return _directionLkw3; }
            set
            {
                _directionLkw3 = value;
                OnPropertyChanged("DirectionLkw3");
            }
        }
        #endregion

        #region RichtungLkw4
        public void RichtungLkw4(LkwRichtungen val)
        {
            if (val == LkwRichtungen.NachRechts) DirectionLkw4 = 1; else DirectionLkw4 = -1;
        }

        private int _directionLkw4;
        public int DirectionLkw4
        {
            get { return _directionLkw4; }
            set
            {
                _directionLkw4 = value;
                OnPropertyChanged("DirectionLkw4");
            }
        }
        #endregion

        #region RichtungLkw5
        public void RichtungLkw5(LkwRichtungen val)
        {
            if (val == LkwRichtungen.NachRechts) DirectionLkw5 = 1; else DirectionLkw5 = -1;
        }

        private int _directionLkw5;
        public int DirectionLkw5
        {
            get { return _directionLkw5; }
            set
            {
                _directionLkw5 = value;
                OnPropertyChanged("DirectionLkw5");
            }
        }
        #endregion




        #region PositionLkw1
        public void PositionLkw1(Punkt pos)
        {
            PosLkw1Left = pos.X;
            PosLkw1Top = pos.Y;
        }

        private double _posLkw1Left;
        public double PosLkw1Left
        {
            get { return _posLkw1Left; }
            set
            {
                _posLkw1Left = value;
                OnPropertyChanged("PosLkw1Left");
            }
        }
        private double _posLkw1Top;
        public double PosLkw1Top
        {
            get { return _posLkw1Top; }
            set
            {
                _posLkw1Top = value;
                OnPropertyChanged("PosLkw1Top");
            }
        }
        #endregion

        #region PositionLkw2
        public void PositionLkw2(Punkt pos)
        {
            PosLkw2Left = pos.X;
            PosLkw2Top = pos.Y;
        }

        private double _posLkw2Left;
        public double PosLkw2Left
        {
            get { return _posLkw2Left; }
            set
            {
                _posLkw2Left = value;
                OnPropertyChanged("PosLkw2Left");
            }
        }
        private double _posLkw2Top;
        public double PosLkw2Top
        {
            get { return _posLkw2Top; }
            set
            {
                _posLkw2Top = value;
                OnPropertyChanged("PosLkw2Top");
            }
        }
        #endregion

        #region PositionLkw3
        public void PositionLkw3(Punkt pos)
        {
            PosLkw3Left = pos.X;
            PosLkw3Top = pos.Y;
        }

        private double _posLkw3Left;
        public double PosLkw3Left
        {
            get { return _posLkw3Left; }
            set
            {
                _posLkw3Left = value;
                OnPropertyChanged("PosLkw3Left");
            }
        }
        private double _posLkw3Top;
        public double PosLkw3Top
        {
            get { return _posLkw3Top; }
            set
            {
                _posLkw3Top = value;
                OnPropertyChanged("PosLkw3Top");
            }
        }
        #endregion

        #region PositionLkw4
        public void PositionLkw4(Punkt pos)
        {
            PosLkw4Left = pos.X;
            PosLkw4Top = pos.Y;
        }

        private double _posLkw4Left;
        public double PosLkw4Left
        {
            get { return _posLkw4Left; }
            set
            {
                _posLkw4Left = value;
                OnPropertyChanged("PosLkw4Left");
            }
        }
        private double _posLkw4Top;
        public double PosLkw4Top
        {
            get { return _posLkw4Top; }
            set
            {
                _posLkw4Top = value;
                OnPropertyChanged("PosLkw4Top");
            }
        }
        #endregion

        #region PositionLkw5
        public void PositionLkw5(Punkt pos)
        {
            PosLkw5Left = pos.X;
            PosLkw5Top = pos.Y;
        }

        private double _posLkw5Left;
        public double PosLkw5Left
        {
            get { return _posLkw5Left; }
            set
            {
                _posLkw5Left = value;
                OnPropertyChanged("PosLkw5Left");
            }
        }
        private double _posLkw5Top;
        public double PosLkw5Top
        {
            get { return _posLkw5Top; }
            set
            {
                _posLkw5Top = value;
                OnPropertyChanged("PosLkw5Top");
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
