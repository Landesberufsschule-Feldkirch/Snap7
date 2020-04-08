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
            paternosterlager = new Model.Paternosterlager();
            ViAnzeige = new VisuAnzeigen(mainWindow, paternosterlager);
        }

        public Model.Paternosterlager Paternosterlager{ get { return paternosterlager; } }

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
        public ICommand BtnAbwaerts
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
