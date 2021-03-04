using LaborLinearachse.Commands;
using LaborLinearachse.Model;
using System.Windows.Input;

namespace LaborLinearachse.ViewModel
{
    public class ViewModel
    {
        public Linearachse Linearachse { get; }
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            Linearachse = new Linearachse();
            ViAnzeige = new VisuAnzeigen(mainWindow, Linearachse);
        }


        private ICommand _btnTaster;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnzeige.Taster);

    }
}