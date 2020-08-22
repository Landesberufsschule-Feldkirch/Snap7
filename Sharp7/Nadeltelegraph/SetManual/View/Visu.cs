using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using Kommunikation;

namespace Nadeltelegraph.SetManual.View
{
    public class Visu : INotifyPropertyChanged
    {
        private readonly MainWindow _mainWindow;

        public Visu(MainWindow mw)
        {
            _mainWindow = mw;

            AsciiHex = "0x----";
            AsciiBin = "--";

            for (var i = 0; i < 64; i++) AlleBitTasten.Add(false);
            for (var i = 0; i < 64; i++) ClickModeTasten.Add("Press");
            for (var i = 0; i < 64; i++) ColorTasten.Add("LightGray");

            for (var i = 0; i < 64; i++) AlleBitToggeln.Add(false);
            for (var i = 0; i < 64; i++) ColorToggeln.Add("LightGray");


            System.Threading.Tasks.Task.Run(VisuTask);
        }

        private void VisuTask()
        {
            while (true)
            {
                if (_mainWindow.Plc != null && _mainWindow.Plc.GetModel() == "Manual")
                {
                    for (var i = 0; i < 64; i++)
                    {
                        _mainWindow.Plc.SetBitAt(Datenbausteine.DigOut, i, BitGesetztTesten(i));
                        ColorTasten[i] = AlleBitTasten[i] ? "LawnGreen" : "LightGray";
                        ColorToggeln[i] = AlleBitToggeln[i] ? "LawnGreen" : "LightGray";

                    }

                    AsciiBin = Convert.ToString(_mainWindow.Plc.GetUint16At(Datenbausteine.DigIn, 0), 2).PadLeft(16, '0');
                    AsciiHex = "0x" + _mainWindow.Plc.GetUint16At(Datenbausteine.DigIn, 0).ToString("X");
                }

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        internal void KnopfTasten(object knopf)
        {
            if (knopf is string nummer)
            {
                var bitnummer = Int32.Parse(nummer);
                AlleBitTasten[bitnummer] = ClickModeButton(bitnummer);
            }
        }

        internal void KnopfToggeln(object knopf)
        {
            if (knopf is string nummer)
            {
                var bitnummer = Int32.Parse(nummer);
                AlleBitToggeln[bitnummer] = !AlleBitToggeln[bitnummer];
            }
        }

        public bool ClickModeButton(int bitnummer)
        {
            if (ClickModeTasten[bitnummer] == "Press")
            {
                ClickModeTasten[bitnummer] = "Release";
                return true;
            }
            else
            {
                ClickModeTasten[bitnummer] = "Press";
            }
            return false;
        }

        internal bool BitGesetztTesten(int i) => AlleBitTasten[i] | AlleBitToggeln[i];


        private string _asciiBin;
        public string AsciiBin
        {
            get => _asciiBin;
            set
            {
                _asciiBin = value;
                OnPropertyChanged(nameof(AsciiBin));
            }
        }

        private string _asciiHex;
        public string AsciiHex
        {
            get => _asciiHex;
            set
            {
                _asciiHex = value;
                OnPropertyChanged(nameof(AsciiHex));
            }
        }


        #region Tasten
        private ObservableCollection<bool> _alleBitTasten = new ObservableCollection<bool>();
        public ObservableCollection<bool> AlleBitTasten
        {
            get => _alleBitTasten;
            set
            {
                _alleBitTasten = value;
                OnPropertyChanged(nameof(AlleBitTasten));
            }
        }

        private ObservableCollection<string> _clickModeTasten = new ObservableCollection<string>();
        public ObservableCollection<string> ClickModeTasten
        {
            get => _clickModeTasten;
            set
            {
                _clickModeTasten = value;
                OnPropertyChanged(nameof(ClickModeTasten));
            }
        }

        private ObservableCollection<string> _colorTasten = new ObservableCollection<string>();
        public ObservableCollection<string> ColorTasten
        {
            get => _colorTasten;
            set
            {
                _colorTasten = value;
                OnPropertyChanged(nameof(AlleBitToggeln));
            }
        }
        #endregion

        #region Toggeln
        private ObservableCollection<bool> _alleBitToggeln = new ObservableCollection<bool>();
        public ObservableCollection<bool> AlleBitToggeln
        {
            get => _alleBitToggeln;
            set
            {
                _alleBitToggeln = value;
                OnPropertyChanged(nameof(AlleBitToggeln));
            }
        }

        private ObservableCollection<string> _colorToggeln = new ObservableCollection<string>();
        public ObservableCollection<string> ColorToggeln
        {
            get => _colorToggeln;
            set
            {
                _colorToggeln = value;
                OnPropertyChanged(nameof(ColorToggeln));
            }
        }
        #endregion






        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members



    }
}