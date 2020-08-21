using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using Kommunikation;

namespace Nadeltelegraph.SetManual.View
{
    public class Visu : INotifyPropertyChanged
    {
        private readonly MainWindow _mainWindow;

        public Visu(MainWindow mw, Model.Nadeltelegraph nt)
        {
            _mainWindow = mw;

            ClickModeBtnP1L = "Press";

            for (var i = 0; i < 64; i++) AlleBit.Add(false);





            System.Threading.Tasks.Task.Run(VisuTask);
        }

        private void VisuTask()
        {
            while (true)
            {
                if (_mainWindow != null && _mainWindow.Plc.GetModel() == "Manual")
                {
                    for (var i = 1; i < 64; i++)
                    {
                        _mainWindow.Plc.SetBitAt(Datenbausteine.DigOut, i, AlleBit[i]);
                    }
                }


                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }


        internal void SetP1L() => _mainWindow.Plc.SetBitAt(Datenbausteine.DigOut, (int)DatenRangieren.BitPosAusgang.P1L, ClickModeButtonP1L());

        public bool ClickModeButtonP1L()
        {
            if (ClickModeBtnP1L == "Press")
            {
                ClickModeBtnP1L = "Release";
                return true;
            }

            ClickModeBtnP1L = "Press";
            return false;
        }

        private string _clickModeBtnP1L;

        public string ClickModeBtnP1L
        {
            get => _clickModeBtnP1L;
            set
            {
                _clickModeBtnP1L = value;
                OnPropertyChanged(nameof(ClickModeBtnP1L));
            }
        }




        private ObservableCollection<bool> _alleBit = new ObservableCollection<bool>();
        public ObservableCollection<bool> AlleBit
        {
            get => _alleBit;
            set
            {
                _alleBit = value;
                OnPropertyChanged(nameof(AlleBit));
            }
        }




        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members



    }
}