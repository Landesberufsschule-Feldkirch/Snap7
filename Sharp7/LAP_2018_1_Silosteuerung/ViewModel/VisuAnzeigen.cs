﻿using System.ComponentModel;
using System.Threading;

namespace LAP_2018_1_Silosteuerung.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        public readonly Model.Foerderanlage foerderanlage;

        public VisuAnzeigen(Model.Foerderanlage fa)
        {
            foerderanlage = fa;

            SpsVersionsInfo = true;
            SpsStatus = "x";
            SpsColor = "LightBlue";

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                Thread.Sleep(10);
            }
        }

        #region SPS Versionsinfo, Status und Farbe

        private bool _spsVersionsInfo;
        public bool SpsVersionsInfo
        {
            get => _spsVersionsInfo;
            set
            {
                _spsVersionsInfo = value;
                OnPropertyChanged(nameof(SpsVersionsInfo));
            }
        }


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