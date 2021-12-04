namespace ConfigPlc;

public class Plc
{
    public GetConfigPlc GetConfigPlc { get; set; }

    public Plc(string pfad)
    {
        GetConfigPlc = new GetConfigPlc();

        GetConfigPlc.SetDiConfig($"{pfad}/DI.json");
        GetConfigPlc.SetDaConfig($"{pfad}/DA.json");
        GetConfigPlc.SetAiConfig($"{pfad}/AI.json");
        GetConfigPlc.SetAaConfig($"{pfad}/AA.json");
    }
    public Plc()
    {
        GetConfigPlc = new GetConfigPlc();
    }
    public void SetPath(string pfad)
    {
        GetConfigPlc.SetDiConfig($"{pfad}/DI.json");
        GetConfigPlc.SetDaConfig($"{pfad}/DA.json");
        GetConfigPlc.SetAiConfig($"{pfad}/AI.json");
        GetConfigPlc.SetAaConfig($"{pfad}/AA.json");
    }
}