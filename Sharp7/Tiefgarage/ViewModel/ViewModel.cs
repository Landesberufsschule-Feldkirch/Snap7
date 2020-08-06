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

        #region BtnDraussenParken

        private ICommand _btnDraussenParken;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnDraussenParken =>
            _btnDraussenParken ?? (_btnDraussenParken =
                new RelayCommand(p => alleFahrzeugePersonen.DraussenParken(), p => true));

        #endregion BtnDraussenParken

        #region BtnDrinnenParken

        private ICommand _btnDrinnenParken;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnDrinnenParken =>
            _btnDrinnenParken ?? (_btnDrinnenParken =
                new RelayCommand(p => alleFahrzeugePersonen.DrinnenParken(), p => true));

        #endregion BtnDrinnenParken

        #region BtnAuto1

        private ICommand _btnAuto1;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAuto1 => _btnAuto1 ?? (_btnAuto1 = new RelayCommand(p => alleFahrzeugePersonen.Auto1(), p => true));

        #endregion BtnAuto1

        #region BtnAuto2

        private ICommand _btnAuto2;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAuto2 => _btnAuto2 ?? (_btnAuto2 = new RelayCommand(p => alleFahrzeugePersonen.Auto2(), p => true));

        #endregion BtnAuto2

        #region BtnAuto3

        private ICommand _btnAuto3;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAuto3 => _btnAuto3 ?? (_btnAuto3 = new RelayCommand(p => alleFahrzeugePersonen.Auto3(), p => true));

        #endregion BtnAuto3

        #region BtnAuto4

        private ICommand _btnAuto4;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAuto4 => _btnAuto4 ?? (_btnAuto4 = new RelayCommand(p => alleFahrzeugePersonen.Auto4(), p => true));

        #endregion BtnAuto4


        #region BtnPerson1

        private ICommand _btnPerson1;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnPerson1 => _btnPerson1 ?? (_btnPerson1 = new RelayCommand(p => alleFahrzeugePersonen.Person1(), p => true));

        // ReSharper disable once UnusedMember.Global
        #endregion BtnPerson1

        #region BtnPerson2

        private ICommand _btnPerson2;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnPerson2 => _btnPerson2 ?? (_btnPerson2 = new RelayCommand(p => alleFahrzeugePersonen.Person2(), p => true));

        #endregion BtnPerson2

        #region BtnPerson3

        private ICommand _btnPerson3;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnPerson3 => _btnPerson3 ?? (_btnPerson3 = new RelayCommand(p => alleFahrzeugePersonen.Person3(), p => true));

        #endregion BtnPerson3

        #region BtnPerson4

        private ICommand _btnPerson4;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnPerson4 => _btnPerson4 ?? (_btnPerson4 = new RelayCommand(p => alleFahrzeugePersonen.Person4(), p => true));

        #endregion BtnPerson4
    }
}