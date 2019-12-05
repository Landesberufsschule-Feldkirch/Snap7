using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Shapes;
using Sharp7;

namespace AmpelsteuerungKieswerk
{
    public partial class MainWindow
    {

        public class LKW
        {

            public Button btnLKW { get; set; }

            public double x_aktuell { get; set; }
            public double y_aktuell { get; set; }
            public double x_alt { get; set; }
            public double y_alt { get; set; }
            public double x_links { get; set; }
            public double y_links { get; set; }
            public double x_rechts { get; set; }
            public double y_rechts { get; set; }

            public LKW(Button btn, double x_links, double y_links, double x_rechts, double y_rechts)
            {
                this.btnLKW = btn;
                this.x_links = x_links;
                this.y_links = y_links;
                this.x_rechts = x_rechts;
                this.y_rechts = y_rechts;


            }


            public void AnzeigeAktualisieren(bool FensterAktiv)
            {
                if (FensterAktiv) {
                    btnLKW.SetValue(Canvas.LeftProperty, x_aktuell);         
                    btnLKW.SetValue(Canvas.TopProperty, y_aktuell);
                }
            }

            public void LKWDatenRangieren(ref byte[] BufferInput, ref byte[] BufferOutput)
            {

            }

            public void linksParken()
            {
                x_aktuell = x_links;
                y_aktuell = y_links;
            }


            public void rechtsParken()
            {
                x_aktuell = x_rechts;
                y_aktuell = y_rechts;
            }



        }

        public ObservableCollection<LKW> gAlleLKW = new ObservableCollection<LKW>();
        public void AlleLKWInitialisieren()
        {
            gAlleLKW.Add(new LKW(btn_lkw_1, 10, 10, 1300, 10));
            gAlleLKW.Add(new LKW(btn_lkw_2, 10, 80, 1300, 80));
            gAlleLKW.Add(new LKW(btn_lkw_3, 10, 150, 1300, 150));
            gAlleLKW.Add(new LKW(btn_lkw_4, 10, 220, 1300, 220));
            gAlleLKW.Add(new LKW(btn_lkw_5, 10, 290, 1300, 290));
        }
    }
}