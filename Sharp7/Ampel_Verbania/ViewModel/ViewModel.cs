using Ampel_Verbania.Commands;
using System.Windows.Input;

namespace Ampel_Verbania.ViewModel
{
    public class ViewModel
    {
        public Model.AmpelVerbania Kata { get; }
        public VisuAnzeigen ViAnz { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            Kata = new Model.AmpelVerbania();
            ViAnz = new VisuAnzeigen(mainWindow, Kata);
        }

        private ICommand _btnTaster;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnz.Taster);
    }
}