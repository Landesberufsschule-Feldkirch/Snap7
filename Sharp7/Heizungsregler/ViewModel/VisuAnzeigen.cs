using System;
using System.ComponentModel;
using System.Threading;

namespace Heizungsregler.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Heizungsregler.Model.Heizungsregler heizungsregler;
        private readonly MainWindow mainWindow;

        public VisuAnzeigen(MainWindow mw, Heizungsregler.Model.Heizungsregler hr)
        {
            mainWindow = mw;
            heizungsregler = hr;

            SpsStatus = "-";
            SpsColor = "LightBlue";

         

            System.Threading.Tasks.Task.Run(() => VisuAnzeigenTask());
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {             


                if (mainWindow.S7_1200 != null)
                {
                    if (mainWindow.S7_1200.GetSpsError()) SpsColor = "Red"; else SpsColor = "LightGray";
                    SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
                }

                Thread.Sleep(10);
            }
        }

        #region SPS Status und Farbe

        private string _spsStatus;

        public string SpsStatus
        {
            get => _spsStatus;
            set
            {
                _spsStatus = value;
                OnPropertyChanged(nameof(SpsStatus));
            }
        }

        private string _spsColor;

        public string SpsColor
        {
            get => _spsColor;
            set
            {
                _spsColor = value;
                OnPropertyChanged(nameof(SpsColor));
            }
        }

        #endregion SPS Status und Farbe


        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}