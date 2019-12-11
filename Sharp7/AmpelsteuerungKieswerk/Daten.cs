using System.Collections.ObjectModel;
using System.Windows;

namespace AmpelsteuerungKieswerk
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<LKW> gAlleLKW = new ObservableCollection<LKW>();
        public ObservableCollection<Ampel> gAlleAmpeln = new ObservableCollection<Ampel>();

        public void AlleLKWInitialisieren()
        {
            gAlleLKW.Add(new LKW(btn_lkw_1, 10, 10, 1300, 10));
            gAlleLKW.Add(new LKW(btn_lkw_2, 10, 80, 1300, 80));
            gAlleLKW.Add(new LKW(btn_lkw_3, 10, 150, 1300, 150));
            gAlleLKW.Add(new LKW(btn_lkw_4, 10, 220, 1300, 220));
            gAlleLKW.Add(new LKW(btn_lkw_5, 10, 290, 1300, 290));
        }


        public void AlleAmpelnInitialisieren()
        {
            gAlleAmpeln.Add(new Ampel(circ_Ampel_links_gruen, circ_Ampel_links_gelb, circ_Ampel_links_rot));
            gAlleAmpeln.Add(new Ampel(circ_Ampel_rechts_gruen, circ_Ampel_rechts_gelb, circ_Ampel_rechts_rot));
        }

        void EinAusgabeFelderInitialisieren()
        {
            foreach (byte b in DigInput) DigInput[b] = 0;
            foreach (byte b in DigOutput) DigOutput[b] = 0;

            foreach (LKW lkw in gAlleLKW) { lkw.linksParken(); }
        }
    }
}