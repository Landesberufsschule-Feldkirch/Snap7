using System.Windows.Input;
using Tiefgarage.Commands;

namespace Tiefgarage.ViewModel
{
    public class ViewModel
    {
        public Model.AlleFahrzeugePersonen AlleFahrzeugePersonen { get; }

        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            AlleFahrzeugePersonen = new Model.AlleFahrzeugePersonen();
            ViAnzeige = new VisuAnzeigen(mainWindow, AlleFahrzeugePersonen);
        }



        private ICommand _btnDraussenParken;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnDraussenParken =>
            _btnDraussenParken ??= new RelayCommand(p => AlleFahrzeugePersonen.DraussenParken(), p => true);


        private ICommand _btnDrinnenParken;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnDrinnenParken =>
            _btnDrinnenParken ??= new RelayCommand(p => AlleFahrzeugePersonen.DrinnenParken(), p => true);

        private ICommand _btnAuto1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAuto1 => _btnAuto1 ??= new RelayCommand(p => AlleFahrzeugePersonen.Auto1(), p => true);


        private ICommand _btnAuto2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAuto2 => _btnAuto2 ??= new RelayCommand(p => AlleFahrzeugePersonen.Auto2(), p => true);

        private ICommand _btnAuto3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAuto3 => _btnAuto3 ??= new RelayCommand(p => AlleFahrzeugePersonen.Auto3(), p => true);


        private ICommand _btnAuto4;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAuto4 => _btnAuto4 ??= new RelayCommand(p => AlleFahrzeugePersonen.Auto4(), p => true);


        private ICommand _btnPerson1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnPerson1 => _btnPerson1 ??= new RelayCommand(p => AlleFahrzeugePersonen.Person1(), p => true);

        // ReSharper disable once UnusedMember.Global

        private ICommand _btnPerson2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnPerson2 => _btnPerson2 ??= new RelayCommand(p => AlleFahrzeugePersonen.Person2(), p => true);


        private ICommand _btnPerson3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnPerson3 => _btnPerson3 ??= new RelayCommand(p => AlleFahrzeugePersonen.Person3(), p => true);


        private ICommand _btnPerson4;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnPerson4 => _btnPerson4 ??= new RelayCommand(p => AlleFahrzeugePersonen.Person4(), p => true);
    }
}