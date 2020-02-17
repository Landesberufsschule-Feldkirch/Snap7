namespace AmpelsteuerungKieswerk.ViewModel
{
    using AmpelsteuerungKieswerk.Commands;
    using System.Windows.Input;

    public class AmpelsteuerungKieswerkViewModel
    {

        public readonly Model.AlleLastKraftWagen alleLastKraftWagen;

        public AmpelsteuerungKieswerkViewModel(MainWindow mainWindow)
        {
            alleLastKraftWagen = new Model.AlleLastKraftWagen(mainWindow);

            BtnLkw1 = new AmpelsteuerungBtnLkw1(this);
            BtnLkw2 = new AmpelsteuerungBtnLkw2(this);
            BtnLkw3 = new AmpelsteuerungBtnLkw3(this);
            BtnLkw4 = new AmpelsteuerungBtnLkw4(this);
            BtnLkw5 = new AmpelsteuerungBtnLkw5(this);
            BtnLinksParken = new AmpelsteuerungBtnLinksParken(this);
            BtnRechtsParken = new AmpelsteuerungBtnRechtsParken(this);
        }

        public Model.AlleLastKraftWagen AlleLastKraftWagen { get { return alleLastKraftWagen; } }

        public bool CanButtonLkw1 { get { return true; } }
        public bool CanButtonLkw2 { get { return true; } }
        public bool CanButtonLkw3 { get { return true; } }
        public bool CanButtonLkw4 { get { return true; } }
        public bool CanButtonLkw5 { get { return true; } }
        public bool CanButtonLinksParken { get { return true; } }
        public bool CanButtonRechtsParken { get { return true; } }


        public ICommand BtnLkw1 { get; private set; }
        public ICommand BtnLkw2 { get; private set; }
        public ICommand BtnLkw3 { get; private set; }
        public ICommand BtnLkw4 { get; private set; }
        public ICommand BtnLkw5 { get; private set; }
        public ICommand BtnLinksParken { get; private set; }
        public ICommand BtnRechtsParken { get; private set; }

        internal void TasterLkw1() { alleLastKraftWagen.TasterLkw1(); }
        internal void TasterLkw2() { alleLastKraftWagen.TasterLkw2(); }
        internal void TasterLkw3() { alleLastKraftWagen.TasterLkw3(); }
        internal void TasterLkw4() { alleLastKraftWagen.TasterLkw4(); }
        internal void TasterLkw5() { alleLastKraftWagen.TasterLkw5(); }
        internal void TasterLinksParken() { alleLastKraftWagen.TasterLinksParken(); }
        internal void TasterRechtsParken() { alleLastKraftWagen.TasterRechtsParken(); }

    }
}
