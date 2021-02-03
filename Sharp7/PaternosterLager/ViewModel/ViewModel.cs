using PaternosterLager.Commands;
using System.Windows.Input;

namespace PaternosterLager.ViewModel
{
    public class ViewModel
    {
        public Model.Paternosterlager Paternosterlager { get; }
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow, double anzahlKisten)
        {
            Paternosterlager = new Model.Paternosterlager(anzahlKisten);
            ViAnzeige = new VisuAnzeigen(mainWindow, Paternosterlager, anzahlKisten);
        }


        private ICommand _btnReset;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnReset => _btnReset ??= new RelayCommand(_ => Paternosterlager.AllesReset(), _ => true);

        private ICommand _btnBuchstabe;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnBuchstabe => _btnBuchstabe ??= new RelayCommand(ViAnzeige.Buchstabe);

        private ICommand _btnAuf;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAuf => _btnAuf ??= new RelayCommand(_ => ViAnzeige.TasterAuf(), _ => true);

        private ICommand _btnAb;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAb => _btnAb ??= new RelayCommand(_ => ViAnzeige.TasterAb(), _ => true);
    }
}