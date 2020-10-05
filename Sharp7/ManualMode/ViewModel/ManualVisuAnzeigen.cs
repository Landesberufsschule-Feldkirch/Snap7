using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace ManualMode.ViewModel
{
    public class ManualVisuAnzeigen : INotifyPropertyChanged
    {
        private readonly ManualMode _manualMode;
        public ManualVisuAnzeigen(ManualMode mm)
        {
            _manualMode = mm;

            for (var i = 0; i < 100; i++)
            {
                ClickModeTasten.Add(System.Windows.Controls.ClickMode.Press);
                FarbeTastenToggelnDa.Add(Brushes.LawnGreen);
                VisibilityDa.Add(Visibility.Hidden);
                BezeichnungDa.Add("-");
                KommentarDa.Add("-");

                FarbeDi.Add(Brushes.LawnGreen);
                VisibilityDi.Add(Visibility.Hidden);
                BezeichnungDi.Add("-");
                KommentarDi.Add("-");
            }

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }


        private void VisuAnzeigenTask()
        {
            while (true)
            {
                if (_manualMode?.Datenstruktur.DigOutput != null)
                {
                    for (var i = 0; i < 100; i++)
                    {
                        var (iByte, bitMuster) = NummerInBitUndBitmuster(i);

                        SetFarbeTastenToggelnDa((_manualMode.Datenstruktur.DigOutput[iByte] & bitMuster) == bitMuster, i);
                        SetFarbeDi((_manualMode.Datenstruktur.DigInput[iByte] & bitMuster) == bitMuster, i);
                    }
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        #region Digitale Ausgänge

        internal void BtnTasten(object taste)
        {
            if (!(taste is string ascii)) return;
            var (iByte, bitMuster) = NummerInBitUndBitmuster(int.Parse(ascii));

            if (ClickModeButtonSchalten(int.Parse(ascii)))
            {
                _manualMode.Datenstruktur.DigOutput[iByte] |= bitMuster;
            }
            else
            {
                _manualMode.Datenstruktur.DigOutput[iByte] &= (byte)~bitMuster;
            }
        }

        internal void BtnToggeln(object taste)
        {
            if (!(taste is string ascii)) return;
            var (iByte, bitMuster) = NummerInBitUndBitmuster(int.Parse(ascii));

            if ((_manualMode.Datenstruktur.DigOutput[iByte] & bitMuster) == bitMuster)
            {
                _manualMode.Datenstruktur.DigOutput[iByte] &= (byte)~bitMuster;
            }
            else
            {
                _manualMode.Datenstruktur.DigOutput[iByte] |= bitMuster;
            }
        }

        internal (int ibyte, byte bitmuster) NummerInBitUndBitmuster(int i)
        {
            var ibyte = i / 8;
            var bitMuster = (byte)(1 << i % 8);

            return (ibyte, bitMuster);
        }



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
        }

        private ObservableCollection<Brush> _farbeTastenToggelnDa = new ObservableCollection<Brush>();
        public ObservableCollection<Brush> FarbeTastenToggelnDa
        {
            get => _farbeTastenToggelnDa;
            set
            {
                _farbeTastenToggelnDa = value;
                OnPropertyChanged("FarbeTastenToggelnDa");
            }
        }


        private ObservableCollection<Visibility> _visibilityDa = new ObservableCollection<Visibility>();
        public ObservableCollection<Visibility> VisibilityDa
        {
            get => _visibilityDa;
            set
            {
                _visibilityDa = value;
                OnPropertyChanged(nameof(VisibilityDa));
            }
        }


        private ObservableCollection<string> _bezeichnungDa = new ObservableCollection<string>();
        public ObservableCollection<string> BezeichnungDa
        {
            get => _bezeichnungDa;
            set
            {
                _bezeichnungDa = value;
                OnPropertyChanged(nameof(BezeichnungDa));
            }
        }


        private ObservableCollection<string> _kommentarDa = new ObservableCollection<string>();
        public ObservableCollection<string> KommentarDa
        {
            get => _kommentarDa;
            set
            {
                _kommentarDa = value;
                OnPropertyChanged(nameof(KommentarDa));
            }
        }

        #endregion

        #region Digitale Eingänge

        public void SetFarbeDi(bool val, int id)
        {
            if (val)
            {
                FarbeDi[id] = Brushes.LawnGreen;
            }
            else
            {
                FarbeDi[id] = Brushes.LightGray;
            }
        }

        private ObservableCollection<Brush> _farbeDi = new ObservableCollection<Brush>();
        public ObservableCollection<Brush> FarbeDi
        {
            get => _farbeDi;
            set
            {
                _farbeDi = value;
                OnPropertyChanged("FarbeDi");
            }
        }

        private ObservableCollection<Visibility> _visibilityDi = new ObservableCollection<Visibility>();
        public ObservableCollection<Visibility> VisibilityDi
        {
            get => _visibilityDi;
            set
            {
                _visibilityDi = value;
                OnPropertyChanged(nameof(VisibilityDi));
            }
        }

        private ObservableCollection<string> _bezeichnungDi = new ObservableCollection<string>();
        public ObservableCollection<string> BezeichnungDi
        {
            get => _bezeichnungDi;
            set
            {
                _bezeichnungDi = value;
                OnPropertyChanged(nameof(BezeichnungDi));
            }
        }


        private ObservableCollection<string> _kommentarDi = new ObservableCollection<string>();
        public ObservableCollection<string> KommentarDi
        {
            get => _kommentarDi;
            set
            {
                _kommentarDi = value;
                OnPropertyChanged(nameof(KommentarDi));
            }
        }

        #endregion




        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}