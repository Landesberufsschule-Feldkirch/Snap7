using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows.Media;
using ManualMode.Model;

namespace ManualMode.ViewModel
{
    public class ManualVisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.ManualMode _manualMode;
        private readonly MainWindow _mainWindow;

        public ManualVisuAnzeigen(MainWindow mw, Model.ManualMode mm)
        {
            _mainWindow = mw;
            _manualMode = mm;

            for (var i = 0; i < 100; i++) ClickModeTasten.Add(System.Windows.Controls.ClickMode.Press);
            for (var i = 0; i < 100; i++) FarbeTastenToggelnDi.Add(System.Windows.Media.Brushes.LawnGreen);

            
            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        internal void TastenDi(object taste)
        {
            if (taste is DiEinstellungen diEinstellungen)
            {
                var status = ClickModeButtonSchalten(diEinstellungen.LaufendeNr);
                _manualMode.BitTastenDi(status, diEinstellungen);
            }
        }

        public void ToggelnDi(object taste)
        {
            if (taste is DiEinstellungen diEinstellungen)
            {
                _manualMode.BitToggelnDi(diEinstellungen);
            }
        }


        private void VisuAnzeigenTask()
        {
            while (true)
            {

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

        public void SetFarbeTastenToggelnDi(bool val, int id) => FarbeTastenToggelnDi[id] = val ? System.Windows.Media.Brushes.LawnGreen : System.Windows.Media.Brushes.Red;

        private ObservableCollection<System.Windows.Media.Brush> _farbeTastenToggelnDi = new ObservableCollection<System.Windows.Media.Brush>();

        public ObservableCollection<System.Windows.Media.Brush> FarbeTastenToggelnDi
        {
            get => _farbeTastenToggelnDi;
            set
            {
                _farbeTastenToggelnDi = value;
                OnPropertyChanged(nameof(FarbeTastenToggelnDi));
            }
        }




        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members


    }
}