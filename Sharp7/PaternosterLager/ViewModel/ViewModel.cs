using PaternosterLager.Commands;
using System.Windows.Input;

namespace PaternosterLager.ViewModel
{
    public class ViewModel
    {
        public VisuAnzeigen ViAnzeige { get; set; }
        public readonly Model.Paternosterlager paternosterlager;

        public ViewModel(MainWindow mainWindow, double anzahlKisten)
        {
            paternosterlager = new Model.Paternosterlager(mainWindow, anzahlKisten);
            ViAnzeige = new VisuAnzeigen(mainWindow, paternosterlager, anzahlKisten);
        }

        // ReSharper disable once UnusedMember.Global
        public Model.Paternosterlager Paternosterlager => paternosterlager;

        #region BtnReset

        private ICommand _btnReset;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnReset => _btnReset ?? (_btnReset = new RelayCommand(p => paternosterlager.AllesReset(), p => true));

        #endregion BtnReset

        #region BtnBuchstabe

        private ICommand _btnBuchstabe;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnBuchstabe => _btnBuchstabe ?? (_btnBuchstabe = new RelayCommand(ViAnzeige.Buchstabe));

        #endregion BtnBuchstabe

        #region BtnAuf

        private ICommand _btnAuf;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAuf => _btnAuf ?? (_btnAuf = new RelayCommand(p => ViAnzeige.TasterAuf(), p => true));

        #endregion BtnAuf

        #region BtnAb

        private ICommand _btnAb;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAb => _btnAb ?? (_btnAb = new RelayCommand(p => ViAnzeige.TasterAb(), p => true));

        #endregion BtnAb
    }
}