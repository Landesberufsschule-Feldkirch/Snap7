namespace AmpelsteuerungKieswerk.Model
{
    using System.ComponentModel;
    using Utilities;
    using static AmpelsteuerungKieswerk.Model.LKW;

    public class VisuAnzeigen
    {
        public VisuAnzeigen()
        {
            ColorB1 = "LightGray";
            ColorB2 = "LightGray";
            ColorB3 = "LightGray";
            ColorB4 = "LightGray";

            ColorLinksRot = "White";
            ColorLinksGelb = "White";
            ColorLinksGruen = "White";
            ColorRechtsRot =   "White";
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


        #region Color B1
        public void FarbeB1(bool val)
        {
            if (val) ColorB1 = "Red"; else ColorB1 = "LightGray";
        }

        private string _ColorB1;
        public string ColorB1
        {
            get { return _ColorB1; }
            set
            {
                _ColorB1 = value;
                OnPropertyChanged("ColorB1");
            }
        }
        #endregion

        #region Color B2
        public void FarbeB2(bool val)
        {
            if (val) ColorB2 = "Red"; else ColorB2 = "LightGray";
        }

        private string _ColorB2;
        public string ColorB2
        {
            get { return _ColorB2; }
            set
            {
                _ColorB2 = value;
                OnPropertyChanged("ColorB2");
            }
        }
        #endregion

        #region Color B3
        public void FarbeB3(bool val)
        {
            if (val) ColorB3 = "Red"; else ColorB3 = "LightGray";
        }

        private string _ColorB3;
        public string ColorB3
        {
            get { return _ColorB3; }
            set
            {
                _ColorB3 = value;
                OnPropertyChanged("ColorB3");
            }
        }
        #endregion

        #region Color B4
        public void FarbeB4(bool val)
        {
            if (val) ColorB4 = "Red"; else ColorB4 = "LightGray";
        }

        private string _ColorB4;
        public string ColorB4
        {
            get { return _ColorB4; }
            set
            {
                _ColorB4 = value;
                OnPropertyChanged("ColorB4");
            }
        }
        #endregion



        #region ColorLinksRot
        public void FarbeLinksRot(bool val)
        {
            if (val) ColorLinksRot = "Red"; else ColorLinksRot = "White";
        }

        private string _ColorLinksRot;
        public string ColorLinksRot
        {
            get { return _ColorLinksRot; }
            set
            {
                _ColorLinksRot = value;
                OnPropertyChanged("ColorLinksRot");
            }
        }
        #endregion

        #region ColorLinksGelb
        public void FarbeLinksGelb(bool val)
        {
            if (val) ColorLinksGelb = "Yellow"; else ColorLinksGelb = "White";
        }

        private string _ColorLinksGelb;
        public string ColorLinksGelb
        {
            get { return _ColorLinksGelb; }
            set
            {
                _ColorLinksGelb = value;
                OnPropertyChanged("ColorLinksGelb");
            }
        }
        #endregion

        #region ColorLinksGruen
        public void FarbeLinksGruen(bool val)
        {
            if (val) ColorLinksGruen = "Green"; else ColorLinksGruen = "White";
        }

        private string _ColorLinksGruen;
        public string ColorLinksGruen
        {
            get { return _ColorLinksGruen; }
            set
            {
                _ColorLinksGruen = value;
                OnPropertyChanged("ColorLinksGruen");
            }
        }
        #endregion

        #region ColorRechtsRot
        public void FarbeRechtsRot(bool val)
        {
            if (val) ColorRechtsRot = "Red"; else ColorRechtsRot = "White";
        }

        private string _ColorRechtsRot;
        public string ColorRechtsRot
        {
            get { return _ColorRechtsRot; }
            set
            {
                _ColorRechtsRot = value;
                OnPropertyChanged("ColorRechtsRot");
            }
        }
        #endregion

        #region ColorRechtsGelb
        public void FarbeRechtsGelb(bool val)
        {
            if (val) ColorRechtsGelb = "Yellow"; else ColorRechtsGelb = "White";
        }

        private string _ColorRechtsGelb;
        public string ColorRechtsGelb
        {
            get { return _ColorRechtsGelb; }
            set
            {
                _ColorRechtsGelb = value;
                OnPropertyChanged("ColorRechtsGelb");
            }
        }
        #endregion

        #region ColorRechtsGruen
        public void FarbeRechtsGruen(bool val)
        {
            if (val) ColorRechtsGruen = "Green"; else ColorRechtsGruen = "White";
        }

        private string _ColorRechtsGruen;
        public string ColorRechtsGruen
        {
            get { return _ColorRechtsGruen; }
            set
            {
                _ColorRechtsGruen = value;
                OnPropertyChanged("ColorRechtsGruen");
            }
        }
        #endregion



        #region RichtungLkw1
        public void RichtungLkw1(LkwRichtungen val)
        {
            if (val == LkwRichtungen.NachRechts) DirectionLkw1 = 1; else DirectionLkw1 = -1;
        }

        private int _DirectionLkw1;
        public int DirectionLkw1
        {
            get { return _DirectionLkw1; }
            set
            {
                _DirectionLkw1 = value;
                OnPropertyChanged("DirectionLkw1");
            }
        }
        #endregion

        #region RichtungLkw2
        public void RichtungLkw2(LkwRichtungen val)
        {
            if (val == LkwRichtungen.NachRechts) DirectionLkw2 = 1; else DirectionLkw2 = -1;
        }

        private int _DirectionLkw2;
        public int DirectionLkw2
        {
            get { return _DirectionLkw2; }
            set
            {
                _DirectionLkw2 = value;
                OnPropertyChanged("DirectionLkw2");
            }
        }
        #endregion

        #region RichtungLkw3
        public void RichtungLkw3(LkwRichtungen val)
        {
            if (val == LkwRichtungen.NachRechts) DirectionLkw3 = 1; else DirectionLkw3 = -1;
        }

        private int _DirectionLkw3;
        public int DirectionLkw3
        {
            get { return _DirectionLkw3; }
            set
            {
                _DirectionLkw3 = value;
                OnPropertyChanged("DirectionLkw3");
            }
        }
        #endregion

        #region RichtungLkw4
        public void RichtungLkw4(LkwRichtungen val)
        {
            if (val == LkwRichtungen.NachRechts) DirectionLkw4 = 1; else DirectionLkw4 = -1;
        }

        private int _DirectionLkw4;
        public int DirectionLkw4
        {
            get { return _DirectionLkw4; }
            set
            {
                _DirectionLkw4 = value;
                OnPropertyChanged("DirectionLkw4");
            }
        }
        #endregion

        #region RichtungLkw5
        public void RichtungLkw5(LkwRichtungen val)
        {
            if (val == LkwRichtungen.NachRechts) DirectionLkw5 = 1; else DirectionLkw5 = -1;
        }

        private int _DirectionLkw5;
        public int DirectionLkw5
        {
            get { return _DirectionLkw5; }
            set
            {
                _DirectionLkw5 = value;
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

        private double _PosLkw1Left;
        public double PosLkw1Left
        {
            get { return _PosLkw1Left; }
            set
            {
                _PosLkw1Left = value;
                OnPropertyChanged("PosLkw1Left");
            }
        }
        private double _PosLkw1Top;
        public double PosLkw1Top
        {
            get { return _PosLkw1Top; }
            set
            {
                _PosLkw1Top = value;
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

        private double _PosLkw2Left;
        public double PosLkw2Left
        {
            get { return _PosLkw2Left; }
            set
            {
                _PosLkw2Left = value;
                OnPropertyChanged("PosLkw2Left");
            }
        }
        private double _PosLkw2Top;
        public double PosLkw2Top
        {
            get { return _PosLkw2Top; }
            set
            {
                _PosLkw2Top = value;
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

        private double _PosLkw3Left;
        public double PosLkw3Left
        {
            get { return _PosLkw3Left; }
            set
            {
                _PosLkw3Left = value;
                OnPropertyChanged("PosLkw3Left");
            }
        }
        private double _PosLkw3Top;
        public double PosLkw3Top
        {
            get { return _PosLkw3Top; }
            set
            {
                _PosLkw3Top = value;
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

        private double _PosLkw4Left;
        public double PosLkw4Left
        {
            get { return _PosLkw4Left; }
            set
            {
                _PosLkw4Left = value;
                OnPropertyChanged("PosLkw4Left");
            }
        }
        private double _PosLkw4Top;
        public double PosLkw4Top
        {
            get { return _PosLkw4Top; }
            set
            {
                _PosLkw4Top = value;
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

        private double _PosLkw5Left;
        public double PosLkw5Left
        {
            get { return _PosLkw5Left; }
            set
            {
                _PosLkw5Left = value;
                OnPropertyChanged("PosLkw5Left");
            }
        }
        private double _PosLkw5Top;
        public double PosLkw5Top
        {
            get { return _PosLkw5Top; }
            set
            {
                _PosLkw5Top = value;
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
