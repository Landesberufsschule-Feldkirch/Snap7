using LaborGetriebemotor.Commands;
using LaborGetriebemotor.Model;
using System.Windows.Input;
using TestAutomat.AutoTesterViewModel;

namespace LaborGetriebemotor.ViewModel
{
    public class ViewModel
    {
        public Getriebemotor Getriebemotor { get; }
        public VisuAnzeigen ViAnzeige { get; set; }
        public AutoTesterViewModel AutoTesterViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            Getriebemotor = new Getriebemotor();
            ViAnzeige = new VisuAnzeigen(mainWindow, Getriebemotor);
            AutoTesterViAnzeige = new AutoTesterViewModel();
        }

        private ICommand _btnTaster;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnzeige.Taster);
    }
}