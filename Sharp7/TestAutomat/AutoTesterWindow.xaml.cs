using System.IO;

namespace TestAutomat
{
    public partial class AutoTesterWindow
    {
        public AutoTester.AutoTester AutoTester;

        public AutoTesterWindow(FileSystemInfo aktuellesProjekt)
        {
            AutoTester = new AutoTester.AutoTester(aktuellesProjekt);

            InitializeComponent();
        }
    }
}
