using LAP_2010_1_Kompressoranlage.Commands;

namespace LAP_2010_1_Kompressoranlage.ViewModel
{
    using System.Windows.Input;

    public class ViewModel
    {
        public Model.Kompressoranlage Kompressoranlage { get; }
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            Kompressoranlage = new Model.Kompressoranlage();
            ViAnzeige = new VisuAnzeigen(mainWindow, Kompressoranlage);
        }


        private ICommand _btnF1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnF1 => _btnF1 ??= new RelayCommand(_ => Kompressoranlage.BtnF1(), _ => true);

        private ICommand _btnS1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS1 => _btnS1 ??= new RelayCommand(_ => ViAnzeige.SetS1(), _ => true);

        private ICommand _btnS2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS2 => _btnS2 ??= new RelayCommand(_ => ViAnzeige.BtnS2(), _ => true);

        private ICommand _btnB2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnB2 => _btnB2 ??= new RelayCommand(_ => Kompressoranlage.BtnB2(), _ => true);
    }
}