using PaternosterLager.Commands;
using System.Windows.Input;

namespace PaternosterLager.ViewModel
{
    public class ViewModel
    {

        public VisuAnzeigen ViAnzeige { get; set; }
        public readonly PaternosterLager.Model.Paternosterlager paternosterlager;

        public ViewModel(MainWindow mainWindow)
        {
            paternosterlager = new Model.Paternosterlager(mainWindow);
            ViAnzeige = new VisuAnzeigen(mainWindow, paternosterlager);
        }

        public Model.Paternosterlager Paternosterlager{ get { return paternosterlager; } }

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
        #endregion

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
        #endregion

        #region BtnAb
        private ICommand _btnAb;
        public ICommand BtnAb
        {
            get
            {
                if (_btnAb == null)
                {
                    _btnAb = new RelayCommand(p =>ViAnzeige.TasterAb(), p => true);
                }
                return _btnAb;
            }
        }
        #endregion
    }
}
