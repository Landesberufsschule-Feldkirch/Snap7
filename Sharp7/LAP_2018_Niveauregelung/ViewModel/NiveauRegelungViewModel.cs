namespace LAP_2018_Niveauregelung.ViewModel
{
    using LAP_2018_Niveauregelung.Commands;
    using System;
    using System.Windows.Input;

    public class NiveauRegelungViewModel
    {

        public readonly Model.NiveauRegelung niveauRegelung;


        public NiveauRegelungViewModel(MainWindow mainWindow)
        {

            niveauRegelung = new Model.NiveauRegelung(mainWindow);

            BtnTasterS1 = new NiveauRegelungTasterS1(this);
            BtnTasterS2 = new NiveauRegelungTasterS2(this);
            BtnTasterS3 = new NiveauRegelungTasterS3(this);
            BtnThermorelaisF1 = new NiveauRegelungThermorelaisF1(this);
            BtnThermorelaisF2 = new NiveauRegelungThermorelaisF2(this);
            BtnSetManualM1 = new NiveauRegelungSetManualM1(this);
            BtnSetManualM2 = new NiveauRegelungSetManualM2(this);
            BtnVentilY1 = new NiveauRegelungVentilY1(this);
        }



        public Model.NiveauRegelung NiveauRegelung { get { return niveauRegelung; } }



        public ICommand BtnTasterS1 { get; private set; }
        public ICommand BtnTasterS2 { get; private set; }
        public ICommand BtnTasterS3 { get; private set; }
        public ICommand BtnThermorelaisF1 { get; private set; }
        public ICommand BtnThermorelaisF2 { get; private set; }
        public ICommand BtnSetManualM1 { get; private set; }
        public ICommand BtnSetManualM2 { get; private set; }
        public ICommand BtnVentilY1 { get; private set; }


        public bool CanButtonS1 { get { return true; } }
        public bool CanButtonS2 { get { return true; } }
        public bool CanButtonS3 { get { return true; } }
        public bool CanThermorelaisF1 { get { return true; } }
        public bool CanThermorelaisF2 { get { return true; } }
        public bool CanSetManualM1 { get { return true; } }
        public bool CanSetManualM2 { get { return true; } }
        public bool CanVentilY1 { get { return true; } }


        internal void TasterS1() { niveauRegelung.TasterS1(); }
        internal void TasterS2() { niveauRegelung.TasterS2(); }
        internal void TasterS3() { niveauRegelung.TasterS3(); }
        internal void ThermorelaisF1() { niveauRegelung.ThermorelaisF1(); }
        internal void ThermorelaisF2() { niveauRegelung.ThermorelaisF2(); }
        internal void VentilY1() { niveauRegelung.VentilY1(); }
        internal void SetManualM1() { niveauRegelung.SetManualM1(); }
        internal void SetManualM2() { niveauRegelung.SetManualM2(); }

    }
}
