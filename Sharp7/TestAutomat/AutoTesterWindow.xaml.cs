using System.IO;

namespace TestAutomat
{
    public partial class AutoTesterWindow
    {
        public AutoTester.AutoTester AutoTester { get; set; }
        public ViewModel.ViewModel ViewModel { get; set; }


        public AutoTesterWindow(FileSystemInfo aktuellesProjekt)
        {
            ViewModel = new ViewModel.ViewModel();
            AutoTester = new AutoTester.AutoTester(ViewModel, aktuellesProjekt);

            DataContext = ViewModel;

            InitializeComponent();

            TestAusgabe.ItemsSource = ViewModel.ViAnzeige.TestAusgabe;

        }
    }
}
