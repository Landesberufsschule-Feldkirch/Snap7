using PaternosterLager.Commands;
using System.Windows.Input;

namespace ElektronischesZahlenschloss.ViewModel
{
    public class ViewModel
    {
        public VisuAnzeigen ViAnzeige { get; set; }
        public readonly PaternosterLager.Model.Paternosterlager paternosterlager;

        public ViewModel(MainWindow mainWindow, double anzahlKisten)
        {
            paternosterlager = new Model.Paternosterlager(mainWindow, anzahlKisten);
            ViAnzeige = new VisuAnzeigen(mainWindow, paternosterlager, anzahlKisten);
        }

        public Model.Paternosterlager Paternosterlager { get { return paternosterlager; } }

        #region BtnReset

        private ICommand _btnReset;

        public ICommand BtnReset
        {
            get
            {
                if (_btnReset == null)
                {
                    _btnReset = new RelayCommand(p => paternosterlager.AllesReset(), p => true);
                }
                return _btnReset;
            }
        }

        #endregion BtnReset

        #region BtnBuchstabe

        private ICommand _btnBuchstabe;

        public ICommand BtnBuchstabe
        {
            get
            {
                if (_btnBuchstabe == null)
                {
                    _btnBuchstabe = new RelayCommand(ViAnzeige.Buchstabe);
                }
                return _btnBuchstabe;
            }
        }

        #endregion BtnBuchstabe

        #region BtnAuf

        private ICommand _btnAuf;

        public ICommand BtnAuf
        {
            get
            {
                if (_btnAuf == null)
                {
                    _btnAuf = new RelayCommand(p => ViAnzeige.TasterAuf(), p => true);
                }
                return _btnAuf;
            }
        }

        #endregion BtnAuf

        #region BtnAb

        private ICommand _btnAb;

        public ICommand BtnAb
        {
            get
            {
                if (_btnAb == null)
                {
                    _btnAb = new RelayCommand(p => ViAnzeige.TasterAb(), p => true);
                }
                return _btnAb;
            }
        }

        #endregion BtnAb
    }
}