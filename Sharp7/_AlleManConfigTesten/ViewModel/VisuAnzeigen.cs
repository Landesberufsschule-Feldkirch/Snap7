using System.Collections.ObjectModel;
using System.ComponentModel;
using _AlleManConfigTesten.Model;

namespace _AlleManConfigTesten.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        public VisuAnzeigen()
        {
            AaAlleDaten.Add(new AaConfig());
            AiAlleDaten.Add(new AiConfig());
            DaAlleDaten.Add(new DaConfig());
            DiAlleDaten.Add(new DiConfig());
        }



        private ObservableCollection<AaConfig> _aaAlleDaten = new ObservableCollection<AaConfig>();
        public ObservableCollection<AaConfig> AaAlleDaten
        {
            get => _aaAlleDaten;
            set
            {
                _aaAlleDaten = value;
                OnPropertyChanged(nameof(AaAlleDaten));
            }
        }

        private ObservableCollection<AiConfig> _aiAlleDaten = new ObservableCollection<AiConfig>();
        public ObservableCollection<AiConfig> AiAlleDaten
        {
            get => _aiAlleDaten;
            set
            {
                _aiAlleDaten = value;
                OnPropertyChanged(nameof(AiAlleDaten));
            }
        }


        private ObservableCollection<DaConfig> _daAlleDaten = new ObservableCollection<DaConfig>();
        public ObservableCollection<DaConfig> DaAlleDaten
        {
            get => _daAlleDaten;
            set
            {
                _daAlleDaten = value;
                OnPropertyChanged(nameof(DaAlleDaten));
            }
        }

        private ObservableCollection<DiConfig> _diAlleDaten = new ObservableCollection<DiConfig>();
        public ObservableCollection<DiConfig> DiAlleDaten
        {
            get => _diAlleDaten;
            set
            {
                _diAlleDaten = value;
                OnPropertyChanged(nameof(DiAlleDaten));
            }
        }


        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}