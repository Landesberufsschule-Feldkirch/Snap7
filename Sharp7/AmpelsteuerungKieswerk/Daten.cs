using System.Collections.ObjectModel;
using System.Windows;

namespace AmpelsteuerungKieswerk
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<LKW> gAlleLKW = new ObservableCollection<LKW>();

        public void AlleLKWInitialisieren()
        {
            gAlleLKW.Add(new LKW(btn_lkw_1, img_lkw_1));
            gAlleLKW.Add(new LKW(btn_lkw_2, img_lkw_2));
            gAlleLKW.Add(new LKW(btn_lkw_3, img_lkw_3));
            gAlleLKW.Add(new LKW(btn_lkw_4, img_lkw_4));
            gAlleLKW.Add(new LKW(btn_lkw_5, img_lkw_5));
        }

        void EinAusgabeFelderInitialisieren()
        {
            foreach (byte b in DigInput) DigInput[b] = 0;
            foreach (byte b in DigOutput) DigOutput[b] = 0;

            foreach (LKW lkw in gAlleLKW) { lkw.LinksParken(); }
        }
    }
}