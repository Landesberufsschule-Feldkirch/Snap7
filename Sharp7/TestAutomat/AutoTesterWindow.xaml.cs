using System;
using System.Collections.ObjectModel;
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
                    row.Background = AutoTesterDataGrid[zeile].Ergebnis switch
                    {
                        global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.CompilerStart => Brushes.Cyan,
                        global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.CompilerErfolgreich => Brushes.LawnGreen,
                        global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.CompilerError => Brushes.Red,
                        global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.TestStart => Brushes.CornflowerBlue,
                        global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.TestEnde => Brushes.CornflowerBlue,
                        global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.Aktiv => Brushes.White,
                        global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.Init => Brushes.Aquamarine,
                        global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.Erfolgreich => Brushes.LawnGreen,
                        global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.Timeout => Brushes.Orange,
                        global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.Fehler => Brushes.Red,
                        global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.Kommentar => Brushes.Silver,
                        global::TestAutomat.AutoTester.Model.AutoTester.TestErgebnis.UnbekanntesErgebnis => Brushes.Red,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }
            };

            Closing += (_, e) =>
            {
                e.Cancel = true;
                Hide();
            };
        }
    }
}