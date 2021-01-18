using System;

namespace Utilities
{
    public static class Simatic
    {
        public static short Simatic_Analog_2_Int16(double wert, double scal) => Convert.ToInt16(Clamp(27648 * wert / scal, -27648, 27648));

        public static int Simatic_Analog_2_Int32(double wert, double scal) => Convert.ToInt32(Clamp(27648 * wert / scal, -27648, 27648));

        public static double Simatic_Analog_2_Double(int wert, double scal) => wert * scal / 27648;

        // ACHTUNG: Siemens verwendet Big Endian!!
        public static byte Simatic_Digital_GetLowByte(uint wert) => (byte)(wert / 256);

        public static byte Simatic_Digital_GetHighByte(uint wert) => (byte)(wert & 255);

        public static uint Simatic_Digital_CombineTwoByte(byte low, byte high) => 256 * (uint)low + high;

        public static double Clamp(double val, double min, double max)
        {
            if (val > max) return max;
            return val < min ? min : val;
        }
    }
}