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
        public Kommunikation.Datenstruktur Datenstruktur { get; set; }
        public GetConfig GetConfig { get; set; }

        public ManualMode(Kommunikation.Datenstruktur datenstruktur)
        {
            ManualViewModel = new ManualViewModel(this);
            Datenstruktur = datenstruktur;

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
            if (Datenstruktur.AnzahlByteDigitalInput > 0)
            {
                var fensterDi = new FensterDi(GetConfig.ConfigDi, ManualViewModel);
                fensterDi.Show();
            }

            if (Datenstruktur.AnzahlByteDigitalOutput > 0)
            {
                var fensterDa = new FensterDa(GetConfig.ConfigDa, ManualViewModel);
                fensterDa.Show();
            }

            if (Datenstruktur.AnzahlByteAnalogInput > 0)
            {
                var fensterAi = new FensterAi(GetConfig.ConfigAi, ManualViewModel);
                fensterAi.Show();
            }

            if (Datenstruktur.AnzahlByteAnalogOutput <= 0) return;
            var fensterAa = new FensterAa(GetConfig.ConfigAa, ManualViewModel);
            fensterAa.Show();
        }
    }
}
