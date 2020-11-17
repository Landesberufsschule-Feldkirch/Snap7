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

            HintergrundRechteck.Fill = HintergrundFarbe;

            ColorAllSegments = Brushes.Green;

            VisibilitySegmentA = Visibility.Visible;
            VisibilitySegmentB = Visibility.Visible;
            VisibilitySegmentC = Visibility.Visible;
            VisibilitySegmentD = Visibility.Visible;
            VisibilitySegmentE = Visibility.Visible;
            VisibilitySegmentF = Visibility.Visible;
            VisibilitySegmentG = Visibility.Visible;
            VisibilitySegmentDp = Visibility.Visible;
        }

        private SolidColorBrush _hintergrundFarbe = Brushes.AntiqueWhite;
        private SolidColorBrush _colorAllSegments = Brushes.Violet;

        private Visibility _visibilitySegmentA = Visibility.Visible;
        private Visibility _visibilitySegmentB = Visibility.Visible;
        private Visibility _visibilitySegmentC = Visibility.Visible;
        private Visibility _visibilitySegmentD = Visibility.Visible;
        private Visibility _visibilitySegmentE = Visibility.Visible;
        private Visibility _visibilitySegmentF = Visibility.Visible;
        private Visibility _visibilitySegmentG = Visibility.Visible;
        private Visibility _visibilitySegmentDp = Visibility.Visible;



        [Description("(Display) Hintergrundfarbe "), Category("Segment Display")]
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



        [Description("(Display) VisibilitySegmentA"), Category("Segment Display")]
        public Visibility VisibilitySegmentA
        {
            set
            {
                _visibilitySegmentA = value;
                SetValue(VisibilitySegmentAProperty, _visibilitySegmentA);
            }
            get => _visibilitySegmentA;
        }

        public static readonly DependencyProperty VisibilitySegmentAProperty =
            DependencyProperty.Register("VisibilitySegmentA", typeof(Visibility), typeof(SegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentAChanged));

        private static void OnVisibilitySegmentAChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => DisplayAktualsieren(d, e);



        [Description("(Display) VisibilitySegmentB"), Category("Segment Display")]
        public Visibility VisibilitySegmentB
        {
            set
            {
                _visibilitySegmentB = value;
                SetValue(VisibilitySegmentBProperty, _visibilitySegmentB);
            }
            get => _visibilitySegmentB;
        }

        public static readonly DependencyProperty VisibilitySegmentBProperty =
            DependencyProperty.Register("VisibilitySegmentB", typeof(Visibility), typeof(SegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentBChanged));

        private static void OnVisibilitySegmentBChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => DisplayAktualsieren(d, e);


        [Description("(Display) VisibilitySegmentC"), Category("Segment Display")]
        public Visibility VisibilitySegmentC
        {
            set
            {
                _visibilitySegmentC = value;
                SetValue(VisibilitySegmentCProperty, _visibilitySegmentC);
            }
            get => _visibilitySegmentC;
        }

        public static readonly DependencyProperty VisibilitySegmentCProperty =
            DependencyProperty.Register("VisibilitySegmentC", typeof(Visibility), typeof(SegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentCChanged));

        private static void OnVisibilitySegmentCChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => DisplayAktualsieren(d, e);


        [Description("(Display) VisibilitySegmentD"), Category("Segment Display")]
        public Visibility VisibilitySegmentD
        {
            set
            {
                _visibilitySegmentD = value;
                SetValue(VisibilitySegmentDProperty, _visibilitySegmentD);
            }
            get => _visibilitySegmentD;
        }

        public static readonly DependencyProperty VisibilitySegmentDProperty =
            DependencyProperty.Register("VisibilitySegmentD", typeof(Visibility), typeof(SegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentDChanged));

        private static void OnVisibilitySegmentDChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => DisplayAktualsieren(d, e);


        [Description("(Display) VisibilitySegmentE"), Category("Segment Display")]
        public Visibility VisibilitySegmentE
        {
            set
            {
                _visibilitySegmentE = value;
                SetValue(VisibilitySegmentEProperty, _visibilitySegmentE);
            }
            get => _visibilitySegmentE;
        }

        public static readonly DependencyProperty VisibilitySegmentEProperty =
            DependencyProperty.Register("VisibilitySegmentE", typeof(Visibility), typeof(SegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentEChanged));

        private static void OnVisibilitySegmentEChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => DisplayAktualsieren(d, e);


        [Description("(Display) VisibilitySegmentF"), Category("Segment Display")]
        public Visibility VisibilitySegmentF
        {
            set
            {
                _visibilitySegmentF = value;
                SetValue(VisibilitySegmentFProperty, _visibilitySegmentF);
            }
            get => _visibilitySegmentF;
        }

        public static readonly DependencyProperty VisibilitySegmentFProperty =
            DependencyProperty.Register("VisibilitySegmentF", typeof(Visibility), typeof(SegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentFChanged));

        private static void OnVisibilitySegmentFChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => DisplayAktualsieren(d, e);


        [Description("(Display) VisibilitySegmentG"), Category("Segment Display")]
        public Visibility VisibilitySegmentG
        {
            set
            {
                _visibilitySegmentG = value;
                SetValue(VisibilitySegmentGProperty, _visibilitySegmentG);
            }
            get => _visibilitySegmentG;
        }

        public static readonly DependencyProperty VisibilitySegmentGProperty =
            DependencyProperty.Register("VisibilitySegmentG", typeof(Visibility), typeof(SegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentGChanged));

        private static void OnVisibilitySegmentGChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => DisplayAktualsieren(d, e);


        [Description("(Display) VisibilitySegmentDp"), Category("Segment Display")]
        public Visibility VisibilitySegmentDp
        {
            set
            {
                _visibilitySegmentDp = value;
                SetValue(VisibilitySegmentDpProperty, _visibilitySegmentDp);
            }
            get => _visibilitySegmentDp;
        }

        public static readonly DependencyProperty VisibilitySegmentDpProperty =
            DependencyProperty.Register("VisibilitySegmentDp", typeof(Visibility), typeof(SegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentDpChanged));

        private static void OnVisibilitySegmentDpChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => DisplayAktualsieren(d, e);





        [Description("(Display) ColorAllSegments"), Category("Segment Display")]
        public SolidColorBrush ColorAllSegments
        {
            set
            {
                _colorAllSegments = value;
                SetValue(ColorAllSegmentsProperty, _colorAllSegments);
            }
            get => _colorAllSegments;
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
                if (SegmentA != null) SegmentA.Fill = ColorAllSegments;
                if (SegmentB != null) SegmentB.Fill = ColorAllSegments;
                if (SegmentC != null) SegmentC.Fill = ColorAllSegments;
                if (SegmentD != null) SegmentD.Fill = ColorAllSegments;
                if (SegmentE != null) SegmentE.Fill = ColorAllSegments;
                if (SegmentF != null) SegmentF.Fill = ColorAllSegments;
                if (SegmentG != null) SegmentG.Fill = ColorAllSegments;
                if (SegmentDp != null) SegmentDp.Fill = ColorAllSegments;
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
                    SegmentA.Fill = ColorAllSegments;
                }


                if (SegmentB != null)
                {
                    SegmentB.Visibility = VisibilitySegmentB;
                    SegmentB.Fill = ColorAllSegments;
                }

                if (SegmentC != null)
                {
                    SegmentC.Visibility = VisibilitySegmentC;
                    SegmentC.Fill = ColorAllSegments;
                }

                if (SegmentD != null)
                {
                    SegmentD.Visibility = VisibilitySegmentD;
                    SegmentD.Fill = ColorAllSegments;
                }

                if (SegmentE != null)
                {
                    SegmentE.Visibility = VisibilitySegmentE;
                    SegmentE.Fill = ColorAllSegments;
                }

                if (SegmentF != null)
                {
                    SegmentF.Visibility = VisibilitySegmentF;
                    SegmentF.Fill = ColorAllSegments;
                }

                if (SegmentG != null)
                {
                    SegmentG.Visibility = VisibilitySegmentG;
                    SegmentG.Fill = ColorAllSegments;
                }

                if (SegmentDp != null)
                {
                    SegmentDp.Visibility = VisibilitySegmentDp;
                    SegmentDp.Fill = ColorAllSegments;
                }

            }));
        }
    }



}