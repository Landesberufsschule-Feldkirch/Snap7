using System.Windows.Input;
using Kata.Commands;

namespace Kata.ViewModel
{
    public class ViewModel
    {
        public Model.Kata Kata { get; }
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel()
        {
            Kata = new Model.Kata();
            ViAnzeige = new VisuAnzeigen(Kata);
        }


        private ICommand _btnTaster;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnzeige.Taster);


    }
}