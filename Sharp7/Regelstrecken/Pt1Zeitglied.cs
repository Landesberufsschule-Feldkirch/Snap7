using PlcDatenTypen;

namespace Regelstrecken;

public class Pt1Zeitglied
{
    private readonly int _zeitKonstanteMs;
    private readonly double _faktor;
    private readonly double _limitMin;
    private readonly double _limitMax;

    private double _neuerSollwert;
    private double _aktuell;

    public Pt1Zeitglied(int zeitKonstanteMs, double faktor, double limitMin, double limitMax)
    {
        _zeitKonstanteMs = zeitKonstanteMs;
        _faktor = faktor;
        _limitMin = limitMin;
        _limitMax = limitMax;

        System.Threading.Tasks.Task.Run(ZeitgliedTask);
    }
    private void ZeitgliedTask()
    {
        while (true)
        {
            EinZyklusAbwarten();

            Thread.Sleep(_zeitKonstanteMs);
        }
        // ReSharper disable once FunctionNeverReturns
    }
    public void SetNeuerEingangswert(double neu) => _neuerSollwert = neu;
    public double GetAktuellerWert() => _aktuell;

    public void TestZeitglied(int anzahl)
    {
        for (var i = 0; i < anzahl; i++) EinZyklusAbwarten();
    }
    private void EinZyklusAbwarten()
    {
        var akt = _aktuell + _faktor * (_neuerSollwert - _aktuell);
        _aktuell = Simatic.Clamp(akt, _limitMin, _limitMax);
    }
}