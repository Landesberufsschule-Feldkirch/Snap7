using LaborGetriebemotor.Commands;
using LaborGetriebemotor.Model;
using System.Windows.Input;

namespace LaborGetriebemotor.ViewModel
{
    public class ViewModel
    {
        public Getriebemotor Getriebemotor { get; }
        public VisuAnzeigen ViAnz { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            Getriebemotor = new Getriebemotor();
            ViAnz = new VisuAnzeigen(mainWindow, Getriebemotor);
        }

        private ICommand _btnTaster;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnz.Taster);

        private ICommand _btnSchalter;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnSchalter => _btnSchalter ??= new RelayCommand(ViAnz.Schalter);
    }
}