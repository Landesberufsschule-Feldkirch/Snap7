using System.Windows.Input;

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




    }
}