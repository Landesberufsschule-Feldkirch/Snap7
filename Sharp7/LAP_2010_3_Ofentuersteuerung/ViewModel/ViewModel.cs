namespace LAP_2010_3_Ofentuersteuerung.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public Model.OfentuerSteuerung OfentuerSteuerung { get; }
        public VisuAnzeigen ViAnz { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            OfentuerSteuerung = new Model.OfentuerSteuerung();
            ViAnz = new VisuAnzeigen(mainWindow, OfentuerSteuerung);
        }

        private ICommand _btnTaster;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnz.Taster);
    }
}