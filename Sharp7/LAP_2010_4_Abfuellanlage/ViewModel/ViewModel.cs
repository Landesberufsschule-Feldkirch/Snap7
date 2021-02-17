using LAP_2010_4_Abfuellanlage.Commands;

namespace LAP_2010_4_Abfuellanlage.ViewModel
{
    using System.Windows.Input;

    public class ViewModel
    {
        public Model.AbfuellAnlage AbfuellAnlage { get; }
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            AbfuellAnlage = new Model.AbfuellAnlage();
            ViAnzeige = new VisuAnzeigen(mainWindow, AbfuellAnlage);
        }


        private ICommand _btnQ1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnQ1 => _btnQ1 ??= new RelayCommand(_ => ViAnzeige.SetQ1(), _ => true);

        private ICommand _btnK1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnK1 => _btnK1 ??= new RelayCommand(_ => ViAnzeige.SetK1(), _ => true);

        private ICommand _btnK2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnK2 => _btnK2 ??= new RelayCommand(_ => ViAnzeige.SetK2(), _ => true);

        private ICommand _btnReset;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnReset => _btnReset ??= new RelayCommand(_ => AbfuellAnlage.AllesReset(), _ => true);

        private ICommand _btnNachuellen;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnNachuellen => _btnNachuellen ??= new RelayCommand(_ => AbfuellAnlage.Nachfuellen(), _ => true);

        private ICommand _btnS1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS1 => _btnS1 ??= new RelayCommand(_ => ViAnzeige.SetS1(), _ => true);

        private ICommand _btnS2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS2 => _btnS2 ??= new RelayCommand(_ => ViAnzeige.BtnS2(), _ => true);
    }
}