using System;
using ManualMode.Model;

namespace ManualMode
{
    public partial class MainWindow
    {

        public enum ManualModeConfig
        {
            Di,
            Da,
            Ai,
            Aa
        }

        public GetConfig GetConfig { get; set; }

        public MainWindow()
        {
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
    }
}
