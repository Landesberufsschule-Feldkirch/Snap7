using ManualMode.ViewModel;
using System.IO;
using System.Windows;

namespace ManualMode
{
    public partial class FensterDi
    {
        public FensterDi(Model.ConfigDi configDi, ManualViewModel mvm)
        {
            var manViewModel = mvm;
            InitializeComponent();
            DataContext = manViewModel;

            var laufenderZaehler = 0;

            foreach (var config in configDi.DigitaleEingaenge)
            {
                if (config.LaufendeNr == laufenderZaehler)
                {
                    manViewModel.ManVisuAnzeigen.VisibilityDi[config.LaufendeNr] = Visibility.Visible;
                    manViewModel.ManVisuAnzeigen.BezeichnungDi[config.LaufendeNr] = config.Bezeichnung;
                    manViewModel.ManVisuAnzeigen.KommentarDi[config.LaufendeNr] = config.Kommentar;

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