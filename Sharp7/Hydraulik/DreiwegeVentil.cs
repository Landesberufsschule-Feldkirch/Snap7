using System.Threading;

namespace Hydraulik
{
    public class DreiwegeVentil
    {
        public enum Richtung
        {
            Stop,
            Oeffnen,
            Schliessen
        }

        private Richtung richtung;
        private double position;
        private readonly double posMin;
        private readonly double posMax;
        private readonly double geschwindigkeit;

        public DreiwegeVentil(double min, double max, double delta)
        {
            position = 0;
            posMin = min;
            posMax = max;
            geschwindigkeit = delta;

            System.Threading.Tasks.Task.Run(() => DreiwegeVentilTask());
        }

        private void DreiwegeVentilTask()
        {
            while (true)
            {
                if (richtung == Richtung.Oeffnen) position += geschwindigkeit;
                if (richtung == Richtung.Schliessen) position -= geschwindigkeit;

                if (position > posMax) position = posMax;
                if (position < posMin) position = posMin;

                Thread.Sleep(10);
            }
        }

        public void SetRichtung(Richtung ri) => richtung = ri;
        public double GetPosition() => position;
    }
}