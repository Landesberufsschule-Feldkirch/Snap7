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

            FarbeLed = "Violet";

            SegmentA = "visible";
            SegmentB = "visible";
            SegmentC = "visible";
            SegmentD = "visible";
            SegmentE = "visible";
            SegmentF = "visible";
            SegmentG = "visible";
            SegmentDp = "visible";

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                if (_siebenSegmentDisplay != null)
                {
                    FarbeLed = _siebenSegmentDisplay.ColorAllSegments;

                    SegmentA = _siebenSegmentDisplay.VisibilitySegmentA;
                    SegmentB = _siebenSegmentDisplay.VisibilitySegmentB;
                    SegmentC = _siebenSegmentDisplay.VisibilitySegmentC;
                    SegmentD = _siebenSegmentDisplay.VisibilitySegmentD;
                    SegmentE = _siebenSegmentDisplay.VisibilitySegmentE;
                    SegmentF = _siebenSegmentDisplay.VisibilitySegmentF;
                    SegmentG = _siebenSegmentDisplay.VisibilitySegmentG;
                    SegmentDp = _siebenSegmentDisplay.VisibilitySegmentDp;
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

        private string _segmentA;
        public string SegmentA
        {
            get => _segmentA;
            set
            {
                _segmentA = value;
                OnPropertyChanged(nameof(SegmentA));
            }
        }

        private string _segmentB;
        public string SegmentB
        {
            get => _segmentB;
            set
            {
                _segmentB = value;
                OnPropertyChanged(nameof(SegmentB));
            }
        }

        private string _segmentC;
        public string SegmentC
        {
            get => _segmentC;
            set
            {
                _segmentC = value;
                OnPropertyChanged(nameof(SegmentC));
            }
        }

        private string _segmentD;
        public string SegmentD
        {
            get => _segmentD;
            set
            {
                _segmentD = value;
                OnPropertyChanged(nameof(SegmentD));
            }
        }

        private string _segmentE;
        public string SegmentE
        {
            get => _segmentE;
            set
            {
                _segmentE = value;
                OnPropertyChanged(nameof(SegmentE));
            }
        }

        private string _segmentF;
        public string SegmentF
        {
            get => _segmentF;
            set
            {
                _segmentF = value;
                OnPropertyChanged(nameof(SegmentF));
            }
        }

        private string _segmentG;
        public string SegmentG
        {
            get => _segmentG;
            set
            {
                _segmentG = value;
                OnPropertyChanged(nameof(SegmentG));
            }
        }

        private string _segmentDp;
        public string SegmentDp
        {
            get => _segmentDp;
            set
            {
                _segmentDp = value;
                OnPropertyChanged(nameof(SegmentDp));
            }
        }
        #endregion

        #region iNotifyPeropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion iNotifyPeropertyChanged Members
    }
}