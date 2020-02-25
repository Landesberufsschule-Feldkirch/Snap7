namespace LAP_2019_Foerderanlage.ViewModel
{
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;
    using Utilities;

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private const int MaterialSiloHoehe = 8 * 35;

        private readonly MainWindow mainWindow;
        public readonly Model.Foerderanlage foerderanlage;

        public VisuAnzeigen(MainWindow mw, Model.Foerderanlage fa)
        {
            mainWindow = mw;
            foerderanlage = fa;

            Margin1 = new System.Windows.Thickness(0, MaterialSiloHoehe * 0.1, 0, 0);

            VisibilityM1Ein = "Hidden";
            VisibilityM2Ein = "Hidden";

            VisibilityY1Ein = "Hidden";
            VisibilityY1Aus = "Visible";

            VisibilityS7Ein = "Visible";
            VisibilityS7Aus = "Hidden";

            VisibilityS8Ein = "Visible";
            VisibilityS8Aus = "Hidden";

            VisibilityPfeilLinkslauf = "Hidden";
            VisibilityPfeilRechtslauf = "Hidden";

            PosWagenBeschriftungLeft = 74;
            PosWagenBeschriftungTop = 106;

            PosWagenLeft = 0;
            PosWagenTop = 0;

            PosWagenInhaltLeft = 12;
            PosWagenInhaltTop = 10;

            WagenFuellstand = 88;

            System.Threading.Tasks.Task.Run(() => VisuAnzeigenTask());
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                FuellstandSilo(foerderanlage.Silo.GetFuellstand());

                SichtbarkeitPfeilLinkslauf(foerderanlage.Q4_LL);
                SichtbarkeitPfeilRechtslauf(foerderanlage.Q3_RL);

                SichtbarkeitM1(foerderanlage.Q3_RL || foerderanlage.Q4_LL);
                SichtbarkeitM2(foerderanlage.XFU);

                SichtbarkeitY1(foerderanlage.Y1);

                SichtbarkeitS7(foerderanlage.Wagen.IstWagenRechts());
                SichtbarkeitS8(foerderanlage.Wagen.IstWagenVoll());

                PositionWagenBeschriftung(foerderanlage.Wagen.GetPosition());
                PositionWagen(foerderanlage.Wagen.GetPosition());
                PositionWagenInhalt(foerderanlage.Wagen.GetPosition(), foerderanlage.Wagen.GetFuellstand());
                WagenFuellstand = foerderanlage.Wagen.GetFuellstand();

                Thread.Sleep(10);
            }
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


        #region FuellstandSilo
        public void FuellstandSilo(double pegel)
        {
            Margin1 = new System.Windows.Thickness(0, MaterialSiloHoehe * (1 - pegel), 0, 0);
        }

        private Thickness _margin1;
        public Thickness Margin1
        {
            get { return _margin1; }
            set
            {
                _margin1 = value;
                OnPropertyChanged("Margin1");
            }
        }
        #endregion


        #region Sichtbarkeit PfeilLinkslauf
        public void SichtbarkeitPfeilLinkslauf(bool val)
        {
            if (val)
            {
                VisibilityPfeilLinkslauf = "Visible";
            }
            else
            {
                VisibilityPfeilLinkslauf = "Hidden";
            }
        }

        private string _visibilityPfeilLinkslauf;
        public string VisibilityPfeilLinkslauf
        {
            get { return _visibilityPfeilLinkslauf; }
            set
            {
                _visibilityPfeilLinkslauf = value;
                OnPropertyChanged("VisibilityPfeilLinkslauf");
            }
        }
        #endregion

        #region Sichtbarkeit PfeilRechtslauf
        public void SichtbarkeitPfeilRechtslauf(bool val)
        {
            if (val)
            {
                VisibilityPfeilRechtslauf = "Visible";
            }
            else
            {
                VisibilityPfeilRechtslauf = "Hidden";
            }
        }

        private string _visibilityPfeilRechtslauf;
        public string VisibilityPfeilRechtslauf
        {
            get { return _visibilityPfeilRechtslauf; }
            set
            {
                _visibilityPfeilRechtslauf = value;
                OnPropertyChanged("VisibilityPfeilRechtslauf");
            }
        }
        #endregion


        #region Sichtbarkeit M1
        public void SichtbarkeitM1(bool val)
        {
            if (val)
            {
                VisibilityM1Ein = "Visible";
            }
            else
            {
                VisibilityM1Ein = "Hidden";
            }
        }

        private string _visibilityM1Ein;
        public string VisibilityM1Ein
        {
            get { return _visibilityM1Ein; }
            set
            {
                _visibilityM1Ein = value;
                OnPropertyChanged("VisibilityM1Ein");
            }
        }
        #endregion

        #region Sichtbarkeit M2
        public void SichtbarkeitM2(bool val)
        {
            if (val)
            {
                VisibilityM2Ein = "Visible";
            }
            else
            {
                VisibilityM2Ein = "Hidden";
            }
        }

        private string _visibilityM2Ein;
        public string VisibilityM2Ein
        {
            get { return _visibilityM2Ein; }
            set
            {
                _visibilityM2Ein = value;
                OnPropertyChanged("VisibilityM2Ein");
            }
        }
        #endregion



        #region Sichtbarkeit Y1
        public void SichtbarkeitY1(bool val)
        {
            if (val)
            {
                VisibilityY1Ein = "Visible";
                VisibilityY1Aus = "Hidden";
            }
            else
            {
                VisibilityY1Ein = "Hidden";
                VisibilityY1Aus = "Visible";
            }
        }

        private string _visibilityY1Ein;
        public string VisibilityY1Ein
        {
            get { return _visibilityY1Ein; }
            set
            {
                _visibilityY1Ein = value;
                OnPropertyChanged("VisibilityY1Ein");
            }
        }

        private string _visibilityY1Aus;

        public string VisibilityY1Aus
        {
            get { return _visibilityY1Aus; }
            set
            {
                _visibilityY1Aus = value;
                OnPropertyChanged("VisibilityY1Aus");
            }
        }
        #endregion

        #region Sichtbarkeit S7
        public void SichtbarkeitS7(bool val)
        {
            if (val)
            {
                VisibilityS7Ein = "Visible";
                VisibilityS7Aus = "Hidden";
            }
            else
            {
                VisibilityS7Ein = "Hidden";
                VisibilityS7Aus = "Visible";
            }
        }

        private string _visibilityS7Ein;
        public string VisibilityS7Ein
        {
            get { return _visibilityS7Ein; }
            set
            {
                _visibilityS7Ein = value;
                OnPropertyChanged("VisibilityS7Ein");
            }
        }

        private string _visibilityS7Aus;

        public string VisibilityS7Aus
        {
            get { return _visibilityS7Aus; }
            set
            {
                _visibilityS7Aus = value;
                OnPropertyChanged("VisibilityS7Aus");
            }
        }
        #endregion

        #region Sichtbarkeit S8
        public void SichtbarkeitS8(bool val)
        {
            if (val)
            {
                VisibilityS8Ein = "Visible";
                VisibilityS8Aus = "Hidden";
            }
            else
            {
                VisibilityS8Ein = "Hidden";
                VisibilityS8Aus = "Visible";
            }
        }

        private string _visibilityS8Ein;
        public string VisibilityS8Ein
        {
            get { return _visibilityS8Ein; }
            set
            {
                _visibilityS8Ein = value;
                OnPropertyChanged("VisibilityS8Ein");
            }
        }

        private string _visibilityS8Aus;

        public string VisibilityS8Aus
        {
            get { return _visibilityS8Aus; }
            set
            {
                _visibilityS8Aus = value;
                OnPropertyChanged("VisibilityS8Aus");
            }
        }
        #endregion



        #region PositionWagenBeschriftung
        public void PositionWagenBeschriftung(Punkt pos)
        {
            PosWagenBeschriftungLeft = pos.X + 74;
            PosWagenBeschriftungTop = pos.Y + 106;
        }

        private double _posWagenBeschriftungLeft;
        public double PosWagenBeschriftungLeft
        {
            get { return _posWagenBeschriftungLeft; }
            set
            {
                _posWagenBeschriftungLeft = value;
                OnPropertyChanged("PosWagenBeschriftungLeft");
            }
        }
        private double _posWagenBeschriftungTop;
        public double PosWagenBeschriftungTop
        {
            get { return _posWagenBeschriftungTop; }
            set
            {
                _posWagenBeschriftungTop = value;
                OnPropertyChanged("PosWagenBeschriftungTop");
            }
        }
        #endregion

        #region PositionWagen
        public void PositionWagen(Punkt pos)
        {
            PosWagenLeft = pos.X;
            PosWagenTop = pos.Y;
        }

        private double _posWagenLeft;
        public double PosWagenLeft
        {
            get { return _posWagenLeft; }
            set
            {
                _posWagenLeft = value;
                OnPropertyChanged("PosWagenLeft");
            }
        }
        private double _posWagenTop;
        public double PosWagenTop
        {
            get { return _posWagenTop; }
            set
            {
                _posWagenTop = value;
                OnPropertyChanged("PosWagenTop");
            }
        }
        #endregion

        #region PositionWagenInhalt
        public void PositionWagenInhalt(Punkt pos, double fuellstand)
        {
            PosWagenInhaltLeft = pos.X + 12;
            PosWagenInhaltTop = pos.Y + 10 + 88 - fuellstand;
        }

        private double _posWagenInhaltLeft;
        public double PosWagenInhaltLeft
        {
            get { return _posWagenInhaltLeft; }
            set
            {
                _posWagenInhaltLeft = value;
                OnPropertyChanged("PosWagenInhaltLeft");
            }
        }
        private double _posWagenInhaltTop;
        public double PosWagenInhaltTop
        {
            get { return _posWagenInhaltTop; }
            set
            {
                _posWagenInhaltTop = value;
                OnPropertyChanged("PosWagenInhaltTop");
            }
        }
        #endregion

        #region WagenFuellstand
        private double _wagenFuellstand;
        public double WagenFuellstand
        {
            get { return _wagenFuellstand; }
            set
            {
                _wagenFuellstand = value;
                OnPropertyChanged("WagenFuellstand");
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
