using System;
using Kommunikation;
using System.IO;
using TestAutomat.AutoTester.Model;
using TestAutomat.PlcDisplay;

namespace TestAutomat
{
    public partial class TestAutomat
    {
        public DirectoryInfo AktuellesProjekt { get; set; }
        public OrdnerLesen ConfigOrdner { get; set; }

        private AutoTesterWindow _autoTesterWindow;
        private PlcWindow _plcWindow;
        private readonly Datenstruktur _datenstruktur;
        private readonly ManualMode.ManualMode _manualMode;
        private readonly Action<Datenstruktur> _callbackPlcWindow;

        public TestAutomat(Datenstruktur datenstruktur, ManualMode.ManualMode manualMode, Action<Datenstruktur> cbPlcWindow)
        {
            _datenstruktur = datenstruktur;
            _manualMode = manualMode;
            _callbackPlcWindow = cbPlcWindow;
        }
        public void SetTestConfig(string autotestconfig) => ConfigOrdner = new OrdnerLesen(autotestconfig);
    }
}