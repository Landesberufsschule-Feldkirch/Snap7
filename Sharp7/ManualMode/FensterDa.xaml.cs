using System;
using ManualMode.ViewModel;
using System.IO;
using System.Windows;
using ManualMode.Model;

namespace ManualMode
{
    public partial class FensterDa
    {
        public FensterDa(ConfigDa configDa, ManualViewModel mvm)
        {
            var manViewModel = mvm;
            InitializeComponent();
            DataContext = manViewModel;

            var laufenderZaehler = 0;

            foreach (var config in configDa.DigitaleAusgaenge)
            {
                if (config.LaufendeNr == laufenderZaehler)
                {
                    switch (config.Type)
                    {
                        case PlcEinUndAusgaengeTypen.BitmusterByte:
                            break;

                        case PlcEinUndAusgaengeTypen.Default:
                            manViewModel.ManVisuAnzeigen.VisibilityDa[config.LaufendeNr] = Visibility.Visible;
                            manViewModel.ManVisuAnzeigen.BezeichnungDa[config.LaufendeNr] = config.Bezeichnung;
                            manViewModel.ManVisuAnzeigen.KommentarDa[config.LaufendeNr] = config.Kommentar;
                            break;
                        case PlcEinUndAusgaengeTypen.Ascii:
                            break;
                        case PlcEinUndAusgaengeTypen.SiemensAnalogwertProzent:
                            break;
                        case PlcEinUndAusgaengeTypen.SiemensAnalogwertPromille:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    laufenderZaehler++;
                }
                else
                {
                    throw new InvalidDataException($"{nameof(FensterDi)} invalid {config.LaufendeNr} ");
                }
            }
        }
    }
}