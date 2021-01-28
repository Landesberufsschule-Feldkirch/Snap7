using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace ManualMode.ViewModel
{
    public partial class ManualVisuAnzeigen
    {
       private void DigitaleEingaengeFarbeSichtbarkeit()
        {
            if (_manualMode?.Datenstruktur?.DigOutput == null) return;

            for (var i = 0; i < 100; i++)
            {
                var (iByte, bitMuster) = NummerInBitUndBitmuster(i);

                SetFarbeTastenToggelnDa((_manualMode.Datenstruktur.DigOutput[iByte] & bitMuster) == bitMuster, i);
                DiSetFarbe((_manualMode.Datenstruktur.DigInput[iByte] & bitMuster) == bitMuster, i);
            }
        }

        public void DiSetFarbe(bool val, int id) => FarbeDi[id] = val ? Brushes.LawnGreen : Brushes.LightGray;
        private ObservableCollection<Brush> _farbeDi = new();

        public ObservableCollection<Brush> FarbeDi
        {
            get => _farbeDi;
            set
            {
                _farbeDi = value;
                OnPropertyChanged(nameof(FarbeDi));
            }
        }

        private ObservableCollection<Visibility> _visibilityDi = new();

        public ObservableCollection<Visibility> VisibilityDi
        {
            get => _visibilityDi;
            set
            {
                _visibilityDi = value;
                OnPropertyChanged(nameof(VisibilityDi));
            }
        }

        private ObservableCollection<string> _bezeichnungDi = new();

        public ObservableCollection<string> BezeichnungDi
        {
            get => _bezeichnungDi;
            set
            {
                _bezeichnungDi = value;
                OnPropertyChanged(nameof(BezeichnungDi));
            }
        }

        private ObservableCollection<string> _kommentarDi = new();

        public ObservableCollection<string> KommentarDi
        {
            get => _kommentarDi;
            set
            {
                _kommentarDi = value;
                OnPropertyChanged(nameof(KommentarDi));
            }
        }
    }
}