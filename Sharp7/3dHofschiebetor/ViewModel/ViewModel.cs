using System.Windows.Input;
using _3dHofschiebetor.Commands;

namespace _3dHofschiebetor.ViewModel
{
    public class ViewModel
    {
        public Model.HofSchiebeTor HofSchiebeTor{ get; }

        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            HofSchiebeTor = new Model.HofSchiebeTor(mainWindow);
            ViAnzeige = new VisuAnzeigen(mainWindow, HofSchiebeTor);
        }

        private ICommand _btnReset;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnReset => _btnReset ?? (_btnReset = new RelayCommand(p => ViAnzeige.AllesReset(), p => true));


    }
}