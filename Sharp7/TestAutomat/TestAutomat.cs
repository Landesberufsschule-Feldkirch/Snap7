using Kommunikation;
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
        private readonly S71200 _plc;

        private int _pltNextDataIndex = 1;

        public TestAutomat(Datenstruktur datenstruktur, Action<Datenstruktur> cbPlcWindow, BeschriftungPlc.BeschriftungenPlc beschriftungenPlc, S71200 plc)
        {
            BeschriftungenPlc = beschriftungenPlc;
            _datenstruktur = datenstruktur;
            _callbackPlcWindow = cbPlcWindow;
            _plc = plc;

            PlotInitialisieren();
        }

        public void SetTestConfig(string ordnerConfigTests) => ConfigOrdner = new OrdnerLesen(ordnerConfigTests);

        public void TaskBeenden()
        {
            // funktioniert nicht
        }
    }
}