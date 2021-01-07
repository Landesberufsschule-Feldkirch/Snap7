using System.IO;
using TestAutomat.AutoTester.ViewModel;

namespace TestAutomat
{
    public partial class AutoTesterWindow
    {
        public AutoTester.Model.AutoTester AutoTester { get; set; }
        public AutoTesterViewModel AutoTesterViewModel { get; set; }

        public AutoTesterWindow(FileSystemInfo aktuellesProjekt)
        {
            AutoTesterViewModel = new AutoTesterViewModel();
            AutoTester = new AutoTester.Model.AutoTester(AutoTesterViewModel, aktuellesProjekt);

            DataContext = AutoTesterViewModel;

            InitializeComponent();

            TestAusgabe.ItemsSource =  AutoTesterViewModel.AutoTesterAnzeige.TestAusgabe;
        }
    }
}