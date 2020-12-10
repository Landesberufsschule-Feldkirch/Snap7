using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _AlleManConfigTesten.Model
{
    public class AlleManConfigTesten
    {
        private readonly ViewModel.ViewModel _viewModel;

        public Einstellungen Einstellungen { get; set; }

        public enum PlcEinUndAusgaengeTypen
        {
            Default,
            Ascii,
            SiemensAnalogwertProzent,
            SiemensAnalogwertPromille,
            BitmusterByte,
            Schieberegler
        }

        internal class MyEnumConverter : JsonConverter<PlcEinUndAusgaengeTypen>
        {
            public override PlcEinUndAusgaengeTypen ReadJson(JsonReader reader, Type objectType, PlcEinUndAusgaengeTypen existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                if (reader.Value == null) return default;
                var token = reader.Value as string ?? reader.Value.ToString();
                var stripped = Regex.Replace(token!, @"<[^>]+>", string.Empty);
                return Enum.TryParse<PlcEinUndAusgaengeTypen>(stripped, out var result) ? result : default;
            }
            public override void WriteJson(JsonWriter writer, PlcEinUndAusgaengeTypen value, JsonSerializer serializer) => writer.WriteValue(value.ToString());
        }

        private readonly IEnumerable<string> _aaDateiListe;
        private readonly IEnumerable<string> _aiDateiListe;
        private readonly IEnumerable<string> _daDateiListe;
        private readonly IEnumerable<string> _diDateiListe;

        public AlleManConfigTesten(ViewModel.ViewModel viewModel)
        {
            _viewModel = viewModel;

            Einstellungen = JsonConvert.DeserializeObject<Einstellungen>(File.ReadAllText("Einstellungen.json"));

            _aaDateiListe = DateiListenEinlesen("AA.json");
            _aiDateiListe = DateiListenEinlesen("AI.json");
            _daDateiListe = DateiListenEinlesen("DA.json");
            _diDateiListe = DateiListenEinlesen("DI.json");

        }

        private IEnumerable<string> DateiListenEinlesen(string jsonFile)
        {
            var quellOrdner = "";
            var filterOk = "";
            var filterNichtOk = "";

            foreach (var dateienFilter in Einstellungen.AlleDateienFilter)
            {
                switch (dateienFilter.Beschreibung)
                {
                    case "Quellordner":
                        quellOrdner = dateienFilter.Quelle;
                        break;
                    case "Suchordner":
                        filterOk = dateienFilter.Quelle;
                        break;
                    case "Debug":
                        filterNichtOk = dateienFilter.Quelle;
                        break;
                }
            }

            var directory = new DirectoryInfo(quellOrdner);
            var folders = directory.GetDirectories("*", SearchOption.AllDirectories);

            return (from info in folders
                    from file in info.GetFiles()
                    let dateiName = file.ToString()
                    where dateiName.Contains(filterOk) && !dateiName.Contains(filterNichtOk) && dateiName.Contains(jsonFile)
                    select file.FullName).ToList();
        }

        internal void AlleConfigEinlesen()
        {
            AaConfigLesen();
            AiConfigLesen();
            DaConfigLesen();
            DiConfigLesen();
        }

        internal void AaConfigLesen()
        {
            foreach (var datei in _aaDateiListe)
            {
                var aaConfig = JsonConvert.DeserializeObject<AaConfig>(File.ReadAllText(datei), new MyEnumConverter());
                _viewModel.ViAnzeige.AaAlleDaten.Add(aaConfig);
            }
        }

        internal void AiConfigLesen()
        {
            foreach (var datei in _aiDateiListe)
            {
                var aiConfig = JsonConvert.DeserializeObject<AiConfig>(File.ReadAllText(datei), new MyEnumConverter());
                _viewModel.ViAnzeige.AiAlleDaten.Add(aiConfig);
            }
        }

        internal void DaConfigLesen()
        {
            foreach (var datei in _daDateiListe)
            {
                var daConfig = JsonConvert.DeserializeObject<DaConfig>(File.ReadAllText(datei), new MyEnumConverter());
                _viewModel.ViAnzeige.DaAlleDaten.Add(daConfig);
            }
        }

        internal void DiConfigLesen()
        {
            foreach (var datei in _diDateiListe)
            {
                var diConfig = JsonConvert.DeserializeObject<DiConfig>(File.ReadAllText(datei), new MyEnumConverter());
                _viewModel.ViAnzeige.DiAlleDaten.Add(diConfig);
            }
        }

    }
}