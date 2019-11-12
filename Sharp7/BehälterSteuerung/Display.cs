using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BehälterSteuerung
{
    public partial class MainWindow
    {

        public void btn_Sichtbarkeit(bool Bedingung, Button btn_Ein, Button btn_Aus)
        {
            if (Bedingung)
            {
                btn_Ein.Visibility = System.Windows.Visibility.Visible;
                btn_Aus.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                btn_Ein.Visibility = System.Windows.Visibility.Hidden;
                btn_Aus.Visibility = System.Windows.Visibility.Visible;
            }
        }

        public void img_Sichtbarkeit(ushort BitPos, Image img_Ein, Image img_Aus, Rectangle rct_Leitung)
        {
            if (BitmusterTesten(DigOutput, Startbyte_0, BitPos))
            {
                img_Ein.Visibility = System.Windows.Visibility.Visible;
                img_Aus.Visibility = System.Windows.Visibility.Hidden;
                rct_Leitung.Fill = System.Windows.Media.Brushes.Blue;
            }
            else
            {
                img_Ein.Visibility = System.Windows.Visibility.Hidden;
                img_Aus.Visibility = System.Windows.Visibility.Visible;
                rct_Leitung.Fill = System.Windows.Media.Brushes.LightBlue;
            }
        }


    }
}
