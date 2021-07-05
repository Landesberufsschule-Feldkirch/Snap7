using LAP_2010_4_Abfuellanlage.Commands;

namespace LAP_2010_4_Abfuellanlage.ViewModel
{
    using System.Windows.Input;

    public class ViewModel
    {
        public Model.AbfuellAnlage AbfuellAnlage { get; }
        public VisuAnzeigen ViAnz { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            AbfuellAnlage = new Model.AbfuellAnlage();
            ViAnz = new VisuAnzeigen(mainWindow, AbfuellAnlage);
        }

        private ICommand _btnTaster;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnz.Taster);
    }
}