using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ManualMode.ViewModel
{
    public partial class ManualVisuAnzeigen : INotifyPropertyChanged
    {
        private readonly ManualMode _manualMode;
        private const double SiemensAnalogSkalierung = 27648;

        public ManualVisuAnzeigen(ManualMode mm)
        {
            _manualMode = mm;

            for (var i = 0; i < 100; i++)
            {
                ClickModeTasten.Add(ClickMode.Press);
                FarbeTastenToggelnDa.Add(Brushes.LawnGreen);
                VisibilityDa.Add(Visibility.Hidden);
                BezeichnungDa.Add("-");
                WertDa.Add("0");
                KommentarDa.Add("-");

                FarbeDi.Add(Brushes.LawnGreen);
                VisibilityDi.Add(Visibility.Hidden);
                BezeichnungDi.Add("-");
                KommentarDi.Add("-");

                ContentAi.Add("-");
                VisibilityAi.Add(Visibility.Hidden);
                BezeichnungAi.Add("-");
                KommentarAi.Add("-");

                ContentAa.Add("-");
                VisibilityAa.Add(Visibility.Hidden);
                BezeichnungAa.Add("-");
                KommentarAa.Add("-");
            }

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                DigitaleEingaengeFarbeSichtbarkeit();
                DigitaleAusgaengeSchaltenFarbeSichtbarkeit();
                AnalogeEingaengeWerteSichtbarkeit();
                AnalogeAusgaengeVeraendernSichtbarkeit();

                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}