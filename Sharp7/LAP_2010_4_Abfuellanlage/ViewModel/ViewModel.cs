using LAP_2010_4_Abfuellanlage.Commands;

namespace LAP_2010_4_Abfuellanlage.ViewModel
{
    using System.Windows.Input;

    public class ViewModel
    {
        private readonly Model.AbfuellAnlage _abfuellAnlage;
        public Model.AbfuellAnlage AbfuellAnlage => _abfuellAnlage;
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            _abfuellAnlage = new Model.AbfuellAnlage();
            ViAnzeige = new VisuAnzeigen(mainWindow, _abfuellAnlage);
        }



        private ICommand _btnQ1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnQ1 => _btnQ1 ?? (_btnQ1 = new RelayCommand(p => ViAnzeige.SetQ1(), p => true));

        private ICommand _btnK1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnK1 => _btnK1 ?? (_btnK1 = new RelayCommand(p => ViAnzeige.SetK1(), p => true));

        private ICommand _btnK2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnK2 => _btnK2 ?? (_btnK2 = new RelayCommand(p => ViAnzeige.SetK2(), p => true));

        private ICommand _btnReset;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnReset => _btnReset ?? (_btnReset = new RelayCommand(p => _abfuellAnlage.AllesReset(), p => true));

        private ICommand _btnNachuellen;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnNachuellen =>
            _btnNachuellen ??
            (_btnNachuellen = new RelayCommand(p => _abfuellAnlage.Nachfuellen(), p => true));

        private ICommand _btnS1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS1 => _btnS1 ?? (_btnS1 = new RelayCommand(p => ViAnzeige.SetS1(), p => true));

        private ICommand _btnS2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS2 => _btnS2 ?? (_btnS2 = new RelayCommand(p => ViAnzeige.BtnS2(), p => true));
    }
}