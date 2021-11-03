using System.Windows.Input;
using Ampel_Verbania.Commands;

namespace Ampel_Verbania.ViewModel
{
    public class ViewModel
    {
        public Model.AmpelVerbania Kata { get; }
        public VisuAnzeigen ViAnz { get; set; }

        public ViewModel()
        {
            Kata = new Model.AmpelVerbania();
            ViAnz = new VisuAnzeigen(Kata);
        }

        private ICommand _btnTaster;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnz.Taster);
    }
}