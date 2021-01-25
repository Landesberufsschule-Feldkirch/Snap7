using System;
using System.Collections.ObjectModel;
using System.Windows;
using ManualMode.Model;

namespace ManualMode.ViewModel
{
    public partial class ManualVisuAnzeigen
    {
        private void AnalogeAusgaengeVeraendernSichtbarkeit()
        {
            if (_manualMode?.GetConfig?.AaConfig?.AnalogeAusgaenge == null) return;

            foreach (var analogeAusgaenge in _manualMode.GetConfig.AaConfig.AnalogeAusgaenge)
            {
                switch (analogeAusgaenge.Type)
                {
                    case PlcEinUndAusgaengeTypen.Default:
                        break;
                    case PlcEinUndAusgaengeTypen.Ascii:
                        break;
                    case PlcEinUndAusgaengeTypen.BitmusterByte:
                        break;
                    case PlcEinUndAusgaengeTypen.SiemensAnalogwertProzent:
                        break;
                    case PlcEinUndAusgaengeTypen.SiemensAnalogwertPromille:
                        break;
                    case PlcEinUndAusgaengeTypen.SiemensAnalogwertSchieberegler:
                        // SiemensAnalogwerte sind immer 16Bit Werte
                        var wertInt = 256 * _manualMode.Datenstruktur.AnalogOutput[analogeAusgaenge.StartByte] +
                                      _manualMode.Datenstruktur.AnalogOutput[analogeAusgaenge.StartByte + 1];
                        // ReSharper disable once ArrangeRedundantParentheses
                        ContentAa[analogeAusgaenge.LaufendeNr] =
                            $"{wertInt} 0x {wertInt:X}  ->  {analogeAusgaenge.MaximalWert * (double)wertInt / SiemensAnalogSkalierung:F1}";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(analogeAusgaenge.Type));
                }
            }
        }

        private ObservableCollection<string> _contentAa = new();

        public ObservableCollection<string> ContentAa
        {
            get => _contentAa;
            set
            {
                _contentAa = value;
                OnPropertyChanged(nameof(ContentAa));
            }
        }

        private ObservableCollection<Visibility> _visibilityAa = new();

        public ObservableCollection<Visibility> VisibilityAa
        {
            get => _visibilityAa;
            set
            {
                _visibilityAa = value;
                OnPropertyChanged(nameof(VisibilityAa));
            }
        }

        private ObservableCollection<string> _bezeichnungAa = new();

        public ObservableCollection<string> BezeichnungAa
        {
            get => _bezeichnungAa;
            set
            {
                _bezeichnungAa = value;
                OnPropertyChanged(nameof(BezeichnungAa));
            }
        }

        private ObservableCollection<string> _kommentarAa = new();

        public ObservableCollection<string> KommentarAa
        {
            get => _kommentarAa;
            set
            {
                _kommentarAa = value;
                OnPropertyChanged(nameof(KommentarAa));
            }
        }
    }
}