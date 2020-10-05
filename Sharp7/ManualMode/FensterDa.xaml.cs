using ManualMode.ViewModel;
using System.IO;
using System.Windows;

namespace ManualMode
{
    public partial class FensterDa
    {
        public FensterDa(Model.ConfigDa configDa, ManualViewModel mvm)
        {
            var manViewModel = mvm;
            InitializeComponent();
            DataContext = manViewModel;

            var laufenderZaehler = 0;

            foreach (var config in configDa.DigitaleAusgaenge)
            {
                if (config.LaufendeNr == laufenderZaehler)
                {
                    manViewModel.ManVisuAnzeigen.VisibilityDa[config.LaufendeNr] = Visibility.Visible;
                    manViewModel.ManVisuAnzeigen.BezeichnungDa[config.LaufendeNr] = config.Bezeichnung;
                    manViewModel.ManVisuAnzeigen.KommentarDa[config.LaufendeNr] = config.Kommentar;

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