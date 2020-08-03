using System.Windows.Input;
using Tiefgarage.Commands;

namespace Tiefgarage.ViewModel
{
    public class ViewModel
    {
        public readonly Model.AlleFahrzeugePersonen alleFahrzeugePersonen;
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            alleFahrzeugePersonen = new Model.AlleFahrzeugePersonen();
            ViAnzeige = new VisuAnzeigen(mainWindow, alleFahrzeugePersonen);
        }

        public Model.AlleFahrzeugePersonen AlleFahrzeugePersonen => alleFahrzeugePersonen;

        #region BtnDraussenParken

        private ICommand _btnDraussenParken;

        public ICommand BtnDraussenParken
        {
            get
            {
                if (_btnDraussenParken == null)
                {
                    _btnDraussenParken = new RelayCommand(p => alleFahrzeugePersonen.DraussenParken(), p => true);
                }
                return _btnDraussenParken;
            }
        }

        #endregion BtnDraussenParken

        #region BtnDrinnenParken

        private ICommand _btnDrinnenParken;

        public ICommand BtnDrinnenParken
        {
            get
            {
                if (_btnDrinnenParken == null)
                {
                    _btnDrinnenParken = new RelayCommand(p => alleFahrzeugePersonen.DrinnenParken(), p => true);
                }
                return _btnDrinnenParken;
            }
        }

        #endregion BtnDrinnenParken

        #region BtnAuto1

        private ICommand _btnAuto1;

        public ICommand BtnAuto1
        {
            get
            {
                if (_btnAuto1 == null)
                {
                    _btnAuto1 = new RelayCommand(p => alleFahrzeugePersonen.Auto1(), p => true);
                }
                return _btnAuto1;
            }
        }

        #endregion BtnAuto1

        #region BtnAuto2

        private ICommand _btnAuto2;

        public ICommand BtnAuto2
        {
            get
            {
                if (_btnAuto2 == null)
                {
                    _btnAuto2 = new RelayCommand(p => alleFahrzeugePersonen.Auto2(), p => true);
                }
                return _btnAuto2;
            }
        }

        #endregion BtnAuto2

        #region BtnAuto3

        private ICommand _btnAuto3;

        public ICommand BtnAuto3
        {
            get
            {
                if (_btnAuto3 == null)
                {
                    _btnAuto3 = new RelayCommand(p => alleFahrzeugePersonen.Auto3(), p => true);
                }
                return _btnAuto3;
            }
        }

        #endregion BtnAuto3

        #region BtnAuto4

        private ICommand _btnAuto4;

        public ICommand BtnAuto4
        {
            get
            {
                if (_btnAuto4 == null)
                {
                    _btnAuto4 = new RelayCommand(p => alleFahrzeugePersonen.Auto4(), p => true);
                }
                return _btnAuto4;
            }
        }

        #endregion BtnAuto4

        #region BtnPerson1

        private ICommand _btnPerson1;

        public ICommand BtnPerson1
        {
            get
            {
                if (_btnPerson1 == null)
                {
                    _btnPerson1 = new RelayCommand(p => alleFahrzeugePersonen.Person1(), p => true);
                }
                return _btnPerson1;
            }
        }

        #endregion BtnPerson1

        #region BtnPerson2

        private ICommand _btnPerson2;

        public ICommand BtnPerson2
        {
            get
            {
                if (_btnPerson2 == null)
                {
                    _btnPerson2 = new RelayCommand(p => alleFahrzeugePersonen.Person2(), p => true);
                }
                return _btnPerson2;
            }
        }

        #endregion BtnPerson2

        #region BtnPerson3

        private ICommand _btnPerson3;

        public ICommand BtnPerson3
        {
            get
            {
                if (_btnPerson3 == null)
                {
                    _btnPerson3 = new RelayCommand(p => alleFahrzeugePersonen.Person3(), p => true);
                }
                return _btnPerson3;
            }
        }

        #endregion BtnPerson3

        #region BtnPerson4

        private ICommand _btnPerson4;

        public ICommand BtnPerson4
        {
            get
            {
                if (_btnPerson4 == null)
                {
                    _btnPerson4 = new RelayCommand(p => alleFahrzeugePersonen.Person4(), p => true);
                }
                return _btnPerson4;
            }
        }

        #endregion BtnPerson4
    }
}