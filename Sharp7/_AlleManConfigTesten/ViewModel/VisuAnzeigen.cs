using _AlleManConfigTesten.Model;
using System.Collections.Generic;
using System.ComponentModel;

namespace _AlleManConfigTesten.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        public void AddAaDaten(AaDaten daten) => AaAlleDaten.Add(daten);
        public void AddAiDaten(AiDaten daten) => AiAlleDaten.Add(daten);
        public void AddDaDaten(DaDaten daten) => DaAlleDaten.Add(daten);
        public void AddDiDaten(DiDaten daten) => DiAlleDaten.Add(daten);



        private List<AaDaten> _aaAlleDaten = new List<AaDaten>();
        public List<AaDaten> AaAlleDaten
        {
            get => _aaAlleDaten;
            set
            {
                _aaAlleDaten = value;
                OnPropertyChanged(nameof(AaAlleDaten));
            }
        }

        private List<AiDaten> _aiAlleDaten = new List<AiDaten>();
        public List<AiDaten> AiAlleDaten
        {
            get => _aiAlleDaten;
            set
            {
                _aiAlleDaten = value;
                OnPropertyChanged(nameof(AiAlleDaten));
            }
        }


        private List<DaDaten> _daAlleDaten = new List<DaDaten>();
        public List<DaDaten> DaAlleDaten
        {
            get => _daAlleDaten;
            set
            {
                _daAlleDaten = value;
                OnPropertyChanged(nameof(DaAlleDaten));
            }
        }

        private List<DiDaten> _diAlleDaten = new List<DiDaten>();
        public List<DiDaten> DiAlleDaten
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