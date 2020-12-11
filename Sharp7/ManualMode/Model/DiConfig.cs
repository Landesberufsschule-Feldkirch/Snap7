﻿using System.Collections.ObjectModel;

namespace ManualMode.Model
{
    public class DiConfig
    {
        public ObservableCollection<DiEinstellungen> DigitaleEingaenge { get; set; } = new ObservableCollection<DiEinstellungen>();
    }

    public class DiEinstellungen
    {
        public int LaufendeNr { get; set; }
        public int StartByte { get; set; }
        public int StartBit { get; set; }
        public int AnzahlBit { get; set; }
        public PlcEinUndAusgaengeTypen Type { get; set; }
        public string Bezeichnung { get; set; }
        public string Kommentar { get; set; }
        
        public DiEinstellungen()
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