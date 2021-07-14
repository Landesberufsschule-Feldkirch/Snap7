using PaternosterLager.Commands;
using System.Windows.Input;

namespace PaternosterLager.ViewModel
{
    public class ViewModel
    {
        public Model.Paternosterlager Paternosterlager { get; }
        public VisuAnzeigen ViAnz { get; set; }
        public ViewModel(MainWindow mainWindow, double anzahlKisten)
        {
            Paternosterlager = new Model.Paternosterlager(anzahlKisten);
            ViAnz = new VisuAnzeigen(mainWindow, Paternosterlager, anzahlKisten);
        }

        private ICommand _btnTaster;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnz.Taster);
    }
}