using Schleifmaschine.Commands;
using System.Windows.Input;

namespace Schleifmaschine.ViewModel
{
    public class ViewModel
    {
        public Model.Schleifmaschine Schleifmaschine { get; }
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            Schleifmaschine = new Model.Schleifmaschine();
            ViAnzeige = new VisuAnzeigen(mainWindow, Schleifmaschine);
        }

        private ICommand _btnTaster;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnzeige.Taster);
    }
}