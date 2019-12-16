using System.Threading;
using System.Windows.Controls;

namespace AmpelsteuerungKieswerk
{
    public partial class MainWindow
    {
        public bool P1_links_gruen = true;
        public bool P2_links_gelb, P3_links_rot;
        public bool P4_rechts_gruen = true;
        public bool P5_rechts_gelb, P6_rechts_rot;
        public bool B1, B2, B3, B4;

        public void Logikfunktionen_Task()
        {
            while (FensterAktiv)
            {
                B1 = false;
                B2 = false;
                B3 = false;
                B4 = false;

                this.Dispatcher.Invoke(() =>
                                   {
                                       foreach (Button btn in gAlleButton)
                                       {
                                           var lkw = btn.Tag as LKW;
                                           var (b1, b2, b3, b4) = lkw.LastwagenFahren();
                                           B1 |= b1;
                                           B2 |= b2;
                                           B3 |= b3;
                                           B4 |= b4;
                                       }
                                   });

                Thread.Sleep(10);// Idente Verzögerung zu Display
            }
        }
    }
}
