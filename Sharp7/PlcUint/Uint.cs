using System;

namespace PlcDatenTypen
{
    public class Uint
    {
        private readonly uint _uintDec;

        public Uint(string zahl)
        {

            if (zahl.Contains("#"))
            {
                // ReSharper disable once ConvertIfStatementToSwitchStatement
                if (zahl.Substring(0, 2) == "2#")
                {
                    _uintDec = Convert.ToUInt16(zahl[2..].Replace("_", ""), 2);
                    return;
                }

                if (zahl.Substring(0, 2) == "8#")
                {
                    _uintDec = Convert.ToUInt16(zahl[2..], 8);
                    return;
                }

                if (zahl.Substring(0, 3) == "16#")
                {
                    _uintDec = Convert.ToUInt16(zahl[3..], 16);
                    return;
                }
            }



            _uintDec = Convert.ToUInt16(zahl);
        }

        public uint GetDec() => _uintDec;

        public bool GetBitGesetzt(int i)
        {
            var bitMuster = (uint)(1 << i);
            return (_uintDec & bitMuster) == bitMuster;
        }
    }
}