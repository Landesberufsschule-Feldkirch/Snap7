namespace Utilities
{
    public class S7Analog
    {

        public static short S7_Analog_2_Short(double db, double scal)
        {
            return (short)(27648 * db / scal);
        }
        public static int S7_Analog_2_Int(double db, double scal)
        {
            return (int)(27648 * db / scal);
        }

        public static double S7_Analog_2_Double(int wert, double scal, int stellen)
        {
            return System.Math.Round(((double)(wert) * scal / 27648), stellen);
        }

    }
}
