using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace SiebenSegmentAnzeige
{
    public partial class SiebenSegmentDisplay
    {

        private string _visibilitySegmentA;
        public string VisibilitySegmentA
        {
            get => _visibilitySegmentA;
            set
            {
                _visibilitySegmentA = value;
                SetValue(VisibilitySegmentAProperty, _visibilitySegmentA);
            }
        }

        public static readonly DependencyProperty VisibilitySegmentAProperty =
            DependencyProperty.Register("VisibilitySegmentA", typeof(string), typeof(SiebenSegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentAChanged));

        private static void OnVisibilitySegmentAChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => DisplayAktualsieren(d, e);


        private string _visibilitySegmentB;
        public string VisibilitySegmentB
        {
            get => _visibilitySegmentB;
            set
            {
                _visibilitySegmentB = value;
                SetValue(VisibilitySegmentBProperty, _visibilitySegmentB);
            }
        }

        public static readonly DependencyProperty VisibilitySegmentBProperty =
            DependencyProperty.Register("VisibilitySegmentB", typeof(string), typeof(SiebenSegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentBChanged));

        private static void OnVisibilitySegmentBChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => DisplayAktualsieren(d, e);

        private string _visibilitySegmentC;
        public string VisibilitySegmentC
        {
            get => _visibilitySegmentC;
            set
            {
                _visibilitySegmentC = value;
                SetValue(VisibilitySegmentCProperty, _visibilitySegmentC);
            }
        }

        public static readonly DependencyProperty VisibilitySegmentCProperty =
            DependencyProperty.Register("VisibilitySegmentC", typeof(string), typeof(SiebenSegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentCChanged));

        private static void OnVisibilitySegmentCChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => DisplayAktualsieren(d, e);

        private string _visibilitySegmentD;
        public string VisibilitySegmentD
        {
            get => _visibilitySegmentD;
            set
            {
                _visibilitySegmentD = value;
                SetValue(VisibilitySegmentDProperty, _visibilitySegmentD);
            }
        }

        public static readonly DependencyProperty VisibilitySegmentDProperty =
            DependencyProperty.Register("VisibilitySegmentD", typeof(string), typeof(SiebenSegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentDChanged));

        private static void OnVisibilitySegmentDChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => DisplayAktualsieren(d, e);

        private string _visibilitySegmentE;
        public string VisibilitySegmentE
        {
            get => _visibilitySegmentE;
            set
            {
                _visibilitySegmentE = value;
                SetValue(VisibilitySegmentEProperty, _visibilitySegmentE);
            }
        }

        public static readonly DependencyProperty VisibilitySegmentEProperty =
            DependencyProperty.Register("VisibilitySegmentE", typeof(string), typeof(SiebenSegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentEChanged));

        private static void OnVisibilitySegmentEChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => DisplayAktualsieren(d, e);

        private string _visibilitySegmentF;
        public string VisibilitySegmentF
        {
            get => _visibilitySegmentF;
            set
            {
                _visibilitySegmentF = value;
                SetValue(VisibilitySegmentFProperty, _visibilitySegmentF);
            }
        }

        public static readonly DependencyProperty VisibilitySegmentFProperty =
            DependencyProperty.Register("VisibilitySegmentF", typeof(string), typeof(SiebenSegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentFChanged));

        private static void OnVisibilitySegmentFChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => DisplayAktualsieren(d, e);

        private string _visibilitySegmentG;
        public string VisibilitySegmentG
        {
            get => _visibilitySegmentG;
            set
            {
                _visibilitySegmentG = value;
                SetValue(VisibilitySegmentGProperty, _visibilitySegmentG);
            }
        }

        public static readonly DependencyProperty VisibilitySegmentGProperty =
            DependencyProperty.Register("VisibilitySegmentG", typeof(string), typeof(SiebenSegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentGChanged));

        private static void OnVisibilitySegmentGChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => DisplayAktualsieren(d, e);

        private string _visibilitySegmentDp;
        public string VisibilitySegmentDp
        {
            get => _visibilitySegmentDp;
            set
            {
                _visibilitySegmentDp = value;
                SetValue(VisibilitySegmentDpProperty, _visibilitySegmentDp);
            }
        }

        public static readonly DependencyProperty VisibilitySegmentDpProperty =
            DependencyProperty.Register("VisibilitySegmentDp", typeof(string), typeof(SiebenSegmentDisplay),
                new PropertyMetadata(OnVisibilitySegmentDpChanged));

        private static void OnVisibilitySegmentDpChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => DisplayAktualsieren(d, e);

        private string _colorAllSegments;
        public string ColorAllSegments
        {
            get => _colorAllSegments;
            set
            {
                _colorAllSegments = value;
                SetValue(ColorAllSegmentsProperty, _colorAllSegments);
            }
        }

        public static readonly DependencyProperty ColorAllSegmentsProperty =
            DependencyProperty.Register("ColorAllSegments", typeof(string), typeof(SiebenSegmentDisplay),
                new PropertyMetadata(OnColorAllSegmentsChanged));

        private static void OnColorAllSegmentsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null && d is SiebenSegmentDisplay siebensSegment)
            {
                siebensSegment.ColorAllSegments = (string)e.NewValue; 
                siebensSegment.FarbenAktualisieren();
                if (siebensSegment.ColorAllSegments=="Yellow") return;
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
                    SegmentA.Fill = (Brush)new BrushConverter().ConvertFromString(ColorAllSegments);
                }

                if (SegmentB != null)
                {
                    SegmentB.Fill = (Brush)new BrushConverter().ConvertFromString(ColorAllSegments);
                }

                if (SegmentC != null)
                {
                    SegmentC.Fill = (Brush)new BrushConverter().ConvertFromString(ColorAllSegments);
                }

                if (SegmentD != null)
                {
                    SegmentD.Fill = (Brush)new BrushConverter().ConvertFromString(ColorAllSegments);
                }

                if (SegmentE != null)
                {
                   SegmentE.Fill = (Brush)new BrushConverter().ConvertFromString(ColorAllSegments);
                }

                if (SegmentF != null)
                {
                    SegmentF.Fill = (Brush)new BrushConverter().ConvertFromString(ColorAllSegments);
                }

                if (SegmentG != null)
                {
                   SegmentG.Fill = (Brush)new BrushConverter().ConvertFromString(ColorAllSegments);
                }

                if (SegmentDp != null)
                {
                    SegmentDp.Fill = (Brush)new BrushConverter().ConvertFromString(ColorAllSegments);
                }

            }));
        }

        private static void DisplayAktualsieren(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var siebensSegment = d as SiebenSegmentDisplay;
            siebensSegment?.SetValue();
        }

        private void SetValue()
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {

                if (SegmentA != null)
                {
                    SegmentA.Visibility = string.Equals(VisibilitySegmentA, "visible", StringComparison.OrdinalIgnoreCase) ? Visibility.Visible : Visibility.Hidden;
                    SegmentA.Fill = (Brush)new BrushConverter().ConvertFromString(ColorAllSegments);
                }

                if (SegmentB != null)
                {
                    SegmentB.Visibility = string.Equals(VisibilitySegmentB, "visible", StringComparison.OrdinalIgnoreCase) ? Visibility.Visible : Visibility.Hidden;
                    SegmentB.Fill = (Brush)new BrushConverter().ConvertFromString(ColorAllSegments);
                }

                if (SegmentC != null)
                {
                    SegmentC.Visibility = string.Equals(VisibilitySegmentC, "visible", StringComparison.OrdinalIgnoreCase) ? Visibility.Visible : Visibility.Hidden;
                    SegmentC.Fill = (Brush)new BrushConverter().ConvertFromString(ColorAllSegments);
                }

                if (SegmentD != null)
                {
                    SegmentD.Visibility = string.Equals(VisibilitySegmentD, "visible", StringComparison.OrdinalIgnoreCase) ? Visibility.Visible : Visibility.Hidden;
                    SegmentD.Fill = (Brush)new BrushConverter().ConvertFromString(ColorAllSegments);
                }

                if (SegmentE != null)
                {
                    SegmentE.Visibility = string.Equals(VisibilitySegmentE, "visible", StringComparison.OrdinalIgnoreCase) ? Visibility.Visible : Visibility.Hidden;
                    SegmentE.Fill = (Brush)new BrushConverter().ConvertFromString(ColorAllSegments);
                }

                if (SegmentF != null)
                {
                    SegmentF.Visibility = string.Equals(VisibilitySegmentF, "visible", StringComparison.OrdinalIgnoreCase) ? Visibility.Visible : Visibility.Hidden;
                    SegmentF.Fill = (Brush)new BrushConverter().ConvertFromString(ColorAllSegments);
                }

                if (SegmentG != null)
                {
                    SegmentG.Visibility = string.Equals(VisibilitySegmentG, "visible", StringComparison.OrdinalIgnoreCase) ? Visibility.Visible : Visibility.Hidden;
                    SegmentG.Fill = (Brush)new BrushConverter().ConvertFromString(ColorAllSegments);
                }

                if (SegmentDp != null)
                {
                    SegmentDp.Visibility = string.Equals(VisibilitySegmentDp, "visible", StringComparison.OrdinalIgnoreCase) ? Visibility.Visible : Visibility.Hidden;
                    SegmentDp.Fill = (Brush)new BrushConverter().ConvertFromString(ColorAllSegments);
                }

            }));
        }
    }
}