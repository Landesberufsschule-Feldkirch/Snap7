namespace TestAutomat.AutoTester.ViewModel
{
    public class AutoTesterViewModel
    {
        public AutoTesterVisuAnzeigen AutoTesterAnzeige { get; set; }
        public AutoTesterViewModel() => AutoTesterAnzeige = new AutoTesterVisuAnzeigen();
    }
}