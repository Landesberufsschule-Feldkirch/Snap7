using System.ComponentModel;
using System.Threading;

namespace SiebenSegmentAnzeige.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly SiebenSegmentDisplay _siebenSegmentDisplay;
        public VisuAnzeigen(SiebenSegmentDisplay uc)
        {
            _siebenSegmentDisplay = uc;

            FarbeLed = "red";
            SichtbarkeitSegmentA(true);
            SichtbarkeitSegmentB(true);
            SichtbarkeitSegmentC(true);
            SichtbarkeitSegmentD(true);
            SichtbarkeitSegmentE(true);
            SichtbarkeitSegmentF(true);
            SichtbarkeitSegmentF(true);
            SichtbarkeitSegmentDp(true);

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                if (_siebenSegmentDisplay != null)
                {
                    SichtbarkeitSegmentA(_siebenSegmentDisplay.LedA);
                    SichtbarkeitSegmentB(_siebenSegmentDisplay.LedB);
                    SichtbarkeitSegmentC(_siebenSegmentDisplay.LedC);
                    SichtbarkeitSegmentD(_siebenSegmentDisplay.LedD);
                    SichtbarkeitSegmentE(_siebenSegmentDisplay.LedE);
                    SichtbarkeitSegmentF(_siebenSegmentDisplay.LedF);
                    SichtbarkeitSegmentG(_siebenSegmentDisplay.LedG);
                    SichtbarkeitSegmentDp(_siebenSegmentDisplay.LedDp);
                }

                Thread.Sleep(500);
            }
            // ReSharper disable once FunctionNeverReturns
        }


        #region Farbe umschalten
        private string _farbeLed;
        public string FarbeLed
        {
            get => _farbeLed;
            set
            {
                _farbeLed = value;
                OnPropertyChanged(nameof(FarbeLed));
            }
        }
        #endregion

        #region Sichtbarkeit Segmente
        public void SichtbarkeitSegmentA(bool val) => VisibilitySegmentA = val ? "visible" : "hidden";

        private string _visibilitySegmentA;
        public string VisibilitySegmentA
        {
            get => _visibilitySegmentA;
            set
            {
                _visibilitySegmentA = value;
                OnPropertyChanged(nameof(VisibilitySegmentA));
            }
        }


        public void SichtbarkeitSegmentB(bool val) => VisibilitySegmentB = val ? "visible" : "hidden";

        private string _visibilitySegmentB;
        public string VisibilitySegmentB
        {
            get => _visibilitySegmentB;
            set
            {
                _visibilitySegmentB = value;
                OnPropertyChanged(nameof(VisibilitySegmentB));
            }
        }

        public void SichtbarkeitSegmentC(bool val) => VisibilitySegmentC = val ? "visible" : "hidden";

        private string _visibilitySegmentC;
        public string VisibilitySegmentC
        {
            get => _visibilitySegmentC;
            set
            {
                _visibilitySegmentC = value;
                OnPropertyChanged(nameof(VisibilitySegmentC));
            }
        }

        public void SichtbarkeitSegmentD(bool val) => VisibilitySegmentD = val ? "visible" : "hidden";

        private string _visibilitySegmentD;
        public string VisibilitySegmentD
        {
            get => _visibilitySegmentD;
            set
            {
                _visibilitySegmentD = value;
                OnPropertyChanged(nameof(VisibilitySegmentD));
            }
        }

        public void SichtbarkeitSegmentE(bool val) => VisibilitySegmentE = val ? "visible" : "hidden";

        private string _visibilitySegmentE;
        public string VisibilitySegmentE
        {
            get => _visibilitySegmentE;
            set
            {
                _visibilitySegmentE = value;
                OnPropertyChanged(nameof(VisibilitySegmentE));
            }
        }

        public void SichtbarkeitSegmentF(bool val) => VisibilitySegmentF = val ? "visible" : "hidden";

        private string _visibilitySegmentF;
        public string VisibilitySegmentF
        {
            get => _visibilitySegmentF;
            set
            {
                _visibilitySegmentF = value;
                OnPropertyChanged(nameof(VisibilitySegmentF));
            }
        }

        public void SichtbarkeitSegmentG(bool val) => VisibilitySegmentG = val ? "visible" : "hidden";

        private string _visibilitySegmentG;
        public string VisibilitySegmentG
        {
            get => _visibilitySegmentG;
            set
            {
                _visibilitySegmentG = value;
                OnPropertyChanged(nameof(VisibilitySegmentG));
            }
        }

        public void SichtbarkeitSegmentDp(bool val) => VisibilitySegmentDp = val ? "visible" : "hidden";

        private string _visibilitySegmentDp;
        public string VisibilitySegmentDp
        {
            get => _visibilitySegmentDp;
            set
            {
                _visibilitySegmentDp = value;
                OnPropertyChanged(nameof(VisibilitySegmentDp));
            }
        }
        #endregion

        #region iNotifyPeropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion iNotifyPeropertyChanged Members
    }
}