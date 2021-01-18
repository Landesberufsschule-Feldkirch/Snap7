using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using TestAutomat.AutoTester.Config;
using TestAutomat.AutoTester.Model;

namespace TestAutomat
{
    public partial class AutoTesterWindow
    {
        public ObservableCollection<TestAusgabe> AutoTesterDataGrid { get; set; }
        public AutoTester.Model.AutoTester AutoTester { get; set; }

        public void UpdateDataGrid(TestAusgabe data) => Dispatcher.Invoke(() => AutoTesterDataGrid.Add(data));

        public AutoTesterWindow(FileSystemInfo aktuellesProjekt, Kommunikation.Datenstruktur datenstruktur)
        {
            AutoTesterDataGrid = new ObservableCollection<TestAusgabe>();
            AutoTester = new AutoTester.Model.AutoTester(this, aktuellesProjekt, datenstruktur);

            InitializeComponent();
            DataGrid.ItemsSource = AutoTesterDataGrid;
            DataGrid.ItemContainerGenerator.StatusChanged += (_, _) =>
            {
                if (DataGrid.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated) return;
                var reihe = AutoTesterDataGrid.Count;
                if (reihe < 1) return;
                var row = (DataGridRow)DataGrid.ItemContainerGenerator.ContainerFromIndex(reihe - 1);

                // ReSharper disable once ConvertSwitchStatementToSwitchExpression
                switch (AutoTesterDataGrid[reihe - 1].Ergebnis)
                {
                    case TestErgebnis.Init: row.Background = Brushes.Aquamarine; break;
                    case TestErgebnis.Erfolgreich: row.Background = Brushes.LawnGreen; break;
                    case TestErgebnis.Timeout: row.Background = Brushes.Orange; break;
                    case TestErgebnis.Fehler: row.Background = Brushes.Red; break;
                    case TestErgebnis.Default: row.Background = Brushes.White; break;
                }
            };
        }
    }
}