namespace AmpelsteuerungKieswerk.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public Model.AlleLastKraftWagen AlleLastKraftWagen { get; }

        public VisuAnzeigen ViAnz { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            AlleLastKraftWagen = new Model.AlleLastKraftWagen();
            ViAnz = new VisuAnzeigen(mainWindow, AlleLastKraftWagen);
        }



        private ICommand _btnLkw1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnLkw1 => _btnLkw1 ??= new RelayCommand(_ => AlleLastKraftWagen.TasterLkw1(), _ => true);

        private ICommand _btnLkw2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnLkw2 => _btnLkw2 ??= new RelayCommand(_ => AlleLastKraftWagen.TasterLkw2(), _ => true);

        private ICommand _btnLkw3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnLkw3 => _btnLkw3 ??= new RelayCommand(_ => AlleLastKraftWagen.TasterLkw3(), _ => true);

        private ICommand _btnLkw4;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnLkw4 => _btnLkw4 ??= new RelayCommand(_ => AlleLastKraftWagen.TasterLkw4(), _ => true);

        private ICommand _btnLkw5;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnLkw5 => _btnLkw5 ??= new RelayCommand(_ => AlleLastKraftWagen.TasterLkw5(), _ => true);

        private ICommand _btnLinksParken;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnLinksParken => _btnLinksParken ??= new RelayCommand(_ => AlleLastKraftWagen.TasterLinksParken(), _ => true);

        private ICommand _btnRechtsParken;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnRechtsParken => _btnRechtsParken ??= new RelayCommand(_ => AlleLastKraftWagen.TasterRechtsParken(), _ => true);
    }
}