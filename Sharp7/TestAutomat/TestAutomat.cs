﻿using Kommunikation;
using System;
using System.IO;
using TestAutomat.Model;

namespace TestAutomat
{
    public partial class TestAutomat
    {
        public DirectoryInfo AktuellesProjekt { get; set; }
        public OrdnerLesen ConfigOrdner { get; set; }
        public BeschriftungPlc.BeschriftungenPlc BeschriftungenPlc { get; set; }


        private AutoTesterWindow _autoTesterWindow;
        private readonly Datenstruktur _datenstruktur;
        private readonly Action<Datenstruktur> _callbackPlcWindow;

        private int _nextDataIndex = 1;

        public TestAutomat(Datenstruktur datenstruktur, Action<Datenstruktur> cbPlcWindow, BeschriftungPlc.BeschriftungenPlc beschriftungenPlc)
        {
            _datenstruktur = datenstruktur;
            _callbackPlcWindow = cbPlcWindow;
            BeschriftungenPlc = beschriftungenPlc;

            PlotInitialisieren();
        }

        public void SetTestConfig(string ordnerConfigTests) => ConfigOrdner = new OrdnerLesen(ordnerConfigTests);

        public void TaskBeenden()
        {
            // funktioniert nicht
        }
    }
}