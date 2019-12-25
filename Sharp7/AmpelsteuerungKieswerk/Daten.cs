using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace AmpelsteuerungKieswerk
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Button> gAlleButton = new ObservableCollection<Button>();

        public void AlleLKWInitialisieren()
        {
            btn_lkw_1.Tag = new LKW(img_lkw_1);
            btn_lkw_2.Tag = new LKW(img_lkw_2);
            btn_lkw_3.Tag = new LKW(img_lkw_3);
            btn_lkw_4.Tag = new LKW(img_lkw_4);
            btn_lkw_5.Tag = new LKW(img_lkw_5);

            gAlleButton.Add(btn_lkw_1);
            gAlleButton.Add(btn_lkw_2);
            gAlleButton.Add(btn_lkw_3);
            gAlleButton.Add(btn_lkw_4);
            gAlleButton.Add(btn_lkw_5);
        }
    }
}