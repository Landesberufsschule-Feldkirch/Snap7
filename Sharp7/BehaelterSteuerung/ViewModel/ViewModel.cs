using System.Windows.Input;
using BehaelterSteuerung.Commands;

namespace BehaelterSteuerung.ViewModel
{
    public class ViewModel
    {
        public Model.BehaelterSteuerung AlleBehaelter { get; }

        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            AlleBehaelter = new Model.BehaelterSteuerung();
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

        private ICommand _btnAutomatik1234;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAutomatik1234 =>
            _btnAutomatik1234 ??= new RelayCommand(p => AlleBehaelter.Automatik1234(), p => true);

        private ICommand _btnAutomatik1324;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAutomatik1324 =>
            _btnAutomatik1324 ??= new RelayCommand(p => AlleBehaelter.Automatik1324(), p => true);

        private ICommand _btnAutomatik1432;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAutomatik1432 =>
            _btnAutomatik1432 ??= new RelayCommand(p => AlleBehaelter.Automatik1432(), p => true);

        private ICommand _btnAutomatik4321;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAutomatik4321 =>
            _btnAutomatik4321 ??= new RelayCommand(p => AlleBehaelter.Automatik4321(), p => true);
    }
}