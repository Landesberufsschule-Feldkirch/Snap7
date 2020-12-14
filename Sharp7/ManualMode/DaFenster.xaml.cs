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


        private const int SpaltenAbstand = 10;
        private const int SpaltenWert = 80;
        private const int SpaltenBezeichnung = 120;
        private const int SpaltenKommentar = 300;

        private const int ZeilenAbstand = 10;
        private const int ZeilenHoehe = 45;

        private readonly FensterFunktionen _fensterFunktionen = new FensterFunktionen();

        public DaFenster(DaConfig daConfig, ManualViewModel mvm)
        {
            DatenTypenBit = false;
            DatenTypenByte = false;
            DatenTypenWord = false;

            InitializeComponent();
            DataContext = mvm;

            var anzahlZeilenConfig = DaDatenLesen(daConfig, mvm);
            if (DatenTypenBit) DaCreateGridBit(anzahlZeilenConfig, daConfig, mvm);
            if (DatenTypenByte) DaCreateGridByte(anzahlZeilenConfig);
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
                            if (DatenTypenByte || DatenTypenWord || DatenTypenLong) throw new ArgumentOutOfRangeException();
                            break;
                        case 8:
                            DatenTypenByte = true;
                            if (DatenTypenBit || DatenTypenWord || DatenTypenLong) throw new ArgumentOutOfRangeException();
                            break;
                        case 16:
                            DatenTypenWord = true;
                            if (DatenTypenBit || DatenTypenByte || DatenTypenLong) throw new ArgumentOutOfRangeException();
                            break;
                        case 32:
                            DatenTypenLong = true;
                            if (DatenTypenBit || DatenTypenByte || DatenTypenWord) throw new ArgumentOutOfRangeException();
                            break;
                        default: throw new ArgumentOutOfRangeException();
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
                        case PlcEinUndAusgaengeTypen.Schieberegler: break;
                        default: throw new ArgumentOutOfRangeException();
                    }

                    anzahlZeilenConfig++;
                }
                else throw new InvalidDataException($"{nameof(DiFenster)} invalid {config.LaufendeNr} ");
            }
            return anzahlZeilenConfig;
        }
    }
}