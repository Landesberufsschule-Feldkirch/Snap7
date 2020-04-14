namespace PaternosterLager.Model
{
    public static class PositionBestimmen
    {

        public static (double x, double y) PositionBerechnen(double pos)
        {
            double x;
            double y;

            const double offset = 800;

            if (pos > offset)
            {
                x =350;
                y = pos - offset;
            }
            else
            {
                x = 150;
                y = pos;
            }



            return (x, y);
        }
    }
}
