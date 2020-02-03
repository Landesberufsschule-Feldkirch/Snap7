namespace BehaelterSteuerung.ViewModel
{
    using BehaelterSteuerung.Command;
    using System.Windows.Input;

    public class BehaelterViewModel
    {
        public readonly Model.AlleBehaelter _alleBehaelter;
        public BehaelterViewModel()
        {
            _alleBehaelter = new Model.AlleBehaelter();

            BtnVentilQ2 = new BehaltersteuerungUpdateVentilQ2(this);
            BtnVentilQ4 = new BehaltersteuerungUpdateVentilQ4(this);
            BtnVentilQ6 = new BehaltersteuerungUpdateVentilQ6(this);
            BtnVentilQ8 = new BehaltersteuerungUpdateVentilQ8(this);

            BtnAutomatik1234 = new BehaltersteuerungUpdateAutomatik1234(this);
            BtnAutomatik1324 = new BehaltersteuerungUpdateAutomatik1324(this);
            BtnAutomatik1432 = new BehaltersteuerungUpdateAutomatik1432(this);
            BtnAutomatik4321 = new BehaltersteuerungUpdateAutomatik4321(this);
        }

        public Model.AlleBehaelter AlleBehaelter { get { return _alleBehaelter; } }


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



        internal void VentilQ2() { _alleBehaelter.VentilQ2(); }
        internal void VentilQ4() { _alleBehaelter.VentilQ4(); }
        internal void VentilQ6() { _alleBehaelter.VentilQ6(); }
        internal void VentilQ8() { _alleBehaelter.VentilQ8(); }
        internal void Automatik1234() { _alleBehaelter.Automatik1234(); }
        internal void Automatik1324() { _alleBehaelter.Automatik1324(); }
        internal void Automatik1432() { _alleBehaelter.Automatik1432(); }
        internal void Automatik4321() { _alleBehaelter.Automatik4321(); }
    }
}
