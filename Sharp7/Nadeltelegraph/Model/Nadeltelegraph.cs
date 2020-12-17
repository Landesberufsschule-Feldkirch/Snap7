using System.Collections.ObjectModel;

namespace Nadeltelegraph.Model
{
    public class Nadeltelegraph
    {
        public char Zeichen { get; set; }
        public bool P0 { get; set; }
        public bool P1L { get; set; }
        public bool P1R { get; set; }
        public bool P2L { get; set; }
        public bool P2R { get; set; }
        public bool P3L { get; set; }
        public bool P3R { get; set; }
        public bool P4L { get; set; }
        public bool P4R { get; set; }
        public bool P5L { get; set; }
        public bool P5R { get; set; }
        public ObservableCollection<Zeiger> AlleZeiger = new();

        public Nadeltelegraph()
        {
            Zeichen = ' ';
            for (var i = 0; i < 10; i++) AlleZeiger.Add(new Zeiger());
        }
    }
}