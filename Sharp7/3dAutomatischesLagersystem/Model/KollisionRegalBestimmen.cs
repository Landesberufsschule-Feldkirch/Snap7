namespace AutomatischesLagersystem.Model
{
    public class KollisionRegalBestimmen
    {
        private bool _kollisionRegalMitSchlitten;

        public KollisionRegalBestimmen()
        {
        }

        internal void SetNeuePositionSchlitten(double x, double y, double z)
        {
            _kollisionRegalMitSchlitten = false;
            if (x < -150 || x > 10700) return;  // Raum vor bzw. hinter dem Regal
            if (y > 2000 && y < 2950) return;   // im freien Raum der durch die Pfosten begrenzt wird

            for (var pfostenNummer = 0; pfostenNummer < 11; pfostenNummer++)        // Pfosten
            {
                var pfostenLinks = 0 + pfostenNummer * 1000;
                var pfostenRechts = 550 + pfostenNummer * 1000;
                if (!(x > pfostenLinks) || !(x < pfostenRechts)) continue;
                _kollisionRegalMitSchlitten = true;
                return;
            }

            if (!(y < 1950) && !(y > 3000)) return;
            for (var regalBrettLage = 0; regalBrettLage < 6; regalBrettLage++)
            {
                var regalBrettUnten = 500 + regalBrettLage * 500;
                var regalBrettOben = 600 + regalBrettLage * 500;

                if (!(z > regalBrettUnten) || !(z < regalBrettOben)) continue;
                for (var regalBrettNummer = 0; regalBrettNummer < 11; regalBrettNummer++)
                {
                    var regalBrettLinks = -150 + regalBrettNummer * 1000;
                    var regalBrettRechts = 700 + regalBrettNummer * 1000;

                    if (!(x > regalBrettLinks) || !(x < regalBrettRechts)) continue;
                    _kollisionRegalMitSchlitten = true;
                    return;
                }
            }
        }

        internal bool GetKollisionRegalMitSchlitten() => _kollisionRegalMitSchlitten;
    }
}