using System.Text;

namespace Kommunikation
{
    public class SharedCode
    {
        public static bool GetBitAtPosition(byte[] buffer, int bitPos)
        {
            byte[] mask = { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80 };

            var bit = bitPos % 8;
            var pos = bitPos / 8;

            if (bit < 0) bit = 0;
            if (bit > 7) bit = 7;
            return (buffer[pos] & mask[bit]) != 0;
        }
        public static int GetSIntAtPosition(byte[] buffer, int pos)
        {
            int value = buffer[pos];
            if (value < 128) return value;
            return value - 256;
        }
        public static void SetBitAtPosition(byte[] buffer, int bitPos, bool value)
        {
            byte[] mask = { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80 };

            var bit = bitPos % 8;
            var pos = bitPos / 8;

            if (bit < 0) bit = 0;
            if (bit > 7) bit = 7;

            if (value) buffer[pos] = (byte)(buffer[pos] | mask[bit]);
            else buffer[pos] = (byte)(buffer[pos] & ~mask[bit]);
        }
        public static void SetSIntAtPosition(byte[] buffer, int pos, int value)
        {
            if (value < -128) value = -128;
            if (value > 127) value = 127;
            buffer[pos] = (byte)value;
        }
        public static void SetIntAtPosition(byte[] buffer, int pos, short value)
        {
            buffer[pos] = (byte)(value >> 8);
            buffer[pos + 1] = (byte)(value & 0x00FF);
        }
        public static void SetUIntAtPosition(byte[] buffer, int pos, ushort value)
        {
            buffer[pos] = (byte)(value >> 8);
            buffer[pos + 1] = (byte)(value & 0x00FF);
        }
    }
}
