using BehaelterSteuerung.Commands;
using System.Windows.Input;

namespace BehaelterSteuerung.ViewModel
{
    public class ViewModel
    {
        public Model.AlleBehaelter AlleBehaelter { get; }

        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            AlleBehaelter = new Model.AlleBehaelter();
            ViAnzeige = new VisuAnzeigen(mainWindow, AlleBehaelter);
        }


        private ICommand _btnVentilQ2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnVentilQ2 => _btnVentilQ2 ??= new RelayCommand(p => AlleBehaelter.VentilQ2(), p => true);

        private ICommand _btnVentilQ4;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnVentilQ4 => _btnVentilQ4 ??= new RelayCommand(p => AlleBehaelter.VentilQ4(), p => true);

        private ICommand _btnVentilQ6;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnVentilQ6 => _btnVentilQ6 ??= new RelayCommand(p => AlleBehaelter.VentilQ6(), p => true);

        private ICommand _btnVentilQ8;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnVentilQ8 => _btnVentilQ8 ??= new RelayCommand(p => AlleBehaelter.VentilQ8(), p => true);
    }
}