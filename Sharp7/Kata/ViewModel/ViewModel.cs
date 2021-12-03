using Kata.Commands;
using System.Windows.Input;

namespace Kata.ViewModel
{
    public class ViewModel
    {
        public Model.Kata Kata { get; }
        public VisuAnzeigen ViAnz { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            Kata = new Model.Kata(mainWindow.DtAutoTests);
            ViAnz = new VisuAnzeigen(mainWindow,Kata);
        }

        private ICommand _btnTaster;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnz.Taster);

        private ICommand _btnSchalter;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnSchalter => _btnSchalter ??= new RelayCommand(ViAnz.Schalter);
    }
}