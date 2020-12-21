namespace TestAutomat.ViewModel
{
    public class AutoTesterViewModel
    {
       
        public AutoTesterVisuAnzeigen ViAnzeige { get; set; }
        public AutoTesterViewModel()
        {
            
            ViAnzeige = new AutoTesterVisuAnzeigen();
        }
    }
}