using System.Collections.ObjectModel;
using System.IO;
using TestAutomat.AutoTester.Model;

namespace TestAutomat
{
    public partial class AutoTesterWindow
    {
        public ObservableCollection<TestAusgabe> AutoTesterDataGrid { get; set; }
        public AutoTester.Model.AutoTester AutoTester { get; set; }

        public void UpdateForReal(TestAusgabe data) => Dispatcher.Invoke(() => AutoTesterDataGrid.Add(data));
        public AutoTesterWindow(FileSystemInfo aktuellesProjekt)
        {
            AutoTesterDataGrid = new ObservableCollection<TestAusgabe>();
            AutoTester = new AutoTester.Model.AutoTester(this, aktuellesProjekt);
            
            InitializeComponent();
            DataGrid.ItemsSource = AutoTesterDataGrid;
        }
    }
}