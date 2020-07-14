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
        private double positionProzent;
        private readonly double posMinProzent;
        private readonly double posMaxProzent;
        private readonly double deltaMillisekunden;
        private const int schrittweite = 10;

        public DreiwegeVentil(double minPosProzent, double maxPosProzent, double laufzeitSekunden)
        {
            positionProzent = minPosProzent;
            posMinProzent = minPosProzent;
            posMaxProzent = maxPosProzent;
            deltaMillisekunden = (posMaxProzent - posMinProzent) * laufzeitSekunden / 1000 * schrittweite;

            System.Threading.Tasks.Task.Run(DreiwegeVentilTask);
        }

        private void DreiwegeVentilTask()
        {
            while (true)
            {
                switch (richtung)
                {
                    case Richtung.Oeffnen:
                        positionProzent += deltaMillisekunden;
                        break;

                    case Richtung.Schliessen:
                        positionProzent -= deltaMillisekunden;
                        break;
                }

                LimitsTesten();

                Thread.Sleep(schrittweite);
            }
        }

        public void SetNeuePosition(Richtung ri, double dauer)
        {
            switch (ri)
            {
                case Richtung.Oeffnen:
                    positionProzent += deltaMillisekunden * dauer;
                    break;

                case Richtung.Schliessen:
                    positionProzent -= deltaMillisekunden * dauer;
                    break;
            }

            LimitsTesten();
        }
        public void SetRichtung(Richtung ri) => richtung = ri;

        public double GetPosition() => positionProzent;

        private void LimitsTesten()
        {
            if (positionProzent > posMaxProzent) positionProzent = posMaxProzent;
            if (positionProzent < posMinProzent) positionProzent = posMinProzent;
        }
    }
}