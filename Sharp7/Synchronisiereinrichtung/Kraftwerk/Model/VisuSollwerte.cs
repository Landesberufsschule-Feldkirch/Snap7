using System.ComponentModel;

namespace Synchronisiereinrichtung.Kraftwerk.Model
{
    public class VisuSollwerte : INotifyPropertyChanged
    {
        public VisuSollwerte()
        {
            ManualVentilstellung = 0;
            ManualErregerstrom = 0;

            NetzSpannungSlider = 400;
            NetzFrequenzSlider = 50;
            NetzCosPhiSlider = 0;
            NetzLeistungSlider = 600;
            SynchAuswahl = SynchronisierungAuswahl.U_f;
        }

        private double _ManualVentilstellung;

        public double ManualVentilstellung
        {
            get { return _ManualVentilstellung; }
            set
            {
                _ManualVentilstellung = value;
                OnPropertyChanged("ManualVentilstellung");
            }
        }

        private double _ManualErregerstrom;

        public double ManualErregerstrom
        {
            get { return _ManualErregerstrom; }
            set
            {
                _ManualErregerstrom = value;
                OnPropertyChanged("ManualErregerstrom");
            }
        }

        private double _NetzSpannungSlider;

        public double NetzSpannungSlider
        {
            get { return _NetzSpannungSlider; }
            set
            {
                _NetzSpannungSlider = value;
                OnPropertyChanged("NetzSpannungSlider");
            }
        }

        private double _NetzFrequenzSlider;

        public double NetzFrequenzSlider
        {
            get { return _NetzFrequenzSlider; }
            set
            {
                _NetzFrequenzSlider = value;
                OnPropertyChanged("NetzFrequenzSlider");
            }
        }

        private double _NetzCosPhiSlider;

        public double NetzCosPhiSlider
        {
            get { return _NetzCosPhiSlider; }
            set
            {
                _NetzCosPhiSlider = value;
                OnPropertyChanged("NetzCosPhiSlider");
            }
        }

        private double _NetzLeistungSlider;

        public double NetzLeistungSlider
        {
            get { return _NetzLeistungSlider; }
            set
            {
                _NetzLeistungSlider = value;
                OnPropertyChanged("NetzLeistungSlider");
            }
        }

        private SynchronisierungAuswahl _SynchAuswahl;

        public SynchronisierungAuswahl SynchAuswahl
        {
            get { return _SynchAuswahl; }
            set
            {
                _SynchAuswahl = value;
                OnPropertyChanged("SynchAuswahl");
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