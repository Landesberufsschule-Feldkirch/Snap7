using PaternosterLager.Commands;
using System.Windows.Input;

namespace PaternosterLager.ViewModel
{
    public class ViewModel
    {
        private readonly Model.Paternosterlager _paternosterlager;
        public Model.Paternosterlager Paternosterlager => _paternosterlager;
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow, double anzahlKisten)
        {
            _paternosterlager = new Model.Paternosterlager(mainWindow, anzahlKisten);
            ViAnzeige = new VisuAnzeigen(mainWindow, _paternosterlager, anzahlKisten);
        }


        private ICommand _btnReset;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnReset => _btnReset ?? (_btnReset = new RelayCommand(p => _paternosterlager.AllesReset(), p => true));

        private ICommand _btnBuchstabe;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnBuchstabe => _btnBuchstabe ?? (_btnBuchstabe = new RelayCommand(ViAnzeige.Buchstabe));

        private ICommand _btnAuf;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAuf => _btnAuf ?? (_btnAuf = new RelayCommand(p => ViAnzeige.TasterAuf(), p => true));

        private ICommand _btnAb;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAb => _btnAb ?? (_btnAb = new RelayCommand(p => ViAnzeige.TasterAb(), p => true));
    }
}