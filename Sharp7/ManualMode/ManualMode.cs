using ManualMode.ViewModel;
using ManualMode.Model;
using System;

namespace ManualMode
{
    public class ManualMode
    {
        public enum ManualModeConfig
        {
            Di,
            Da,
            Ai,
            Aa
        }

        private readonly ManualViewModel _manualViewModel;

        private readonly int _anzByteDigitalInput;
        private readonly int _anzByteDigitalOutput;
        private readonly int _anzByteAnalogInput;
        private readonly int _anzByteAnalogOutput;

        public byte[] ByteDigitalInput { get; set; }
        public byte[] ByteDigitalOutput { get; set; }
        public byte[] ByteAnalogInput { get; set; }
        public byte[] ByteAnalogOutput { get; set; }

        public GetConfig GetConfig { get; set; }

        public ManualMode(byte[] byteDigInput, byte[] byteDigitalOutput, byte[] byteAnalogInput, byte[] byteAnalogOutput, int anzByteDigInput, int anzByteDigOutput, int anzByteAnalogInput, int anzByteAnalogOutput)
        {
            _manualViewModel = new ManualViewModel(this);

            ByteDigitalInput = byteDigInput;
            ByteDigitalOutput = byteDigitalOutput;
            ByteAnalogInput = byteAnalogInput;
            ByteAnalogOutput = byteAnalogOutput;

            _anzByteDigitalInput = anzByteDigInput;
            _anzByteDigitalOutput = anzByteDigOutput;
            _anzByteAnalogInput = anzByteAnalogInput;
            _anzByteAnalogOutput = anzByteAnalogOutput;

            GetConfig = new GetConfig();
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
                var fensterDi = new FensterDi(GetConfig.ConfigDi, _manualViewModel);
                fensterDi.Show();
            }

            if (_anzByteDigitalOutput > 0)
            {
                var fensterDa = new FensterDa(GetConfig.ConfigDa, _manualViewModel);
                fensterDa.Show();
            }

            if (_anzByteAnalogInput > 0)
            {
                var fensterAi = new FensterAi(GetConfig.ConfigAi, _manualViewModel);
                fensterAi.Show();
            }

            if (_anzByteAnalogOutput > 0)
            {
                var fensterAa = new FensterAa(GetConfig.ConfigAa, _manualViewModel);
                fensterAa.Show();
            }
        }


        public void BitToggelnDa(DaEinstellungen einstellungen)
        {
            var bitMuster = (byte)(1 << einstellungen.StartBit);
            if ((ByteDigitalOutput[einstellungen.StartByte] & bitMuster) == bitMuster)
            {
                ByteDigitalOutput[einstellungen.StartByte] &= (byte)~bitMuster;
            }
            else
            {
                ByteDigitalOutput[einstellungen.StartByte] |= bitMuster;
            }
        }

        public void BitTastenDa(bool status, DaEinstellungen einstellungen)
        {
            var bitMuster = (byte)(1 << einstellungen.StartBit);
            if (status)
            {
                ByteDigitalOutput[einstellungen.StartByte] |= bitMuster;
            }
            else
            {
                ByteDigitalOutput[einstellungen.StartByte] &= (byte)~bitMuster;
            }
        }
    }
}
