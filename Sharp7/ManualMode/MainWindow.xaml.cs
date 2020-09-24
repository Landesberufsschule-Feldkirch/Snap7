using ManualMode.Model;
using System;

namespace ManualMode
{
    public partial class MainWindow
    {

        private readonly int _anzByteDigitalInput;
        private readonly int _anzByteDigOutput;
        private readonly int _anzByteAnalogInput;
        private readonly int _anzByteAnalogOutput;

        private readonly byte[] _byteDigitalInput;
        private readonly byte[] _byteDigitalOutput;
        private readonly byte[] _byteAnalogInput;
        private readonly byte[] _byteAnalogOutput;



        public enum ManualModeConfig
        {
            Di,
            Da,
            Ai,
            Aa
        }

        public GetConfig GetConfig { get; set; }

        public MainWindow(byte[] byteDigInput, byte[] byteDigitalOutput, byte[] byteAnalogInput, byte[] byteAnalogOutput, int anzByteDigInput, int anzByteDigOutput, int anzByteAnalogInput, int anzByteAnalogOutput)
        {

            _byteDigitalInput = byteDigInput;
            _byteDigitalOutput = byteDigitalOutput;
            _byteAnalogInput = byteAnalogInput;
            _byteAnalogOutput = byteAnalogOutput;

            _anzByteDigitalInput = anzByteDigInput;
            _anzByteDigOutput = anzByteDigOutput;
            _anzByteAnalogInput = anzByteAnalogInput;
            _anzByteAnalogOutput = anzByteAnalogOutput;


            GetConfig = new GetConfig();

            InitializeComponent();
           

           

            
        }


        public void SetManualConfig(ManualModeConfig config, string pfad)
        {
            switch (config)
            {
                case ManualModeConfig.Di:
                    GetConfig.SetDiConfig(pfad);
                    break;
                case ManualModeConfig.Da:
                    GetConfig.SetDaConfig(pfad);
                    break;
                case ManualModeConfig.Ai:
                    GetConfig.SetAiConfig(pfad);
                    break;
                case ManualModeConfig.Aa:
                    GetConfig.SetAaConfig(pfad);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(config), config, null);
            }
        }

        public void FensterAnzeigen()
        {


            if (_anzByteDigitalInput > 0)
            {
                var fensterDi = new FensterDi(GetConfig.ConfigDi);
                fensterDi.Show();
            }


            if (_anzByteDigitalInput > 0)
            {
                var fensterDa = new FensterDa(GetConfig.ConfigDa);
                fensterDa.Show();
            }

            if (_anzByteAnalogInput > 0)
            {
                var fensterAi = new FensterAi(GetConfig.ConfigAi);
                fensterAi.Show();
            }

            if (_anzByteAnalogOutput > 0)
            {
                var fensterAa = new FensterAa(GetConfig.ConfigAa);
                fensterAa.Show();
            }

        }
    }
}
