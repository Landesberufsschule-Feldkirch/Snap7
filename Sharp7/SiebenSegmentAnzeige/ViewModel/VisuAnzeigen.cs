using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows;

namespace SiebenSegmentAnzeige.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly SiebenSegmentDisplay _siebenSegmentDisplay;
        public VisuAnzeigen(SiebenSegmentDisplay uc)
        {
            _siebenSegmentDisplay = uc;

            FarbeLed = Color.Violet;


            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                if (_siebenSegmentDisplay != null)
                {
                    FarbeLed = _siebenSegmentDisplay.ColorSegment;

                    SegmentA = _siebenSegmentDisplay.VisibilitySegmentA;


                }

                Thread.Sleep(500);
            }
            // ReSharper disable once FunctionNeverReturns
        }


        #region Farbe umschalten

        private Color _farbeLed;
        public Color FarbeLed
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

        private Visibility _segmentA;
        public Visibility SegmentA
        {
            get => _segmentA;
            set
            {
                _segmentA = value;
                OnPropertyChanged(nameof(SegmentA));
            }
        }

        private Visibility _segmentB;
        public Visibility SegmentB
        {
            get => _segmentB;
            set
            {
                _segmentB = value;
                OnPropertyChanged(nameof(SegmentB));
            }
        }

        private Visibility _segmentC;
        public Visibility SegmentC
        {
            get => _segmentC;
            set
            {
                _segmentC = value;
                OnPropertyChanged(nameof(SegmentC));
            }
        }

        private Visibility _segmentD;
        public Visibility SegmentD
        {
            get => _segmentD;
            set
            {
                _segmentD = value;
                OnPropertyChanged(nameof(SegmentD));
            }
        }

        private Visibility _segmentE;
        public Visibility SegmentE
        {
            get => _segmentE;
            set
            {
                _segmentE = value;
                OnPropertyChanged(nameof(SegmentE));
            }
        }

        private Visibility _segmentF;
        public Visibility SegmentF
        {
            get => _segmentF;
            set
            {
                _segmentF = value;
                OnPropertyChanged(nameof(SegmentF));
            }
        }

        private Visibility _segmentG;
        public Visibility SegmentG
        {
            get => _segmentG;
            set
            {
                _segmentG = value;
                OnPropertyChanged(nameof(SegmentG));
            }
        }

        private Visibility _segmentDp;
        public Visibility SegmentDp
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