using System;

namespace PlcDatenTypen
{
    public class Uint
    {
        private readonly ulong _uintDec;

        public Uint(ulong zahl) => _uintDec = zahl;

        public Uint(string zahl)
        {
            if (zahl.Contains("#"))
            {
                // ReSharper disable once ConvertIfStatementToSwitchStatement
                if (zahl.Substring(0, 2) == "2#")
                {
                    _uintDec = Convert.ToUInt64(zahl.Substring(2).Replace("_", ""), 2);
                    return;
                }

                if (zahl.Substring(0, 2) == "8#")
                {
                    _uintDec = Convert.ToUInt64(zahl.Substring(2), 8);
                    return;
                }

                // ReSharper disable once InvertIf
                if (zahl.Substring(0, 3) == "16#")
                {
                    _uintDec = Convert.ToUInt64(zahl.Substring(3), 16);
                    return;
                }

                throw new ArgumentOutOfRangeException(nameof(zahl));
            }

            _uintDec = Convert.ToUInt64(zahl);
        }

        public ulong GetDec() => _uintDec;
        public string GetBin4Bit()
        {
            if (_uintDec > Math.Pow(2, 4) - 1) return "uuups - zu große Zahl!";

            var binaer = Convert.ToString((long)_uintDec, 2).PadLeft(4, '0');
            return $"2#{binaer}";
        }
        public string GetBin8Bit()
        {
            if (_uintDec > Math.Pow(2, 8) - 1) return "uuups - zu große Zahl!";

            var binaer = Convert.ToString((long)_uintDec, 2).PadLeft(8, '0');
            return $"2#{binaer.Substring(0, 4)}_{binaer.Substring(4, 4)}";
        }
        public string GetBin16Bit()
        {
            if (_uintDec > Math.Pow(2, 16) - 1) return "uuups - zu große Zahl!";

            var binaer = Convert.ToString((long)_uintDec, 2).PadLeft(16, '0');
            return $"2#{binaer.Substring(0, 4)}_{binaer.Substring(4, 4)}"
                    + $"_{binaer.Substring(8, 4)}_{binaer.Substring(12, 4)}";
        }
        public string GetBin12Bit()
        {
            if (_uintDec > Math.Pow(2, 12) - 1) return "uuups - zu große Zahl!";

            var binaer = Convert.ToString((long)_uintDec, 2).PadLeft(12, '0');
            return $"2#{binaer.Substring(0, 4)}_{binaer.Substring(4, 4)}"
                   + $"_{binaer.Substring(8, 4)}";
        }
        public string GetBin32Bit()
        {
            if (_uintDec > Math.Pow(2, 32) - 1) return "uuups - zu große Zahl!";

            var binaer = Convert.ToString((long)_uintDec, 2).PadLeft(32, '0');
            return $"2#{binaer.Substring(0, 4)}_{binaer.Substring(4, 4)}"
                   + $"_{binaer.Substring(8, 4)}_{binaer.Substring(12, 4)}"
                   + $"_{binaer.Substring(16, 4)}_{binaer.Substring(20, 4)}"
                   + $"_{binaer.Substring(24, 4)}_{binaer.Substring(28, 4)}";
        }
        public string GetBinBit(int anzahlBit)
        {
            switch (anzahlBit)
            {
                case 0: return GetBin();
                case 4: return GetBin4Bit();
                case 8: return GetBin8Bit();
                case 12: return GetBin12Bit();
                case 16: return GetBin16Bit();
                case 32: return GetBin32Bit();
            }

            return "--2#--";
        }
        public string GetBin()
        {
            var anzahlBit = GetAnzahlBit();

            if (anzahlBit < 5) return GetBin4Bit();
            if (anzahlBit < 9) return GetBin8Bit();
            if (anzahlBit < 13) return GetBin12Bit();
            return anzahlBit < 17 ? GetBin16Bit() : GetBin32Bit();
        }
        public string GetHex4Bit()
        {
            if (_uintDec > Math.Pow(2, 4) - 1) return "uuups - zu große Zahl!";
            var hex = Convert.ToString((long)_uintDec, 16).ToUpper();
            return $"16#{hex}";
        }
        public string GetHex8Bit()
        {
            if (_uintDec > Math.Pow(2, 8) - 1) return "uuups - zu große Zahl!";
            var hex = Convert.ToString((long)_uintDec, 16).PadLeft(2, '0').ToUpper();
            return $"16#{hex}";
        }
        public string GetHex12Bit()
        {
            if (_uintDec > Math.Pow(2, 12) - 1) return "uuups - zu große Zahl!";
            var hex = Convert.ToString((long)_uintDec, 16).PadLeft(3, '0').ToUpper();
            return $"16#{hex}";
        }
        public string GetHex16Bit()
        {
            if (_uintDec > Math.Pow(2, 16) - 1) return "uuups - zu große Zahl!";
            var hex = Convert.ToString((long)_uintDec, 16).PadLeft(4, '0').ToUpper();
            return $"16#{hex}";
        }
        public string GetHex32Bit()
        {
            if (_uintDec > Math.Pow(2, 32) - 1) return "uuups - zu große Zahl!";
            var hex = Convert.ToString((long)_uintDec, 16).PadLeft(8, '0').ToUpper();
            return $"16#{hex}";
        }
        public string GetHexBit(int anzahlBit)
        {
            switch (anzahlBit)
            {
                case 0: return GetHex();
                case 4: return GetHex4Bit();
                case 8: return GetHex8Bit();
                case 12: return GetHex12Bit();
                case 16: return GetHex16Bit();
                case 32: return GetHex32Bit();
            }

            return "--16#--";
        }
        public string GetHex()
        {
            var anzahlBit = GetAnzahlBit();

            if (anzahlBit < 5) return GetHex4Bit();
            if (anzahlBit < 9) return GetHex8Bit();
            if (anzahlBit < 13) return GetHex12Bit();
            return anzahlBit < 17 ? GetHex16Bit() : GetHex32Bit();
        }
        public uint GetAnzahlBit()
        {
            if (_uintDec == 0) return 0;
            return 1 + (uint)Math.Log(_uintDec, 2.0);
        }
        public bool GetBitGesetzt(int i)
        {
            var bitMuster = (uint)(1 << i);
            return (_uintDec & bitMuster) == bitMuster;
        }
    }
}