using System.Windows.Input;
using BehälterSteuerung.Commands;

namespace BehälterSteuerung.ViewModel
{
    public class ViewModel
    {
        public readonly Model.BehaelterSteuerung alleBehaelter;
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            alleBehaelter = new Model.BehaelterSteuerung();
            ViAnzeige = new VisuAnzeigen(mainWindow, alleBehaelter);
        }

        public Model.BehaelterSteuerung AlleBehaelter => alleBehaelter;

        #region BtnVentilQ2

        private ICommand _btnVentilQ2;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnVentilQ2 => _btnVentilQ2 ?? (_btnVentilQ2 = new RelayCommand(p => alleBehaelter.VentilQ2(), p => true));

        #endregion BtnVentilQ2

        #region BtnVentilQ4

        private ICommand _btnVentilQ4;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnVentilQ4 => _btnVentilQ4 ?? (_btnVentilQ4 = new RelayCommand(p => alleBehaelter.VentilQ4(), p => true));

        #endregion BtnVentilQ4

        #region BtnVentilQ6

        private ICommand _btnVentilQ6;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnVentilQ6 => _btnVentilQ6 ?? (_btnVentilQ6 = new RelayCommand(p => alleBehaelter.VentilQ6(), p => true));

        #endregion BtnVentilQ6

        #region BtnVentilQ8

        private ICommand _btnVentilQ8;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnVentilQ8 => _btnVentilQ8 ?? (_btnVentilQ8 = new RelayCommand(p => alleBehaelter.VentilQ8(), p => true));

        #endregion BtnVentilQ8

        #region BtnAutomatik1234

        private ICommand _btnAutomatik1234;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAutomatik1234 =>
            _btnAutomatik1234 ??
            (_btnAutomatik1234 = new RelayCommand(p => alleBehaelter.Automatik1234(), p => true));

        #endregion BtnAutomatik1234

        #region BtnAutomatik1324

        private ICommand _btnAutomatik1324;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAutomatik1324 =>
            _btnAutomatik1324 ??
            (_btnAutomatik1324 = new RelayCommand(p => alleBehaelter.Automatik1324(), p => true));

        #endregion BtnAutomatik1324

        #region BtnAutomatik1432

        private ICommand _btnAutomatik1432;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAutomatik1432 =>
            _btnAutomatik1432 ??
            (_btnAutomatik1432 = new RelayCommand(p => alleBehaelter.Automatik1432(), p => true));

        #endregion BtnAutomatik1432

        #region BtnAutomatik4321

        private ICommand _btnAutomatik4321;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAutomatik4321 =>
            _btnAutomatik4321 ??
            (_btnAutomatik4321 = new RelayCommand(p => alleBehaelter.Automatik4321(), p => true));

        #endregion BtnAutomatik4321
    }
}