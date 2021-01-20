using Kommunikation;
using ManualMode.Model;
using ManualMode.ViewModel;
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

        public ManualViewModel ManualViewModel { get; set; }
        public GetConfig GetConfig { get; set; }
        public Datenstruktur Datenstruktur { get; set; }

        private IPlc _plc;
        private readonly S71200.BetriebsartProjekt _betriebsartProjekt;
        private readonly Action<Datenstruktur> _cbInput;
        private readonly Action<Datenstruktur> _cbOutput;

        public ManualMode(Datenstruktur datenstruktur, IPlc plc, Action<Datenstruktur> cbInput, Action<Datenstruktur> cbOutput)
        {
            Datenstruktur = datenstruktur;
            _plc = plc;
            _betriebsartProjekt = _plc.GetBetriebsartProjekt();
            _cbInput = cbInput;
            _cbOutput = cbOutput;

            ManualViewModel = new ManualViewModel(this);

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

        public void ManualModeStarten()
        {
            if (_plc.GetPlcModus() == "S7-1200")
            {
                _plc.SetPlcModus("Manual");
                _plc.SetTaskRunning(false);
                _plc = new Manual(Datenstruktur, _cbInput, _cbOutput);
            }
            FensterAnzeigen();
        }

        public void FensterAnzeigen()
        {
            if (Datenstruktur.AnzahlByteDigitalInput > 0)
            {
                var diFenster = new DiFenster(GetConfig.DiConfig, ManualViewModel);
                diFenster.Show();
            }

            if (Datenstruktur.AnzahlByteDigitalOutput > 0)
            {
                var daFenster = new DaFenster(GetConfig.DaConfig, ManualViewModel);
                daFenster.Show();
            }

            if (Datenstruktur.AnzahlByteAnalogInput > 0)
            {
                var aiFenster = new AiFenster(GetConfig.AiConfig, ManualViewModel);
                aiFenster.Show();
            }

            if (Datenstruktur.AnzahlByteAnalogOutput <= 0) return;
            var aaFenster = new AaFenster(GetConfig.AaConfig, ManualViewModel);
            aaFenster.Show();
        }



    }
}
