using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;
using TestAutomat.Model;

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
                        AutoTester.TestErgebnis.CompilerStart => Brushes.Cyan,
                        AutoTester.TestErgebnis.CompilerErfolgreich => Brushes.LawnGreen,
                        AutoTester.TestErgebnis.CompilerError => Brushes.Red,
                        AutoTester.TestErgebnis.TestStart => Brushes.CornflowerBlue,
                        AutoTester.TestErgebnis.TestEnde => Brushes.CornflowerBlue,
                        AutoTester.TestErgebnis.Aktiv => Brushes.White,
                        AutoTester.TestErgebnis.Init => Brushes.Aquamarine,
                        AutoTester.TestErgebnis.Erfolgreich => Brushes.LawnGreen,
                        AutoTester.TestErgebnis.Timeout => Brushes.Orange,
                        AutoTester.TestErgebnis.Fehler => Brushes.Red,
                        AutoTester.TestErgebnis.Kommentar => Brushes.White,
                        AutoTester.TestErgebnis.Version => Brushes.White,
                        AutoTester.TestErgebnis.UnbekanntesErgebnis => Brushes.Red,
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