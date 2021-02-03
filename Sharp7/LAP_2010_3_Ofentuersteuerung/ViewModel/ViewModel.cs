namespace LAP_2010_3_Ofentuersteuerung.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public Model.OfentuerSteuerung OfentuerSteuerung { get; }
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            OfentuerSteuerung = new Model.OfentuerSteuerung();
            ViAnzeige = new VisuAnzeigen(mainWindow, OfentuerSteuerung);
        }


        private ICommand _setManualQ1;
        // ReSharper disable once UnusedMember.Global
        public ICommand SetManualQ1 => _setManualQ1 ??= new RelayCommand(_ => ViAnzeige.SetManualQ1(), _ => true);

        private ICommand _setManualQ2;
        // ReSharper disable once UnusedMember.Global
        public ICommand SetManualQ2 => _setManualQ2 ??= new RelayCommand(_ => ViAnzeige.SetManualQ2(), _ => true);

        private ICommand _btnS1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS1 => _btnS1 ??= new RelayCommand(_ => ViAnzeige.SetS1(), _ => true);

        private ICommand _btnS2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS2 => _btnS2 ??= new RelayCommand(_ => ViAnzeige.SetS2(), _ => true);

        private ICommand _btnS3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS3 => _btnS3 ??= new RelayCommand(_ => ViAnzeige.SetS3(), _ => true);

        private ICommand _btnB3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnB3 => _btnB3 ??= new RelayCommand(_ => ViAnzeige.SetB3(), _ => true);
    }
}