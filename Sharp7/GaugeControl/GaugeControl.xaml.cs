// by Antonis Ntit (antonis68)
// email: spanomarias68@gmail.com
using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
 

namespace Gauge
{
     
    public partial class GaugeControl : UserControl
    {
       
        private TweenMotionLib.TweenMotion motion = new TweenMotionLib.TweenMotion();
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
        System.Windows.Media.Effects.DropShadowEffect shadow;
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
        private int arcMinValue = 0;
        private int arcMaxValue = 100;
        private int maxW = 300;
        private int maxH = 300;
        private int minW = 80;
        private int minH = 80;
        private int arcFirstColorEndPos = 100;
        private int arcMidleColorEndPos = 130;
        private int colorCount = 0;
        private double arcOpacity = 100;
        private double needleHeigth;
        private int startPos = 0;
        private int endPos = 0;
        private double initPos = 0;
        private double prevPos = 0;
        private double motionTime=1;
        private arcStyle _arcStyle;
        private int fontSize=12;
        private double labelOpacity = 1;
        private bool isFirst = true;
        private int gaugeTextFontSize = 14;
        private string descrText = "Gauge";
        private double gaugeTextOpacity = 1;
        private Visibility backVisible;
        motionType typeMotion;
        public enum arcStyle { Flat, Αscending, Descending };

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
            back_front.Fill = arcOutBackColor;
            front.Fill = arcBackColor;
            shadow = new System.Windows.Media.Effects.DropShadowEffect();
            shadow.ShadowDepth = 5;
            shadow.Opacity = .4;
            needleHeigth = arcHeight;
            if (isFirst) { setStyle(); }
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
            double factor = (double)arcLength / (arcMaxValue - arcMinValue);
            value *= factor;
            initPos = 90 - (ArcLength + ArcOffset);
            startPos = (int)initPos + (int)prevPos;
            endPos = (int)initPos + (int)value;
            motion.Start(typeMotion.ToString(), startPos, endPos, motionTime);
            prevPos = value;
        }
        // Set style of control
        private void setStyle()
        {
            removeChildren();
            colorCount = ArcLength;
            double counter = 0;
            double radial = 0;
            double sin = 0;
            double cos = 0;
            double tempH = 0; // temporary height 
            box = null;
            double noFlat = 0;
            double _arcHeight = arcHeight;
            needleFront.Margin = new Thickness { Left = arcX - 10, Top = arcY - 60 };
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

            for (int i = offset; i < arcLength + offset+arcScale; i+=arcScale)
            {               
                counter += noFlat;
                _arcHeight = tempH + counter;
                box = new Rectangle();
                box.Width = _arcHeight;
                box.Height = 4;
                box.Opacity = arcOpacity / 100;
                if (colorCount > arcMidleColorEndPos) { box.Fill = arcEndColor; }
                else if (colorCount > arcFirstColorEndPos) { box.Fill = arcMidleColor; }
                else { box.Fill = ArcFirstColor; }
                box.Name = "box_" + i;
                radial = i * (Math.PI / 180);
                sin = Math.Sin(radial) * arcDiameter;
                cos = Math.Cos(radial) * arcDiameter;
                Canvas.SetLeft(box, arcX + sin);
                Canvas.SetTop(box, ArcY + cos);
                box.RenderTransform = new RotateTransform(90 - i);
                canvas.Children.Add(box);
                colorCount -= arcScale;
               
            }
            canvas.Children.Add(setNeedle());
            Canvas.SetZIndex(needle, 1);
            setLabel();
            
        }
        // Create the needle
        private Line setNeedle()
        {
            double tail= (arcDiameter + needleHeigth) * (10f/100f)-8;
            needle = new Line();
            needle.X1 = ArcX - 30-tail;
            needle.X2 = ArcX + arcDiameter + needleHeigth - 8;
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
            double labelScale =  (arcMaxValue - arcMinValue);
            labelScale /= labelQuantity;
            double radial;         
            double sin;
            double cos;
            for (int i = 0; i <= arcLength; i++)
            {
                
                if ( i % modul  == 0 )
                {
                    lbl = new Label();
                    lbl.Name = "label_" + i;
                    lbl.Content =  Math.Round(labelCount,1).ToString() ;
                    lbl.Foreground = labelColor;
                    lbl.FontFamily = fontFamily;
                    lbl.FontSize = fontSize;
                    lbl.Opacity = labelOpacity;
                    radial = (i + labelOffset ) * (Math.PI / 180);
                    sin = Math.Sin(radial) * labelDiameter;
                    cos = Math.Cos(radial) * labelDiameter;
                    Canvas.SetLeft(lbl, labelX + sin);
                    Canvas.SetTop(lbl, labelY + cos);
                    canvas.Children.Add(lbl);
                    labelCount -= labelScale;
                   
                }
               
            }
            Label _descrLbl = getDescrText();
            Canvas.SetLeft(_descrLbl, gaugeTextX);
            Canvas.SetTop(_descrLbl, gaugeTextY);
            canvas.Children.Add( _descrLbl );
            
        }
        // Description text
        private Label getDescrText()
        {
            Label descrLbl = new Label();
            descrLbl.Name = "descr_label";
            descrLbl.Content =  descrText;
            descrLbl.FontFamily = gaugeTextFont;
            descrLbl.FontSize = gaugeTextFontSize;
            descrLbl.Foreground = descrTextColor;
            return descrLbl;
        }
        public void removeChildren()
        {
            canvas.Children.Clear();
        }
        private void removeLabels()
        {
            foreach (UIElement element in canvas.Children)
            {
                if (element is Label)
                {
                    Label lbl = (Label)element;
                    if (lbl.Name != "descr_label")
                    {
                        canvas.Children.Remove(lbl);
                    }
                }
            }
        }
        // Change the labels
        private void changeLabels()
        {
            foreach (UIElement element in canvas.Children)
            {                
                if (element is Label)
                {
                    Label lbl = (Label)element;
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
        }


        // ---------------------- public variable
        [Description("(Gauge) Set the time raise of the needle"),Category("Gauge Control") ]
        public double GaugeTime
        {
            set
            {
                motionTime = value;
            
            }
            get
            {
                return motionTime;
            }
        }

        [Description("(Gauge) Choose the motion type of the needle. "), Category("Gauge Control")]
        public motionType GaugeMotionType
        {
            set
            {
                typeMotion = value;
            }
            get
            {
                return typeMotion;
            }
        }

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
            get
            {
                return gaugeTextOpacity*100;
            }
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
            get
            {
                return gaugeTextFontSize;
            }
        }

        [Description("(Gauge) Choose font of the description text"), Category("Gauge Control")]
        public FontFamily GaugeTextFont
        {
            set
            {
                gaugeTextFont = value;
                changeLabels();
            }
            get
            {
                return gaugeTextFont;
            }
        }

        [Description("(Gauge) Set X position of the description text . int"), Category("Gauge Control")]
        public int GaugeTextX
        {
            set
            {
                gaugeTextX = value;
                changeLabels();
            }
            get
            {
                return gaugeTextX;
            }
        }

        [Description("(Gauge) Set Y position of the description text . int"), Category("Gauge Control")]
        public int GaugeTextY
        {
            set
            {
               gaugeTextY = value;
               changeLabels();
            }
            get
            {
                return gaugeTextY;
            }
        }

        [Description("(Gauge) Set color of the description text. string "), Category("Gauge Control")]
        public SolidColorBrush GaugeTextColor
        {
            set
            {
                descrTextColor = value;
                changeLabels();
            }
            get
            {
                return descrTextColor;
            }
        }

        [Description("(Gauge) Set description text of the gauge. string "), Category("Gauge Control")]
        public string GaugeText
        {
            set
            {
                descrText = value;
                changeLabels();
            }
            get
            {
                return descrText;
            }
        }

        [Description("(Gauge) Set the length of the needle. double "), Category("Gauge Control")]
        public double NeedleLength
        {
            set
            {
                needleHeigth = value;
                setStyle();
                   
            }
            get
            {
                return needleHeigth;
            }
        }

        [Description("(Gauge) Choose how much numbers labels you want to visible (Default is 10).The number must be an integer divider of the ArcLength"), Category("Gauge Control")]
        public int LabelQuantity
        {
            set
            {
                if (value <= 1) { value = 1; }
                if ( arcLength % value != 0) { return; }
                labelQuantity = value;
                setStyle();
            }
            get
            {
                return labelQuantity;
            }
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
            get
            {
                return labelOpacity*100;
            }
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
              get
              {
                return fontSize;
             }
            }

        [Description("(Gauge) Choose numbers labels font "), Category("Gauge Control")]
        public FontFamily LabelFont
        {

            set
            {
                fontFamily = value;
                changeLabels();
                
            }
            get
            {
                return fontFamily;
            }
        }

        [Description("(Gauge) Set the minimum number needle point. int "), Category("Gauge Control")]
        public int ArcMinValue
        {
            set
            {
                if (value >= arcMaxValue) { return; }
                arcMinValue = value;
                setStyle();
            }
            get
            {
                return arcMinValue;
            }
        }

        [Description("(Gauge) Set the maximum number needle point. int "), Category("Gauge Control")]
        public int ArcMaxValue
        {
            set
            {
                if (value <= arcMinValue) { return; }
                arcMaxValue = value;
                setStyle();
            }
            get
            {
                return arcMaxValue;
            }
        }

        [Description("(Gauge) Test the gauge.Give value between minimum and maximum"), Category("Gauge Control")]
        public double TestGauge
        {
            set
            {
                setValue(value);
            }
            get
            {
                return 0;
            }
        }

        [Description("(Gauge) Arc style "), Category("Gauge Control")]
        public arcStyle ArcStyle
        {
            set
            {             
                _arcStyle = value;
                setStyle();
            }
            get
            {
              return _arcStyle;
            }
        }

        [Description("(Gauge) Numbers labelscolor "), Category("Gauge Control")]
        public SolidColorBrush LabelColor
        {
            set
            {
                labelColor = value;
                changeLabels();
            }
            get
            {
                return labelColor;
            }
        }

        [Description("(Gauge) Numbers labels diameter. int "), Category("Gauge Control")]
        public int LabelDiameter
        {
            set
            {
                labelDiameter = value;
                setStyle();
            }
            get
            {
                return labelDiameter;
            }
        }

        [Description("(Gauge) Numbers labels offset. int "), Category("Gauge Control")]
        public int LabelOffset
        {
            set
            {
                labelOffset = value;
                setStyle();
            }
            get
            {
                return labelOffset;
            }
        }

        [Description("(Gauge) Set X position of numbers labels. int "), Category("Gauge Control")]
        public int LabelX
        {
            set
            {
                labelX = value;
                setStyle();
                
            }
            get
            {
                return labelX;
            }
        }

        [Description("(Gauge) Set Y position of numbers labels. int "), Category("Gauge Control")]
        public int LabelY
        {
            set
            {
                labelY = value;
                setStyle();
            }
            get
            {
                return labelY;
            }
        }

        [Description("(Gauge) Set neddle color"), Category("Gauge Control")]
        public SolidColorBrush NeedleColor
        {
            set
            {
                needle.Stroke = needleColor = value;
            }
            get
            {
                return needleColor;
            }
        }

        [Description("(Gauge) Set gauge width (80-300)  . double "), Category("Gauge Control")]
        public double ArcBackWidth
        {
            set
            {
                if (value >= maxW) { value = maxW; }
                else if (value<= minW) { value = minW; }
                back.Width=back_front.Width = value;
                front.Width=front_back.Width=glass_canvas.Width =  value - 20;
                front_back_mask.Width = value - 45;
            }
            get
            {
                return back.Width;
            }
        }

        [Description("(Gauge) Set gauge height (80-300)  . double "), Category("Gauge Control")]
        public double ArcBackHeight
        {
            set
            {
                if (value >= maxH) { value = maxH; }
                else if (value <= minH) { value = minH; }
                back.Height=back_front.Height = value;
                front.Height =front_back.Height=glass_canvas.Height = value - 20;
                front_back_mask.Height = value - 45;
            }
            get
            {
                return back.Height;
            }
        }

        [Description("(Gauge) Arc opacity 0-100  . int "), Category("Gauge Control")]
        public double ArcOpacity
        {
            set
            {
                if (value <= 0) { value = 0; }
                else if (value >= 100) { value = 100; }
                arcOpacity = value;
                setStyle();
            }
            get
            {
                return arcOpacity;
            }
        }

        [Description("(Gauge) Set X position of the arc . int "), Category("Gauge Control")]
        public int ArcX
        {
            set
            {
                arcX = value;
                setStyle();

            }
            get
            {
                return arcX;
            }
        }

        [Description("(Gauge) Set Y position of the arc . int "), Category("Gauge Control")]
        public int ArcY
        {
            set
            {
                arcY = value;
                setStyle();
            }
            get
            {
                return arcY;
            }
        }

        [Description("(Gauge) Set gauge around color . "), Category("Gauge Control")]
        public SolidColorBrush ArcOutBackColor
        {
            set
            {
                arcOutBackColor = value;
                back_front.Fill = value;
            }
            get
            {
                return arcOutBackColor;
            }
        }

        [Description("(Gauge) Set gauge background color . "), Category("Gauge Control")]
        public SolidColorBrush ArcBackColor
        {
            set
            {
                arcBackColor = value;
                front.Fill = value;
            }
            get
            {
                return arcBackColor;
            }
        }

        [Description("(Gauge) Set first color in arc. "), Category("Gauge Control")]
        public SolidColorBrush ArcFirstColor
        {
            set
            {
                arcFirstColor = value;
                setStyle();
            }
            get
            {
                return arcFirstColor;
            }
        }

        [Description("(Gauge) Set secondly color in arc. "), Category("Gauge Control")]
        public SolidColorBrush ArcMidleColor
        {
            set
            {
                arcMidleColor = value;
                setStyle();
            }
            get
            {
                return arcMidleColor;
            }
        }

        [Description("(Gauge) Set end position last color of the arc."), Category("Gauge Control")]
        public SolidColorBrush ArcEndColor
        {
            set
            {
                arcEndColor = value;
                setStyle();
            }
            get
            {
                return arcEndColor;
            }
        }

        [Description("(Gauge) Set end position first color of the arc."), Category("Gauge Control")]
        public int ArcFirstColorEndPos
        {
            set
            {
                arcFirstColorEndPos = value;
                setStyle();
            }
            get
            {
                return arcFirstColorEndPos;
            }
        }

        [Description("(Gauge) Set end position secondly color of the arc."), Category("Gauge Control")]
        public int ArcMidleColorEndPos
        {
            set
            {
                arcMidleColorEndPos = value;
                setStyle();
            }
            get
            {
                return arcMidleColorEndPos;
            }
        }

        [Description("(Gauge) Arc scale. int "), Category("Gauge Control")]
        public int ArcScale
        {
            set
            {
                if (value <= 0) { value = 1; }
                arcScale = value;
                setStyle();
            }
            get
            {
                return arcScale;
            }

        }

        [Description("(Gauge) Arc offset length. int "), Category("Gauge Control")]
        public int ArcOffset
        {
            set
            {
                if (value <= 0) { value = 0; }
                offset = value;
                setStyle();
                // InvalidateVisual();
                
            }
            get
            {
                return offset;
            }
        }

        [Description("(Gauge) Arc diameter. int "), Category("Gauge Control")]
        public int ArcDiameter
        {
            set
            {
                if (value <= 0) { value = 0; }
                arcDiameter = value;
                setStyle();
            }
            get
            {
                return arcDiameter;
            }
        }
      
        [Description("(Gauge) Arc periphery length. int "), Category("Gauge Control")]
        public int ArcLength
        {
            set
            {
                if (value <= 0) { value = 0; }
                else if (value >= 360) { value = 360; }
                arcLength = value;
                setStyle();
            }
            get
            {
                return arcLength;
            }
        }
       
        [Description("(Gauge) Arc height. double "), Category("Gauge Control")]
        public double ArcHeight
        {
            set
            {
                if (value <= 1) { value = 1; }
                arcHeight = value;
                setStyle();
            }
            get
            {
                return Math.Round(arcHeight);
            }
        }

        [Description("(Gauge) Hide background of the gauge "), Category("Gauge Control")]
        public Visibility GaugeBackVisible
        {
            set
            {
             backVisible = value ;
             front.Visibility= front_back.Visibility = front_back_mask.Visibility = value;
             back_front.Visibility =  back.Visibility = front.Visibility = glass_canvas.Visibility = value;
            }
            get
            {
                return backVisible ;
            }
        }
    }

     

}
