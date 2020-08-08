namespace AmpelsteuerungKieswerk.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public readonly Model.AlleLastKraftWagen alleLastKraftWagen;
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            alleLastKraftWagen = new Model.AlleLastKraftWagen();
            ViAnzeige = new VisuAnzeigen(mainWindow, alleLastKraftWagen);
        }

        // ReSharper disable once UnusedMember.Global
        public Model.AlleLastKraftWagen AlleLastKraftWagen => alleLastKraftWagen;

        #region BtnLkw1

        private ICommand _btnLkw1;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnLkw1 => _btnLkw1 ?? (_btnLkw1 = new RelayCommand(p => alleLastKraftWagen.TasterLkw1(), p => true));

        #endregion BtnLkw1

        #region BtnLkw2

        private ICommand _btnLkw2;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnLkw2 => _btnLkw2 ?? (_btnLkw2 = new RelayCommand(p => alleLastKraftWagen.TasterLkw2(), p => true));

        #endregion BtnLkw2

        #region BtnLkw3

        private ICommand _btnLkw3;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnLkw3 => _btnLkw3 ?? (_btnLkw3 = new RelayCommand(p => alleLastKraftWagen.TasterLkw3(), p => true));

        #endregion BtnLkw3

        #region BtnLkw4

        private ICommand _btnLkw4;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnLkw4 => _btnLkw4 ?? (_btnLkw4 = new RelayCommand(p => alleLastKraftWagen.TasterLkw4(), p => true));

        #endregion BtnLkw4

        #region BtnLkw5

        private ICommand _btnLkw5;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnLkw5 => _btnLkw5 ?? (_btnLkw5 = new RelayCommand(p => alleLastKraftWagen.TasterLkw5(), p => true));

        #endregion BtnLkw5

        #region BtnLinksParken

        private ICommand _btnLinksParken;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnLinksParken =>
            _btnLinksParken ?? (_btnLinksParken =
                new RelayCommand(p => alleLastKraftWagen.TasterLinksParken(), p => true));

        #endregion BtnLinksParken

        #region BtnRechtsParken

        private ICommand _btnRechtsParken;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnRechtsParken =>
            _btnRechtsParken ?? (_btnRechtsParken =
                new RelayCommand(p => alleLastKraftWagen.TasterRechtsParken(), p => true));

        #endregion BtnRechtsParken
    }
}