// by Antonis Ntit (antonis68)
// email: spanomarias68@gmail.com

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Windows.Controls.Canvas;

namespace GaugeControl
{
    public partial class GaugeControl
    {
        private readonly TweenMotion _motion = new();
        private FontFamily _fontFamily = new("Tahoma");
        private FontFamily _gaugeTextFont = new("Tahoma");
        private SolidColorBrush _labelColor = Brushes.Black;
        private SolidColorBrush _arcFirstColor = Brushes.Green;
        private SolidColorBrush _arcMidleColor = Brushes.Yellow;
        private SolidColorBrush _arcEndColor = Brushes.Red;
        private SolidColorBrush _arcOutBackColor = Brushes.Black;
        private SolidColorBrush _arcBackColor = Brushes.Ivory;
        private SolidColorBrush _needleColor = Brushes.Red;
        private SolidColorBrush _descrTextColor = Brushes.Black;
        private readonly System.Windows.Media.Effects.DropShadowEffect _shadow;
        private Rectangle _box;
        private Line _needle;
        private Label _lbl;
        private int _offset = 60;
        private int _arcLength = 240;
        private double _arcHeight = 15;
        private int _arcDiameter = 120;
        private int _arcX = 148;
        private int _arcY = 156;
        private int _gaugeTextX = 125;
        private int _gaugeTextY = 220;
        private int _arcScale = 1;
        private int _labelDiameter = 100;
        private int _labelOffset = 60;
        private int _labelX = 136;
        private int _labelY = 142;
        private int _labelQuantity = 10;
        private int _arcMinValue;
        private int _arcMaxValue = 100;
        private const int MaxW = 300;
        private const int MaxH = 300;
        private const int MinW = 80;
        private const int MinH = 80;
        private int _arcFirstColorEndPos = 100;
        private int _arcMidleColorEndPos = 130;
        private int _colorCount;
        private double _arcOpacity = 100;
        private double _needleHeigth;
        private int _startPos;
        private int _endPos;
        private double _initPos;
        private double _prevPos;
        private ArcStyles _arcStyles;
        private int _fontSize = 12;
        private double _labelOpacity = 1;
        private readonly bool _isFirst = true;
        private int _gaugeTextFontSize = 14;
        private string _descrText = "Gauge";
        private double _gaugeTextOpacity = 1;
        private Visibility _backVisible;


        // ReSharper disable UnusedMember.Global
        public enum ArcStyles { Flat, Αscending, Descending }
        // ReSharper restore UnusedMember.Global


        // ReSharper disable UnusedMember.Global
        public enum MotionType
        {
            EaseOutExpo, EaseOutCubic, EaseOutQuart, EaseOutQuint,
            EaseOutSine, EaseOutQuad, EaseOutCirc, LinearTween,
            EaseInOutQuart,
            EaseInQuint,
            EaseInOutQuint,
            EaseInOutSine,
            EaseInCirc,
            EaseInOutCirc,
            EaseInOutExpo,
            EaseInQuad,
            EaseInOutQuad,
            EaseInCubic,
            EaseInOutCubic,
            EaseInSine,
            EaseInExpo,
            EaseInQuart
        }
        // ReSharper restore UnusedMember.Global

        public GaugeControl()
        {
            InitializeComponent();
            _motion.MyMotion += MotionMyMotion;
            BackFront.Fill = _arcOutBackColor;
            Front.Fill = _arcBackColor;
            _shadow = new System.Windows.Media.Effects.DropShadowEffect { ShadowDepth = 5, Opacity = .4 };
            _needleHeigth = _arcHeight;
            if (_isFirst) { SetStyle(); }
            _isFirst = false;
        }

        private void MotionMyMotion(double value, MotionType motionType) =>
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                _shadow.Direction = value;
                _needle.Effect = _shadow;
                _needle.RenderTransform = new RotateTransform(value, ArcX, _arcY);
            }));

        public void SetValue(double value)
        {
            if (value <= _arcMinValue) { value = _arcMinValue; }
            else if (value >= _arcMaxValue) { value = _arcMaxValue; }
            value -= _arcMinValue;
            var factor = (double)_arcLength / (_arcMaxValue - _arcMinValue);
            value *= factor;
            _initPos = 90 - (ArcLength + ArcOffset);
            _startPos = (int)_initPos + (int)_prevPos;
            _endPos = (int)_initPos + (int)value;
            _motion.Start(GaugeMotionType, _startPos, _endPos, GaugeTime);
            _prevPos = value;
        }

        private void SetStyle()
        {
            RemoveChildren();
            _colorCount = ArcLength;
            double counter = 0;
            double tempH = 0; // temporary height 
            _box = null;
            double noFlat = 0;
            var arcHeight = _arcHeight;
            NeedleFront.Margin = new Thickness { Left = _arcX - 10, Top = _arcY - 60 };
            if (_arcStyles.ToString() == "Flat")
            {
                tempH = arcHeight;
            }
            else
            {
                noFlat = arcHeight / _arcLength;
                if (_arcStyles.ToString() == "Αscending")
                {
                    tempH = 0;
                    counter = arcHeight / _arcScale + 1;
                    noFlat *= -1;
                }
            }

            for (var i = _offset; i < _arcLength + _offset + _arcScale; i += _arcScale)
            {
                counter += noFlat;
                arcHeight = tempH + counter;
                _box = new Rectangle { Width = arcHeight, Height = 4, Opacity = _arcOpacity / 100 };
                if (_colorCount > _arcMidleColorEndPos) { _box.Fill = _arcEndColor; }
                else if (_colorCount > _arcFirstColorEndPos) { _box.Fill = _arcMidleColor; }
                else { _box.Fill = ArcFirstColor; }
                _box.Name = "box_" + i;
                var radial = i * (Math.PI / 180);
                var sin = Math.Sin(radial) * _arcDiameter;
                var cos = Math.Cos(radial) * _arcDiameter;
                SetLeft(_box, _arcX + sin);
                SetTop(_box, ArcY + cos);
                _box.RenderTransform = new RotateTransform(90 - i);
                Canvas.Children.Add(_box);
                _colorCount -= _arcScale;

            }
            Canvas.Children.Add(SetNeedle());
            Panel.SetZIndex(_needle, 1);
            SetLabel();
        }

        private Line SetNeedle()
        {
            var tail = (_arcDiameter + _needleHeigth) * (10f / 100f) - 8;
            _needle = new Line { X1 = ArcX - 30 - tail, X2 = ArcX + _arcDiameter + _needleHeigth - 8 };
            _needle.Y1 = _needle.Y2 = _arcY;
            _needle.Stroke = _needleColor;
            _needle.StrokeThickness = 4;
            _needle.StrokeEndLineCap = PenLineCap.Triangle;
            _needle.RenderTransform = new RotateTransform(90 - (_arcLength + ArcOffset), ArcX, _arcY);
            _needle.Effect = _shadow;
            return _needle;
        }

        private void SetLabel()
        {
            double labelCount = _arcMaxValue;
            var modul = (double)_arcLength / _labelQuantity;
            double labelScale = _arcMaxValue - _arcMinValue;
            labelScale /= _labelQuantity;
            for (var i = 0; i <= _arcLength; i++)
            {
                if (i % modul != 0) continue;
                _lbl = new Label
                {
                    Name = "label_" + i,
                    Content = Math.Round(labelCount, 1).ToString("F1"),
                    Foreground = _labelColor,
                    FontFamily = _fontFamily,
                    FontSize = _fontSize,
                    Opacity = _labelOpacity
                };
                var radial = (i + _labelOffset) * (Math.PI / 180);
                var sin = Math.Sin(radial) * _labelDiameter;
                var cos = Math.Cos(radial) * _labelDiameter;
                SetLeft(_lbl, _labelX + sin);
                SetTop(_lbl, _labelY + cos);
                Canvas.Children.Add(_lbl);
                labelCount -= labelScale;

            }
            var descrLbl = GetDescrText();
            SetLeft(descrLbl, _gaugeTextX);
            SetTop(descrLbl, _gaugeTextY);
            Canvas.Children.Add(descrLbl);
        }

        private Label GetDescrText()
        {
            var descrLbl = new Label
            {
                Name = "descr_label",
                Content = _descrText,
                FontFamily = _gaugeTextFont,
                FontSize = _gaugeTextFontSize,
                Foreground = _descrTextColor
            };
            return descrLbl;
        }
        public void RemoveChildren() => Canvas.Children.Clear();

        // ReSharper disable once UnusedMember.Local
        private void RemoveLabels()
        {
            foreach (UIElement element in Canvas.Children)
            {
                if (!(element is Label)) continue;

                var lbl = (Label)element;
                if (lbl.Name != "descr_label")
                {
                    Canvas.Children.Remove(lbl);
                }
            }
        }
        // Change the labels
        private void ChangeLabels()
        {
            foreach (UIElement element in Canvas.Children)
            {
                if (!(element is Label)) continue;

                var lbl = (Label)element;
                if (lbl.Name != "descr_label")
                {
                    lbl.Foreground = _labelColor;
                    lbl.Opacity = _labelOpacity;
                    lbl.FontSize = _fontSize;
                    lbl.FontFamily = _fontFamily;
                }
                else
                {
                    lbl.Content = _descrText;
                    lbl.Foreground = _descrTextColor;
                    lbl.FontFamily = GaugeTextFont;
                    lbl.FontSize = _gaugeTextFontSize;
                    lbl.Opacity = _gaugeTextOpacity;
                    SetLeft(lbl, _gaugeTextX);
                    SetTop(lbl, _gaugeTextY);
                }
            }
        }


        // ---------------------- public variable
        [Description("(Gauge) Set the time raise of the needle"), Category("Gauge Control")]
        public double GaugeTime { set; get; } = 1;

        [Description("(Gauge) Choose the motion type of the needle. "), Category("Gauge Control")]
        public MotionType GaugeMotionType { set; get; }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Change opacity of the description text (0-100) . int"), Category("Gauge Control")]
        public double GaugeTextOpacity
        {
            set
            {
                if (value <= 0) { value = 0; }
                else if (value >= 100) { value = 100; }
                _gaugeTextOpacity = value / 100;
                ChangeLabels();
            }
            get => _gaugeTextOpacity * 100;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Change font size of the description text . int"), Category("Gauge Control")]
        public int GaugeTextFontSize
        {
            set
            {
                if (value >= 80) { value = 80; }
                _gaugeTextFontSize = value;
                ChangeLabels();
            }
            get => _gaugeTextFontSize;
        }

        [Description("(Gauge) Choose font of the description text"), Category("Gauge Control")]
        public FontFamily GaugeTextFont
        {
            set
            {
                _gaugeTextFont = value;
                ChangeLabels();
            }
            get => _gaugeTextFont;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Set X position of the description text . int"), Category("Gauge Control")]
        public int GaugeTextX
        {
            set
            {
                _gaugeTextX = value;
                ChangeLabels();
            }
            get => _gaugeTextX;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Set Y position of the description text . int"), Category("Gauge Control")]
        public int GaugeTextY
        {
            set
            {
                _gaugeTextY = value;
                ChangeLabels();
            }
            get => _gaugeTextY;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Set color of the description text. string "), Category("Gauge Control")]
        public SolidColorBrush GaugeTextColor
        {
            set
            {
                _descrTextColor = value;
                ChangeLabels();
            }
            get => _descrTextColor;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Set description text of the gauge. string "), Category("Gauge Control")]
        public string GaugeText
        {
            set
            {
                _descrText = value;
                ChangeLabels();
            }
            get => _descrText;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Set the length of the needle. double "), Category("Gauge Control")]
        public double NeedleLength
        {
            set
            {
                _needleHeigth = value;
                SetStyle();
            }
            get => _needleHeigth;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Choose how much numbers labels you want to visible (Default is 10).The number must be an integer divider of the ArcLength"), Category("Gauge Control")]
        public int LabelQuantity
        {
            set
            {
                if (value <= 1) { value = 1; }
                if (_arcLength % value != 0) { return; }
                _labelQuantity = value;
                SetStyle();
            }
            get => _labelQuantity;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Numbers labels opacity (0-100) . double"), Category("Gauge Control")]
        public double LabelOpacity
        {
            set
            {
                if (value <= 0) { value = 0; }
                else if (value >= 100) { value = 100; }
                value /= 100;
                _labelOpacity = value;
                ChangeLabels();
            }
            get => _labelOpacity * 100;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Numbers labels font size. 6 - 72"), Category("Gauge Control")]
        public int LabelFontSize
        {
            set
            {
                if (value <= 6) { value = 6; }
                else if (value >= 72) { value = 72; }
                _fontSize = value;
                ChangeLabels();
            }
            get => _fontSize;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Choose numbers labels font "), Category("Gauge Control")]
        public FontFamily LabelFont
        {
            set
            {
                _fontFamily = value;
                ChangeLabels();

            }
            get => _fontFamily;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Set the minimum number needle point. int "), Category("Gauge Control")]
        public int ArcMinValue
        {
            set
            {
                if (value >= _arcMaxValue) { return; }
                _arcMinValue = value;
                SetStyle();
            }
            get => _arcMinValue;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Set the maximum number needle point. int "), Category("Gauge Control")]
        public int ArcMaxValue
        {
            set
            {
                if (value <= _arcMinValue) { return; }
                _arcMaxValue = value;
                SetStyle();
            }
            get => _arcMaxValue;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Test the gauge.Give value between minimum and maximum"), Category("Gauge Control")]
        public double TestGauge
        {
            set => SetValue(value);
            get => 0;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Arc style "), Category("Gauge Control")]
        public ArcStyles ArcStyle
        {
            set
            {
                _arcStyles = value;
                SetStyle();
            }
            get => _arcStyles;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Numbers labelscolor "), Category("Gauge Control")]
        public SolidColorBrush LabelColor
        {
            set
            {
                _labelColor = value;
                ChangeLabels();
            }
            get => _labelColor;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Numbers labels diameter. int "), Category("Gauge Control")]
        public int LabelDiameter
        {
            set
            {
                _labelDiameter = value;
                SetStyle();
            }
            get => _labelDiameter;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Numbers labels offset. int "), Category("Gauge Control")]
        public int LabelOffset
        {
            set
            {
                _labelOffset = value;
                SetStyle();
            }
            get => _labelOffset;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Set X position of numbers labels. int "), Category("Gauge Control")]
        public int LabelX
        {
            set
            {
                _labelX = value;
                SetStyle();
            }
            get => _labelX;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Set Y position of numbers labels. int "), Category("Gauge Control")]
        public int LabelY
        {
            set
            {
                _labelY = value;
                SetStyle();
            }
            get => _labelY;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Set neddle color"), Category("Gauge Control")]
        public SolidColorBrush NeedleColor
        {
            set => _needle.Stroke = _needleColor = value;
            get => _needleColor;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Set gauge width (80-300)  . double "), Category("Gauge Control")]
        public double ArcBackWidth
        {
            set
            {
                if (value >= MaxW) { value = MaxW; }
                else if (value <= MinW) { value = MinW; }
                Back.Width = BackFront.Width = value;
                Front.Width = FrontBack.Width = GlassCanvas.Width = value - 20;
                FrontBackMask.Width = value - 45;
            }
            get => Back.Width;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Set gauge height (80-300)  . double "), Category("Gauge Control")]
        public double ArcBackHeight
        {
            set
            {
                if (value >= MaxH) { value = MaxH; }
                else if (value <= MinH) { value = MinH; }
                Back.Height = BackFront.Height = value;
                Front.Height = FrontBack.Height = GlassCanvas.Height = value - 20;
                FrontBackMask.Height = value - 45;
            }
            get => Back.Height;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Arc opacity 0-100  . int "), Category("Gauge Control")]
        public double ArcOpacity
        {
            set
            {
                if (value <= 0) { value = 0; }
                else if (value >= 100) { value = 100; }
                _arcOpacity = value;
                SetStyle();
            }
            get => _arcOpacity;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Set X position of the arc . int "), Category("Gauge Control")]
        public int ArcX
        {
            set
            {
                _arcX = value;
                SetStyle();
            }
            get => _arcX;
        }

        [Description("(Gauge) Set Y position of the arc . int "), Category("Gauge Control")]
        public int ArcY
        {
            set
            {
                _arcY = value;
                SetStyle();
            }
            get => _arcY;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Set gauge around color . "), Category("Gauge Control")]
        public SolidColorBrush ArcOutBackColor
        {
            set
            {
                _arcOutBackColor = value;
                BackFront.Fill = value;
            }
            get => _arcOutBackColor;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Set gauge background color . "), Category("Gauge Control")]
        public SolidColorBrush ArcBackColor
        {
            set
            {
                _arcBackColor = value;
                Front.Fill = value;
            }
            get => _arcBackColor;
        }

        [Description("(Gauge) Set first color in arc. "), Category("Gauge Control")]
        public SolidColorBrush ArcFirstColor
        {
            set
            {
                _arcFirstColor = value;
                SetStyle();
            }
            get => _arcFirstColor;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Set secondly color in arc. "), Category("Gauge Control")]
        public SolidColorBrush ArcMidleColor
        {
            set
            {
                _arcMidleColor = value;
                SetStyle();
            }
            get => _arcMidleColor;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Set end position last color of the arc."), Category("Gauge Control")]
        public SolidColorBrush ArcEndColor
        {
            set
            {
                _arcEndColor = value;
                SetStyle();
            }
            get => _arcEndColor;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Set end position first color of the arc."), Category("Gauge Control")]
        public int ArcFirstColorEndPos
        {
            set
            {
                _arcFirstColorEndPos = value;
                SetStyle();
            }
            get => _arcFirstColorEndPos;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Set end position secondly color of the arc."), Category("Gauge Control")]
        public int ArcMidleColorEndPos
        {
            set
            {
                _arcMidleColorEndPos = value;
                SetStyle();
            }
            get => _arcMidleColorEndPos;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Arc scale. int "), Category("Gauge Control")]
        public int ArcScale
        {
            set
            {
                if (value <= 0) { value = 1; }
                _arcScale = value;
                SetStyle();
            }
            get => _arcScale;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Arc offset length. int "), Category("Gauge Control")]
        public int ArcOffset
        {
            set
            {
                if (value <= 0) { value = 0; }
                _offset = value;
                SetStyle();
                // InvalidateVisual();
            }
            get => _offset;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Arc diameter. int "), Category("Gauge Control")]
        public int ArcDiameter
        {
            set
            {
                if (value <= 0) { value = 0; }
                _arcDiameter = value;
                SetStyle();
            }
            get => _arcDiameter;
        }

        [Description("(Gauge) Arc periphery length. int "), Category("Gauge Control")]
        public int ArcLength
        {
            set
            {
                if (value <= 0) { value = 0; }
                else if (value >= 360) { value = 360; }
                _arcLength = value;
                SetStyle();
            }
            get => _arcLength;
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Arc height. double "), Category("Gauge Control")]
        public double ArcHeight
        {
            set
            {
                if (value <= 1) { value = 1; }
                _arcHeight = value;
                SetStyle();
            }
            get => Math.Round(_arcHeight);
        }

        // ReSharper disable once UnusedMember.Global
        [Description("(Gauge) Hide background of the gauge "), Category("Gauge Control")]
        public Visibility GaugeBackVisible
        {
            set
            {
                _backVisible = value;
                SichtbarkeitSetzen(_backVisible);
            }
            get => _backVisible;
        }

        private void SichtbarkeitSetzen(Visibility v)
        {
            Front.Visibility = FrontBack.Visibility = FrontBackMask.Visibility = v;
            BackFront.Visibility = Back.Visibility = Front.Visibility = GlassCanvas.Visibility = v;
            GesamtesGrid.Visibility = v;
        }

        private Visibility _sichtbarkeit;
        public Visibility Sichtbarkeit
        {
            get => _sichtbarkeit;
            set
            {
                _sichtbarkeit = value;
                SetValue(SichtbarkeitValueProperty, _sichtbarkeit);
            }
        }

        public static readonly DependencyProperty SichtbarkeitValueProperty =
            DependencyProperty.Register("Sichtbarkeit", typeof(Visibility), typeof(GaugeControl),
                new PropertyMetadata(Visibility.Visible, OnSichtbarkeitPropertyChanged));

        private static void OnSichtbarkeitPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var neuerWert = (Visibility)e.NewValue;
            var gauge = d as GaugeControl;
            gauge?.SichtbarkeitSetzen(neuerWert);
        }


        private double _currentValue;
        public double CurrentValue
        {
            get => _currentValue;
            set
            {
                _currentValue = value;
                SetValue(CurrentValueProperty, _currentValue);
            }
        }

        public static readonly DependencyProperty CurrentValueProperty =
            DependencyProperty.Register("CurrentValue", typeof(double), typeof(GaugeControl),
                new PropertyMetadata(double.MinValue, OnCurrentValuePropertyChanged));

        private static void OnCurrentValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var neuerWert = (double)e.NewValue;
            var gauge = d as GaugeControl;
            gauge?.SetValue(neuerWert);
        }
    }
}
