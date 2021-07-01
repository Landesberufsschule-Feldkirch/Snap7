using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _AlleManConfigTesten.Model
{
    public class ManConfigTesten
    {
        private readonly ViewModel.ViewModel _viewModel;

        public Einstellungen Einstellungen { get; set; }

        public enum PlcEinUndAusgaengeTypen
        {
            Default,
            // ReSharper disable UnusedMember.Global
            Ascii,
            SiemensAnalogwertProzent,
            SiemensAnalogwertPromille,
            BitmusterByte,
            Schieberegler
            // ReSharper restore UnusedMember.Global
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

        public ManConfigTesten(ViewModel.ViewModel viewModel)
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


            return folders.SelectMany(info => info.GetFiles(), (info, file) => new { info, file })
                .Select(abc => new { t = abc, dateiName = abc.file.ToString() })
                .Where(t =>
                    t.dateiName.Contains(filterOk) && !t.dateiName.Contains(filterNichtOk) &&
                    t.dateiName.Contains(jsonFile))
                .Select(t => t.t.file.FullName).ToList();
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
            foreach (var dateiName in _aaDateiListe)
            {
                var aaConfig = JsonConvert.DeserializeObject<AaConfig>(File.ReadAllText(dateiName), new MyEnumConverter());
                if (!(aaConfig?.AnalogeAusgaenge.Count > 0)) continue;

                foreach (var analogAusgang in aaConfig.AnalogeAusgaenge)
                {
                    _viewModel.ViAnzeige.AddAaDaten(new AaDaten(
                        dateiName, analogAusgang.LaufendeNr,
                        analogAusgang.StartByte, analogAusgang.StartBit, analogAusgang.AnzahlBit,
                        analogAusgang.MinimalWert, analogAusgang.MaximalWert, analogAusgang.Schrittweite,
                        analogAusgang.Type,
                        analogAusgang.Bezeichnung, analogAusgang.Kommentar));
                }
            }
        }

        internal void AiConfigLesen()
        {
            foreach (var dateiName in _aiDateiListe)
            {
                var aiConfig = JsonConvert.DeserializeObject<AiConfig>(File.ReadAllText(dateiName), new MyEnumConverter());
                if (!(aiConfig?.AnalogeEingaenge.Count > 0)) continue;

                foreach (var analogEingang in aiConfig.AnalogeEingaenge)
                {
                    _viewModel.ViAnzeige.AddAiDaten(new AiDaten(
                        dateiName, analogEingang.LaufendeNr,
                        analogEingang.StartByte, analogEingang.StartBit, analogEingang.AnzahlBit,
                        analogEingang.Type,
                        analogEingang.Bezeichnung, analogEingang.Kommentar));
                }
            }
        }

        internal void DaConfigLesen()
        {
            foreach (var dateiName in _daDateiListe)
            {
                var daConfig = JsonConvert.DeserializeObject<DaConfig>(File.ReadAllText(dateiName), new MyEnumConverter());

                if (!(daConfig?.DigitaleAusgaenge.Count > 0)) continue;

                foreach (var digitalAusgang in daConfig.DigitaleAusgaenge)
                {
                    _viewModel.ViAnzeige.AddDaDaten(new DaDaten(
                        dateiName, digitalAusgang.LaufendeNr,
                        digitalAusgang.StartByte, digitalAusgang.StartBit, digitalAusgang.AnzahlBit,
                        digitalAusgang.Type,
                        digitalAusgang.Bezeichnung, digitalAusgang.Kommentar));
                }
            }
        }

        internal void DiConfigLesen()
        {
            foreach (var dateiName in _diDateiListe)
            {
                var diConfig = JsonConvert.DeserializeObject<DiConfig>(File.ReadAllText(dateiName), new MyEnumConverter());

                if (!(diConfig?.DigitaleEingaenge.Count > 0)) continue;

                foreach (var digitalEingang in diConfig.DigitaleEingaenge)
                {
                    _viewModel.ViAnzeige.AddDiDaten(new DiDaten(dateiName, digitalEingang.LaufendeNr, digitalEingang.StartByte, digitalEingang.StartBit, digitalEingang.AnzahlBit, digitalEingang.Type, digitalEingang.Bezeichnung, digitalEingang.Kommentar));
                }
            }
        }

    }
}