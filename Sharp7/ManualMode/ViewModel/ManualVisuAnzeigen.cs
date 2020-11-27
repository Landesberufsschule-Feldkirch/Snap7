using ManualMode.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace ManualMode.ViewModel
{
    public class ManualVisuAnzeigen : INotifyPropertyChanged
    {
        private readonly ManualMode _manualMode;
        private const double SiemensAnalogSkalierung = 27648;


        public ManualVisuAnzeigen(ManualMode mm)
        {
            _manualMode = mm;

            for (var i = 0; i < 100; i++)
            {
                ClickModeTasten.Add(System.Windows.Controls.ClickMode.Press);
                FarbeTastenToggelnDa.Add(Brushes.LawnGreen);
                VisibilityDa.Add(Visibility.Hidden);
                BezeichnungDa.Add("-");
                WertDa.Add("0");
                KommentarDa.Add("-");

                FarbeDi.Add(Brushes.LawnGreen);
                VisibilityDi.Add(Visibility.Hidden);
                BezeichnungDi.Add("-");
                KommentarDi.Add("-");

                ContentAi.Add("-");
                VisibilityAi.Add(Visibility.Hidden);
                BezeichnungAi.Add("-");
                KommentarAi.Add("-");

                ContentAa.Add("-");
                VisibilityAa.Add(Visibility.Hidden);
                BezeichnungAa.Add("-");
                KommentarAa.Add("-");
            }

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {

            while (true)
            {
                DigitaleEingaengeFarbeSichtbarkeit();
                DigitaleAusgaengeSchaltenFarbeSichtbarkeit();
                AnalogeEingaengeWerteSichtbarkeit();
                AnalogeAusgaengeVeraendernSichtbarkeit();

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }
        

        #region Digitale Eingänge
        private void DigitaleEingaengeFarbeSichtbarkeit()
        {
            if (_manualMode?.Datenstruktur.DigOutput == null) return;

            for (var i = 0; i < 100; i++)
            {
                var (iByte, bitMuster) = NummerInBitUndBitmuster(i);

                SetFarbeTastenToggelnDa((_manualMode.Datenstruktur.DigOutput[iByte] & bitMuster) == bitMuster, i);
                DiSetFarbe((_manualMode.Datenstruktur.DigInput[iByte] & bitMuster) == bitMuster, i);
            }
        }

        public void DiSetFarbe(bool val, int id) => FarbeDi[id] = val ? Brushes.LawnGreen : Brushes.LightGray;

        private ObservableCollection<Brush> _farbeDi = new ObservableCollection<Brush>();
        public ObservableCollection<Brush> FarbeDi
        {
            get => _farbeDi;
            set
            {
                _farbeDi = value;
                OnPropertyChanged("FarbeDi");
            }
        }

        private ObservableCollection<Visibility> _visibilityDi = new ObservableCollection<Visibility>();
        public ObservableCollection<Visibility> VisibilityDi
        {
            get => _visibilityDi;
            set
            {
                _visibilityDi = value;
                OnPropertyChanged(nameof(VisibilityDi));
            }
        }

        private ObservableCollection<string> _bezeichnungDi = new ObservableCollection<string>();
        public ObservableCollection<string> BezeichnungDi
        {
            get => _bezeichnungDi;
            set
            {
                _bezeichnungDi = value;
                OnPropertyChanged(nameof(BezeichnungDi));
            }
        }


        private ObservableCollection<string> _kommentarDi = new ObservableCollection<string>();
        public ObservableCollection<string> KommentarDi
        {
            get => _kommentarDi;
            set
            {
                _kommentarDi = value;
                OnPropertyChanged(nameof(KommentarDi));
            }
        }

        #endregion

        #region Digitale Ausgänge

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
                            case PlcEinUndAusgaengeTypen.BitmusterByte:
                                break;
                            case PlcEinUndAusgaengeTypen.Default:
                                break;
                            case PlcEinUndAusgaengeTypen.Ascii:
                                break;
                            case PlcEinUndAusgaengeTypen.SiemensAnalogwertProzent:
                                break;
                            case PlcEinUndAusgaengeTypen.SiemensAnalogwertPromille:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(digitaleAusgaenge.Type));
                        }
                        break;

                }
            }
        }
        internal void BtnTasten(object taste)
        {
            if (!(taste is string ascii)) return;

            var (iByte, bitMuster) = NummerInBitUndBitmuster(int.Parse(ascii));

            if (ClickModeButtonSchalten(int.Parse(ascii)))
            {
                _manualMode.Datenstruktur.DigOutput[iByte] |= bitMuster;
            }
            else
            {
                _manualMode.Datenstruktur.DigOutput[iByte] &= (byte)~bitMuster;
            }
        }

        internal void BtnToggeln(object taste)
        {
            if (!(taste is string ascii)) return;

            var (iByte, bitMuster) = NummerInBitUndBitmuster(int.Parse(ascii));

            if ((_manualMode.Datenstruktur.DigOutput[iByte] & bitMuster) == bitMuster)
            {
                _manualMode.Datenstruktur.DigOutput[iByte] &= (byte)~bitMuster;
            }
            else
            {
                _manualMode.Datenstruktur.DigOutput[iByte] |= bitMuster;
            }
        }

        internal (int ibyte, byte bitmuster) NummerInBitUndBitmuster(int i)
        {
            var ibyte = i / 8;
            var bitMuster = (byte)(1 << i % 8);

            return (ibyte, bitMuster);
        }


        public bool ClickModeButtonSchalten(int i)
        {
            if (ClickModeTasten[i] == System.Windows.Controls.ClickMode.Press)
            {
                ClickModeTasten[i] = System.Windows.Controls.ClickMode.Release;
                return true;
            }
            ClickModeTasten[i] = System.Windows.Controls.ClickMode.Press;
            return false;
        }

        private ObservableCollection<System.Windows.Controls.ClickMode> _clickModeTasten = new ObservableCollection<System.Windows.Controls.ClickMode>();
        public ObservableCollection<System.Windows.Controls.ClickMode> ClickModeTasten
        {
            get => _clickModeTasten;
            set
            {
                _clickModeTasten = value;
                OnPropertyChanged(nameof(ClickModeTasten));
            }
        }


        public void SetFarbeTastenToggelnDa(bool val, int id) => FarbeTastenToggelnDa[id] = val ? Brushes.LawnGreen : Brushes.LightGray;

        private ObservableCollection<Brush> _farbeTastenToggelnDa = new ObservableCollection<Brush>();
        public ObservableCollection<Brush> FarbeTastenToggelnDa
        {
            get => _farbeTastenToggelnDa;
            set
            {
                _farbeTastenToggelnDa = value;
                OnPropertyChanged("FarbeTastenToggelnDa");
            }
        }


        private ObservableCollection<Visibility> _visibilityDa = new ObservableCollection<Visibility>();
        public ObservableCollection<Visibility> VisibilityDa
        {
            get => _visibilityDa;
            set
            {
                _visibilityDa = value;
                OnPropertyChanged(nameof(VisibilityDa));
            }
        }


        private ObservableCollection<string> _bezeichnungDa = new ObservableCollection<string>();
        public ObservableCollection<string> BezeichnungDa
        {
            get => _bezeichnungDa;
            set
            {
                _bezeichnungDa = value;
                OnPropertyChanged(nameof(BezeichnungDa));
            }
        }

        private ObservableCollection<string> _wertDa = new ObservableCollection<string>();
        public ObservableCollection<string> WertDa
        {
            get => _wertDa;
            set
            {
                _wertDa = value;
                OnPropertyChanged(nameof(WertDa));
            }
        }

        private ObservableCollection<string> _kommentarDa = new ObservableCollection<string>();
        public ObservableCollection<string> KommentarDa
        {
            get => _kommentarDa;
            set
            {
                _kommentarDa = value;
                OnPropertyChanged(nameof(KommentarDa));
            }
        }

        #endregion

        #region Analoge Eingänge

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
                                ContentAi[analogeEingaenge.LaufendeNr] = wertByte + " 0x" + wertByte.ToString("X");
                                break;
                            case PlcEinUndAusgaengeTypen.Ascii:
                                ContentAi[analogeEingaenge.LaufendeNr] = wertByte + " 0x" + wertByte.ToString("X") + " '" + (char)wertByte + "'";
                                break;
                            case PlcEinUndAusgaengeTypen.SiemensAnalogwertProzent:
                                break;
                            case PlcEinUndAusgaengeTypen.SiemensAnalogwertPromille:
                                break;
                            case PlcEinUndAusgaengeTypen.BitmusterByte:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(analogeEingaenge.Type));
                        }
                        break;
                    case 16:
                        var wertInt = 256 * _manualMode.Datenstruktur.AnalogInput[analogeEingaenge.StartByte] + _manualMode.Datenstruktur.AnalogInput[analogeEingaenge.StartByte + 1];
                        switch (analogeEingaenge.Type)
                        {
                            case PlcEinUndAusgaengeTypen.Default:
                                ContentAi[analogeEingaenge.LaufendeNr] = wertInt + " 0x" + wertInt.ToString("X");
                                break;
                            case PlcEinUndAusgaengeTypen.SiemensAnalogwertProzent:
                                ContentAi[analogeEingaenge.LaufendeNr] = wertInt + " 0x" + wertInt.ToString("X") + " -> " + (100 * (double)wertInt / SiemensAnalogSkalierung).ToString("F1") + "%";
                                break;
                            case PlcEinUndAusgaengeTypen.SiemensAnalogwertPromille:
                                ContentAi[analogeEingaenge.LaufendeNr] = wertInt + " 0x" + wertInt.ToString("X") + " -> " + (1000 * (double)wertInt / SiemensAnalogSkalierung).ToString("F1") + "‰";
                                break;
                            case PlcEinUndAusgaengeTypen.Ascii:
                                break;
                            case PlcEinUndAusgaengeTypen.BitmusterByte:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(analogeEingaenge.Type));
                        }
                        break;
                }
            }
        }



        private ObservableCollection<string> _contentAi = new ObservableCollection<string>();
        public ObservableCollection<string> ContentAi
        {
            get => _contentAi;
            set
            {
                _contentAi = value;
                OnPropertyChanged("ContentAi");
            }
        }

        private ObservableCollection<Visibility> _visibilityAi = new ObservableCollection<Visibility>();
        public ObservableCollection<Visibility> VisibilityAi
        {
            get => _visibilityAi;
            set
            {
                _visibilityAi = value;
                OnPropertyChanged(nameof(VisibilityAi));
            }
        }

        private ObservableCollection<string> _bezeichnungAi = new ObservableCollection<string>();
        public ObservableCollection<string> BezeichnungAi
        {
            get => _bezeichnungAi;
            set
            {
                _bezeichnungAi = value;
                OnPropertyChanged(nameof(BezeichnungAi));
            }
        }


        private ObservableCollection<string> _kommentarAi = new ObservableCollection<string>();
        public ObservableCollection<string> KommentarAi
        {
            get => _kommentarAi;
            set
            {
                _kommentarAi = value;
                OnPropertyChanged(nameof(KommentarAi));
            }
        }

        #endregion

        #region Analoge Ausgänge

        private void AnalogeAusgaengeVeraendernSichtbarkeit()
        {
            if (_manualMode?.GetConfig?.AaConfig?.AnalogeAusgaenge == null) return;


            foreach (var analogeAusgaenge in _manualMode.GetConfig.AaConfig.AnalogeAusgaenge)
            {
                switch (analogeAusgaenge.AnzahlBit)
                {
                    case 8:
                        break;

                    case 16:
                        break;



                }
            }

        }



        private ObservableCollection<string> _contentAa = new ObservableCollection<string>();
        public ObservableCollection<string> ContentAa
        {
            get => _contentAa;
            set
            {
                _contentAa = value;
                OnPropertyChanged("ContentAa");
            }
        }

        private ObservableCollection<Visibility> _visibilityAa = new ObservableCollection<Visibility>();
        public ObservableCollection<Visibility> VisibilityAa
        {
            get => _visibilityAa;
            set
            {
                _visibilityAa = value;
                OnPropertyChanged(nameof(VisibilityAa));
            }
        }

        private ObservableCollection<string> _bezeichnungAa = new ObservableCollection<string>();
        public ObservableCollection<string> BezeichnungAa
        {
            get => _bezeichnungAa;
            set
            {
                _bezeichnungAa = value;
                OnPropertyChanged(nameof(BezeichnungAa));
            }
        }


        private ObservableCollection<string> _kommentarAa = new ObservableCollection<string>();
        public ObservableCollection<string> KommentarAa
        {
            get => _kommentarAa;
            set
            {
                _kommentarAa = value;
                OnPropertyChanged(nameof(KommentarAa));
            }
        }

        #endregion


        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}