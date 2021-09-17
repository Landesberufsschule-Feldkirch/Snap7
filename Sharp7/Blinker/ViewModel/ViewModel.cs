using Blinker.Commands;
using System.Windows.Input;

namespace Blinker.ViewModel
{
    public class ViewModel
    {
        public Model.Blinker Blinker { get; }
        public VisuAnzeigen ViAnz { get; set; }

        public ViewModel(MainWindow mw)
        {
            var mainWindow = mw;

            Blinker = new Model.Blinker();
            ViAnz = new VisuAnzeigen(mainWindow, Blinker);
        }
        private ICommand _btnTaster;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnz.Taster);
    }
}