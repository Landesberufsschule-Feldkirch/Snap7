using System;
using System.Collections.ObjectModel;
using System.Windows;
using ManualMode.Model;

namespace ManualMode.ViewModel
{
    public partial class ManualVisuAnzeigen
    {
        private void AnalogeEingaengeWerteSichtbarkeit()
        {
            if (_manualMode?.GetConfig?.AiConfig?.AnalogeEingaenge == null) return;

            foreach (var analogeEingaenge in _manualMode.GetConfig.AiConfig.AnalogeEingaenge)
            {
                switch (analogeEingaenge.AnzahlBit)
                {
                    case 8:
                        var wertByte = _manualMode.Datenstruktur.AnalogInput[analogeEingaenge.StartByte];
                        switch (analogeEingaenge.Type)
                        {
                            case PlcEinUndAusgaengeTypen.Default:
                                ContentAi[analogeEingaenge.LaufendeNr] = $"{wertByte} 0x{wertByte:X}";
                                break;
                            case PlcEinUndAusgaengeTypen.Ascii:
                                ContentAi[analogeEingaenge.LaufendeNr] = wertByte switch
                                {
                                    (byte)0x0d => $"{wertByte} 0x{wertByte:X} 'Enter'",
                                    (byte)0x20 => $"{wertByte} 0x{wertByte:X} 'Space'",
                                    _ => $"{wertByte} 0x{wertByte:X} '{(char)wertByte}'"
                                };

                                break;
                            case PlcEinUndAusgaengeTypen.SiemensAnalogwertProzent:
                            case PlcEinUndAusgaengeTypen.SiemensAnalogwertPromille:
                            case PlcEinUndAusgaengeTypen.BitmusterByte:
                            case PlcEinUndAusgaengeTypen.SiemensAnalogwertSchieberegler: break;
                            default: throw new ArgumentOutOfRangeException(nameof(analogeEingaenge.Type));
                        }

                        break;
                    case 16:
                        var wertInt = 256 * _manualMode.Datenstruktur.AnalogInput[analogeEingaenge.StartByte] +
                                      _manualMode.Datenstruktur.AnalogInput[analogeEingaenge.StartByte + 1];
                        switch (analogeEingaenge.Type)
                        {
                            case PlcEinUndAusgaengeTypen.Default:
                                ContentAi[analogeEingaenge.LaufendeNr] = $"{wertInt} 0x {wertInt:X}";
                                break;
                            case PlcEinUndAusgaengeTypen.SiemensAnalogwertProzent:
                                // ReSharper disable once ArrangeRedundantParentheses
                                ContentAi[analogeEingaenge.LaufendeNr] =
                                    $"{wertInt} 0x {wertInt:X}  ->  {100 * (double)wertInt / SiemensAnalogSkalierung:F1} %";
                                break;
                            case PlcEinUndAusgaengeTypen.SiemensAnalogwertPromille:
                                // ReSharper disable once ArrangeRedundantParentheses
                                ContentAi[analogeEingaenge.LaufendeNr] =
                                    $"{wertInt} 0x {wertInt:X} ->  {1000 * (double)wertInt / SiemensAnalogSkalierung:F1} + ‰";
                                break;
                            case PlcEinUndAusgaengeTypen.Ascii:
                            case PlcEinUndAusgaengeTypen.BitmusterByte:
                            case PlcEinUndAusgaengeTypen.SiemensAnalogwertSchieberegler: break;
                            default: throw new ArgumentOutOfRangeException(nameof(analogeEingaenge.Type));
                        }

                        break;
                }
            }
        }

        private ObservableCollection<string> _contentAi = new();

        public ObservableCollection<string> ContentAi
        {
            get => _contentAi;
            set
            {
                _contentAi = value;
                OnPropertyChanged(nameof(ContentAi));
            }
        }

        private ObservableCollection<Visibility> _visibilityAi = new();

        public ObservableCollection<Visibility> VisibilityAi
        {
            get => _visibilityAi;
            set
            {
                _visibilityAi = value;
                OnPropertyChanged(nameof(VisibilityAi));
            }
        }

        private ObservableCollection<string> _bezeichnungAi = new();

        public ObservableCollection<string> BezeichnungAi
        {
            get => _bezeichnungAi;
            set
            {
                _bezeichnungAi = value;
                OnPropertyChanged(nameof(BezeichnungAi));
            }
        }

        private ObservableCollection<string> _kommentarAi = new();

        public ObservableCollection<string> KommentarAi
        {
            get => _kommentarAi;
            set
            {
                _kommentarAi = value;
                OnPropertyChanged(nameof(KommentarAi));
            }
        }
    }
}