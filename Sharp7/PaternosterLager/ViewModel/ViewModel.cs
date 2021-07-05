using PaternosterLager.Commands;
using System.Windows.Input;

namespace PaternosterLager.ViewModel
{
    public class ViewModel
    {
        public Model.Paternosterlager Paternosterlager { get; }
        public VisuAnzeigen ViAnz { get; set; }
        public ViewModel(MainWindow mainWindow, double anzahlKisten)
        {
            Paternosterlager = new Model.Paternosterlager(anzahlKisten);
            ViAnz = new VisuAnzeigen(mainWindow, Paternosterlager, anzahlKisten);
        }


        private ICommand _btnReset;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnReset => _btnReset ??= new RelayCommand(_ => Paternosterlager.AllesReset(), _ => true);

        private ICommand _btnBuchstabe;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnBuchstabe => _btnBuchstabe ??= new RelayCommand(ViAnz.Buchstabe);

        private ICommand _btnAuf;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAuf => _btnAuf ??= new RelayCommand(_ => ViAnz.TasterAuf(), _ => true);

        private ICommand _btnAb;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAb => _btnAb ??= new RelayCommand(_ => ViAnz.TasterAb(), _ => true);
    }
}