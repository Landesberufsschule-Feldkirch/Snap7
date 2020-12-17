using ManualMode.Model;
using ManualMode.ViewModel;
using System;
using System.IO;
using System.Windows;

namespace ManualMode
{
    public partial class DaFenster
    {
        public static bool DatenTypenBit { get; set; }
        public static bool DatenTypenByte { get; set; }
        public static bool DatenTypenWord { get; set; }
        public static bool DatenTypenLong { get; set; }

        public DaFenster(DaConfig daConfig, ManualViewModel mvm)
        {
            DatenTypenBit = false;
            DatenTypenByte = false;
            DatenTypenWord = false;

            InitializeComponent();
            DataContext = mvm;

            var anzahlZeilenConfig = DaDatenLesen(daConfig, mvm);
            if (DatenTypenBit) DaCreateGridBit(anzahlZeilenConfig, daConfig, mvm);
            if (DatenTypenByte) DaCreateGridByte(anzahlZeilenConfig, mvm);
            if (DatenTypenWord) DaCreateGridWord();
            if (DatenTypenLong) DaCreateGridLong();
        }
        private static int DaDatenLesen(DaConfig daConfig, ManualViewModel manualViewModel)
        {
            var anzahlZeilenConfig = 0;

            foreach (var config in daConfig.DigitaleAusgaenge)
            {
                if (config.LaufendeNr == anzahlZeilenConfig)
                {
                    switch (config.AnzahlBit)
                    {
                        case 1:
                            DatenTypenBit = true;
                            if (DatenTypenByte || DatenTypenWord || DatenTypenLong) throw new ArgumentOutOfRangeException(config.AnzahlBit.ToString());
                            break;
                        case 8:
                            DatenTypenByte = true;
                            if (DatenTypenBit || DatenTypenWord || DatenTypenLong) throw new ArgumentOutOfRangeException(config.AnzahlBit.ToString());
                            break;
                        case 16:
                            DatenTypenWord = true;
                            if (DatenTypenBit || DatenTypenByte || DatenTypenLong) throw new ArgumentOutOfRangeException(config.AnzahlBit.ToString());
                            break;
                        case 32:
                            DatenTypenLong = true;
                            if (DatenTypenBit || DatenTypenByte || DatenTypenWord) throw new ArgumentOutOfRangeException(config.AnzahlBit.ToString());
                            break;
                        default: throw new ArgumentOutOfRangeException(config.AnzahlBit.ToString());
                    }

                    switch (config.Type)
                    {
                        case PlcEinUndAusgaengeTypen.BitmusterByte:
                        case PlcEinUndAusgaengeTypen.Default:
                            manualViewModel.ManVisuAnzeigen.VisibilityDa[config.LaufendeNr] = Visibility.Visible;
                            manualViewModel.ManVisuAnzeigen.BezeichnungDa[config.LaufendeNr] = config.Bezeichnung;
                            manualViewModel.ManVisuAnzeigen.KommentarDa[config.LaufendeNr] = config.Kommentar;
                            break;
                        case PlcEinUndAusgaengeTypen.Ascii: break;
                        case PlcEinUndAusgaengeTypen.SiemensAnalogwertProzent: break;
                        case PlcEinUndAusgaengeTypen.SiemensAnalogwertPromille: break;
                        case PlcEinUndAusgaengeTypen.SiemensAnalogwertSchieberegler: break;
                        default: throw new ArgumentOutOfRangeException(config.Type.ToString());
                    }

                    anzahlZeilenConfig++;
                }
                else throw new InvalidDataException($"{nameof(DiFenster)} invalid {config.LaufendeNr} ");
            }
            return anzahlZeilenConfig;
        }
    }
}