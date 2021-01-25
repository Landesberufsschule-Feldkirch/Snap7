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
        private readonly Action<Datenstruktur> _cbInput;
        private readonly Action<Datenstruktur> _cbOutput;

        private DiFenster _diFenster;
        private DaFenster _daFenster;
        private AiFenster _aiFenster;
        private AaFenster _aaFenster;

        private bool _fensterDiAktiv;
        private bool _fensterDaAktiv;
        private bool _fensterAiAktiv;
        private bool _fensterAaAktiv;

        public ManualMode(Datenstruktur datenstruktur, IPlc plc, Action<Datenstruktur> cbInput, Action<Datenstruktur> cbOutput)
        {
            Datenstruktur = datenstruktur;
            _plc = plc;
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
            if (_fensterDiAktiv) return;
            if (Datenstruktur.AnzahlByteDigitalInput > 0 && Datenstruktur.GetBetriebsartProjekt() != BetriebsartProjekt.AutomatischerSoftwareTest)
            {
                _fensterDiAktiv = true;
                _diFenster = new DiFenster(GetConfig.DiConfig, ManualViewModel);
                _diFenster.Show();
            }

            if (_fensterDaAktiv) return;
            if (Datenstruktur.AnzahlByteDigitalOutput > 0)
            {
                _fensterDaAktiv = true;
                _daFenster = new DaFenster(GetConfig.DaConfig, ManualViewModel);
                _daFenster.Show();
            }

            if (_fensterAiAktiv) return;
            if (Datenstruktur.AnzahlByteAnalogInput > 0 && Datenstruktur.GetBetriebsartProjekt() != BetriebsartProjekt.AutomatischerSoftwareTest)
            {
                _fensterAiAktiv = true;
                _aiFenster = new AiFenster(GetConfig.AiConfig, ManualViewModel);
                _aiFenster.Show();
            }

            if (_fensterAaAktiv) return;
            if (Datenstruktur.AnzahlByteAnalogOutput == 0 || Datenstruktur.GetBetriebsartProjekt() == BetriebsartProjekt.AutomatischerSoftwareTest) return;
            _fensterAaAktiv = true;
            _aaFenster = new AaFenster(GetConfig.AaConfig, ManualViewModel);
            _aaFenster.Show();
        }

        public void SetSichtbarkeitFenster()
        {
            switch (Datenstruktur.GetBetriebsartProjekt())
            {
                case BetriebsartProjekt.LaborPlatte: break;

                case BetriebsartProjekt.Simulation:
                    if (_fensterDiAktiv) _diFenster.SetSichtbar();
                    if (_fensterAiAktiv) _aiFenster.SetSichtbar();
                    if (_fensterAaAktiv) _aaFenster.SetSichtbar();
                    break;

                case BetriebsartProjekt.AutomatischerSoftwareTest:

                    if (_fensterDiAktiv) _diFenster.SetUnsichtbar();
                    if (_fensterAiAktiv) _aiFenster.SetUnsichtbar();
                    if (_fensterAaAktiv) _aaFenster.SetUnsichtbar();
                    break;
            }
        }
    }
}