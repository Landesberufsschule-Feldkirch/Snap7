using ManualMode.Model;
using ManualMode.ViewModel;
using System;
using System.IO;
using System.Windows;

namespace ManualMode
{
    public partial class AaFenster
    {
        public static bool DatenTypenBit { get; set; }
        public static bool DatenTypenByte { get; set; }
        public static bool DatenTypenWord { get; set; }
        public static bool DatenTypenLong { get; set; }

        private const int SpaltenAbstand = 10;
        private const int SpaltenWert = 300;
        private const int SpaltenBezeichnung = 120;
        private const int SpaltenKommentar = 300;

        private const int ZeilenAbstand = 10;
        private const int ZeilenHoehe = 45;

        public AaFenster(AaConfig aaConfig, ManualViewModel mvm)
        {
            DatenTypenBit = false;
            DatenTypenByte = false;
            DatenTypenWord = false;

            var manualViewModel = mvm;
            InitializeComponent();
            DataContext = manualViewModel;

            var anzahlZeilenConfig = AaDatenLesen(aaConfig, manualViewModel);

            if (DatenTypenBit) AaCreateGridBit(anzahlZeilenConfig);
            if (DatenTypenByte) AaCreateGridByte();
            if (DatenTypenWord) AaCreateGridWord(anzahlZeilenConfig);
            if (DatenTypenLong) AaCreateGridLong();
        }
        private static int AaDatenLesen(AaConfig aaConfig, ManualViewModel manualViewModel)
        {
            var anzahlZeilenConfig = 0;

            foreach (var config in aaConfig.AnalogeAusgaenge)
            {
                if (config.LaufendeNr == anzahlZeilenConfig)
                {
                    switch (config.AnzahlBit)
                    {
                        case 1:
                            DatenTypenBit = true;
                            if (DatenTypenByte || DatenTypenWord || DatenTypenLong) throw new ArgumentOutOfRangeException(nameof(config.AnzahlBit));
                            break;
                        case 8:
                            DatenTypenByte = true;
                            if (DatenTypenBit || DatenTypenWord || DatenTypenLong) throw new ArgumentOutOfRangeException(nameof(config.AnzahlBit));
                            break;
                        case 16:
                            DatenTypenWord = true;
                            if (DatenTypenBit || DatenTypenByte || DatenTypenLong) throw new ArgumentOutOfRangeException(nameof(config.AnzahlBit));
                            break;
                        case 32:
                            DatenTypenLong = true;
                            if (DatenTypenBit || DatenTypenByte || DatenTypenWord) throw new ArgumentOutOfRangeException(nameof(config.AnzahlBit));
                            break;
                        default: throw new ArgumentOutOfRangeException(nameof(config.AnzahlBit));
                    }

                    manualViewModel.ManVisuAnzeigen.VisibilityAa[config.LaufendeNr] = Visibility.Visible;
                    manualViewModel.ManVisuAnzeigen.BezeichnungAa[config.LaufendeNr] = config.Bezeichnung;
                    manualViewModel.ManVisuAnzeigen.KommentarAa[config.LaufendeNr] = config.Kommentar;

                    anzahlZeilenConfig++;
                }
                else throw new InvalidDataException($"{nameof(DiFenster)} invalid {config.LaufendeNr} ");
            }
            return anzahlZeilenConfig;
        }
    }
}