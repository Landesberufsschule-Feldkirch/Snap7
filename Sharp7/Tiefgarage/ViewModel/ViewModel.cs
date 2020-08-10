using System.Windows.Input;
using Tiefgarage.Commands;

namespace Tiefgarage.ViewModel
{
    public class ViewModel
    {
        private readonly Model.AlleFahrzeugePersonen _alleFahrzeugePersonen;
        public Model.AlleFahrzeugePersonen AlleFahrzeugePersonen => _alleFahrzeugePersonen;
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            _alleFahrzeugePersonen = new Model.AlleFahrzeugePersonen();
            ViAnzeige = new VisuAnzeigen(mainWindow, _alleFahrzeugePersonen);
        }



        private ICommand _btnDraussenParken;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnDraussenParken =>
            _btnDraussenParken ?? (_btnDraussenParken =
                new RelayCommand(p => _alleFahrzeugePersonen.DraussenParken(), p => true));


        private ICommand _btnDrinnenParken;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnDrinnenParken =>
            _btnDrinnenParken ?? (_btnDrinnenParken =
                new RelayCommand(p => _alleFahrzeugePersonen.DrinnenParken(), p => true));

        private ICommand _btnAuto1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAuto1 => _btnAuto1 ?? (_btnAuto1 = new RelayCommand(p => _alleFahrzeugePersonen.Auto1(), p => true));


        private ICommand _btnAuto2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAuto2 => _btnAuto2 ?? (_btnAuto2 = new RelayCommand(p => _alleFahrzeugePersonen.Auto2(), p => true));

        private ICommand _btnAuto3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAuto3 => _btnAuto3 ?? (_btnAuto3 = new RelayCommand(p => _alleFahrzeugePersonen.Auto3(), p => true));


        private ICommand _btnAuto4;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAuto4 => _btnAuto4 ?? (_btnAuto4 = new RelayCommand(p => _alleFahrzeugePersonen.Auto4(), p => true));


        private ICommand _btnPerson1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnPerson1 => _btnPerson1 ?? (_btnPerson1 = new RelayCommand(p => _alleFahrzeugePersonen.Person1(), p => true));

        // ReSharper disable once UnusedMember.Global

        private ICommand _btnPerson2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnPerson2 => _btnPerson2 ?? (_btnPerson2 = new RelayCommand(p => _alleFahrzeugePersonen.Person2(), p => true));


        private ICommand _btnPerson3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnPerson3 => _btnPerson3 ?? (_btnPerson3 = new RelayCommand(p => _alleFahrzeugePersonen.Person3(), p => true));


        private ICommand _btnPerson4;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnPerson4 => _btnPerson4 ?? (_btnPerson4 = new RelayCommand(p => _alleFahrzeugePersonen.Person4(), p => true));
    }
}