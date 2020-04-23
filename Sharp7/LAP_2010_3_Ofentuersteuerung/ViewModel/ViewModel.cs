namespace LAP_2010_3_Ofentuersteuerung.ViewModel
{
    using LAP_2010_3_Ofentuersteuerung.Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public VisuAnzeigen ViAnzeige { get; set; }
        public readonly LAP_2010_3_Ofentuersteuerung.Model.OfentuerSteuerung ofentuerSteuerung;

        public ViewModel(MainWindow mainWindow)
        {
            ofentuerSteuerung = new Model.OfentuerSteuerung();
            ViAnzeige = new VisuAnzeigen(mainWindow, ofentuerSteuerung);
        }

        public Model.OfentuerSteuerung OfentuerSteuerung { get { return ofentuerSteuerung; } }

        #region SetManualQ1

        private ICommand _setManualQ1;

        public ICommand SetManualQ1
        {
            get
            {
                if (_setManualQ1 == null)
                {
                    _setManualQ1 = new RelayCommand(p => ViAnzeige.SetManualQ1(), p => true);
                }
                return _setManualQ1;
            }
        }

        #endregion SetManualQ1

        #region SetManualQ2

        private ICommand _setManualQ2;

        public ICommand SetManualQ2
        {
            get
            {
                if (_setManualQ2 == null)
                {
                    _setManualQ2 = new RelayCommand(p => ViAnzeige.SetManualQ2(), p => true);
                }
                return _setManualQ2;
            }
        }

        #endregion SetManualQ2

        #region BtnS1

        private ICommand _btnS1;

        public ICommand BtnS1
        {
            get
            {
                if (_btnS1 == null)
                {
                    _btnS1 = new RelayCommand(p => ViAnzeige.SetS1(), p => true);
                }
                return _btnS1;
            }
        }

        #endregion BtnS1

        #region BtnS2

        private ICommand _btnS2;

        public ICommand BtnS2
        {
            get
            {
                if (_btnS2 == null)
                {
                    _btnS2 = new RelayCommand(p => ViAnzeige.SetS2(), p => true);
                }
                return _btnS2;
            }
        }

        #endregion BtnS2

        #region BtnS3

        private ICommand _btnS3;

        public ICommand BtnS3
        {
            get
            {
                if (_btnS3 == null)
                {
                    _btnS3 = new RelayCommand(p => ViAnzeige.SetS3(), p => true);
                }
                return _btnS3;
            }
        }

        #endregion BtnS3

        #region BtnB3

        private ICommand _btnB3;

        public ICommand BtnB3
        {
            get
            {
                if (_btnB3 == null)
                {
                    _btnB3 = new RelayCommand(p => ViAnzeige.SetB3(), p => true);
                }
                return _btnB3;
            }
        }

        #endregion BtnB3
    }
}