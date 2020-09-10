using System.Windows.Input;
using BehälterSteuerung.Commands;

namespace BehälterSteuerung.ViewModel
{
    public class ViewModel
    {
        private readonly Model.BehaelterSteuerung _alleBehaelter;
        public Model.BehaelterSteuerung AlleBehaelter => _alleBehaelter;
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            _alleBehaelter = new Model.BehaelterSteuerung();
            ViAnzeige = new VisuAnzeigen(mainWindow, _alleBehaelter);
        }



        private ICommand _btnVentilQ2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnVentilQ2 => _btnVentilQ2 ??= new RelayCommand(p => _alleBehaelter.VentilQ2(), p => true);

        private ICommand _btnVentilQ4;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnVentilQ4 => _btnVentilQ4 ??= new RelayCommand(p => _alleBehaelter.VentilQ4(), p => true);

        private ICommand _btnVentilQ6;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnVentilQ6 => _btnVentilQ6 ??= new RelayCommand(p => _alleBehaelter.VentilQ6(), p => true);

        private ICommand _btnVentilQ8;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnVentilQ8 => _btnVentilQ8 ??= new RelayCommand(p => _alleBehaelter.VentilQ8(), p => true);

        private ICommand _btnAutomatik1234;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAutomatik1234 =>
            _btnAutomatik1234 ??= new RelayCommand(p => _alleBehaelter.Automatik1234(), p => true);

        private ICommand _btnAutomatik1324;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAutomatik1324 =>
            _btnAutomatik1324 ??= new RelayCommand(p => _alleBehaelter.Automatik1324(), p => true);

        private ICommand _btnAutomatik1432;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAutomatik1432 =>
            _btnAutomatik1432 ??= new RelayCommand(p => _alleBehaelter.Automatik1432(), p => true);

        private ICommand _btnAutomatik4321;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAutomatik4321 =>
            _btnAutomatik4321 ??= new RelayCommand(p => _alleBehaelter.Automatik4321(), p => true);
    }
}