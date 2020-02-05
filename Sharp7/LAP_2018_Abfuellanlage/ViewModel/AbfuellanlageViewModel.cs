namespace LAP_2018_Abfuellanlage.ViewModel
{

    using LAP_2018_Abfuellanlage.Commands;
    using System.Windows.Input;


    class AbfuellanlageViewModel
    {

        public readonly Model.AlleFlaschen alleFlaschen;

        public AbfuellanlageViewModel(MainWindow mainWindow) {

            alleFlaschen = new Model.AlleFlaschen(mainWindow);

            BtnTasterS1 = new AbfuellanlageBtnTaster_S1(this);

        
        
        
        }



        public Model.AlleFlaschen AlleFlaschen { get { return alleFlaschen; } }


        public bool CanButtonS1{get{return true;} }



        public ICommand BtnTasterS1 { get; private set; }



        internal void TasterS1() { alleFlaschen.TasterS1(); }

    }
}
