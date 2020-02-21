using System;
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
            NetzPhasenverschiebungSlider = 90; // der Einstellbereich geht von 0 ..180
            NetzLeistungSlider = 600;
            SynchAuswahl = SynchronisierungAuswahl.U_f;
        }

        #region Ventil

        public double Y() { return ManualVentilstellung; }

        private double _manualVentilstellung;

        public double ManualVentilstellung
        {
            get { return _manualVentilstellung; }
            set
            {
                _manualVentilstellung = value;
                OnPropertyChanged("ManualVentilstellung");
            }
        }

        #endregion

        #region Erregerstrom
        public double Ie() { return ManualErregerstrom; }

        private double _manualErregerstrom;

        public double ManualErregerstrom
        {
            get { return _manualErregerstrom; }
            set
            {
                _manualErregerstrom = value;
                OnPropertyChanged("ManualErregerstrom");
            }
        }

        #endregion

        #region Netzspannung

        public double Netz_U() { return NetzSpannungSlider; }

        private double _netzSpannungSlider;

        public double NetzSpannungSlider
        {
            get { return _netzSpannungSlider; }
            set
            {
                _netzSpannungSlider = value;
                OnPropertyChanged("NetzSpannungSlider");
            }
        }

        #endregion

        #region Netzfrequenz

        public double Netz_f() { return NetzFrequenzSlider; }

        private double _netzFrequenzSlider;

        public double NetzFrequenzSlider
        {
            get { return _netzFrequenzSlider; }
            set
            {
                _netzFrequenzSlider = value;
                OnPropertyChanged("NetzFrequenzSlider");
            }
        }

        #endregion

        #region NetzLeistungsfaktor

        public double Netz_CosPhi()
        {
            // Der Slider geht fast von 0 bis 180 ==> -90° bis 90°
            if (_netzPhasenverschiebungSlider < 90) return (-1) * Math.Cos(Math.PI * (_netzPhasenverschiebungSlider - 90) / 180);
            else return Math.Cos(Math.PI * (_netzPhasenverschiebungSlider - 90) / 180);
        }

        private double _netzPhasenverschiebungSlider;

        public double NetzPhasenverschiebungSlider
        {
            get { return _netzPhasenverschiebungSlider; }
            set
            {
                _netzPhasenverschiebungSlider = value;
                OnPropertyChanged("NetzPhasenverschiebungSlider");
            }
        }

        #endregion

        #region Netzleistung

        public double Netz_P() { return NetzLeistungSlider; }

        private double _netzLeistungSlider;

        public double NetzLeistungSlider
        {
            get { return _netzLeistungSlider; }
            set
            {
                _netzLeistungSlider = value;
                OnPropertyChanged("NetzLeistungSlider");
            }
        }

        #endregion

        private SynchronisierungAuswahl _synchAuswahl;

        public SynchronisierungAuswahl SynchAuswahl
        {
            get { return _synchAuswahl; }
            set
            {
                _synchAuswahl = value;
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