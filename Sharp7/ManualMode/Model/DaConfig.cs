﻿using System.Collections.ObjectModel;

namespace ManualMode.Model
{
    public class DaConfig
    {
        public ObservableCollection<DaEinstellungen> DigitaleAusgaenge { get; set; } = new ObservableCollection<DaEinstellungen>();
    }

    public class DaEinstellungen
    {
        public int LaufendeNr { get; set; }
        public int StartByte { get; set; }
        public int StartBit { get; set; }
        public int AnzahlBit { get; set; }
        public PlcEinUndAusgaengeTypen Type { get; set; }
        public string Bezeichnung { get; set; }
        public string Kommentar { get; set; }


        public DaEinstellungen()
        {
            LaufendeNr = 0;
            StartByte = 0;
            StartBit = 0;
            AnzahlBit = 0;
            Type = PlcEinUndAusgaengeTypen.Default;
            Bezeichnung = "";
            Kommentar = "";
        }
    }
}