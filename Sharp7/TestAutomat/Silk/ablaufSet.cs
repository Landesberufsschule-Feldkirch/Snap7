using PlcDatenTypen;

namespace TestAutomat.Silk
{
    internal class AblaufSet
    {
        private readonly Uint _bitMuster;
        private readonly ZeitDauer _dauer;
        private readonly string _kommentar;

        public AblaufSet(ulong bitMuster, string dauer, string kommentar)
        {
            _bitMuster = new Uint(bitMuster);
            _dauer = new ZeitDauer(dauer);
            _kommentar = kommentar;
        }

        internal Uint GetBitmuster() => _bitMuster;
        internal ZeitDauer GetDauer() => _dauer;
        internal string GetKommentar() => _kommentar;
    }
}