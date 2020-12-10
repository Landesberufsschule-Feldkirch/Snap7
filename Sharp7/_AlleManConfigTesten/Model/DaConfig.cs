﻿using System.Collections.ObjectModel;

namespace _AlleManConfigTesten.Model
{
    public class DaConfig
    {
        public ObservableCollection<DaEinstellungen> DigitaleAusgaenge { get; set; } = new ObservableCollection<DaEinstellungen>();
    }

    public class DaEinstellungen
    {
        public DaEinstellungen()
        {
            LaufendeNr = 0;
            StartByte = 0;
            StartBit = 0;
            AnzahlBit = 0;
            Type = AlleManConfigTesten.PlcEinUndAusgaengeTypen.Default;
            Bezeichnung = "";
            Kommentar = "";
        }

        public int LaufendeNr { get; set; }
        public int StartByte { get; set; }
        public int StartBit { get; set; }
        public int AnzahlBit { get; set; }
        public AlleManConfigTesten.PlcEinUndAusgaengeTypen Type { get; set; }
        public string Bezeichnung { get; set; }
        public string Kommentar { get; set; }
    }
}