using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using TestAutomat.AutoTester.Model;

namespace TestAutomat
{
    public partial class AutoTesterWindow
    {
        public int DataGridId { get; set; }
        public ObservableCollection<TestAusgabe> AutoTesterDataGrid { get; set; }
        

        public void UpdateDataGrid(TestAusgabe data) => Dispatcher.Invoke(() =>
        {
            var zeile = data.Nr;

            if (AutoTesterDataGrid.Count <= zeile) AutoTesterDataGrid.Add(data);
            else AutoTesterDataGrid[zeile] = data;
        });

        public AutoTesterWindow()
        {
            AutoTesterDataGrid = new ObservableCollection<TestAusgabe>();

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
                    if (row == null) continue;

                    // ReSharper disable once ConvertSwitchStatementToSwitchExpression
                    switch (AutoTesterDataGrid[zeile].Ergebnis)
                    {
                        case global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.CompilerStart: row.Background = Brushes.Cyan; break;
                        case global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.CompilerErfolgreich: row.Background = Brushes.LawnGreen; break;
                        case global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.CompilerError: row.Background = Brushes.Red; break;
                        case global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.TestStart: row.Background = Brushes.CornflowerBlue; break;
                        case global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.TestEnde: row.Background = Brushes.CornflowerBlue; break;

                        case global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.Aktiv: row.Background = Brushes.White; break;
                        case global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.Init: row.Background = Brushes.Aquamarine; break;
                        case global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.Erfolgreich: row.Background = Brushes.LawnGreen; break;
                        case global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.Timeout: row.Background = Brushes.Orange; break;
                        case global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.Fehler: row.Background = Brushes.Red; break;
                        case global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.UnbekanntesErgebnis: row.Background = Brushes.Red; break;
                        default: throw new ArgumentOutOfRangeException();
                    }
                }
            };

            Closing += AutoTester_Closing;
        }

        private void AutoTester_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}