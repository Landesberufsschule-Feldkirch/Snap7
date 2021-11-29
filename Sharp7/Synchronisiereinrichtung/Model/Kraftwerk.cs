
using Synchronisiereinrichtung.Model;
using System;
using System.Threading;
using Utilities;


namespace Synchronisiereinrichtung.Kraftwerk.Model
{
    public class Kraftwerk
    {
        public readonly Drehstromgenerator Generator = new(0.35, 1 / 30.0);
        public readonly Statemachine KraftwerkStatemachine;

        public double FrequenzDifferenz { get; set; }
        public double OptimalerSpannungswert { get; set; }
        public bool Q1 { get; set; }
        public bool KraftwerkStarten { get; set; }
        public bool KraftwerkStoppen { get; set; }
        public double VentilY { get; set; }
        public double GeneratorIe { get; set; }
        public double GeneratorN { get; set; }
        public double GeneratorF { get; set; }
        public double GeneratorU { get; set; }
        public double GeneratorP { get; set; }
        public double GeneratorCosPhi { get; set; }
        public double SpannungsdifferenzGeneratorNetz { get; set; }
        public double NetzU { get; set; }
        public double NetzF { get; set; }
        public double NetzP { get; set; }
        public double NetzCosPhi { get; set; }
        public bool MessgeraetAnzeigen { get; set; }
        public bool MaschineTot { get; set; }
        public SynchronisierungAuswahl SynchAuswahl { get; set; }
        public double SpannungsDifferenz { get; set; }
        public double MessgeraetOptimalerBereich { get; set; }

        public Kraftwerk()
        {
            FrequenzDifferenz = 5;
            KraftwerkStatemachine = new Synchronisiereinrichtung.Model.Statemachine(this);
            System.Threading.Tasks.Task.Run(KraftwerkTask);
        }
        public void KraftwerkTask()
        {
            const double zeitdauer = 10;//ms

            double generatorWinkel = 0;
            double netzWinkel = 0;

            while (true)
            {
                KraftwerkStatemachine.Fire(Synchronisiereinrichtung.Model.Statemachine.Trigger.Aktualisieren);

                netzWinkel = Synchronisiereinrichtung.Model.DrehstromZeiger.WinkelBerechnen(zeitdauer, NetzF, netzWinkel);
                generatorWinkel = Synchronisiereinrichtung.Model.DrehstromZeiger.WinkelBerechnen(zeitdauer, GeneratorF, generatorWinkel);

                var netzMomentanspannung = Synchronisiereinrichtung.Model.DrehstromZeiger.GetSpannung(netzWinkel, NetzU);
                var generatorMomentanspannung = Synchronisiereinrichtung.Model.DrehstromZeiger.GetSpannung(generatorWinkel, GeneratorU);

                FrequenzDifferenz = Math.Abs(NetzF - GeneratorF);
                var spannungsDiff = new Zeiger(generatorMomentanspannung, netzMomentanspannung);

                switch (SynchAuswahl)
                {
                    case SynchronisierungAuswahl.UfPhase:
                    case SynchronisierungAuswahl.UfPhaseLeistung:
                    case SynchronisierungAuswahl.UfPhaseLeistungsfaktor:
                        SpannungsdifferenzGeneratorNetz = spannungsDiff.Laenge();
                        break;

                    case SynchronisierungAuswahl.Uf:
                        break;
                    default:
                        SpannungsdifferenzGeneratorNetz = Math.Abs(NetzU - GeneratorU);
                        break;
                }

                SpannungsDifferenz = SpannungsdifferenzGeneratorNetz;

                Thread.Sleep((int)zeitdauer);
            }
            // ReSharper disable once FunctionNeverReturns
        }
        internal void Starten()
        {
            KraftwerkStarten = true;
            KraftwerkStoppen = false;
        }
        internal void Stoppen()
        {
            KraftwerkStoppen = true;
            KraftwerkStarten = false;
        }
        internal void Synchronisieren() => KraftwerkStatemachine.Fire(Synchronisiereinrichtung.Model.Statemachine.Trigger.Synchronisieren);
        internal void Reset() => KraftwerkStatemachine.Fire(Synchronisiereinrichtung.Model.Statemachine.Trigger.Reset);
    }
}