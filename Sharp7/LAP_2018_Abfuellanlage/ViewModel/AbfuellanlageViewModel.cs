namespace LAP_2018_Abfuellanlage.ViewModel
{
    using LAP_2018_Abfuellanlage.Commands;
    using System.Windows.Input;

    class AbfuellanlageViewModel
    {

        public readonly Model.AlleFlaschen alleFlaschen;

        public AbfuellanlageViewModel(MainWindow mainWindow)
        {
            alleFlaschen = new Model.AlleFlaschen(mainWindow);

            BtnTasterF5 = new AbfuellanlageBtnTasterF5(this);
            BtnTasterS1 = new AbfuellanlageBtnTasterS1(this);
            BtnTasterS2 = new AbfuellanlageBtnTasterS2(this);
            BtnTasterS3 = new AbfuellanlageBtnTasterS3(this);
            BtnTasterS4 = new AbfuellanlageBtnTasterS4(this);
        }


        public Model.AlleFlaschen AlleFlaschen { get { return alleFlaschen; } }

        public bool CanButtonF5 { get { return true; } }
        public bool CanButtonS1 { get { return true; } }
        public bool CanButtonS2 { get { return true; } }
        public bool CanButtonS3 { get { return true; } }
        public bool CanButtonS4 { get { return true; } }

        public ICommand BtnTasterF5 { get; private set; }
        public ICommand BtnTasterS1 { get; private set; }
        public ICommand BtnTasterS2 { get; private set; }
        public ICommand BtnTasterS3 { get; private set; }
        public ICommand BtnTasterS4 { get; private set; }


        internal void TasterF5() { alleFlaschen.TasterF5(); }
        internal void TasterS1() { alleFlaschen.TasterS1(); }
        internal void TasterS2() { alleFlaschen.TasterS2(); }
        internal void TasterS3() { alleFlaschen.TasterS3(); }
        internal void TasterS4() { alleFlaschen.TasterS5(); }
    }
}
