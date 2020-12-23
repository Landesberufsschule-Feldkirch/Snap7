namespace TestAutomat
{
    public partial class AutoTesterWindow
    {
        public AutoTester.AutoTester AutoTester;

        public AutoTesterWindow(System.IO.DirectoryInfo aktuellesProjekt)
        {
            AutoTester = new AutoTester.AutoTester(aktuellesProjekt);

            InitializeComponent();
        }
    }
}
