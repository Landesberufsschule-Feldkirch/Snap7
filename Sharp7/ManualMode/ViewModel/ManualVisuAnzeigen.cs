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
            if (taste is DaEinstellungen daEinstellungen)
            {
                var status = ClickModeButtonSchalten(daEinstellungen.LaufendeNr);
                _manualMode.BitTastenDa(status, daEinstellungen);
            }
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
            int BitPosition;
            while (true)
            {
                if (_manualMode.ByteAnalogOutput != null)
                {
                    BitPosition = 0;
                    for (int posByte = 0; posByte < 10; posByte++)
                    {
                        for (int posBit = 0; posBit < 8; posBit++)
                        {
                            var bitMuster = 1 << posBit;
                            if ((_manualMode.ByteAnalogOutput[posByte] & bitMuster) == bitMuster) SetFarbeTastenToggelnDa(true, BitPosition); else SetFarbeTastenToggelnDa(false, BitPosition);
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
            else
            {
                ClickModeTasten[i] = System.Windows.Controls.ClickMode.Press;
            }
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

        public void SetFarbeTastenToggelnDa(bool val, int id) => FarbeTastenToggelnDa[id] = val ? System.Windows.Media.Brushes.LawnGreen : System.Windows.Media.Brushes.Red;

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