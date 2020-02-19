namespace LAP_2018_Abfuellanlage.ViewModel
{
    using LAP_2018_Abfuellanlage.Commands;
    using System;
    using System.Windows.Input;

    public class AbfuellanlageViewModel
    {

        public readonly Model.AlleFlaschen alleFlaschen;

        public AbfuellanlageViewModel(MainWindow mainWindow)
        {
            alleFlaschen = new Model.AlleFlaschen(mainWindow);

            BtnNachfuellen = new AbfuellanlageBtnNachfuellen(this);
            BtnTasterF5 = new AbfuellanlageBtnTasterF5(this);
            BtnTasterS1 = new AbfuellanlageBtnTasterS1(this);
            BtnTasterS2 = new AbfuellanlageBtnTasterS2(this);
            BtnTasterS3 = new AbfuellanlageBtnTasterS3(this);
            BtnTasterS4 = new AbfuellanlageBtnTasterS4(this);
            SetManualK1 = new AbfuellanlageSetManualK1(this);
            SetManualK2 = new AbfuellanlageSetManualK2(this);
            SetManualM1 = new AbfuellanlageSetManualM1(this);
        }


        public Model.AlleFlaschen AlleFlaschen { get { return alleFlaschen; } }


        public bool CanButtonNachfuellen { get { return true; } }
        public bool CanButtonF5 { get { return true; } }
        public bool CanButtonS1 { get { return true; } }
        public bool CanButtonS2 { get { return true; } }
        public bool CanButtonS3 { get { return true; } }
        public bool CanButtonS4 { get { return true; } }
        public bool CanSetManualK1 { get { return true; } }
        public bool CanSetManualK2 { get { return true; } }
        public bool CanSetManualM1 { get { return true; } }



        public ICommand BtnNachfuellen { get; private set; }
        public ICommand BtnTasterF5 { get; private set; }
        public ICommand BtnTasterS1 { get; private set; }
        public ICommand BtnTasterS2 { get; private set; }
        public ICommand BtnTasterS3 { get; private set; }
        public ICommand BtnTasterS4 { get; private set; }
        public ICommand SetManualK1 { get; private set; }
        public ICommand SetManualK2 { get; private set; }
        public ICommand SetManualM1 { get; private set; }


        internal void TasterNachfuellen() { alleFlaschen.TasterNachfuellen(); }
        internal void TasterF5() { alleFlaschen.TasterF5(); }
        internal void TasterS1() { alleFlaschen.TasterS1(); }
        internal void TasterS2() { alleFlaschen.TasterS2(); }
        internal void TasterS3() { alleFlaschen.TasterS3(); }
        internal void TasterS4() { alleFlaschen.TasterS4(); }
        internal void TasterK1() { alleFlaschen.SetManualK1(); }
        internal void TasterK2() { alleFlaschen.SetManualK2(); }
        internal void TasterM1() { alleFlaschen.SetManualM1(); }

    }
}
