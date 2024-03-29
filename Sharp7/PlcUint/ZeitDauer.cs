﻿using System;
using System.Linq;

namespace PlcDatenTypen
{
    public class ZeitDauer
    {
        public long DauerMs { get; }

        public ZeitDauer(string dauer)
        {
            const long dauer1S = 1000;
            const long dauer1M = 60 * dauer1S;
            const long dauer1H = 60 * dauer1M;
            const long dauer1D = 24 * dauer1H;
            if (dauer.Length == 0)
            {
                DauerMs = 0;
                return;
            }

            if (dauer.StartsWith("T#", StringComparison.OrdinalIgnoreCase))

            {
                // IEC Zeitangabe
                var iecZeit = dauer.Substring(2).ToUpper();
                var posD = iecZeit.IndexOf("D", StringComparison.Ordinal);
                var posH = iecZeit.IndexOf("H", StringComparison.Ordinal);
                var posM = iecZeit.IndexOf("M", StringComparison.Ordinal);
                var posS = iecZeit.IndexOf("S", StringComparison.Ordinal);
                var posMs = iecZeit.IndexOf("MS", StringComparison.Ordinal);

                DauerMs = 0;
                var anfangZahl = 0;
                if (posD > -1)
                {
                    var tage = iecZeit.Substring(anfangZahl, posD);
                    DauerMs = long.Parse(tage) * dauer1D;
                    anfangZahl = posD + 1;
                }

                if (posH > -1)
                {
                    var stunden = iecZeit.Substring(anfangZahl, posH - anfangZahl);
                    DauerMs += long.Parse(stunden) * dauer1H;
                    anfangZahl = posH + 1;
                }

                if (posM > -1 && posM != posMs)
                {
                    var minuten = iecZeit.Substring(anfangZahl, posM - anfangZahl);
                    DauerMs += long.Parse(minuten) * dauer1M;
                    anfangZahl = posM + 1;
                }

                if (posS > -1 && posS != posMs + 1)
                {
                    var sekunden = iecZeit.Substring(anfangZahl, posS - anfangZahl);
                    DauerMs += long.Parse(sekunden) * dauer1S;
                    anfangZahl = posS + 1;
                }

                // ReSharper disable once InvertIf
                if (posMs > -1)
                {
                    var milliSekunden = iecZeit.Substring(anfangZahl, posMs - anfangZahl);
                    DauerMs += long.Parse(milliSekunden);
                }
            }
            else
            {
                DauerMs = dauer.All(char.IsDigit) ? long.Parse(dauer) : 0;
            }
        }
        public static string ConvertLong2Ms(long zeit)
        {
            if (zeit < 1000) return $"{zeit}ms";
            var ms = zeit % 1000;

            zeit /= 1000;
            if (zeit < 60) return $"{zeit}s {ms}ms";

            var s = zeit % 60;
            zeit /= 60;
            var min = zeit;
            if (zeit < 60) return $"{min}min {s}s {ms}ms";

            var h = zeit / 60;
            min = zeit % 60;
            return $"{h}h {min}min {s}s {ms}ms";
        }
    }
}