// by Antonis Ntit (antonis68)
// email: spanomarias68@gmail.com

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GaugeControl
{

    public partial class GaugeControl
    {
       
        private readonly TweenMotionLib.TweenMotion motion = new TweenMotionLib.TweenMotion();
        private FontFamily fontFamily = new FontFamily("Tahoma");
        private FontFamily gaugeTextFont = new FontFamily("Tahoma");
        private SolidColorBrush labelColor = Brushes.Black;
        private SolidColorBrush arcFirstColor = Brushes.Green;
        private SolidColorBrush arcMidleColor = Brushes.Yellow;
        private SolidColorBrush arcEndColor = Brushes.Red;
        private SolidColorBrush arcOutBackColor = Brushes.Black;
        private SolidColorBrush arcBackColor = Brushes.Ivory;
        private SolidColorBrush needleColor = Brushes.Red;
        private SolidColorBrush descrTextColor = Brushes.Black;
        readonly System.Windows.Media.Effects.DropShadowEffect shadow;
        private Rectangle box;
        private Line needle;
        private Label lbl;
        private int offset = 60;
        private int arcLength = 240;
        private double arcHeight = 15;
        private int arcDiameter = 120;
        private int arcX = 148;
        private int arcY = 156;
        private int gaugeTextX=125;
        private int gaugeTextY=220;
        private int arcScale = 1;
        private int labelDiameter = 100;
        private int labelOffset = 60;
        private int labelX = 136;
        private int labelY = 142;
        private int labelQuantity = 10;
        private int arcMinValue;
        private int arcMaxValue = 100;
        private const int maxW = 300;
        private const int maxH = 300;
        private const int minW = 80;
        private const int minH = 80;
        private int arcFirstColorEndPos = 100;
        private int arcMidleColorEndPos = 130;
        private int colorCount;
        private double arcOpacity = 100;
        private double needleHeigth;
        private int startPos;
        private int endPos;
        private double initPos;
        private double prevPos;
        private arcStyle _arcStyle;
        private int fontSize=12;
        private double labelOpacity = 1;
        private readonly bool isFirst = true;
        private int gaugeTextFontSize = 14;
        private string descrText = "Gauge";
        private double gaugeTextOpacity = 1;
        private Visibility backVisible;

        public enum arcStyle { Flat, Αscending, Descending }

        // Types of motion 
        public enum motionType
        {
            easeOutExpo, easeOutCubic, easeOutQuart, easeOutQuint,
            easeOutSine, easeOutQuad, easeOutCirc,linearTween
        }
           
        public GaugeControl()
        {
            InitializeComponent();
            motion.onMotion += Motion_onMotion;
            BackFront.Fill = arcOutBackColor;
            Front.Fill = arcBackColor;
            shadow = new System.Windows.Media.Effects.DropShadowEffect {ShadowDepth = 5, Opacity = .4};
            needleHeigth = arcHeight;
            if (isFirst) { SetStyle(); }
            isFirst = false;
            
          
        }
        // Event of TweenMotion
        private void Motion_onMotion(double value, string type)
        {        
            shadow.Direction = value;
            needle.Effect = shadow;
            needle.RenderTransform = new RotateTransform(value, ArcX, arcY);           
        }
        /// <summary>
        ///  Set value to the gauge
        /// </summary>
        /// <param name="value"></param>
        public void setValue(double value)
        {
            if (value <= arcMinValue) { value = arcMinValue; }
            else if (value >= arcMaxValue) { value = arcMaxValue; }
            value -= arcMinValue;
            var factor = (double)arcLength / (arcMaxValue - arcMinValue);
            value *= factor;
            initPos = 90 - (ArcLength + ArcOffset);
            startPos = (int)initPos + (int)prevPos;
            endPos = (int)initPos + (int)value;
            motion.Start(GaugeMotionType.ToString(), startPos, endPos, GaugeTime);
            prevPos = value;
        }
        // Set style of control
        private void SetStyle()
        {
            removeChildren();
            colorCount = ArcLength;
            double counter = 0;
            double tempH = 0; // temporary height 
            box = null;
            double noFlat = 0;
            var _arcHeight = arcHeight;
            NeedleFront.Margin = new Thickness { Left = arcX - 10, Top = arcY - 60 };
            if (_arcStyle.ToString() == "Flat")
            {
              tempH = _arcHeight;
            }
            else
            {
                noFlat = _arcHeight / arcLength;
                if (_arcStyle.ToString() == "Αscending")
                {
                    tempH = 0;
                    counter = _arcHeight/ arcScale + 1;
                    noFlat *= -1;
                }
                
            }                      

            for (var i = offset; i < arcLength + offset+arcScale; i+=arcScale)
            {               
                counter += noFlat;
                _arcHeight = tempH + counter;
                box = new Rectangle {Width = _arcHeight, Height = 4, Opacity = arcOpacity / 100};
                if (colorCount > arcMidleColorEndPos) { box.Fill = arcEndColor; }
                else if (colorCount > arcFirstColorEndPos) { box.Fill = arcMidleColor; }
                else { box.Fill = ArcFirstColor; }
                box.Name = "box_" + i;
                var radial = i * (Math.PI / 180);
                var sin = Math.Sin(radial) * arcDiameter;
                var cos = Math.Cos(radial) * arcDiameter;
                Canvas.SetLeft(box, arcX + sin);
                Canvas.SetTop(box, ArcY + cos);
                box.RenderTransform = new RotateTransform(90 - i);
                Canvas.Children.Add(box);
                colorCount -= arcScale;
               
            }
            Canvas.Children.Add(setNeedle());
            Canvas.SetZIndex(needle, 1);
            setLabel();
            
        }
        // Create the needle
        private Line setNeedle()
        {
            var tail= (arcDiameter + needleHeigth) * (10f/100f)-8;
            needle = new Line {X1 = ArcX - 30 - tail, X2 = ArcX + arcDiameter + needleHeigth - 8};
            needle.Y1 = needle.Y2 = arcY;
            needle.Stroke = needleColor;
            needle.StrokeThickness = 4;
            needle.StrokeEndLineCap = PenLineCap.Triangle;
            needle.RenderTransform = new RotateTransform(90 - (arcLength + ArcOffset), ArcX, arcY);
            needle.Effect = shadow;
            return needle;
        }
        // Create the labels
        private void setLabel()
        {
            double labelCount = arcMaxValue;
            double modul =  arcLength/labelQuantity;
            double labelScale =  arcMaxValue - arcMinValue;
            labelScale /= labelQuantity;
            for (var i = 0; i <= arcLength; i++)
            {
                if (i % modul != 0) continue;
                lbl = new Label
                {
                    Name = "label_" + i,
                    Content = Math.Round(labelCount, 1).ToString(),
                    Foreground = labelColor,
                    FontFamily = fontFamily,
                    FontSize = fontSize,
                    Opacity = labelOpacity
                };
                var radial = (i + labelOffset ) * (Math.PI / 180);
                var sin = Math.Sin(radial) * labelDiameter;
                var cos = Math.Cos(radial) * labelDiameter;
                Canvas.SetLeft(lbl, labelX + sin);
                Canvas.SetTop(lbl, labelY + cos);
                Canvas.Children.Add(lbl);
                labelCount -= labelScale;

            }
            var _descrLbl = getDescrText();
            Canvas.SetLeft(_descrLbl, gaugeTextX);
            Canvas.SetTop(_descrLbl, gaugeTextY);
            Canvas.Children.Add( _descrLbl );
            
        }
        // Description text
        private Label getDescrText()
        {
            var descrLbl = new Label
            {
                Name = "descr_label",
                Content = descrText,
                FontFamily = gaugeTextFont,
                FontSize = gaugeTextFontSize,
                Foreground = descrTextColor
            };
            return descrLbl;
        }
        public void removeChildren()
        {
            Canvas.Children.Clear();
        }
        private void removeLabels()
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
        private void changeLabels()
        {
            foreach (UIElement element in Canvas.Children)
            {
                if (!(element is Label)) continue;

                var lbl = (Label)element;
                if (lbl.Name != "descr_label")
                {
                    lbl.Foreground = labelColor;
                    lbl.Opacity = labelOpacity;
                    lbl.FontSize = fontSize;
                    lbl.FontFamily = fontFamily;
                        
                }
                else
                {
                    lbl.Content = descrText;
                    lbl.Foreground = descrTextColor;
                    lbl.FontFamily = GaugeTextFont;
                    lbl.FontSize = gaugeTextFontSize;
                    lbl.Opacity = gaugeTextOpacity;
                    Canvas.SetLeft(lbl, gaugeTextX);
                    Canvas.SetTop(lbl, gaugeTextY);
                }
            }
        }


        // ---------------------- public variable
        [Description("(Gauge) Set the time raise of the needle"),Category("Gauge Control") ]
        public double GaugeTime { set; get; } = 1;

        [Description("(Gauge) Choose the motion type of the needle. "), Category("Gauge Control")]
        public motionType GaugeMotionType { set; get; }

        [Description("(Gauge) Change opacity of the description text (0-100) . int"), Category("Gauge Control")]
        public double GaugeTextOpacity
        {
            set
            {
                if (value <= 0) { value = 0; }
                else if (value >= 100) { value = 100; }
                gaugeTextOpacity = value / 100;
                changeLabels();
            }
            get => gaugeTextOpacity*100;
        }

        [Description("(Gauge) Change font size of the description text . int"), Category("Gauge Control")]
        public int GaugeTextFontSize
        {
            set
            {
                if (value >= 80) { value = 80; }
                gaugeTextFontSize = value;
                changeLabels();
            }
            get => gaugeTextFontSize;
        }

        [Description("(Gauge) Choose font of the description text"), Category("Gauge Control")]
        public FontFamily GaugeTextFont
        {
            set
            {
                gaugeTextFont = value;
                changeLabels();
            }
            get => gaugeTextFont;
        }

        [Description("(Gauge) Set X position of the description text . int"), Category("Gauge Control")]
        public int GaugeTextX
        {
            set
            {
                gaugeTextX = value;
                changeLabels();
            }
            get => gaugeTextX;
        }

        [Description("(Gauge) Set Y position of the description text . int"), Category("Gauge Control")]
        public int GaugeTextY
        {
            set
            {
               gaugeTextY = value;
               changeLabels();
            }
            get => gaugeTextY;
        }

        [Description("(Gauge) Set color of the description text. string "), Category("Gauge Control")]
        public SolidColorBrush GaugeTextColor
        {
            set
            {
                descrTextColor = value;
                changeLabels();
            }
            get => descrTextColor;
        }

        [Description("(Gauge) Set description text of the gauge. string "), Category("Gauge Control")]
        public string GaugeText
        {
            set
            {
                descrText = value;
                changeLabels();
            }
            get => descrText;
        }

        [Description("(Gauge) Set the length of the needle. double "), Category("Gauge Control")]
        public double NeedleLength
        {
            set
            {
                needleHeigth = value;
                SetStyle();
                   
            }
            get => needleHeigth;
        }

        [Description("(Gauge) Choose how much numbers labels you want to visible (Default is 10).The number must be an integer divider of the ArcLength"), Category("Gauge Control")]
        public int LabelQuantity
        {
            set
            {
                if (value <= 1) { value = 1; }
                if ( arcLength % value != 0) { return; }
                labelQuantity = value;
                SetStyle();
            }
            get => labelQuantity;
        }

        [Description("(Gauge) Numbers labels opacity (0-100) . double"), Category("Gauge Control")]
        public double LabelOpacity
        {
        set
            {
                if (value <= 0){ value = 0; }
                else if (value >= 100) { value = 100; }
                value/= 100;
                labelOpacity = value;
                changeLabels();
                 
            }
            get => labelOpacity*100;
        }

        [Description("(Gauge) Numbers labels font size. 6 - 72"), Category("Gauge Control")]
        public int LabelFontSize
            {
             set
             {
                if (value <= 6) { value = 6; }
                else if (value >= 72) { value = 72; }
                fontSize = value;
                changeLabels();
             }
              get => fontSize;
        }

        [Description("(Gauge) Choose numbers labels font "), Category("Gauge Control")]
        public FontFamily LabelFont
        {

            set
            {
                fontFamily = value;
                changeLabels();
                
            }
            get => fontFamily;
        }

        [Description("(Gauge) Set the minimum number needle point. int "), Category("Gauge Control")]
        public int ArcMinValue
        {
            set
            {
                if (value >= arcMaxValue) { return; }
                arcMinValue = value;
                SetStyle();
            }
            get => arcMinValue;
        }

        [Description("(Gauge) Set the maximum number needle point. int "), Category("Gauge Control")]
        public int ArcMaxValue
        {
            set
            {
                if (value <= arcMinValue) { return; }
                arcMaxValue = value;
                SetStyle();
            }
            get => arcMaxValue;
        }

        [Description("(Gauge) Test the gauge.Give value between minimum and maximum"), Category("Gauge Control")]
        public double TestGauge
        {
            set => setValue(value);
            get => 0;
        }

        [Description("(Gauge) Arc style "), Category("Gauge Control")]
        public arcStyle ArcStyle
        {
            set
            {             
                _arcStyle = value;
                SetStyle();
            }
            get => _arcStyle;
        }

        [Description("(Gauge) Numbers labelscolor "), Category("Gauge Control")]
        public SolidColorBrush LabelColor
        {
            set
            {
                labelColor = value;
                changeLabels();
            }
            get => labelColor;
        }

        [Description("(Gauge) Numbers labels diameter. int "), Category("Gauge Control")]
        public int LabelDiameter
        {
            set
            {
                labelDiameter = value;
                SetStyle();
            }
            get => labelDiameter;
        }

        [Description("(Gauge) Numbers labels offset. int "), Category("Gauge Control")]
        public int LabelOffset
        {
            set
            {
                labelOffset = value;
                SetStyle();
            }
            get => labelOffset;
        }

        [Description("(Gauge) Set X position of numbers labels. int "), Category("Gauge Control")]
        public int LabelX
        {
            set
            {
                labelX = value;
                SetStyle();
                
            }
            get => labelX;
        }

        [Description("(Gauge) Set Y position of numbers labels. int "), Category("Gauge Control")]
        public int LabelY
        {
            set
            {
                labelY = value;
                SetStyle();
            }
            get => labelY;
        }

        [Description("(Gauge) Set neddle color"), Category("Gauge Control")]
        public SolidColorBrush NeedleColor
        {
            set => needle.Stroke = needleColor = value;
            get => needleColor;
        }

        [Description("(Gauge) Set gauge width (80-300)  . double "), Category("Gauge Control")]
        public double ArcBackWidth
        {
            set
            {
                if (value >= maxW) { value = maxW; }
                else if (value<= minW) { value = minW; }
                Back.Width=BackFront.Width = value;
                Front.Width=FrontBack.Width=GlassCanvas.Width =  value - 20;
                FrontBackMask.Width = value - 45;
            }
            get => Back.Width;
        }

        [Description("(Gauge) Set gauge height (80-300)  . double "), Category("Gauge Control")]
        public double ArcBackHeight
        {
            set
            {
                if (value >= maxH) { value = maxH; }
                else if (value <= minH) { value = minH; }
                Back.Height=BackFront.Height = value;
                Front.Height =FrontBack.Height=GlassCanvas.Height = value - 20;
                FrontBackMask.Height = value - 45;
            }
            get => Back.Height;
        }

        [Description("(Gauge) Arc opacity 0-100  . int "), Category("Gauge Control")]
        public double ArcOpacity
        {
            set
            {
                if (value <= 0) { value = 0; }
                else if (value >= 100) { value = 100; }
                arcOpacity = value;
                SetStyle();
            }
            get => arcOpacity;
        }

        [Description("(Gauge) Set X position of the arc . int "), Category("Gauge Control")]
        public int ArcX
        {
            set
            {
                arcX = value;
                SetStyle();

            }
            get => arcX;
        }

        [Description("(Gauge) Set Y position of the arc . int "), Category("Gauge Control")]
        public int ArcY
        {
            set
            {
                arcY = value;
                SetStyle();
            }
            get => arcY;
        }

        [Description("(Gauge) Set gauge around color . "), Category("Gauge Control")]
        public SolidColorBrush ArcOutBackColor
        {
            set
            {
                arcOutBackColor = value;
                BackFront.Fill = value;
            }
            get => arcOutBackColor;
        }

        [Description("(Gauge) Set gauge background color . "), Category("Gauge Control")]
        public SolidColorBrush ArcBackColor
        {
            set
            {
                arcBackColor = value;
                Front.Fill = value;
            }
            get => arcBackColor;
        }

        [Description("(Gauge) Set first color in arc. "), Category("Gauge Control")]
        public SolidColorBrush ArcFirstColor
        {
            set
            {
                arcFirstColor = value;
                SetStyle();
            }
            get => arcFirstColor;
        }

        [Description("(Gauge) Set secondly color in arc. "), Category("Gauge Control")]
        public SolidColorBrush ArcMidleColor
        {
            set
            {
                arcMidleColor = value;
                SetStyle();
            }
            get => arcMidleColor;
        }

        [Description("(Gauge) Set end position last color of the arc."), Category("Gauge Control")]
        public SolidColorBrush ArcEndColor
        {
            set
            {
                arcEndColor = value;
                SetStyle();
            }
            get => arcEndColor;
        }

        [Description("(Gauge) Set end position first color of the arc."), Category("Gauge Control")]
        public int ArcFirstColorEndPos
        {
            set
            {
                arcFirstColorEndPos = value;
                SetStyle();
            }
            get => arcFirstColorEndPos;
        }

        [Description("(Gauge) Set end position secondly color of the arc."), Category("Gauge Control")]
        public int ArcMidleColorEndPos
        {
            set
            {
                arcMidleColorEndPos = value;
                SetStyle();
            }
            get => arcMidleColorEndPos;
        }

        [Description("(Gauge) Arc scale. int "), Category("Gauge Control")]
        public int ArcScale
        {
            set
            {
                if (value <= 0) { value = 1; }
                arcScale = value;
                SetStyle();
            }
            get => arcScale;
        }

        [Description("(Gauge) Arc offset length. int "), Category("Gauge Control")]
        public int ArcOffset
        {
            set
            {
                if (value <= 0) { value = 0; }
                offset = value;
                SetStyle();
                // InvalidateVisual();
                
            }
            get => offset;
        }

        [Description("(Gauge) Arc diameter. int "), Category("Gauge Control")]
        public int ArcDiameter
        {
            set
            {
                if (value <= 0) { value = 0; }
                arcDiameter = value;
                SetStyle();
            }
            get => arcDiameter;
        }
      
        [Description("(Gauge) Arc periphery length. int "), Category("Gauge Control")]
        public int ArcLength
        {
            set
            {
                if (value <= 0) { value = 0; }
                else if (value >= 360) { value = 360; }
                arcLength = value;
                SetStyle();
            }
            get => arcLength;
        }
       
        [Description("(Gauge) Arc height. double "), Category("Gauge Control")]
        public double ArcHeight
        {
            set
            {
                if (value <= 1) { value = 1; }
                arcHeight = value;
                SetStyle();
            }
            get => Math.Round(arcHeight);
        }

        [Description("(Gauge) Hide background of the gauge "), Category("Gauge Control")]
        public Visibility GaugeBackVisible
        {
            set
            {
             backVisible = value ;
             Front.Visibility= FrontBack.Visibility = FrontBackMask.Visibility = value;
             BackFront.Visibility =  Back.Visibility = Front.Visibility = GlassCanvas.Visibility = value;
            }
            get => backVisible;
        }
    }

     

}
