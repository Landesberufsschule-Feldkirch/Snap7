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

        public TestAutomat(Datenstruktur datenstruktur, ManualMode.ManualMode manualMode)
        {
            _datenstruktur = datenstruktur;
            _manualMode = manualMode;
        }
        public void SetTestConfig(string autotestconfig)
        {
            ConfigOrdner = new OrdnerLesen(autotestconfig);

            AutoTester.Silk.Silk.Compile(ConfigOrdner.AlleTestOrdner[0] + "\\testSource.ssc");
        }
    }
}