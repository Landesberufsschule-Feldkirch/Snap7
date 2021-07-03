using LAP_2018_1_Silosteuerung.Commands;
using System.Windows.Input;

namespace LAP_2018_1_Silosteuerung.ViewModel
{
    public class ViewModel
    {
        public Model.Silosteuerung Silosteuerung { get; }
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            Silosteuerung = new Model.Silosteuerung();
            ViAnzeige = new VisuAnzeigen(mainWindow, Silosteuerung);
        }

        private ICommand _btnTaster;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnzeige.Taster);

        private ICommand _btnSchalter;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnSchalter => _btnSchalter ??= new RelayCommand(ViAnzeige.Schalter);
    }
}