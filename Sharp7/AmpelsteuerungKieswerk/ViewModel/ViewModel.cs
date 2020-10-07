namespace AmpelsteuerungKieswerk.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public Model.AlleLastKraftWagen AlleLastKraftWagen { get; }

        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            AlleLastKraftWagen = new Model.AlleLastKraftWagen();
            ViAnzeige = new VisuAnzeigen(mainWindow, AlleLastKraftWagen);
        }



        private ICommand _btnLkw1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnLkw1 => _btnLkw1 ??= new RelayCommand(p => AlleLastKraftWagen.TasterLkw1(), p => true);

        private ICommand _btnLkw2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnLkw2 => _btnLkw2 ??= new RelayCommand(p => AlleLastKraftWagen.TasterLkw2(), p => true);

        private ICommand _btnLkw3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnLkw3 => _btnLkw3 ??= new RelayCommand(p => AlleLastKraftWagen.TasterLkw3(), p => true);

        private ICommand _btnLkw4;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnLkw4 => _btnLkw4 ??= new RelayCommand(p => AlleLastKraftWagen.TasterLkw4(), p => true);

        private ICommand _btnLkw5;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnLkw5 => _btnLkw5 ??= new RelayCommand(p => AlleLastKraftWagen.TasterLkw5(), p => true);

        private ICommand _btnLinksParken;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnLinksParken =>
            _btnLinksParken ??= new RelayCommand(p => AlleLastKraftWagen.TasterLinksParken(), p => true);

        private ICommand _btnRechtsParken;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnRechtsParken =>
            _btnRechtsParken ??= new RelayCommand(p => AlleLastKraftWagen.TasterRechtsParken(), p => true);
    }
}