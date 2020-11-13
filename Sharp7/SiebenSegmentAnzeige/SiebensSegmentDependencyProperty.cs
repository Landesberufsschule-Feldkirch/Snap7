using System.Windows;

namespace SiebenSegmentAnzeige
{
    public partial class SiebenSegmentDisplay
    {

        private Visibility _visibilitySegmentA;
        public Visibility VisibilitySegmentA
        {
            get => _visibilitySegmentA;
            set
            {
                _visibilitySegmentA = value;
                SetValue(VisibilitySegmentAProperty, _visibilitySegmentA);
            }
        }

        public static readonly DependencyProperty VisibilitySegmentAProperty =
            DependencyProperty.Register("VisibilitySegmentA", typeof(Visibility), typeof(SiebenSegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentAChanged));

        private static void OnVisibilitySegmentAChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }


        private Visibility _visibilitySegmentB;
        public Visibility VisibilitySegmentB
        {
            get => _visibilitySegmentB;
            set
            {
                _visibilitySegmentB = value;
                SetValue(VisibilitySegmentBProperty, _visibilitySegmentB);
            }
        }

        public static readonly DependencyProperty VisibilitySegmentBProperty =
            DependencyProperty.Register("VisibilitySegmentB", typeof(Visibility), typeof(SiebenSegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentBChanged));

        private static void OnVisibilitySegmentBChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        private Visibility _visibilitySegmentC;
        public Visibility VisibilitySegmentC
        {
            get => _visibilitySegmentC;
            set
            {
                _visibilitySegmentC = value;
                SetValue(VisibilitySegmentCProperty, _visibilitySegmentC);
            }
        }

        public static readonly DependencyProperty VisibilitySegmentCProperty =
            DependencyProperty.Register("VisibilitySegmentC", typeof(Visibility), typeof(SiebenSegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentCChanged));

        private static void OnVisibilitySegmentCChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        private Visibility _visibilitySegmentD;
        public Visibility VisibilitySegmentD
        {
            get => _visibilitySegmentD;
            set
            {
                _visibilitySegmentD = value;
                SetValue(VisibilitySegmentDProperty, _visibilitySegmentD);
            }
        }

        public static readonly DependencyProperty VisibilitySegmentDProperty =
            DependencyProperty.Register("VisibilitySegmentD", typeof(Visibility), typeof(SiebenSegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentDChanged));

        private static void OnVisibilitySegmentDChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        private Visibility _visibilitySegmentE;
        public Visibility VisibilitySegmentE
        {
            get => _visibilitySegmentE;
            set
            {
                _visibilitySegmentE = value;
                SetValue(VisibilitySegmentEProperty, _visibilitySegmentE);
            }
        }

        public static readonly DependencyProperty VisibilitySegmentEProperty =
            DependencyProperty.Register("VisibilitySegmentE", typeof(Visibility), typeof(SiebenSegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentEChanged));

        private static void OnVisibilitySegmentEChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        private Visibility _visibilitySegmentF;
        public Visibility VisibilitySegmentF
        {
            get => _visibilitySegmentF;
            set
            {
                _visibilitySegmentF = value;
                SetValue(VisibilitySegmentFProperty, _visibilitySegmentF);
            }
        }

        public static readonly DependencyProperty VisibilitySegmentFProperty =
            DependencyProperty.Register("VisibilitySegmentF", typeof(Visibility), typeof(SiebenSegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentFChanged));

        private static void OnVisibilitySegmentFChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        private Visibility _visibilitySegmentG;
        public Visibility VisibilitySegmentG
        {
            get => _visibilitySegmentG;
            set
            {
                _visibilitySegmentG = value;
                SetValue(VisibilitySegmentGProperty, _visibilitySegmentG);
            }
        }

        public static readonly DependencyProperty VisibilitySegmentGProperty =
            DependencyProperty.Register("VisibilitySegmentG", typeof(Visibility), typeof(SiebenSegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentGChanged));

        private static void OnVisibilitySegmentGChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }

        private Visibility _visibilitySegmentDp;
        public Visibility VisibilitySegmentDp
        {
            get => _visibilitySegmentDp;
            set
            {
                _visibilitySegmentDp = value;
                SetValue(VisibilitySegmentDpProperty, _visibilitySegmentDp);
            }
        }

        public static readonly DependencyProperty VisibilitySegmentDpProperty =
            DependencyProperty.Register("VisibilitySegmentDp", typeof(Visibility), typeof(SiebenSegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentDpChanged));

        private static void OnVisibilitySegmentDpChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }
    }
}