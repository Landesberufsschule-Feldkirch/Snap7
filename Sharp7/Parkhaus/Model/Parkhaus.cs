using System;
using System.Threading;

namespace Parkhaus.Model
{
    public class Parkhaus
    {
        public byte[] BesetzteParkPlaetze { get; set; } = new byte[16];

        public int FreieParkplaetze { get; set; }
        public int FreieParkplaetzeSoll { get; set; }

        public bool ParkhausReihe1 { get; set; } // Signal von der SPS
        public bool ParkhausReihe2 { get; set; }
        public bool ParkhausReihe3 { get; set; }
        public bool ParkhausReihe4 { get; set; }

        public bool ParkhausSpalte1 { get; set; }
        public bool ParkhausSpalte2 { get; set; }
        public bool ParkhausSpalte3 { get; set; }
        public bool ParkhausSpalte4 { get; set; }
        public bool ParkhausSpalte5 { get; set; }
        public bool ParkhausSpalte6 { get; set; }
        public bool ParkhausSpalte7 { get; set; }
        public bool ParkhausSpalte8 { get; set; }

        public Parkhaus()
        {
            var random = new Random();
            random.NextBytes(BesetzteParkPlaetze);

            System.Threading.Tasks.Task.Run(ParkhausTask);
        }
        private void ParkhausTask()
        {
            while (true)
            {
                ParkhausSpalte1 = ParkhausSpalte2 = ParkhausSpalte3 = ParkhausSpalte4 = ParkhausSpalte5 = ParkhausSpalte6 = ParkhausSpalte7 = ParkhausSpalte8 = false;

                if (ParkhausReihe1) (ParkhausSpalte1, ParkhausSpalte2, ParkhausSpalte3, ParkhausSpalte4, ParkhausSpalte5, ParkhausSpalte6, ParkhausSpalte7, ParkhausSpalte8) = AlleBitLesen(BesetzteParkPlaetze[0]);
                if (ParkhausReihe2) (ParkhausSpalte1, ParkhausSpalte2, ParkhausSpalte3, ParkhausSpalte4, ParkhausSpalte5, ParkhausSpalte6, ParkhausSpalte7, ParkhausSpalte8) = AlleBitLesen(BesetzteParkPlaetze[1]);
                if (ParkhausReihe3) (ParkhausSpalte1, ParkhausSpalte2, ParkhausSpalte3, ParkhausSpalte4, ParkhausSpalte5, ParkhausSpalte6, ParkhausSpalte7, ParkhausSpalte8) = AlleBitLesen(BesetzteParkPlaetze[2]);
                if (ParkhausReihe4) (ParkhausSpalte1, ParkhausSpalte2, ParkhausSpalte3, ParkhausSpalte4, ParkhausSpalte5, ParkhausSpalte6, ParkhausSpalte7, ParkhausSpalte8) = AlleBitLesen(BesetzteParkPlaetze[3]);

                FreieParkplaetzeSoll = 0;
                for (var i = 0; i < 4; i++)
                {
                    FreieParkplaetzeSoll += 8 - GesetzteBitZaehlen(BesetzteParkPlaetze[i]);
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }
        internal static int GesetzteBitZaehlen(byte wert)
        {
            var ergebnis = 0;
            for (var i = 0; i < 8; i++)
            {
                var bitMuster = (byte)(1 << i);
                if ((wert & bitMuster) == bitMuster) ergebnis++;
            }
            return ergebnis;
        }
        internal static (bool b0, bool b1, bool b2, bool b3, bool b4, bool b5, bool b6, bool b7) AlleBitLesen(byte parkPlaetzte)
        {
            var b0 = BitMaskierenByte(parkPlaetzte, 0);
            var b1 = BitMaskierenByte(parkPlaetzte, 1);
            var b2 = BitMaskierenByte(parkPlaetzte, 2);
            var b3 = BitMaskierenByte(parkPlaetzte, 3);
            var b4 = BitMaskierenByte(parkPlaetzte, 4);
            var b5 = BitMaskierenByte(parkPlaetzte, 5);
            var b6 = BitMaskierenByte(parkPlaetzte, 6);
            var b7 = BitMaskierenByte(parkPlaetzte, 7);

            return (b0, b1, b2, b3, b4, b5, b6, b7);
        }
        internal static bool BitMaskierenByte(byte parkPlaetzte, int i)
        {
            var bitMuster = (byte)(1 << i % 8);
            return (parkPlaetzte & bitMuster) == bitMuster;
        }
    }
}