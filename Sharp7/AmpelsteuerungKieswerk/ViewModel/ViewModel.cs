namespace AmpelsteuerungKieswerk.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        private readonly Model.AlleLastKraftWagen _alleLastKraftWagen;
        public Model.AlleLastKraftWagen AlleLastKraftWagen => _alleLastKraftWagen;
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            _alleLastKraftWagen = new Model.AlleLastKraftWagen();
            ViAnzeige = new VisuAnzeigen(mainWindow, _alleLastKraftWagen);
        }



        private ICommand _btnLkw1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnLkw1 => _btnLkw1 ??= new RelayCommand(p => _alleLastKraftWagen.TasterLkw1(), p => true);

        private ICommand _btnLkw2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnLkw2 => _btnLkw2 ??= new RelayCommand(p => _alleLastKraftWagen.TasterLkw2(), p => true);

        private ICommand _btnLkw3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnLkw3 => _btnLkw3 ??= new RelayCommand(p => _alleLastKraftWagen.TasterLkw3(), p => true);

        private ICommand _btnLkw4;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnLkw4 => _btnLkw4 ??= new RelayCommand(p => _alleLastKraftWagen.TasterLkw4(), p => true);

        private ICommand _btnLkw5;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnLkw5 => _btnLkw5 ??= new RelayCommand(p => _alleLastKraftWagen.TasterLkw5(), p => true);

        private ICommand _btnLinksParken;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnLinksParken =>
            _btnLinksParken ??= new RelayCommand(p => _alleLastKraftWagen.TasterLinksParken(), p => true);

        private ICommand _btnRechtsParken;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnRechtsParken =>
            _btnRechtsParken ??= new RelayCommand(p => _alleLastKraftWagen.TasterRechtsParken(), p => true);
    }
}