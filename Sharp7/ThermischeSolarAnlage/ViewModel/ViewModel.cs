using ThermischeSolarAnlage.Model;

namespace ThermischeSolarAnlage.ViewModel
{
    public class ViewModel
    {
        public Test Test { get; }
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel()
        {
            Test = new Test();
            ViAnzeige = new VisuAnzeigen();
        }
    }
}