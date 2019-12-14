﻿using System.Threading.Tasks;

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
            this.Dispatcher.Invoke(() =>
            {
                //GridRasterEinblenden();
            });

            while (FensterAktiv)
            {
                B1 = false;
                B2 = false;
                B3 = false;
                B4 = false;

                foreach (LKW lkw in gAlleLKW)
                {
                    var (b1, b2, b3, b4) = lkw.LastwagenFahren();
                    B1 |= b1;
                    B2 |= b2;
                    B3 |= b3;
                    B4 |= b4;
                }

                this.Dispatcher.Invoke(() =>
                                   {
                                       foreach (LKW lkw in gAlleLKW) lkw.AnzeigeAktualisieren(FensterAktiv);
                                       AnzeigeAktualisieren(FensterAktiv);
                                   });

                Task.Delay(100);
            }
        }

    }
}
