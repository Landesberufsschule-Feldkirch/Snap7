namespace PaternosterLager.Model
{
    public static class PositionBestimmen
    {

        public static (double x, double y) PositionBerechnen(double pos)
        {
            double x;
            double y;

            if (pos > 100)
            {
                x = 200;
                y = pos - 100;
            }
            else
            {
                x = 100;
                y = pos;
            }



            return (x, y);
        }
    }
}
