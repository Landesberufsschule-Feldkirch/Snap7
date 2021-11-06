using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

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
            Schieberegler,
            SiemensAnalogwertSchieberegler
            // ReSharper restore UnusedMember.Global
        }

        private readonly IEnumerable<string> _aaDateiListe;
        private readonly IEnumerable<string> _aiDateiListe;
        private readonly IEnumerable<string> _daDateiListe;
        private readonly IEnumerable<string> _diDateiListe;

        public ManConfigTesten(ViewModel.ViewModel viewModel)
        {
            _viewModel = viewModel;
            try
            {
                Einstellungen = JsonConvert.DeserializeObject<Einstellungen>(File.ReadAllText("Einstellungen.json"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Datei nicht gefunden: Einstellungen.json" + " --> " + ex);
            }

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
                try
                {
                    var aaConfig = JsonConvert.DeserializeObject<AaConfig>(File.ReadAllText(dateiName));
                    if (!(aaConfig?.AnalogeAusgaenge.Count > 0)) continue;

                    foreach (var analogAusgang in aaConfig.AnalogeAusgaenge)
                    {
                        _viewModel.ViAnz.AddAaDaten(new AaDaten(
                            dateiName, analogAusgang.LaufendeNr,
                            analogAusgang.StartByte, analogAusgang.StartBit, analogAusgang.AnzahlBit,
                            analogAusgang.MinimalWert, analogAusgang.MaximalWert, analogAusgang.Schrittweite,
                            analogAusgang.Type,
                            analogAusgang.Bezeichnung, analogAusgang.Kommentar));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Datei nicht gefunden:" + dateiName + " --> " + ex);
                }
            }
        }
        internal void AiConfigLesen()
        {
            foreach (var dateiName in _aiDateiListe)
            {
                try
                {
                    var aiConfig = JsonConvert.DeserializeObject<AiConfig>(File.ReadAllText(dateiName));

                    if (!(aiConfig?.AnalogeEingaenge.Count > 0)) continue;

                    foreach (var analogEingang in aiConfig.AnalogeEingaenge)
                    {
                        _viewModel.ViAnz.AddAiDaten(new AiDaten(
                            dateiName, analogEingang.LaufendeNr,
                            analogEingang.StartByte, analogEingang.StartBit, analogEingang.AnzahlBit,
                            analogEingang.Type,
                            analogEingang.Bezeichnung, analogEingang.Kommentar));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Datei nicht gefunden:" + dateiName + " --> " + ex);
                }
            }
        }
        internal void DaConfigLesen()
        {
            foreach (var dateiName in _daDateiListe)
            {
                try
                {
                    var daConfig = JsonConvert.DeserializeObject<DaConfig>(File.ReadAllText(dateiName));

                    if (!(daConfig?.DigitaleAusgaenge.Count > 0)) continue;

                    foreach (var digitalAusgang in daConfig.DigitaleAusgaenge)
                    {
                        _viewModel.ViAnz.AddDaDaten(new DaDaten(
                            dateiName, digitalAusgang.LaufendeNr,
                            digitalAusgang.StartByte, digitalAusgang.StartBit, digitalAusgang.AnzahlBit,
                            digitalAusgang.Type,
                            digitalAusgang.Bezeichnung, digitalAusgang.Kommentar));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Datei nicht gefunden:" + dateiName + " --> " + ex);
                }
            }
        }
        internal void DiConfigLesen()
        {
            foreach (var dateiName in _diDateiListe)
            {
                try
                {
                    var diConfig = JsonConvert.DeserializeObject<DiConfig>(File.ReadAllText(dateiName));

                    if (!(diConfig?.DigitaleEingaenge.Count > 0)) continue;

                    foreach (var digitalEingang in diConfig.DigitaleEingaenge)
                    {
                        _viewModel.ViAnz.AddDiDaten(new DiDaten(dateiName, digitalEingang.LaufendeNr, digitalEingang.StartByte, digitalEingang.StartBit, digitalEingang.AnzahlBit, digitalEingang.Type, digitalEingang.Bezeichnung, digitalEingang.Kommentar));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Datei nicht gefunden:" + dateiName + " --> " + ex);
                }
            }
        }
    }
}