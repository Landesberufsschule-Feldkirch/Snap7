namespace WordClock.Model
{
    using System.ComponentModel;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        public VisuAnzeigen()
        {
            SpsStatus = "-";
            SpsColor = "LightBlue";

            WinkelStunden = 0;
            WinkelMinuten = 0;
            WinkelSekunden = 0;
        }


        #region SPS Status und Farbe
        private string _spsStatus;
        public string SpsStatus
        {
            get { return _spsStatus; }
            set
            {
                _spsStatus = value;
                OnPropertyChanged("SpsStatus");
            }
        }

        private string _spsColor;
        public string SpsColor
        {
            get { return _spsColor; }
            set
            {
                _spsColor = value;
                OnPropertyChanged("SpsColor");
            }
        }
        #endregion



        #region WinkelStunden
        private double _winkelStunden;
        public double WinkelStunden
        {
            get { return _winkelStunden; }
            set
            {
                _winkelStunden = value;
                OnPropertyChanged("WinkelStunden");
            }
        }
        #endregion

        #region WinkelMinuten
        private double _winkelMinuten;
        public double WinkelMinuten
        {
            get { return _winkelMinuten; }
            set
            {
                _winkelMinuten = value;
                OnPropertyChanged("WinkelMinuten");
            }
        }
        #endregion

        #region WinkelSekunden
        private double _winkelSekunden;
        public double WinkelSekunden
        {
            get { return _winkelSekunden; }
            set
            {
                _winkelSekunden = value;
                OnPropertyChanged("WinkelSekunden");
            }
        }
        #endregion



        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion iNotifyPeropertyChanged Members
    }
}
