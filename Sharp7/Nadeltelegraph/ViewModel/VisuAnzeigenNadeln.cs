namespace Nadeltelegraph.ViewModel
{
    using System.ComponentModel;

    public partial class VisuAnzeigen : INotifyPropertyChanged
    {
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
            get => _angleNeedle1;
            set
            {
                _angleNeedle1 = value;
                OnPropertyChanged(nameof(AngleNeedle1));
            }
        }

        #endregion Winkel Nadel 1

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
            get => _angleNeedle2;
            set
            {
                _angleNeedle2 = value;
                OnPropertyChanged(nameof(AngleNeedle2));
            }
        }

        #endregion Winkel Nadel 2

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
            get => _angleNeedle3;
            set
            {
                _angleNeedle3 = value;
                OnPropertyChanged(nameof(AngleNeedle3));
            }
        }

        #endregion Winkel Nadel 3

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
            get => _angleNeedle4;
            set
            {
                _angleNeedle4 = value;
                OnPropertyChanged(nameof(AngleNeedle4));
            }
        }

        #endregion Winkel Nadel 4

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
            get => _angleNeedle5;
            set
            {
                _angleNeedle5 = value;
                OnPropertyChanged(nameof(AngleNeedle5));
            }
        }

        #endregion Winkel Nadel 5
    }
}