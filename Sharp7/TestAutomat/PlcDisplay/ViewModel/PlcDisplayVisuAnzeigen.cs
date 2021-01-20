using Kommunikation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows.Media;

namespace TestAutomat.PlcDisplay.ViewModel
{
    public class PlcDisplayVisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Datenstruktur _datenstruktur;

        public PlcDisplayVisuAnzeigen(Datenstruktur datenstruktur)
        {
            _datenstruktur = datenstruktur;

            for (var i = 0; i < 100; i++)
            {
                FarbeDi.Add(Brushes.LawnGreen);
                FarbeDa.Add(Brushes.LawnGreen);
            }

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                for (var i = 0; i < 16; i++)
                {
                    DiSetFarbe(BitTesten(_datenstruktur.DigInput, i), i);
                    DaSetFarbe(BitTesten(_datenstruktur.DigOutput, i), i);
                }


                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }
        


        public void DiSetFarbe(bool val, int id) => FarbeDi[id] = val ? Brushes.LawnGreen : Brushes.DarkGray;

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

        public void DaSetFarbe(bool val, int id) => FarbeDa[id] = val ? Brushes.LawnGreen : Brushes.DarkGray;

        private ObservableCollection<Brush> _farbeDa = new();
        public ObservableCollection<Brush> FarbeDa
        {
            get => _farbeDa;
            set
            {
                _farbeDa = value;
                OnPropertyChanged(nameof(FarbeDa));
            }
        }
        
        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        // ReSharper disable once UnusedMember.Local
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
        
        private static bool BitTesten(IReadOnlyList<byte> datenArray, int i)
        {
            var ibyte = i / 8;
            var bitMuster = (byte)(1 << i % 8);

            return (datenArray[ibyte] & bitMuster) == bitMuster;
        }

    }
}