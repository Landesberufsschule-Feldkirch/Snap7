namespace WordClock.Model
{
    using System.ComponentModel;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        public VisuAnzeigen()
        {
            WinkelStunden = 0;
            WinkelMinuten = 0;
            WinkelSekunden = 0;
        }

        private double _WinkelStunden;
        public double WinkelStunden
        {
            get { return _WinkelStunden; }
            set
            {
                _WinkelStunden = value;
                OnPropertyChanged("WinkelStunden");
            }
        }

        
        private double _WinkelMinuten;
        public double WinkelMinuten
        {
            get { return _WinkelMinuten; }
            set
            {
                _WinkelMinuten = value;
                OnPropertyChanged("WinkelMinuten");
            }
        }


        private double _WinkelSekunden;
        public double WinkelSekunden
        {
            get { return _WinkelSekunden; }
            set
            {
                _WinkelSekunden = value;
                OnPropertyChanged("WinkelSekunden");
            }
        }




        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion iNotifyPeropertyChanged Members
    }
}
