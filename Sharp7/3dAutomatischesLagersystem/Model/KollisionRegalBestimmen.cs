using System;

namespace AutomatischesLagersystem.Model
{
    public class KollisionRegalBestimmen
    {
        private bool kollisionRegalMitSchlitten;

        public KollisionRegalBestimmen()
        {

        }

        internal void SetNeuePositionSchlitten(double x, double y, double z)
        {
            kollisionRegalMitSchlitten = false;
            if (x < -150 || x > 10700) return;  // Raum vor bzw. hinter dem Regal
            if (y > 2000 && y < 2950) return;   // im freien Raum der durch die Pfosten begrenzt wird

            for (int pfostenNummer = 0; pfostenNummer < 11; pfostenNummer++)        // Pfosten
            {
                int pfostenLinks = 0 + pfostenNummer * 1000;
                int pfostenRechts = 550 + pfostenNummer * 1000;
                if (x > pfostenLinks && x < pfostenRechts)
                {
                    kollisionRegalMitSchlitten = true;
                    return;
                }
            }


            if (y < 1950 || y > 3000)           // Regalbretter
            {
                for (int regalBrettLage = 0; regalBrettLage < 6; regalBrettLage++)
                {
                    int regalBrettUnten = 500 + regalBrettLage * 500;
                    int regalBrettOben = 600 + regalBrettLage * 500;

                    if (z > regalBrettUnten && z < regalBrettOben)
                    {
                        for (int regalBrettNummer = 0; regalBrettNummer < 11; regalBrettNummer++)
                        {
                            int regalBrettLinks = -150 + regalBrettNummer * 1000;
                            int regalBrettRechts = 700 + regalBrettNummer * 1000;

                            if (x > regalBrettLinks && x < regalBrettRechts)
                            {
                                kollisionRegalMitSchlitten = true;
                                return;
                            }
                        }
                    }
                }
            }
        }


        internal bool GetKollisionRegalMitSchlitten() => kollisionRegalMitSchlitten;
    }
}
