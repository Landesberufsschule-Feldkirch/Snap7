namespace Parkhaus.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public Model.Parkhaus Parkhaus { get; }
        public VisuAnzeigen ViAnz { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            Parkhaus = new Model.Parkhaus();
            ViAnz = new VisuAnzeigen(mainWindow, Parkhaus);
        }

        private ICommand _btnTaster;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnz.Taster);
    }
}