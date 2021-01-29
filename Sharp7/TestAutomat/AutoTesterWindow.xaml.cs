using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using TestAutomat.AutoTester.Model;

namespace TestAutomat
{
    public partial class AutoTesterWindow
    {
        public ObservableCollection<TestAusgabe> AutoTesterDataGrid { get; set; }
        public AutoTester.Model.AutoTester MyAutoTester { get; set; }

        public void UpdateDataGrid(TestAusgabe data) => Dispatcher.Invoke(() =>
        {
            var zeile = data.Nr;

            if (AutoTesterDataGrid.Count <= zeile) AutoTesterDataGrid.Add(data);
            else AutoTesterDataGrid[zeile] = data;
        });

        public AutoTesterWindow(FileSystemInfo aktuellesProjekt, Kommunikation.Datenstruktur datenstruktur)
        {
            AutoTesterDataGrid = new ObservableCollection<TestAusgabe>();
            MyAutoTester = new AutoTester.Model.AutoTester(this, aktuellesProjekt, datenstruktur);

            InitializeComponent();
            DataGrid.ItemsSource = AutoTesterDataGrid;
            DataGrid.ItemContainerGenerator.StatusChanged += (_, _) =>
            {
                if (DataGrid.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated) return;
                var count = AutoTesterDataGrid.Count;
                if (count < 1) return;

                for (var zeile = 0; zeile < count; zeile++)
                {
                    var row = (DataGridRow)DataGrid.ItemContainerGenerator.ContainerFromIndex(zeile);

                    // ReSharper disable once ConvertSwitchStatementToSwitchExpression
                    switch (AutoTesterDataGrid[zeile].Ergebnis)
                    {
                        case global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.Aktiv: row.Background = Brushes.White; break;
                        case global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.Init: row.Background = Brushes.Aquamarine; break;
                        case global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.Erfolgreich: row.Background = Brushes.LawnGreen; break;
                        case global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.Timeout: row.Background = Brushes.Orange; break;
                        case global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.Fehler: row.Background = Brushes.Red; break;
                        default: throw new ArgumentOutOfRangeException();
                    }
                }
            };
        }
    }
}