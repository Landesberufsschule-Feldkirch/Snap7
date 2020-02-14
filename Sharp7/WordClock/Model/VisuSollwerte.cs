namespace WordClock.Model
{
    using System.ComponentModel;

    public class VisuSollwerte : INotifyPropertyChanged
    {
        public VisuSollwerte()
        {
            GeschwindigkeitSlider = 1;
        }

        public int GeschwindigkeitZeit() { return (int)GeschwindigkeitSlider; }

        private double _GeschwindigkeitSlider;

        public double GeschwindigkeitSlider
        {
            get { return _GeschwindigkeitSlider; }
            set
            {
                _GeschwindigkeitSlider = value;
                OnPropertyChanged("GeschwindigkeitSlider");
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