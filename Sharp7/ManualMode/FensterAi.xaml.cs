using ManualMode.ViewModel;
using System.IO;
using System.Windows;

namespace ManualMode
{
    public partial class FensterAi
    {
        public FensterAi(Model.ConfigAi configAi,  ManualViewModel mvm)
        {
            var manViewModel = mvm;
            InitializeComponent();
            DataContext = manViewModel;

            var laufenderZaehler = 0;

            foreach (var config in configAi.AnalogeEingaenge)
            {
                if (config.LaufendeNr == laufenderZaehler)
                {
                    manViewModel.ManVisuAnzeigen.VisibilityAi[config.LaufendeNr] = Visibility.Visible;
                    manViewModel.ManVisuAnzeigen.BezeichnungAi[config.LaufendeNr] = config.Bezeichnung;
                    manViewModel.ManVisuAnzeigen.KommentarAi[config.LaufendeNr] = config.Kommentar;

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