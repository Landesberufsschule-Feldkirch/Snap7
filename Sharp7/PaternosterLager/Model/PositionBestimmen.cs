using System;

namespace PaternosterLager.Model
{
    public static class PositionBestimmen
    {

        private const double obenBahn = 50;
        private const double linksBahn = 150;
        private const double breiteBahn = 200;
        private const double hoeheBahn = 850;

        public static (double x, double y) ZapfenPositionBerechnen(double pos, double breiteZapfen)
        {
            double x = 0;
            double y = 0;
            double winkel = 0;

            double rundungBahn = Math.PI * (breiteBahn + breiteZapfen) / 2;
            double umfangBahn = 2 * hoeheBahn + Math.PI * (breiteBahn + breiteZapfen);

            if (pos < hoeheBahn / 2)
            {
                // rechts rauf
                x = linksBahn + breiteBahn;
                y = obenBahn + hoeheBahn / 2 - pos;
            }
            else if (pos < hoeheBahn / 2 + rundungBahn)
            {
                // obere Rundung
                winkel = 0;


            }
            else if (pos < hoeheBahn * 1.5 + rundungBahn)
            {
                // links runter
                x = linksBahn - breiteZapfen;
                y = obenBahn + (pos - hoeheBahn / 2 - rundungBahn);
            }
            else if (pos < hoeheBahn * 1.5 + 2 * rundungBahn)
            {
                // untere Rundung
            }
            else
            {
                // rechts rauf
                x = linksBahn + breiteBahn;
                y = obenBahn + hoeheBahn - (pos - 1.5 * hoeheBahn - 2 * rundungBahn);
            }









            return (x, y);
        }
    }
}
