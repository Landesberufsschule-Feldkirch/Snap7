namespace LAP_2010_1_Kompressoranlage.ViewModel
{
    using BehaelterSteuerung.Commands;
    using BehaelterSteuerung.ViewModel;
    using System.Windows.Input;

    public class KompressoranlageViewModel
    {

        public VisuAnzeigen ViAnzeige { get; set; }
        public readonly LAP_2010_1_Kompressoranlage.Model.Kompressoranlage kompressoranlage;

        public KompressoranlageViewModel(MainWindow mainWindow)
        {
            kompressoranlage = new Model.Kompressoranlage();
            ViAnzeige = new VisuAnzeigen(mainWindow, kompressoranlage);
        }

        public Model.Kompressoranlage Kompressoranlage { get { return kompressoranlage; } }



        #region BtnF5
        private ICommand _btnF5;
        public ICommand BtnF5
        {
            get
            {
                if (_btnF5 == null)
                {
                    _btnF5 = new RelayCommand(p => kompressoranlage.BtnF5(), p => true);
                }
                return _btnF5;
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
        
        #region BtnS7
        private ICommand _btnS7;
        public ICommand BtnS7
        {
            get
            {
                if (_btnS7 == null)
                {
                    _btnS7 = new RelayCommand(p => kompressoranlage.BtnS7(), p => true);
                }
                return _btnS7;
            }
        }
        #endregion

    }
}
