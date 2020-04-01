namespace LAP_2010_1_Kompressoranlage.ViewModel
{
    using BehaelterSteuerung.Commands;
    using BehaelterSteuerung.ViewModel;
    using System.Windows.Input;

    public class ViewModel
    {

        public VisuAnzeigen ViAnzeige { get; set; }
        public readonly LAP_2010_1_Kompressoranlage.Model.Kompressoranlage kompressoranlage;

        public ViewModel(MainWindow mainWindow)
        {
            kompressoranlage = new Model.Kompressoranlage();
            ViAnzeige = new VisuAnzeigen(mainWindow, kompressoranlage);
        }

        public Model.Kompressoranlage Kompressoranlage { get { return kompressoranlage; } }



        #region BtnF1
        private ICommand _btnF1;
        public ICommand BtnF1
        {
            get
            {
                if (_btnF1 == null)
                {
                    _btnF1 = new RelayCommand(p => kompressoranlage.BtnF1(), p => true);
                }
                return _btnF1;
            }
        }
        #endregion

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
        #endregion

        #region BtnS2
        private ICommand _btnS2;
        public ICommand BtnS2
        {
            get
            {
                if (_btnS2 == null)
                {
                    _btnS2 = new RelayCommand(p => ViAnzeige.BtnS2(), p => true);
                }
                return _btnS2;
            }
        }
        #endregion

        #region BtnB1
        private ICommand _btnB1;
        public ICommand BtnB1
        {
            get
            {
                if (_btnB1 == null)
                {
                    _btnB1 = new RelayCommand(p => kompressoranlage.BtnB1(), p => true);
                }
                return _btnB1;
            }
        }
        #endregion
    }
}
