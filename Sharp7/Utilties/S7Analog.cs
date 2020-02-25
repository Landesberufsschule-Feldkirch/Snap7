using System;

namespace Utilities
{
    public static class S7Analog
    {
        public static short S7_Analog_2_Short(double db, double scal)
        {
            return Convert.ToInt16(Clamp(27648 * db / scal));
        }

        public static int S7_Analog_2_Int(double db, double scal)
        {
            return Convert.ToInt32(Clamp(27648 * db / scal));
        }

        public static double S7_Analog_2_Double(int wert, double scal)
        {
            return wert * scal / 27648;
        }

        private static double Clamp(double val)
        {
            double max = 27648;
            double min = -27648;
            if (val > max) return max;
            if (val < min) return min;
            return val;
        }
    }
}