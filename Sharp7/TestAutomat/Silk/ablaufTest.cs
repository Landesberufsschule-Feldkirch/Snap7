using PlcDatenTypen;

namespace TestAutomat.Silk
{
    internal class AblaufTest
    {
        private readonly Uint _bitMuster;
        private readonly Uint _bitMaske;
        private readonly ZeitDauer _dauer;
        private bool _schrittAktiv;
        private readonly ZeitDauer _timeout;
        private readonly string _kommentar;

        public AblaufTest(ulong bitMuster, ulong bitMaske, string dauer, string timeout, string kommentar)
        {
            _bitMuster = new Uint(bitMuster);
            _bitMaske = new Uint(bitMaske);
            _dauer = new ZeitDauer(dauer);
            _schrittAktiv =false;
            _timeout = new ZeitDauer(timeout);
            _kommentar = kommentar;
        }
        public long GetTimeoutMs() => _timeout.GetZeitDauerMs();
        internal Uint GetBitMaske() => _bitMaske;
        internal Uint GetBitMuster() => _bitMuster;
        internal string GetKommentar() => _kommentar;
        internal bool GetSchrittAktiv() => _schrittAktiv;
        internal void SetSchrittAktiv(bool wert) => _schrittAktiv= wert;
    }
}