namespace LAP_2018_1_Silosteuerung.ViewModel
{
    public class ViewModel
    {
        public VisuAnzeigen ViAnzeige { get; set; }
        public readonly Model.Foerderanlage foerderanlage;

        public ViewModel()
        {
            foerderanlage = new Model.Foerderanlage();
            ViAnzeige = new VisuAnzeigen(foerderanlage);
        }

        public Model.Foerderanlage Foerderanlage { get { return foerderanlage; } }
    }
}