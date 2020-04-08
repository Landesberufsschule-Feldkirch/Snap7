using PaternosterLager.ViewModel;
using System.Threading;

namespace PaternosterLager.Model
{
    public class Paternosterlager
    {
        public bool RichtungAuf { get; set; }
        public bool RichtungAb { get; set; }
        public double Geschwindigkeit { get; set; }

        private const double tempo = 0.01;

        private readonly KomplettesKettenRegal komplettesKettenRegal = new KomplettesKettenRegal();
        public Paternosterlager()
        {

            System.Threading.Tasks.Task.Run(() => PaternosterLagerTask());
        }

        private void PaternosterLagerTask()
        {
            while (true)
            {
                if (RichtungAuf) Geschwindigkeit = tempo;
                if (RichtungAb) Geschwindigkeit = -tempo;

                komplettesKettenRegal.SetGeschwindigkeit(Geschwindigkeit);

                Thread.Sleep(10);
            }
        }

      
    }
}