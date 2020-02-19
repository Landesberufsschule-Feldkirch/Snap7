namespace BehaelterSteuerung.ViewModel
{
    using BehaelterSteuerung.Command;
    using System.Windows.Input;

    public class BehaelterViewModel
    {
        public readonly Model.AlleBehaelter alleBehaelter;
        public BehaelterViewModel(MainWindow mw)
        {
            alleBehaelter = new Model.AlleBehaelter(mw);

            BtnVentilQ2 = new BehaltersteuerungBtnVentilQ2(this);
            BtnVentilQ4 = new BehaltersteuerungBtnVentilQ4(this);
            BtnVentilQ6 = new BehaltersteuerungBtnVentilQ6(this);
            BtnVentilQ8 = new BehaltersteuerungBtnVentilQ8(this);

            BtnAutomatik1234 = new BehaltersteuerungBtnAutomatik1234(this);
            BtnAutomatik1324 = new BehaltersteuerungBtnAutomatik1324(this);
            BtnAutomatik1432 = new BehaltersteuerungBtnAutomatik1432(this);
            BtnAutomatik4321 = new BehaltersteuerungBtnAutomatik4321(this);
        }

        public Model.AlleBehaelter AlleBehaelter { get { return alleBehaelter; } }


        public bool CanUpdateVentilQ2 { get { return true; } }
        public bool CanUpdateVentilQ4 { get { return true; } }
        public bool CanUpdateVentilQ6 { get { return true; } }
        public bool CanUpdateVentilQ8 { get { return true; } }
        public bool CanUpdateAutomatik1234 { get { return true; } }
        public bool CanUpdateAutomatik1324 { get { return true; } }
        public bool CanUpdateAutomatik1432 { get { return true; } }
        public bool CanUpdateAutomatik4321 { get { return true; } }

        

        public ICommand BtnVentilQ2 { get; private set; }
        public ICommand BtnVentilQ4 { get; private set; }
        public ICommand BtnVentilQ6 { get; private set; }
        public ICommand BtnVentilQ8 { get; private set; }
        public ICommand BtnAutomatik1234 { get; private set; }
        public ICommand BtnAutomatik1324 { get; private set; }
        public ICommand BtnAutomatik1432 { get; private set; }
        public ICommand BtnAutomatik4321 { get; private set; }



        internal void VentilQ2() { alleBehaelter.VentilQ2(); }
        internal void VentilQ4() { alleBehaelter.VentilQ4(); }
        internal void VentilQ6() { alleBehaelter.VentilQ6(); }
        internal void VentilQ8() { alleBehaelter.VentilQ8(); }
        internal void Automatik1234() { alleBehaelter.Automatik1234(); }
        internal void Automatik1324() { alleBehaelter.Automatik1324(); }
        internal void Automatik1432() { alleBehaelter.Automatik1432(); }
        internal void Automatik4321() { alleBehaelter.Automatik4321(); }
    }
}
