namespace LAP_2010_4_Abfuellanlage.ViewModel
{
    using BehaelterSteuerung.Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public VisuAnzeigen ViAnzeige { get; set; }
        public readonly LAP_2010_4_Abfuellanlage.Model.AbfuellAnlage abfuellAnlage;

        public ViewModel(MainWindow mainWindow)
        {
            abfuellAnlage = new Model.AbfuellAnlage();
            ViAnzeige = new VisuAnzeigen(mainWindow, abfuellAnlage);
        }

        public Model.AbfuellAnlage AbfuellAnlage => abfuellAnlage;

        #region BtnQ1

        private ICommand _btnQ1;

        public ICommand BtnQ1 => _btnQ1 ?? (_btnQ1 = new RelayCommand(p => ViAnzeige.SetQ1(), p => true));

        #endregion BtnQ1

        #region BtnK1

        private ICommand _btnK1;

        public ICommand BtnK1 => _btnK1 ?? (_btnK1 = new RelayCommand(p => ViAnzeige.SetK1(), p => true));

        #endregion BtnK1

        #region BtnK2

        private ICommand _btnK2;

        public ICommand BtnK2 => _btnK2 ?? (_btnK2 = new RelayCommand(p => ViAnzeige.SetK2(), p => true));

        #endregion BtnK2

        #region BtnReset

        private ICommand _btnReset;

        public ICommand BtnReset => _btnReset ?? (_btnReset = new RelayCommand(p => abfuellAnlage.AllesReset(), p => true));

        #endregion BtnReset

        #region BtnNachuellen

        private ICommand _btnNachuellen;

        public ICommand BtnNachuellen =>
            _btnNachuellen ??
            (_btnNachuellen = new RelayCommand(p => abfuellAnlage.Nachfuellen(), p => true));

        #endregion BtnNachuellen

        #region BtnS1

        private ICommand _btnS1;

        public ICommand BtnS1 => _btnS1 ?? (_btnS1 = new RelayCommand(p => ViAnzeige.SetS1(), p => true));

        #endregion BtnS1

        #region BtnS2

        private ICommand _btnS2;

        public ICommand BtnS2 => _btnS2 ?? (_btnS2 = new RelayCommand(p => ViAnzeige.BtnS2(), p => true));

        #endregion BtnS2
    }
}