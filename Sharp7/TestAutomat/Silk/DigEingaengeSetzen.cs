using PlcDatenTypen;

namespace TestAutomat.Silk;

internal class DigEingaengeSetzen
{
    public enum StatusDigEingaenge
    {
        Init = 0,
        SchrittAktiv,
        SchrittAbgeschlossen
    }

    private static int _aktuellerSchritt;

    private StatusDigEingaenge _statusDigEingaenge;
    private readonly Uint _bitMuster;
    private readonly ZeitDauer _dauer;
    private readonly string _kommentar;
    private long _startZeit;

    public DigEingaengeSetzen(ulong bitMuster, string dauer, string kommentar)
    {
        _statusDigEingaenge = StatusDigEingaenge.Init;
        _bitMuster = new Uint(bitMuster);
        _dauer = new ZeitDauer(dauer);
        _kommentar = kommentar;
    }

    internal void SetStartzeit(long zeit) => _startZeit = zeit;
    internal long GetEndZeit() => _startZeit + _dauer.DauerMs;

    internal static int GetAktuellerSchritt() => _aktuellerSchritt;
    internal static void SetAktuellerSchritt(int schritt) => _aktuellerSchritt = schritt;
    internal static void SetNaechsterSchritt() => _aktuellerSchritt++;

    internal StatusDigEingaenge GetAktuellerStatus() => _statusDigEingaenge;
    internal void SetAktuellerStatus(StatusDigEingaenge status) => _statusDigEingaenge = status;

    internal Uint GetBitmuster() => _bitMuster;
    internal ZeitDauer GetDauer() => _dauer;
    internal string GetKommentar() => _kommentar;
}