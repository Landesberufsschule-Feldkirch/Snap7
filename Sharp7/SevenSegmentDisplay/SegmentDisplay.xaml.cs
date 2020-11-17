using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace SegmentDisplay
{
    public partial class SegmentDisplay
    {
        
        public SegmentDisplay()
        {
            InitializeComponent();

            ColorAllSegments = Brushes.Green;

            HintergrundRechteck.Fill = HintergrundFarbe;

        }


        // ReSharper disable once UnusedMember.Global
        [Description("(SegmentDisplay) Hintergrundfarbe "), Category("SegmentDisplay")]

        private SolidColorBrush _hintergrundFarbe = Brushes.AntiqueWhite;
        public SolidColorBrush HintergrundFarbe
        {
            set
            {
                _hintergrundFarbe = value;
                ChangeHintergrundfarbe();
            }
            get => _hintergrundFarbe;
        }

        private void ChangeHintergrundfarbe()
        {

            Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action(() => { HintergrundRechteck.Fill = HintergrundFarbe; }));


        }


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
            DependencyProperty.Register("VisibilitySegmentA", typeof(Visibility), typeof(SegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentAChanged));

        private static void OnVisibilitySegmentAChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => DisplayAktualsieren(d, e);


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
            DependencyProperty.Register("VisibilitySegmentB", typeof(Visibility), typeof(SegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentBChanged));

        private static void OnVisibilitySegmentBChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => DisplayAktualsieren(d, e);

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
            DependencyProperty.Register("VisibilitySegmentC", typeof(Visibility), typeof(SegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentCChanged));

        private static void OnVisibilitySegmentCChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => DisplayAktualsieren(d, e);

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
            DependencyProperty.Register("VisibilitySegmentD", typeof(Visibility), typeof(SegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentDChanged));

        private static void OnVisibilitySegmentDChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => DisplayAktualsieren(d, e);

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
            DependencyProperty.Register("VisibilitySegmentE", typeof(Visibility), typeof(SegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentEChanged));

        private static void OnVisibilitySegmentEChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => DisplayAktualsieren(d, e);

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
            DependencyProperty.Register("VisibilitySegmentF", typeof(Visibility), typeof(SegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentFChanged));

        private static void OnVisibilitySegmentFChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => DisplayAktualsieren(d, e);

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
            DependencyProperty.Register("VisibilitySegmentG", typeof(Visibility), typeof(SegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentGChanged));

        private static void OnVisibilitySegmentGChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => DisplayAktualsieren(d, e);

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
            DependencyProperty.Register("VisibilitySegmentDp", typeof(Visibility), typeof(SegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentDpChanged));

        private static void OnVisibilitySegmentDpChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => DisplayAktualsieren(d, e);



        private SolidColorBrush _colorAllSegments;
        public SolidColorBrush ColorAllSegments
        {
            get => _colorAllSegments;
            set
            {
                _colorAllSegments = value;
                SetValue(ColorAllSegmentsProperty, _colorAllSegments);
            }
        }

        public static readonly DependencyProperty ColorAllSegmentsProperty =
            DependencyProperty.Register("ColorAllSegments", typeof(SolidColorBrush), typeof(SegmentDisplay),
                new PropertyMetadata(OnColorAllSegmentsChanged));

        private static void OnColorAllSegmentsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null && d is SegmentDisplay sevenSegmentDisplay)
            {
                sevenSegmentDisplay.ColorAllSegments = (SolidColorBrush)e.NewValue;
                sevenSegmentDisplay.FarbenAktualisieren();
                if (sevenSegmentDisplay.ColorAllSegments == Brushes.Yellow) return;
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

        private void FarbenAktualisieren()
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {

                if (SegmentA != null)
                {
                    SegmentA.Fill = (Brush)ColorAllSegments;
                }

                if (SegmentB != null)
                {
                    SegmentB.Fill = (Brush)ColorAllSegments;
                }

                if (SegmentC != null)
                {
                    SegmentC.Fill = (Brush)ColorAllSegments;
                }

                if (SegmentD != null)
                {
                    SegmentD.Fill = (Brush)ColorAllSegments;
                }

                if (SegmentE != null)
                {
                    SegmentE.Fill = (Brush)ColorAllSegments;
                }

                if (SegmentF != null)
                {
                    SegmentF.Fill = (Brush)ColorAllSegments;
                }

                if (SegmentG != null)
                {
                    SegmentG.Fill = (Brush)ColorAllSegments;
                }

                if (SegmentDp != null)
                {
                    SegmentDp.Fill = (Brush)ColorAllSegments;
                }

            }));
        }

        private static void DisplayAktualsieren(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var siebensSegment = d as SegmentDisplay;
            siebensSegment?.SetValue();
        }

        private void SetValue()
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {

                if (SegmentA != null)
                {
                    SegmentA.Visibility = VisibilitySegmentA;
                    SegmentA.Fill = (Brush)ColorAllSegments;
                }

                if (SegmentB != null)
                {
                    SegmentB.Visibility = VisibilitySegmentB;
                    SegmentB.Fill = (Brush)ColorAllSegments;
                }

                if (SegmentC != null)
                {
                    SegmentC.Visibility =VisibilitySegmentC;
                    SegmentC.Fill = (Brush)ColorAllSegments;
                }

                if (SegmentD != null)
                {
                    SegmentD.Visibility = VisibilitySegmentD;
                    SegmentD.Fill = (Brush)ColorAllSegments;
                }

                if (SegmentE != null)
                {
                    SegmentE.Visibility = VisibilitySegmentE;
                    SegmentE.Fill = (Brush)ColorAllSegments;
                }

                if (SegmentF != null)
                {
                    SegmentF.Visibility = VisibilitySegmentF;
                    SegmentF.Fill = (Brush)ColorAllSegments;
                }

                if (SegmentG != null)
                {
                    SegmentG.Visibility = VisibilitySegmentG;
                    SegmentG.Fill = (Brush)ColorAllSegments;
                }

                if (SegmentDp != null)
                {
                    SegmentDp.Visibility = VisibilitySegmentDp;
                    SegmentDp.Fill = (Brush)ColorAllSegments;
                }

            }));
        }
    }



}