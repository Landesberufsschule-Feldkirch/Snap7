using ManualMode.Model;
using ManualMode.ViewModel;
using System;
using System.IO;
using System.Windows;

namespace ManualMode
{
    public partial class DiFenster
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

        public DiFenster(DiConfig diConfig, ManualViewModel mvm)
        {
            DatenTypenBit = false;
            DatenTypenByte = false;
            DatenTypenWord = false;

            InitializeComponent();
            DataContext = mvm;

            var anzahlZeilenConfig = DiDatenLesen(diConfig, mvm);

            if (DatenTypenBit) DiCreateGridBit(anzahlZeilenConfig, diConfig);
            if (DatenTypenByte) DiCreateGridByte();
            if (DatenTypenWord) DiCreateGridWord();
            if (DatenTypenLong) DiCreateGridLong();
        }
        private static int DiDatenLesen(DiConfig diConfig, ManualViewModel manViewModel)
        {
            var anzahlZeilenConfig = 0;
            var aktuellesBit = 0;
            var aktuellesByte = 0;

            foreach (var config in diConfig.DigitaleEingaenge)
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
                        case PlcEinUndAusgaengeTypen.Default:
                            {
                                if (config.AnzahlBit == 1)
                                {
                                    if (config.StartByte == aktuellesByte && config.StartBit == aktuellesBit)
                                    {
                                        aktuellesBit++;
                                        if (aktuellesBit > 7)
                                        {
                                            aktuellesBit = 0;
                                            aktuellesByte++;
                                        }
                                    }
                                    else throw new InvalidDataException("Byte und Bit müssen schön gefüllt sein!");
                                }

                                break;
                            }
                        case PlcEinUndAusgaengeTypen.Ascii: break;
                        case PlcEinUndAusgaengeTypen.SiemensAnalogwertProzent: break;
                        case PlcEinUndAusgaengeTypen.SiemensAnalogwertPromille: break;
                        case PlcEinUndAusgaengeTypen.BitmusterByte: break;
                        case PlcEinUndAusgaengeTypen.Schieberegler: break;
                        default: throw new InvalidDataException(config.Type.ToString() + config.AnzahlBit);
                    }


                    manViewModel.ManVisuAnzeigen.VisibilityDi[config.LaufendeNr] = Visibility.Visible;
                    manViewModel.ManVisuAnzeigen.BezeichnungDi[config.LaufendeNr] = config.Bezeichnung;
                    manViewModel.ManVisuAnzeigen.KommentarDi[config.LaufendeNr] = config.Kommentar;

                    anzahlZeilenConfig++;
                }
                else
                {
                    throw new InvalidDataException($"{nameof(DiFenster)} invalid {config.LaufendeNr} ");
                }
            }

            return anzahlZeilenConfig;
        }
    }
}