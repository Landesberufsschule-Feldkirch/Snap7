using ThermischeSolarAnlage.Model;

namespace ThermischeSolarAnlage.ViewModel;

public class ViewModel
{
    public Test Test { get; }
    public VisuAnzeigen ViAnz { get; set; }

    public ViewModel()
    {
        Test = new Test();
        ViAnz = new VisuAnzeigen();
    }
}