namespace _TestProjekt.ViewModel
{
    public class ViewModel
    {
        public Model.Test Test { get; }
        public VisuAnzeigen ViAnz { get; set; }

        public ViewModel()
        {
            Test = new Model.Test();
            ViAnz = new VisuAnzeigen();
        }
    }
}