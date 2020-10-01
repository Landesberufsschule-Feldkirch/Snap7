using ManualMode.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows.Media;

namespace ManualMode.ViewModel
{
    public class ManualVisuAnzeigen : INotifyPropertyChanged
    {
        private readonly ManualMode _manualMode;
        public Brush BackgroundButton = new SolidColorBrush(Colors.Green);
        public ManualVisuAnzeigen(ManualMode mm)
        {
            _manualMode = mm;

            for (var i = 0; i < 100; i++) ClickModeTasten.Add(System.Windows.Controls.ClickMode.Press);
            for (var i = 0; i < 100; i++) FarbeTastenToggelnDa.Add(Brushes.LawnGreen);

            FarbeTastenToggelnDa.CollectionChanged += FarbeTastenToggelnDa_CollectionChanged;
            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }


        internal void ToggelnDa(object taste)
        {
            if (taste is DaEinstellungen daEinstellungen)
            {
                _manualMode.BitToggelnDa(daEinstellungen);
            }
        }

        private void FarbeTastenToggelnDa_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //  _farbeTastenToggelnDa = value;
            OnPropertyChanged(nameof(FarbeTastenToggelnDa));
            _manualMode.ManualViewModel.ManVisuAnzeigen.BackgroundButton = new SolidColorBrush(Colors.Red);
            OnPropertyChanged("BackgroundButton");
        }

        internal void TastenDa(object taste)
        {
            if (!(taste is DaEinstellungen daEinstellungen)) return;
            var status = ClickModeButtonSchalten(daEinstellungen.LaufendeNr);
            _manualMode.BitTastenDa(status, daEinstellungen);
        }




        private void VisuAnzeigenTask()
        {
            while (true)
            {
                if (_manualMode.Datenstruktur.DigOutput!= null)
                {
                    for (var posByte = 0; posByte < 10; posByte++)
                    {
                        for (var posBit = 0; posBit < 8; posBit++)
                        {
                            var bitMuster = 1 << posBit;
                            SetFarbeTastenToggelnDa((_manualMode.Datenstruktur.DigOutput[posByte] & bitMuster) == bitMuster,
                                posBit + 8 * posByte);
                        }
                    }
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }




        #region ClickModeButtonSchalten

        public bool ClickModeButtonSchalten(int i)
        {
            if (ClickModeTasten[i] == System.Windows.Controls.ClickMode.Press)
            {
                ClickModeTasten[i] = System.Windows.Controls.ClickMode.Release;
                return true;
            }
            ClickModeTasten[i] = System.Windows.Controls.ClickMode.Press;
            return false;
        }

        private ObservableCollection<System.Windows.Controls.ClickMode> _clickModeTasten = new ObservableCollection<System.Windows.Controls.ClickMode>();
        public ObservableCollection<System.Windows.Controls.ClickMode> ClickModeTasten
        {
            get => _clickModeTasten;
            set
            {
                _clickModeTasten = value;
                OnPropertyChanged(nameof(ClickModeTasten));
            }
        }

        #endregion

        public void SetFarbeTastenToggelnDa(bool val, int id)
        {
            if (val)
            {
                FarbeTastenToggelnDa[id] = Brushes.LawnGreen;
            }
            else
            {
                FarbeTastenToggelnDa[id] = Brushes.LightGray;
            }
            OnPropertyChanged(nameof(FarbeTastenToggelnDa_CollectionChanged));
        }

        private ObservableCollection<Brush> _farbeTastenToggelnDa = new ObservableCollection<Brush>();

        public ObservableCollection<Brush> FarbeTastenToggelnDa
        {
            get => _farbeTastenToggelnDa;
            set
            {
                _farbeTastenToggelnDa = value;
                OnPropertyChanged("BackgroundButton");
            }
        }




        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members


        public void Buchstabe(object taste)
        {
            if (!(taste is string ascii)) return;
            var bittle = int.Parse(ascii);
            var bitMuster = (byte)(1 << bittle);
            if ((_manualMode.Datenstruktur.DigOutput[0] & bitMuster) == bitMuster)
            {
                _manualMode.Datenstruktur.DigOutput[0] &= (byte)~bitMuster;
            }
            else
            {
                _manualMode.Datenstruktur.DigOutput[0] |= bitMuster;
            }
        }
    }
}