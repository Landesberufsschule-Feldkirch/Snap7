using System.Collections.ObjectModel;

namespace Nadeltelegraph.ViewModel
{
    using System.ComponentModel;

    public partial class VisuAnzeigen : INotifyPropertyChanged
    {
        public void WinkelEinstellen(bool rechts, bool links, int zeiger)
        {
            AlleWinkel[zeiger] = 0;
            if (rechts) AlleWinkel[zeiger] = WinkelNadel;
            if (links) AlleWinkel[zeiger] = -WinkelNadel;
        }

        private ObservableCollection<int> _alleWinkel = new ObservableCollection<int>();

        public ObservableCollection<int> AlleWinkel
        {
            get => _alleWinkel;
            set
            {
                _alleWinkel = value;
                OnPropertyChanged(nameof(AlleWinkel));
            }
        }
    }
}