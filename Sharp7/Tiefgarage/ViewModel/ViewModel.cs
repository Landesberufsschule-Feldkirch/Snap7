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
        public ICommand BtnDraussenParken => _btnDraussenParken ??= new RelayCommand(_ => AlleFahrzeugePersonen.DraussenParken(), _ => true);


        private ICommand _btnDrinnenParken;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnDrinnenParken => _btnDrinnenParken ??= new RelayCommand(_ => AlleFahrzeugePersonen.DrinnenParken(), _ => true);

        private ICommand _btnAuto1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAuto1 => _btnAuto1 ??= new RelayCommand(_ => AlleFahrzeugePersonen.Auto1(), _ => true);


        private ICommand _btnAuto2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAuto2 => _btnAuto2 ??= new RelayCommand(_ => AlleFahrzeugePersonen.Auto2(), _ => true);

        private ICommand _btnAuto3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAuto3 => _btnAuto3 ??= new RelayCommand(_ => AlleFahrzeugePersonen.Auto3(), _ => true);


        private ICommand _btnAuto4;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAuto4 => _btnAuto4 ??= new RelayCommand(_ => AlleFahrzeugePersonen.Auto4(), _ => true);


        private ICommand _btnPerson1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnPerson1 => _btnPerson1 ??= new RelayCommand(_ => AlleFahrzeugePersonen.Person1(), _ => true);

        // ReSharper disable once UnusedMember.Global

        private ICommand _btnPerson2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnPerson2 => _btnPerson2 ??= new RelayCommand(_ => AlleFahrzeugePersonen.Person2(), _ => true);


        private ICommand _btnPerson3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnPerson3 => _btnPerson3 ??= new RelayCommand(_ => AlleFahrzeugePersonen.Person3(), _ => true);


        private ICommand _btnPerson4;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnPerson4 => _btnPerson4 ??= new RelayCommand(_ => AlleFahrzeugePersonen.Person4(), _ => true);
    }
}