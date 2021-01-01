using Kommunikation;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TestAutomat.Model;

namespace TestAutomat
{
    public class TestAutomat
    {
        public DirectoryInfo AktuellesProjekt { get; set; }
        public OrdnerLesen ConfigOrdner { get; set; }

        private AutoTesterWindow _autoTesterWindow;
        private PlcWindow _plcWindow;
        private readonly Datenstruktur _datenstruktur;
        private readonly ManualMode.ManualMode _manualMode;

        public TestAutomat(Datenstruktur datenstruktur, ManualMode.ManualMode manualMode)
        {
            _datenstruktur = datenstruktur;
            _manualMode = manualMode;
        }
        public void SetTestConfig(string autotestconfig) => ConfigOrdner = new OrdnerLesen(autotestconfig);
        public void TabItemFuellen(TabItem tabItemAutomatischerSoftwareTest)
        {
            var autoTestGrid = new Grid
            {
                Name = "AutoTestGrid",
                Background = Brushes.Yellow
            };

            foreach (var row in new[] { 10, 50, 10, 500 })
                autoTestGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(row) });

            foreach (var column in new[] { 10, 150, 100, 10, 300, 300, 150 })
                autoTestGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(column) });

            tabItemAutomatischerSoftwareTest.Content = autoTestGrid;

            var btnManualTest = new Button
            {
                Name = "BtnManualTest",
                IsEnabled = false,
                Visibility = Visibility.Hidden,
                Content = "ManualWindow",
                Padding = new Thickness(5, 5, 5, 5),
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.Black),
                Margin = new Thickness(3, 3, 3, 3)
            };

            btnManualTest.Click += (_, _) => _manualMode.ManualModeStarten();

            Grid.SetColumn(btnManualTest, 5);
            Grid.SetRow(btnManualTest, 1);
            autoTestGrid.Children.Add(btnManualTest);


            var btnStart = new Button
            {
                Name = "BtnTestStarten",
                IsEnabled = false,
                Content = "Test starten",
                FontSize = 22,
                Padding = new Thickness(5, 5, 5, 5),
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.Black),
                Margin = new Thickness(3, 3, 3, 3)
            };

            btnStart.Click += (_, _) =>
            {
                btnStart.IsEnabled = true;
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    btnManualTest.Visibility = Visibility.Visible;
                    btnManualTest.IsEnabled = true;
                }
                TestAutomatStarten(AktuellesProjekt);
            };

            Grid.SetColumn(btnStart, 1);
            Grid.SetRow(btnStart, 1);
            autoTestGrid.Children.Add(btnStart);


            var stackPanel = new StackPanel
            {
                Name = "StackPanel",
                Background = Brushes.GreenYellow
            };

            Grid.SetColumn(stackPanel, 1);
            Grid.SetColumnSpan(stackPanel, 2);
            Grid.SetRow(stackPanel, 3);
            autoTestGrid.Children.Add(stackPanel);

            var webBrowser = new WebBrowser { Name = "WebBrowser" };

            Grid.SetColumn(webBrowser, 4);
            Grid.SetColumnSpan(webBrowser, 2);
            Grid.SetRow(webBrowser, 3);
            autoTestGrid.Children.Add(webBrowser);

            TestProjekteEinfuellen(btnStart, stackPanel, webBrowser);
        }
        public void TestProjekteEinfuellen(Button btnStart, StackPanel stackPanel, WebBrowser webBrowser)
        {
            foreach (var projekt in ConfigOrdner.AlleTestOrdner)
            {
                var rdo = new RadioButton
                {
                    GroupName = "TestProjekte",
                    Name = projekt.Name,
                    FontSize = 14,
                    Content = projekt.Name,
                    VerticalAlignment = VerticalAlignment.Top,
                    Tag = projekt
                };
                rdo.Checked += (sender, _) =>
                {
                    if (!(sender is RadioButton { Tag: DirectoryInfo } rb)) return;

                    btnStart.IsEnabled = true;
                    btnStart.Background = new SolidColorBrush(Colors.LawnGreen);

                    AktuellesProjekt = rb.Tag as DirectoryInfo;

                    if (AktuellesProjekt == null) return;
                    var dateiName = $@"{AktuellesProjekt.FullName}\index.html";

                    var htmlSeite = File.Exists(dateiName) ? File.ReadAllText(dateiName) : "--??--";

                    var dataHtmlSeite = Encoding.UTF8.GetBytes(htmlSeite);
                    var stmHtmlSeite = new MemoryStream(dataHtmlSeite, 0, dataHtmlSeite.Length);

                    webBrowser.NavigateToStream(stmHtmlSeite);
                };

                stackPanel.Children.Add(rdo);
            }
        }
        private void TestAutomatStarten(FileSystemInfo aktuellesProjekt)
        {
            _autoTesterWindow = new AutoTesterWindow(aktuellesProjekt);
            _autoTesterWindow.Show();

            _plcWindow = new PlcWindow(_datenstruktur, aktuellesProjekt,_manualMode,  _autoTesterWindow);
            _plcWindow.Show();
        }
    }
}