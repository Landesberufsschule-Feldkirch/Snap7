namespace LAP_2018_1_Silosteuerung.ViewModel
{
    public class ViewModel
    {
        public VisuAnzeigen ViAnzeige { get; set; }
        public readonly Model.Foerderanlage foerderanlage;

        public ViewModel(MainWindow mainWindow)
        {
            foerderanlage = new Model.Foerderanlage(mainWindow);
            ViAnzeige = new VisuAnzeigen(mainWindow, foerderanlage);
        }

        public Model.Foerderanlage Foerderanlage { get { return foerderanlage; } }


    }
}
