using System.Collections.Generic;
using System.Threading;

namespace LAP_2010_4_Abfuellanlage.Model;

public class AbfuellAnlage
{
    public List<BlechDosen> AlleDosen { get; set; }
    public bool S1 { get; set; } // Taster Reset
    public bool S2 { get; set; } // Taster Start
    public bool B1 { get; set; } // Behälter Füllstand
    public bool B2 { get; set; } // EineDose Dose Füllen
    public bool Q1 { get; set; } // Motor Förderband
    public bool K1 { get; set; } // Spule Magazin Förderband
    public bool K2 { get; set; } // Spule Füllen
    public bool P1 { get; set; } // Meldeleuchte Behälter Leer
    public double Pegel { get; set; }

    private readonly int _anzahlDosen;
    private int _aktuelleDose;
    private const double LeerGeschwindigkeit = 0.002;

    public AbfuellAnlage()
    {
        Pegel = 0.9;

        AlleDosen = new List<BlechDosen>
        {
            new(_anzahlDosen++),
            new(_anzahlDosen++),
            new(_anzahlDosen++),
            new(_anzahlDosen++),
            new(_anzahlDosen++)
        };

        System.Threading.Tasks.Task.Run(AbfuellAnlageTask);
    }
    private void AbfuellAnlageTask()
    {
        while (true)
        {
            if (K2) Pegel -= LeerGeschwindigkeit;
            if (Pegel < 0) Pegel = 0;

            B1 = Pegel > 0.1;

            if (K1) AlleDosen[_aktuelleDose].DosenVereinzeln();

            B2 = false;
            foreach (var dose in AlleDosen)
            {
                bool lichtschranke;
                (lichtschranke, _aktuelleDose) = dose.DosenBewegen(Q1, _anzahlDosen, _aktuelleDose);
                B2 |= lichtschranke;
            }

            Thread.Sleep(10);
        }
        // ReSharper disable once FunctionNeverReturns
    }
    internal void Nachfuellen() => Pegel = 1;
    internal void AllesReset()
    {
        Pegel = 0.9;
        _aktuelleDose = 0;
        foreach (var dose in AlleDosen) { dose.Reset(); }
    }
}