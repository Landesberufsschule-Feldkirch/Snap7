namespace AutomatischesLagersystem.Model
{
    public class KollisionRegalBestimmen
    {
        private bool kollisionRegal;

        public KollisionRegalBestimmen()
        {

        }

        internal void SetNeuePositionSchlitten(double x, double y, double z)
        {
            kollisionRegal = false;
            if (y > 2000 && y < 2950) return; // im freien Raum der durch die Pfosten begrenzt wird

            kollisionRegal = true;

        }


        internal bool GetKollisionRegal() => kollisionRegal;
    }
}
