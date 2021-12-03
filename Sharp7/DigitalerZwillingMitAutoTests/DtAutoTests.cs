using System.Windows.Controls;
using BeschriftungPlc;
using ConfigPlc;
using Kommunikation;

namespace DigitalerZwillingMitAutoTests
{
    public class DtAutoTests
    {

        public Datenstruktur Datenstruktur { get; set; }
        public BeschriftungenPlc BeschriftungenPlc { get; set; }
        public Plc ConfigPlc { get; set; }
        public DisplayPlc.DisplayPlc DisplayPlc { get; set; }
        public PlcDaemon PlcDaemon { get; set; }
        public TestAutomat.TestAutomat TestAutomat { get; set; }

        private string? _pathConfigPlc;
        private string? _pathConfigTests;
        private TabItem _tabItemAutoTest;
        private Action<Datenstruktur, bool> _callbackRangieren;

        private int _anzByteDigInput;
        private int _anzByteDigOutput;
        private int _anzByteAnalogInput;
        private int _anzByteAnalogOutput;


        public DtAutoTests()
        {
            Datenstruktur = new Datenstruktur();
            PlcDaemon = new PlcDaemon();
        }

        public void SetTabItemAutoTests(TabItem tabItemAutoTest) => _tabItemAutoTest = tabItemAutoTest;
        public void SetConfigPlc(string pathConfigPlc) => _pathConfigPlc = pathConfigPlc;
        public void SetConfigTests(string configtests) => _pathConfigTests = configtests;
        public void SetDatenRangierenCallback(Action<Datenstruktur, bool> cbRangieren) => _callbackRangieren = cbRangieren;

        public void SetInputOutput(int anzByteDigInput, int anzByteDigOutput, int anzByteAnalogInput, int anzByteAnalogOutput)
        {
            _anzByteDigInput = anzByteDigInput;
            _anzByteDigOutput = anzByteDigOutput;
            _anzByteAnalogInput = anzByteAnalogInput;
            _anzByteAnalogOutput = anzByteAnalogOutput;
        }


        public void Starten()
        {
            BeschriftungenPlc = new BeschriftungenPlc();
            Datenstruktur.SetDigInput(_anzByteDigInput);
            Datenstruktur.SetDigOutput(_anzByteDigOutput);
            Datenstruktur.SetAnalogInput(_anzByteAnalogInput);
            Datenstruktur.SetAnalogOutput(_anzByteAnalogOutput);

            ConfigPlc = new Plc(_pathConfigPlc);

            DisplayPlc = new DisplayPlc.DisplayPlc(Datenstruktur, ConfigPlc, BeschriftungenPlc);

            PlcDaemon.SetReferenzDatenstruktur(Datenstruktur);
            PlcDaemon.SetCallback(_callbackRangieren);
            PlcDaemon.Starten();

            TestAutomat = new TestAutomat.TestAutomat(Datenstruktur, DisplayPlc.EventBeschriftungAktualisieren, BeschriftungenPlc, PlcDaemon.Plc);
            TestAutomat.SetTestConfig(_pathConfigTests);
            TestAutomat.TabItemFuellen(_tabItemAutoTest, DisplayPlc);
        }



    }
}