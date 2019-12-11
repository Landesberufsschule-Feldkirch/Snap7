using System.Windows.Media;
using System.Windows.Shapes;

namespace AmpelsteuerungKieswerk
{
    class Ampel
    {
        public enum AmpelFarben
        {
            gruen = 0,
            gelb,
            rot
        }

        private enum AmpelStatus
        {
            gruen = 0,
            gruen_blinkend_Aus_0, // 4 mal
            gruen_blinkend_Ein_0,
            gruen_blinkend_Aus_1,
            gruen_blinkend_Ein_1,
            gruen_blinkend_Aus_2,
            gruen_blinkend_Ein_2,
            gruen_blinkend_Aus_3,
            gruen_blinkend_Ein_3,
            gelb, // 3" bei max. 50km/h
            rot,
            rot_und_gelb
        }

        public AmpelFarben AmpelFarbe { get; set; }
        private AmpelStatus InternetStatus;

        Ellipse circGruen;
        Ellipse circGelb;
        Ellipse circRot;

        public Ampel(Ellipse Gruen, Ellipse Gelb, Ellipse Rot)
        {
            this.circGruen = Gruen;
            this.circGelb = Gelb;
            this.circRot = Rot;
        }

        public void FarbenAktualisieren()
        {
            switch (InternetStatus)
            {
                case AmpelStatus.gruen:
                case AmpelStatus.gruen_blinkend_Ein_0:
                case AmpelStatus.gruen_blinkend_Ein_1:
                case AmpelStatus.gruen_blinkend_Ein_2:
                case AmpelStatus.gruen_blinkend_Ein_3:
                    circGruen.Fill = Brushes.Green;
                    circGelb.Fill = Brushes.White;
                    circRot.Fill = Brushes.White;
                    break;

                case AmpelStatus.gruen_blinkend_Aus_0:
                case AmpelStatus.gruen_blinkend_Aus_1:
                case AmpelStatus.gruen_blinkend_Aus_2:
                case AmpelStatus.gruen_blinkend_Aus_3:
                    circGruen.Fill = Brushes.White;
                    circGelb.Fill = Brushes.White;
                    circRot.Fill = Brushes.White;
                    break;

                case AmpelStatus.gelb:
                    circGruen.Fill = Brushes.White;
                    circGelb.Fill = Brushes.Yellow;
                    circRot.Fill = Brushes.White;
                    break;

                case AmpelStatus.rot:
                    circGruen.Fill = Brushes.White;
                    circGelb.Fill = Brushes.White;
                    circRot.Fill = Brushes.Red;
                    break;

                case AmpelStatus.rot_und_gelb:
                    circGruen.Fill = Brushes.White;
                    circGelb.Fill = Brushes.Yellow;
                    circRot.Fill = Brushes.Red;
                    break;

                default:
                    break;
            }
        }


    }
}
