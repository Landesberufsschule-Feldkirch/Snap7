namespace _TestProjekt.ViewModel
{
    public class ViewModel
    {
        public Model.Test Test { get; }
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel()
        {
            Test = new Model.Test();
            ViAnzeige = new VisuAnzeigen();
        }
    }
}