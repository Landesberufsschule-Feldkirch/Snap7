using ManualMode.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;

namespace ManualMode.ViewModel
{
    public class ManualVisuAnzeigen : INotifyPropertyChanged
    {
        private readonly ManualMode _manualMode;

        public ManualVisuAnzeigen(ManualMode mm)
        {
            _manualMode = mm;

            for (var i = 0; i < 100; i++) ClickModeTasten.Add(System.Windows.Controls.ClickMode.Press);
            for (var i = 0; i < 100; i++) FarbeTastenToggelnDa.Add(System.Windows.Media.Brushes.LawnGreen);


            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        internal void TastenDa(object taste)
        {
            if (!(taste is DaEinstellungen daEinstellungen)) return;
            var status = ClickModeButtonSchalten(daEinstellungen.LaufendeNr);
            _manualMode.BitTastenDa(status, daEinstellungen);
        }

        public void ToggelnDa(object taste)
        {
            if (taste is DaEinstellungen daEinstellungen)
            {
                _manualMode.BitToggelnDa(daEinstellungen);
            }
        }


        private void VisuAnzeigenTask()
        {
            while (true)
            {
                if (_manualMode.ByteDigitalOutput != null)
                {
                    for (var posByte = 0; posByte < 10; posByte++)
                    {
                        for (var posBit = 0; posBit < 8; posBit++)
                        {
                            var bitMuster = 1 << posBit;
                            SetFarbeTastenToggelnDa((_manualMode.ByteDigitalOutput[posByte] & bitMuster) == bitMuster,
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

        public void SetFarbeTastenToggelnDa(bool val, int id) => FarbeTastenToggelnDa[id] = val ? System.Windows.Media.Brushes.LawnGreen : System.Windows.Media.Brushes.LightGray;

        private ObservableCollection<System.Windows.Media.Brush> _farbeTastenToggelnDa = new ObservableCollection<System.Windows.Media.Brush>();

        public ObservableCollection<System.Windows.Media.Brush> FarbeTastenToggelnDa
        {
            get => _farbeTastenToggelnDa;
            set
            {
                _farbeTastenToggelnDa = value;
                OnPropertyChanged(nameof(FarbeTastenToggelnDa));
            }
        }




        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members


    }
}