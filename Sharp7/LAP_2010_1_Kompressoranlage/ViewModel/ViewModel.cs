using LAP_2010_1_Kompressoranlage.Commands;

namespace LAP_2010_1_Kompressoranlage.ViewModel
{
    using System.Windows.Input;

    public class ViewModel
    {
        public Model.Kompressoranlage Kompressoranlage { get; }
        public VisuAnzeigen ViAnz { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            Kompressoranlage = new Model.Kompressoranlage();
            ViAnz = new VisuAnzeigen(mainWindow, Kompressoranlage);
        }

        private ICommand _btnTaster;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnz.Taster);

        private ICommand _btnSchalter;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnSchalter => _btnSchalter ??= new RelayCommand(ViAnz.Schalter);
    }
}