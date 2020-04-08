namespace PaternosterLager.ViewModel
{
    using System.Collections.ObjectModel;

    public class KomplettesKettenRegal
    {
        public ObservableCollection<KettengliedRegal> AlleKettengliedRegale { get; set; }

        public KomplettesKettenRegal()
        {
            AlleKettengliedRegale = new ObservableCollection<KettengliedRegal>();
            for (var i = 0; i < 20; i++) AlleKettengliedRegale.Add(new KettengliedRegal(i));
        }
       

         public void SetGeschwindigkeit(double geschwindigkeit)
        {
            foreach (var data in AlleKettengliedRegale)
            {
                data.setGeschwindigkeit(geschwindigkeit);
            }
        }
    }
}