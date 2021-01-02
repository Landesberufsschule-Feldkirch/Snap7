using System.IO;
using Kommunikation;

namespace TestAutomat
{
    public partial class AutoTesterWindow
    {
        public AutoTester.AutoTester AutoTester { get; set; }
        public ViewModel.ViewModel ViewModel { get; set; }


        public AutoTesterWindow(FileSystemInfo aktuellesProjekt, Datenstruktur datenstruktur)
        {
            ViewModel = new ViewModel.ViewModel(datenstruktur);
            AutoTester = new AutoTester.AutoTester(ViewModel, aktuellesProjekt);

            DataContext = ViewModel;

            InitializeComponent();

            TestAusgabe.ItemsSource = ViewModel.ViAnzeige.TestAusgabe;
        }
    }
}
