using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LAP_2018_Abfuellanlage
{
    public partial class MainWindow
    {


        public ObservableCollection<Flaschen> gAlleFlaschen = new ObservableCollection<Flaschen>();

        public void AlleFlaschenInitialisieren()
        {
            gAlleFlaschen.Add(new Flaschen(0, imgFlasche_1));
            gAlleFlaschen.Add(new Flaschen(1, imgFlasche_2));
            gAlleFlaschen.Add(new Flaschen(2, imgFlasche_3));
            gAlleFlaschen.Add(new Flaschen(3, imgFlasche_4));
            gAlleFlaschen.Add(new Flaschen(4, imgFlasche_5));
            gAlleFlaschen.Add(new Flaschen(5, imgFlasche_6));
        }
        public void AlleFlaschenParken()
        {
            foreach (Flaschen flasche in gAlleFlaschen) { flasche.FlaschenParken(); }
        }

    }
}