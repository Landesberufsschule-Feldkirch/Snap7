﻿using System.Collections.ObjectModel;

namespace ManualMode.Model
{
    public class ConfigAi
    {
        public ObservableCollection<AiEinstellungen> AnalogeEingaenge { get; set; } = new ObservableCollection<AiEinstellungen>();
    }

    public class AiEinstellungen
    {
        public AiEinstellungen()
        {
            LaufendeNr = 0;
            StartByte = 0;
            StartBit = 0;
            AnzahlBit = 0;
            Bezeichnung = "";
            Kommentar = "";
        }

        public int LaufendeNr { get; set; }
        public int StartByte { get; set; }
        public int StartBit { get; set; }
        public int AnzahlBit { get; set; }
        public string Bezeichnung { get; set; }
        public string Kommentar { get; set; }

    }
}
