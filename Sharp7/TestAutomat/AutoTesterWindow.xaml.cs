using System.IO;
using TestAutomat.AutoTester.ViewModel;

namespace TestAutomat
{
    public partial class AutoTesterWindow
    {
        public AutoTester.Config.AutoTester AutoTester { get; set; }
        public AutoTesterViewModel AutoTesterViewModel { get; set; }

        public AutoTesterWindow(FileSystemInfo aktuellesProjekt)
        {
            AutoTesterViewModel = new AutoTesterViewModel();
            AutoTester = new AutoTester.Config.AutoTester(AutoTesterViewModel, aktuellesProjekt);

            DataContext = AutoTesterViewModel;

            InitializeComponent();

            TestAusgabe.ItemsSource = AutoTesterViewModel.AutoTesterAnzeige.TestAusgabe;
        }
    }
}