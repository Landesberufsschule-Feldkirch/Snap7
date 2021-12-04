namespace Heizungsregler.Model;

public class Heizkessel
{
    private readonly Regelstrecken.Pt1Zeitglied _kesselZeitglied;

    private const int Zeitkonstante = 10;
    private const double Faktor = 0.01;
    private const double KesselMinTemperatur = 0;
    private const double KesselMaxTemperatur = 160;
    public Heizkessel() => _kesselZeitglied = new Regelstrecken.Pt1Zeitglied(Zeitkonstante, Faktor, KesselMinTemperatur, KesselMaxTemperatur);
    public void SetBrenner(double brennerLeistung) => _kesselZeitglied.SetNeuerEingangswert(brennerLeistung);
    public double GetKesselTemperatur() => _kesselZeitglied.GetAktuellerWert();
}