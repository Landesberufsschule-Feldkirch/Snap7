using System.IO;

namespace ManualMode.Model
{
    public class GetConfig
    {
        public ConfigDi ConfigDi { get; set; }
        public ConfigDa ConfigDa { get; set; }
        public ConfigAi ConfigAi { get; set; }
        public ConfigAa ConfigAa { get; set; }

        public void SetDiConfig(string pfad) => ConfigDi = Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigDi>(File.ReadAllText(pfad));
        internal void SetDaConfig(string pfad) => ConfigDa = Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigDa>(File.ReadAllText(pfad));
        internal void SetAiConfig(string pfad) => ConfigAi = Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigAi>(File.ReadAllText(pfad));
        public void SetAaConfig(string pfad) => ConfigAa = Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigAa>(File.ReadAllText(pfad));
    }
}