using System.Windows.Input;
using Kata.Commands;

namespace Kata.ViewModel
{
    public class ViewModel
    {
        public Model.Kata Kata { get; }
        public VisuAnzeigen ViAnz { get; set; }

        public ViewModel()
        {
            Kata = new Model.Kata();
            ViAnz = new VisuAnzeigen(Kata);
        }

        private ICommand _btnTaster;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnz.Taster);

        private ICommand _btnSchalter;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnSchalter => _btnSchalter ??= new RelayCommand(ViAnz.Schalter);
    }
}