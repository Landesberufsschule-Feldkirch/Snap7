using System;

namespace Utilities
{
    public static class S7Analog
    {
        public static short S7_Analog_2_Int16(double db, double scal) => Convert.ToInt16(Clamp((27648 * db / scal), -27648, 27648));

        public static int S7_Analog_2_Int32(double db, double scal) => Convert.ToInt32(Clamp((27648 * db / scal), -27648, 27648));

        public static double S7_Analog_2_Double(int wert, double scal) => wert * scal / 27648;

        public static double Clamp(double val, double min, double max)
        {
            if (val > max) return max;
            return val < min ? min : val;
        }
    }
}