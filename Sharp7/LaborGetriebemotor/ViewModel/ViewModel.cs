using System.Windows.Input;
using LaborGetriebemotor.Commands;
using LaborGetriebemotor.Model;

namespace LaborGetriebemotor.ViewModel
{
    public class ViewModel
    {
        public Getriebemotor Getriebemotor { get; }
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            Getriebemotor = new Getriebemotor();
            ViAnzeige = new VisuAnzeigen(mainWindow, Getriebemotor);
        }


        private ICommand _btnTaster;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnzeige.Taster);

    }
}