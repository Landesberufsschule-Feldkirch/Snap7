using Kommunikation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace DisplayPlc.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Datenstruktur _datenstruktur;
        private readonly DisplayPlc _displayPlc;
        private readonly ConfigPlc.Plc _configPlc;
        private readonly BeschriftungPlc.BeschriftungenPlc _beschriftungenPlc;

        public VisuAnzeigen(Datenstruktur datenstruktur, ConfigPlc.Plc configPlc, BeschriftungPlc.BeschriftungenPlc beschriftungenPlc, DisplayPlc displayPlc)
        {
            _datenstruktur = datenstruktur;
            _configPlc = configPlc;
            _beschriftungenPlc = beschriftungenPlc;
            _displayPlc = displayPlc;

            for (var i = 0; i < 100; i++)
            {
                FarbeDa.Add(Brushes.LawnGreen);
                FarbeDi.Add(Brushes.LawnGreen);
                VisibilityDa.Add(Visibility.Visible);
                VisibilityDi.Add(Visibility.Visible);
                LabelDa.Add("-");
                LabelDi.Add("-");
                KommentarDa.Add("-");
                KommentarDi.Add("-");
                BezeichnungDa.Add("-");
                BezeichnungDi.Add("-");
            }

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                for (var i = 0; i < 16; i++)
                {
                    DaSetFarbe(BitTesten(_datenstruktur.DigOutput, i), i);
                    DiSetFarbe(BitTesten(_datenstruktur.DigInput, i), i);
                    DaSetSichtbarkeit(_displayPlc.PlcAusgaenge.GetBitGesetzt(i), i);
                    DiSetSichtbarkeit(_displayPlc.PlcEingaenge.GetBitGesetzt(i), i);
                    DaSetLabel($"DaLabel{i}", i);
                    DiSetLabel($"DiLabel{i}", i);

                    if (i < _configPlc.GetConfigPlc.DaConfig.DigitaleAusgaenge.Count)
                    {
                        DaSetKommentar(_configPlc.GetConfigPlc.DaConfig.DigitaleAusgaenge[i].Kommentar, i);
                        DaSetBezeichnung(_configPlc.GetConfigPlc.DaConfig.DigitaleAusgaenge[i].Bezeichnung, i);
                    }

                    if (i >= _configPlc.GetConfigPlc.DiConfig.DigitaleEingaenge.Count) continue;

                    DiSetKommentar(_configPlc.GetConfigPlc.DiConfig.DigitaleEingaenge[i].Kommentar, i);
                    DiSetBezeichnung(_configPlc.GetConfigPlc.DiConfig.DigitaleEingaenge[i].Bezeichnung, i);
                }

                if (_datenstruktur.BetriebsartProjekt == BetriebsartProjekt.AutomatischerSoftwareTest)
                {
                    if (_beschriftungenPlc.BeschriftungenStruktur.DaBeschriftungen != null && _beschriftungenPlc.BeschriftungenStruktur.DaBeschriftungen.DaBeschriftung.Count > 0)
                    {
                        foreach (var beschriftungDaDaten in _beschriftungenPlc.BeschriftungenStruktur.DaBeschriftungen.DaBeschriftung)
                        {
                            var id = beschriftungDaDaten.Bit + 8 * beschriftungDaDaten.Byte;
                            DaSetBezeichnung(beschriftungDaDaten.Bezeichnung, id);
                            DaSetKommentar(beschriftungDaDaten.Kommentar, id);
                        }
                    }

                    if (_beschriftungenPlc.BeschriftungenStruktur.DiBeschriftungen != null && _beschriftungenPlc.BeschriftungenStruktur.DiBeschriftungen.DiBeschriftung.Count > 0)
                    {
                        foreach (var beschriftungDiDaten in _beschriftungenPlc.BeschriftungenStruktur.DiBeschriftungen.DiBeschriftung)
                        {
                            var id = beschriftungDiDaten.Bit + 8 * beschriftungDiDaten.Byte;
                            DiSetBezeichnung(beschriftungDiDaten.Bezeichnung, id);
                            DiSetKommentar(beschriftungDiDaten.Kommentar, id);
                        }
                    }


                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        #region DaSichtbarkeit
        private void DaSetSichtbarkeit(bool b, int i) => VisibilityDa[i] = b ? Visibility.Visible : Visibility.Hidden;

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
        #endregion

        #region DiSichtbarkeit
        private void DiSetSichtbarkeit(bool b, int i) => VisibilityDi[i] = b ? Visibility.Visible : Visibility.Hidden;


        private ObservableCollection<Visibility> _visibilityDi = new();
        public ObservableCollection<Visibility> VisibilityDi
        {
            get => _visibilityDi;
            set
            {
                _visibilityDi = value;
                OnPropertyChanged(nameof(VisibilityDi));
            }
        }
        #endregion

        #region DaFarbe

        public void DaSetFarbe(bool val, int id) => FarbeDa[id] = val ? Brushes.LawnGreen : Brushes.DarkGray;

        private ObservableCollection<Brush> _farbeDa = new();
        public ObservableCollection<Brush> FarbeDa
        {
            get => _farbeDa;
            set
            {
                _farbeDa = value;
                OnPropertyChanged(nameof(FarbeDa));
            }
        }

        #endregion

        #region DiFarbe

        public void DiSetFarbe(bool val, int id) => FarbeDi[id] = val ? Brushes.LawnGreen : Brushes.DarkGray;

        private ObservableCollection<Brush> _farbeDi = new();
        public ObservableCollection<Brush> FarbeDi
        {
            get => _farbeDi;
            set
            {
                _farbeDi = value;
                OnPropertyChanged(nameof(FarbeDi));
            }
        }

        #endregion

        #region DaBezeichnung

        public void DaSetBezeichnung(string text, int id) => BezeichnungDa[id] = text;

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

        #endregion

        #region DiBezeichnung

        public void DiSetBezeichnung(string text, int id) => BezeichnungDi[id] = text;

        private ObservableCollection<string> _bezeichnungDi = new();
        public ObservableCollection<string> BezeichnungDi
        {
            get => _bezeichnungDi;
            set
            {
                _bezeichnungDi = value;
                OnPropertyChanged(nameof(BezeichnungDi));
            }
        }

        #endregion

        #region DaKommentar

        public void DaSetKommentar(string text, int id) => KommentarDa[id] = text;

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

        #endregion

        #region DiKommentar

        public void DiSetKommentar(string text, int id) => KommentarDi[id] = text;

        private ObservableCollection<string> _kommentarDi = new();
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

        #region DaLabel

        public void DaSetLabel(string text, int id) => LabelDa[id] = text;

        private ObservableCollection<string> _labelDa = new();
        public ObservableCollection<string> LabelDa
        {
            get => _labelDa;
            set
            {
                _labelDa = value;
                OnPropertyChanged(nameof(LabelDa));
            }
        }

        #endregion

        #region DiLabel

        public void DiSetLabel(string text, int id) => LabelDi[id] = text;

        private ObservableCollection<string> _labelDi = new();
        public ObservableCollection<string> LabelDi
        {
            get => _labelDi;
            set
            {
                _labelDi = value;
                OnPropertyChanged(nameof(LabelDi));
            }
        }

        #endregion

        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        // ReSharper disable once UnusedMember.Local
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members

        private static bool BitTesten(IReadOnlyList<byte> datenArray, int i)
        {
            var ibyte = i / 8;
            var bitMuster = (byte)(1 << i % 8);

            return (datenArray[ibyte] & bitMuster) == bitMuster;
        }
    }
}