namespace ConfigPlc
{
    public class Plc
    {
        public GetConfig GetConfig { get; set; }

        public Plc(string pfad)
        {
            GetConfig = new GetConfig();

            GetConfig.SetDiConfig($"{pfad}/DI.json");
            GetConfig.SetDaConfig($"{pfad}/DA.json");
            GetConfig.SetAiConfig($"{pfad}/AI.json");
            GetConfig.SetAaConfig($"{pfad}/AA.json");
        }
    }
}