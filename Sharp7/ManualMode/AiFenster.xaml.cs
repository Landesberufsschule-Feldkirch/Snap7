using ManualMode.Model;
using ManualMode.ViewModel;
using System;
using System.IO;
using System.Windows;

namespace ManualMode
{
    public partial class AiFenster
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

        private readonly FensterFunktionen _fensterFunktionen = new FensterFunktionen();

        public AiFenster(AiConfig aiConfig, ManualViewModel mvm)
        {
            DatenTypenBit = false;
            DatenTypenByte = false;
            DatenTypenWord = false;

            InitializeComponent();
            DataContext = mvm;

            var anzahlZeilenConfig = AiDatenLesen(aiConfig, mvm);
            if (DatenTypenBit) AiCreateGridBit(anzahlZeilenConfig);
            if (DatenTypenByte) AiCreateGridByte(anzahlZeilenConfig);
            if (DatenTypenWord) AiCreateGridWord();
            if (DatenTypenLong) AiCreateGridLong();
        }
        private static int AiDatenLesen(AiConfig aiConfig, ManualViewModel manViewModel)
        {
            var anzahlZeilenConfig = 0;

            foreach (var config in aiConfig.AnalogeEingaenge)
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

                    manViewModel.ManVisuAnzeigen.VisibilityAi[config.LaufendeNr] = Visibility.Visible;
                    manViewModel.ManVisuAnzeigen.BezeichnungAi[config.LaufendeNr] = config.Bezeichnung;
                    manViewModel.ManVisuAnzeigen.KommentarAi[config.LaufendeNr] = config.Kommentar;

                    anzahlZeilenConfig++;
                }
                else throw new InvalidDataException($"{nameof(DiFenster)} invalid {config.LaufendeNr} ");
            }
            return anzahlZeilenConfig;
        }
      }
}