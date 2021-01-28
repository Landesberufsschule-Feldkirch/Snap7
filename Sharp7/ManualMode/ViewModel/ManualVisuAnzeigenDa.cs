using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ManualMode.Model;

namespace ManualMode.ViewModel
{
    public partial class ManualVisuAnzeigen
    {
        private void DigitaleAusgaengeSchaltenFarbeSichtbarkeit()
        {
            if (_manualMode?.GetConfig?.DaConfig?.DigitaleAusgaenge == null) return;

            foreach (var digitaleAusgaenge in _manualMode.GetConfig.DaConfig.DigitaleAusgaenge)
            {
                switch (digitaleAusgaenge.AnzahlBit)
                {
                    case 1:
                        break;

                    case 8:
                        switch (digitaleAusgaenge.Type)
                        {
                            case PlcEinUndAusgaengeTypen.BitmusterByte: break;
                            case PlcEinUndAusgaengeTypen.Default: break;
                            case PlcEinUndAusgaengeTypen.Ascii: break;
                            case PlcEinUndAusgaengeTypen.SiemensAnalogwertProzent: break;
                            case PlcEinUndAusgaengeTypen.SiemensAnalogwertPromille: break;
                            case PlcEinUndAusgaengeTypen.SiemensAnalogwertSchieberegler: break;
                            default: throw new ArgumentOutOfRangeException(nameof(digitaleAusgaenge.Type));
                        }

                        break;
                }
            }
        }

        internal void BtnTasten(object taste)
        {
            if (taste is not string ascii) return;

            var (iByte, bitMuster) = NummerInBitUndBitmuster(int.Parse(ascii));

            if (ClickModeButtonSchalten(int.Parse(ascii)))
            {
                _manualMode.Datenstruktur.DigOutput[iByte] |= bitMuster;
            }
            else
            {
                _manualMode.Datenstruktur.DigOutput[iByte] &= (byte) ~bitMuster;
            }
        }

        internal void BtnToggeln(object taste)
        {
            if (taste is not string ascii) return;

            var (iByte, bitMuster) = NummerInBitUndBitmuster(int.Parse(ascii));

            if ((_manualMode.Datenstruktur.DigOutput[iByte] & bitMuster) == bitMuster)
            {
                _manualMode.Datenstruktur.DigOutput[iByte] &= (byte) ~bitMuster;
            }
            else
            {
                _manualMode.Datenstruktur.DigOutput[iByte] |= bitMuster;
            }
        }

        internal (int ibyte, byte bitmuster) NummerInBitUndBitmuster(int i)
        {
            var ibyte = i / 8;
            var bitMuster = (byte) (1 << i % 8);

            return (ibyte, bitMuster);
        }

        public bool ClickModeButtonSchalten(int i)
        {
            if (ClickModeTasten[i] == ClickMode.Press)
            {
                ClickModeTasten[i] = ClickMode.Release;
                return true;
            }

            ClickModeTasten[i] = ClickMode.Press;
            return false;
        }

        private ObservableCollection<ClickMode> _clickModeTasten = new();

        public ObservableCollection<ClickMode> ClickModeTasten
        {
            get => _clickModeTasten;
            set
            {
                _clickModeTasten = value;
                OnPropertyChanged(nameof(ClickModeTasten));
            }
        }

        public void SetFarbeTastenToggelnDa(bool val, int id) =>
            FarbeTastenToggelnDa[id] = val ? Brushes.LawnGreen : Brushes.LightGray;

        private ObservableCollection<Brush> _farbeTastenToggelnDa = new();

        public ObservableCollection<Brush> FarbeTastenToggelnDa
        {
            get => _farbeTastenToggelnDa;
            set
            {
                _farbeTastenToggelnDa = value;
                OnPropertyChanged(nameof(FarbeTastenToggelnDa));
            }
        }

        private ObservableCollection<Visibility> _visibilityDa = new();

        public ObservableCollection<Visibility> VisibilityDa
        {
            get => _visibilityDa;
            set
            {
                _visibilityDa = value;
                OnPropertyChanged(nameof(VisibilityDa));
            }
        }

        private ObservableCollection<string> _bezeichnungDa = new();

        public ObservableCollection<string> BezeichnungDa
        {
            get => _bezeichnungDa;
            set
            {
                _bezeichnungDa = value;
                OnPropertyChanged(nameof(BezeichnungDa));
            }
        }

        private ObservableCollection<string> _wertDa = new();

        public ObservableCollection<string> WertDa
        {
            get => _wertDa;
            set
            {
                _wertDa = value;
                OnPropertyChanged(nameof(WertDa));
            }
        }

        private ObservableCollection<string> _kommentarDa = new();

        public ObservableCollection<string> KommentarDa
        {
            get => _kommentarDa;
            set
            {
                _kommentarDa = value;
                OnPropertyChanged(nameof(KommentarDa));
            }
        }
    }
}