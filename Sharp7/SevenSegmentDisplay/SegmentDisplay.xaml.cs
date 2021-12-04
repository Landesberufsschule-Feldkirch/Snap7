using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace SegmentDisplay;

public partial class SegmentDisplay
{
    private Brush _colorBackground;
    private Brush _colorSegments;

    private Visibility _segmentA;
    private Visibility _segmentB;
    private Visibility _segmentC;
    private Visibility _segmentD;
    private Visibility _segmentE;
    private Visibility _segmentF;
    private Visibility _segmentG;
    private Visibility _segmentDp;

    public SegmentDisplay()
    {
        ColorBackground = Brushes.AliceBlue;
        ColorSegments = Brushes.Violet;

        SegmentA = Visibility.Visible;
        SegmentB = Visibility.Visible;
        SegmentC = Visibility.Visible;
        SegmentD = Visibility.Visible;
        SegmentE = Visibility.Visible;
        SegmentF = Visibility.Visible;
        SegmentG = Visibility.Visible;
        SegmentDp = Visibility.Visible;

        InitializeComponent();
    }



    [Description("(Display) ColorBackground"), Category("Segment Display")]
    public Brush ColorBackground
    {
        set
        {
            _colorBackground = value;
            SetValue(ColorBackgroundProperty, _colorBackground);
        }
        get => _colorBackground;
    }

    public static readonly DependencyProperty ColorBackgroundProperty =
        DependencyProperty.Register("ColorBackground", typeof(SolidColorBrush), typeof(SegmentDisplay),
            new PropertyMetadata(OnDisplayChanged));


    [Description("(Display) ColorSegments"), Category("Segment Display")]
    public Brush ColorSegments
    {
        set
        {
            _colorSegments = value;
            SetValue(ColorSegmentsProperty, _colorSegments);
        }
        get => _colorSegments;
    }

    public static readonly DependencyProperty ColorSegmentsProperty =
        DependencyProperty.Register("ColorSegments", typeof(SolidColorBrush), typeof(SegmentDisplay),
            new PropertyMetadata(OnDisplayChanged));




    [Description("(Display) SegmentA"), Category("Segment Display")]
    public Visibility SegmentA
    {
        get => _segmentA;
        set
        {
            _segmentA = value;
            SetValue(SegmentAProperty, _segmentA);
        }
    }

    public static readonly DependencyProperty SegmentAProperty =
        DependencyProperty.Register("SegmentA", typeof(Visibility), typeof(SegmentDisplay),
            new PropertyMetadata(OnDisplayChanged));



    [Description("(Display) SegmentB"), Category("Segment Display")]
    public Visibility SegmentB
    {
        get => _segmentB;
        set
        {
            _segmentB = value;
            SetValue(SegmentBProperty, _segmentB);
        }
    }

    public static readonly DependencyProperty SegmentBProperty =
        DependencyProperty.Register("SegmentB", typeof(Visibility), typeof(SegmentDisplay),
            new PropertyMetadata(OnDisplayChanged));

    [Description("(Display) SegmentC"), Category("Segment Display")]
    public Visibility SegmentC
    {
        get => _segmentC;
        set
        {
            _segmentC = value;
            SetValue(SegmentCProperty, _segmentC);
        }
    }

    public static readonly DependencyProperty SegmentCProperty =
        DependencyProperty.Register("SegmentC", typeof(Visibility), typeof(SegmentDisplay),
            new PropertyMetadata(OnDisplayChanged));


    [Description("(Display) SegmentD"), Category("Segment Display")]
    public Visibility SegmentD
    {
        get => _segmentD;
        set
        {
            _segmentD = value;
            SetValue(SegmentDProperty, _segmentD);
        }
    }

    public static readonly DependencyProperty SegmentDProperty =
        DependencyProperty.Register("SegmentD", typeof(Visibility), typeof(SegmentDisplay),
            new PropertyMetadata(OnDisplayChanged));

    [Description("(Display) SegmentE"), Category("Segment Display")]
    public Visibility SegmentE
    {
        get => _segmentE;
        set
        {
            _segmentE = value;
            SetValue(SegmentEProperty, _segmentE);
        }

    }

    public static readonly DependencyProperty SegmentEProperty =
        DependencyProperty.Register("SegmentE", typeof(Visibility), typeof(SegmentDisplay),
            new PropertyMetadata(OnDisplayChanged));


    [Description("(Display) SegmentF"), Category("Segment Display")]
    public Visibility SegmentF
    {
        get => _segmentF;
        set
        {
            _segmentF = value;
            SetValue(SegmentFProperty, _segmentF);
        }
    }

    public static readonly DependencyProperty SegmentFProperty =
        DependencyProperty.Register("SegmentF", typeof(Visibility), typeof(SegmentDisplay),
            new PropertyMetadata(OnDisplayChanged));


    [Description("(Display) SegmentG"), Category("Segment Display")]
    public Visibility SegmentG
    {
        get => _segmentG;
        set
        {
            _segmentG = value;
            SetValue(SegmentGProperty, _segmentG);
        }
    }

    public static readonly DependencyProperty SegmentGProperty =
        DependencyProperty.Register("SegmentG", typeof(Visibility), typeof(SegmentDisplay),
            new PropertyMetadata(OnDisplayChanged));


    [Description("(Display) SegmentDp"), Category("Segment Display")]
    public Visibility SegmentDp
    {
        get => _segmentDp;
        set
        {
            _segmentDp = value;
            SetValue(SegmentDpProperty, _segmentDp);
        }
    }

    public static readonly DependencyProperty SegmentDpProperty =
        DependencyProperty.Register("SegmentDp", typeof(Visibility), typeof(SegmentDisplay),
            new PropertyMetadata(OnDisplayChanged));



    private static void OnDisplayChanged(DependencyObject obj, DependencyPropertyChangedEventArgs arg)
    {
        if (!(obj is SegmentDisplay siebenSegment)) return;

        switch (arg.Property.Name)
        {
            case "SegmentA": siebenSegment.SegmentA = (Visibility)arg.NewValue; break;
            case "SegmentB": siebenSegment.SegmentB = (Visibility)arg.NewValue; break;
            case "SegmentC": siebenSegment.SegmentC = (Visibility)arg.NewValue; break;
            case "SegmentD": siebenSegment.SegmentD = (Visibility)arg.NewValue; break;
            case "SegmentE": siebenSegment.SegmentE = (Visibility)arg.NewValue; break;
            case "SegmentF": siebenSegment.SegmentF = (Visibility)arg.NewValue; break;
            case "SegmentG": siebenSegment.SegmentG = (Visibility)arg.NewValue; break;
            case "SegmentDp": siebenSegment.SegmentDp = (Visibility)arg.NewValue; break;

            case "ColorBackground": siebenSegment.ColorBackground = (SolidColorBrush)arg.NewValue; break;
            case "ColorSegments": siebenSegment.ColorSegments = (SolidColorBrush)arg.NewValue; break;
        }
        siebenSegment.SetValue();
    }


    private void SetValue()
    {
        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
        {
            if (HintergrundRechteck != null)
            {
                HintergrundRechteck.Fill = ColorBackground;
            }

            if (SegA != null)
            {
                SegA.Visibility = SegmentA;
                SegA.Fill = ColorSegments;
            }

            if (SegB != null)
            {
                SegB.Visibility = SegmentB;
                SegB.Fill = ColorSegments;
            }

            if (SegC != null)
            {
                SegC.Visibility = SegmentC;
                SegC.Fill = ColorSegments;
            }

            if (SegD != null)
            {
                SegD.Visibility = SegmentD;
                SegD.Fill = ColorSegments;
            }

            if (SegE != null)
            {
                SegE.Visibility = SegmentE;
                SegE.Fill = ColorSegments;
            }

            if (SegF != null)
            {
                SegF.Visibility = SegmentF;
                SegF.Fill = ColorSegments;
            }

            if (SegG != null)
            {
                SegG.Visibility = SegmentG;
                SegG.Fill = ColorSegments;
            }

            if (SegDp == null) return;

            SegDp.Visibility = SegmentDp;
            SegDp.Fill = ColorSegments;

        }));
    }
}