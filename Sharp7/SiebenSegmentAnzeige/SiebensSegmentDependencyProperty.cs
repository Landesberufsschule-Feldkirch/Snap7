using System.Windows;

namespace SiebenSegmentAnzeige
{
    public partial class SiebenSegmentDisplay
    {

        private bool _visibilitySegmentA;
        public bool VisibilitySegmentA
        {
            get => _visibilitySegmentA;
            set
            {
                _visibilitySegmentA = value;
                SetValue(VisibilitySegmentAProperty, _visibilitySegmentA);
            }
        }

        public static readonly DependencyProperty VisibilitySegmentAProperty =
            DependencyProperty.Register("VisSegA", typeof(bool), typeof(SiebenSegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentAChanged));

        private static void OnVisibilitySegmentAChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }


        private bool _visibilitySegmentB;
        public bool VisibilitySegmentB
        {
            get => _visibilitySegmentB;
            set
            {
                _visibilitySegmentB = value;
                SetValue(VisibilitySegmentBProperty, _visibilitySegmentB);
            }
        }

        public static readonly DependencyProperty VisibilitySegmentBProperty =
            DependencyProperty.Register("VisSegB", typeof(bool), typeof(SiebenSegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentBChanged));

        private static void OnVisibilitySegmentBChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        private bool _visibilitySegmentC;
        public bool VisibilitySegmentC
        {
            get => _visibilitySegmentC;
            set
            {
                _visibilitySegmentC = value;
                SetValue(VisibilitySegmentCProperty, _visibilitySegmentC);
            }
        }

        public static readonly DependencyProperty VisibilitySegmentCProperty =
            DependencyProperty.Register("VisSegC", typeof(bool), typeof(SiebenSegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentCChanged));

        private static void OnVisibilitySegmentCChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        private bool _visibilitySegmentD;
        public bool VisibilitySegmentD
        {
            get => _visibilitySegmentD;
            set
            {
                _visibilitySegmentD = value;
                SetValue(VisibilitySegmentDProperty, _visibilitySegmentD);
            }
        }

        public static readonly DependencyProperty VisibilitySegmentDProperty =
            DependencyProperty.Register("VisSegD", typeof(bool), typeof(SiebenSegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentDChanged));

        private static void OnVisibilitySegmentDChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        private bool _visibilitySegmentE;
        public bool VisibilitySegmentE
        {
            get => _visibilitySegmentE;
            set
            {
                _visibilitySegmentE = value;
                SetValue(VisibilitySegmentEProperty, _visibilitySegmentE);
            }
        }

        public static readonly DependencyProperty VisibilitySegmentEProperty =
            DependencyProperty.Register("VisSegE", typeof(bool), typeof(SiebenSegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentEChanged));

        private static void OnVisibilitySegmentEChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        private bool _visibilitySegmentF;
        public bool VisibilitySegmentF
        {
            get => _visibilitySegmentF;
            set
            {
                _visibilitySegmentF = value;
                SetValue(VisibilitySegmentFProperty, _visibilitySegmentF);
            }
        }

        public static readonly DependencyProperty VisibilitySegmentFProperty =
            DependencyProperty.Register("VisSegF", typeof(bool), typeof(SiebenSegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentFChanged));

        private static void OnVisibilitySegmentFChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        private bool _visibilitySegmentG;
        public bool VisibilitySegmentG
        {
            get => _visibilitySegmentG;
            set
            {
                _visibilitySegmentG = value;
                SetValue(VisibilitySegmentGProperty, _visibilitySegmentG);
            }
        }

        public static readonly DependencyProperty VisibilitySegmentGProperty =
            DependencyProperty.Register("VisSegG", typeof(bool), typeof(SiebenSegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentGChanged));

        private static void OnVisibilitySegmentGChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        private bool _visibilitySegmentDp;
        public bool VisibilitySegmentDp
        {
            get => _visibilitySegmentDp;
            set
            {
                _visibilitySegmentDp = value;
                SetValue(VisibilitySegmentDpProperty, _visibilitySegmentDp);
            }
        }

        public static readonly DependencyProperty VisibilitySegmentDpProperty =
            DependencyProperty.Register("VisSegDp", typeof(bool), typeof(SiebenSegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentDpChanged));

        private static void OnVisibilitySegmentDpChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }
    }
}