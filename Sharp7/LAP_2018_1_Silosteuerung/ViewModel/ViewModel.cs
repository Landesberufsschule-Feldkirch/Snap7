namespace LAP_2018_1_Silosteuerung.ViewModel
{
    public class ViewModel
    {
        private readonly Model.Foerderanlage _foerderanlage;
        public Model.Foerderanlage Foerderanlage => _foerderanlage;
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel()
        {
            _foerderanlage = new Model.Foerderanlage();
            ViAnzeige = new VisuAnzeigen(_foerderanlage);
        }
    }
}