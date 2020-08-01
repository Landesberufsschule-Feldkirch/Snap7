namespace Nadeltelegraph.ViewModel
{
    using System.ComponentModel;

    public partial class VisuAnzeigen : INotifyPropertyChanged
    {
        #region Breite 1 UpRight

        public void Breite1UpRight(bool val)
        {
            if (val) Width1UpRight = BreiteBreit; else Width1UpRight = BreiteSchmal;
        }

        private int _width1UpRight;

        public int Width1UpRight
        {
            get => _width1UpRight;
            set
            {
                _width1UpRight = value;
                OnPropertyChanged(nameof(Width1UpRight));
            }
        }

        #endregion Breite 1 UpRight

        #region Breite 2 UpRight

        public void Breite2UpRight(bool val)
        {
            if (val) Width2UpRight = BreiteBreit; else Width2UpRight = BreiteSchmal;
        }

        private int _width2UpRight;

        public int Width2UpRight
        {
            get => _width2UpRight;
            set
            {
                _width2UpRight = value;
                OnPropertyChanged(nameof(Width2UpRight));
            }
        }

        #endregion Breite 2 UpRight

        #region Breite 3 UpRight

        public void Breite3UpRight(bool val)
        {
            if (val) Width3UpRight = BreiteBreit; else Width3UpRight = BreiteSchmal;
        }

        private int _width3UpRight;

        public int Width3UpRight
        {
            get => _width3UpRight;
            set
            {
                _width3UpRight = value;
                OnPropertyChanged(nameof(Width3UpRight));
            }
        }

        #endregion Breite 3 UpRight

        #region Breite 4 UpRight

        public void Breite4UpRight(bool val)
        {
            if (val) Width4UpRight = BreiteBreit; else Width4UpRight = BreiteSchmal;
        }

        private int _width4UpRight;

        public int Width4UpRight
        {
            get => _width4UpRight;
            set
            {
                _width4UpRight = value;
                OnPropertyChanged(nameof(Width4UpRight));
            }
        }

        #endregion Breite 4 UpRight

        #region Breite 2 UpLeft

        public void Breite2UpLeft(bool val)
        {
            if (val) Width2UpLeft = BreiteBreit; else Width2UpLeft = BreiteSchmal;
        }

        private int _width2UpLeft;

        public int Width2UpLeft
        {
            get => _width2UpLeft;
            set
            {
                _width2UpLeft = value;
                OnPropertyChanged(nameof(Width2UpLeft));
            }
        }

        #endregion Breite 2 UpLeft

        #region Breite 3 UpLeft

        public void Breite3UpLeft(bool val)
        {
            if (val) Width3UpLeft = BreiteBreit; else Width3UpLeft = BreiteSchmal;
        }

        private int _width3UpLeft;

        public int Width3UpLeft
        {
            get => _width3UpLeft;
            set
            {
                _width3UpLeft = value;
                OnPropertyChanged(nameof(Width3UpLeft));
            }
        }

        #endregion Breite 3 UpLeft

        #region Breite 4 UpLeft

        public void Breite4UpLeft(bool val)
        {
            if (val) Width4UpLeft = BreiteBreit; else Width4UpLeft = BreiteSchmal;
        }

        private int _width4UpLeft;

        public int Width4UpLeft
        {
            get => _width4UpLeft;
            set
            {
                _width4UpLeft = value;
                OnPropertyChanged(nameof(Width4UpLeft));
            }
        }

        #endregion Breite 4 UpLeft

        #region Breite 5 UpLeft

        public void Breite5UpLeft(bool val)
        {
            if (val) Width5UpLeft = BreiteBreit; else Width5UpLeft = BreiteSchmal;
        }

        private int _width5UpLeft;

        public int Width5UpLeft
        {
            get => _width5UpLeft;
            set
            {
                _width5UpLeft = value;
                OnPropertyChanged(nameof(Width5UpLeft));
            }
        }

        #endregion Breite 5 UpLeft

        #region Breite 1 DownRight

        public void Breite1DownRight(bool val)
        {
            if (val) Width1DownRight = BreiteBreit; else Width1DownRight = BreiteSchmal;
        }

        private int _width1DownRight;

        public int Width1DownRight
        {
            get => _width1DownRight;
            set
            {
                _width1DownRight = value;
                OnPropertyChanged(nameof(Width1DownRight));
            }
        }

        #endregion Breite 1 DownRight

        #region Breite 2 DownRight

        public void Breite2DownRight(bool val)
        {
            if (val) Width2DownRight = BreiteBreit; else Width2DownRight = BreiteSchmal;
        }

        private int _width2DownRight;

        public int Width2DownRight
        {
            get => _width2DownRight;
            set
            {
                _width2DownRight = value;
                OnPropertyChanged(nameof(Width2DownRight));
            }
        }

        #endregion Breite 2 DownRight

        #region Breite 3 DownRight

        public void Breite3DownRight(bool val)
        {
            if (val) Width3DownRight = BreiteBreit; else Width3DownRight = BreiteSchmal;
        }

        private int _width3DownRight;

        public int Width3DownRight
        {
            get => _width3DownRight;
            set
            {
                _width3DownRight = value;
                OnPropertyChanged(nameof(Width3DownRight));
            }
        }

        #endregion Breite 3 DownRight

        #region Breite 4 DownRight

        public void Breite4DownRight(bool val)
        {
            if (val) Width4DownRight = BreiteBreit; else Width4DownRight = BreiteSchmal;
        }

        private int _width4DownRight;

        public int Width4DownRight
        {
            get => _width4DownRight;
            set
            {
                _width4DownRight = value;
                OnPropertyChanged(nameof(Width4DownRight));
            }
        }

        #endregion Breite 4 DownRight

        #region Breite 2 DownLeft

        public void Breite2DownLeft(bool val)
        {
            if (val) Width2DownLeft = BreiteBreit; else Width2DownLeft = BreiteSchmal;
        }

        private int _width2DownLeft;

        public int Width2DownLeft
        {
            get => _width2DownLeft;
            set
            {
                _width2DownLeft = value;
                OnPropertyChanged(nameof(Width2DownLeft));
            }
        }

        #endregion Breite 2 DownLeft

        #region Breite 3 DownLeft

        public void Breite3DownLeft(bool val)
        {
            if (val) Width3DownLeft = BreiteBreit; else Width3DownLeft = BreiteSchmal;
        }

        private int _width3DownLeft;

        public int Width3DownLeft
        {
            get => _width3DownLeft;
            set
            {
                _width3DownLeft = value;
                OnPropertyChanged(nameof(Width3DownLeft));
            }
        }

        #endregion Breite 3 DownLeft

        #region Breite 4 DownLeft

        public void Breite4DownLeft(bool val)
        {
            if (val) Width4DownLeft = BreiteBreit; else Width4DownLeft = BreiteSchmal;
        }

        private int _width4DownLeft;

        public int Width4DownLeft
        {
            get => _width4DownLeft;
            set
            {
                _width4DownLeft = value;
                OnPropertyChanged(nameof(Width4DownLeft));
            }
        }

        #endregion Breite 4 DownLeft

        #region Breite 5 DownLeft

        public void Breite5DownLeft(bool val)
        {
            if (val) Width5DownLeft = BreiteBreit; else Width5DownLeft = BreiteSchmal;
        }

        private int _width5DownLeft;

        public int Width5DownLeft
        {
            get => _width5DownLeft;
            set
            {
                _width5DownLeft = value;
                OnPropertyChanged(nameof(Width5DownLeft));
            }
        }

        #endregion Breite 5 DownLeft

    }
}