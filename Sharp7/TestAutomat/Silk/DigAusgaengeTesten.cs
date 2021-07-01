using PlcDatenTypen;

namespace TestAutomat.Silk
{
    internal class DigAusgaengeTesten
    {

        public enum StatusDigAusgaenge
        {
            Init = 0,
            AufBitmusterWarten,
            BitmusterLiegtAn,
            SchrittAbgeschlossen,
            Timeout
        }

        private static int _aktuellerSchritt;

        private StatusDigAusgaenge _statusDigAusgaenge;

        private readonly Uint _bitMuster;
        private readonly Uint _bitMaske;
        private readonly ZeitDauer _timeout;
        private readonly string _kommentar;
        private long _startZeit;
        private readonly long _dauerMin;
        private readonly long _dauerMax;


        public DigAusgaengeTesten(ulong bitMuster, ulong bitMaske, string dauer, double toleranz, string timeout, string kommentar)
        {
            _statusDigAusgaenge = StatusDigAusgaenge.Init;

            _bitMuster = new Uint(bitMuster);
            _bitMaske = new Uint(bitMaske);
            var dauer1 = new ZeitDauer(dauer);
            _dauerMin = (long)(dauer1.DauerMs * (1 - toleranz));
            _dauerMax = (long)(dauer1.DauerMs * (1 + toleranz));
            _timeout = new ZeitDauer(timeout);
            _kommentar = kommentar;
        }

        internal void SetStartzeit(long zeit) => _startZeit = zeit;
        internal long GetZeitdauerMin() => _startZeit + _dauerMin;
        internal long GetZeitdauerMax() => _startZeit + _dauerMax;
        internal static int GetAktuellerSchritt() => _aktuellerSchritt;
        internal static void SetAktuellerSchritt(int schritt) => _aktuellerSchritt = schritt;
        internal static void SetNaechsterSchritt() => _aktuellerSchritt++;


        internal StatusDigAusgaenge GetAktuellerStatus() => _statusDigAusgaenge;
        internal void SetAktuellerStatus(StatusDigAusgaenge status) => _statusDigAusgaenge = status;


        public long GetTimeoutMs() => _startZeit + _timeout.DauerMs;
        internal Uint GetBitMaske() => _bitMaske;
        internal Uint GetBitMuster() => _bitMuster;
        internal string GetKommentar() => _kommentar;
    }
}