using LaborLinearantrieb.Commands;
using LaborLinearantrieb.Model;
using System.Windows.Input;

namespace LaborLinearantrieb.ViewModel
{
    public class ViewModel
    {
        public Linearantrieb Linearantrieb { get; }
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            Linearantrieb = new Linearantrieb();
            ViAnzeige = new VisuAnzeigen(mainWindow, Linearantrieb);
        }


        private ICommand _btnTaster;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnzeige.Taster);

    }
}