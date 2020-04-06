namespace PaternosterLager.ViewModel
{
    using System.Collections.ObjectModel;

    public class KomplettesKettenRegal
    {
        public ObservableCollection<KettengliedRegal> AlleKettengliedRegale { get; set; }

        public KomplettesKettenRegal()
        {
            AlleKettengliedRegale = new ObservableCollection<KettengliedRegal>
            {
                new KettengliedRegal(100,0),
                new KettengliedRegal(100,100),
                new KettengliedRegal(100,200),
                new KettengliedRegal(100,300)
            };
        }
    }
}