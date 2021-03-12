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
        private readonly PlotWindow _plotWindow;
        private readonly Datenstruktur _datenstruktur;
        private readonly ConfigPlc.Plc _configPlc;
        private readonly Action<Datenstruktur> _callbackPlcWindow;

        public TestAutomat(Datenstruktur datenstruktur, ConfigPlc.Plc configPlc, Action<Datenstruktur> cbPlcWindow, BeschriftungPlc.BeschriftungenPlc beschriftungenPlc)
        {
            _plotWindow = new PlotWindow();
            _datenstruktur = datenstruktur;
            _configPlc = configPlc;
            _callbackPlcWindow = cbPlcWindow;
            BeschriftungenPlc = beschriftungenPlc;
        }
        public void SetTestConfig(string ordnerConfigTests) => ConfigOrdner = new OrdnerLesen(ordnerConfigTests);

        public void TaskBeenden()
        {
            // funktioniert nicht
        }
    }
}