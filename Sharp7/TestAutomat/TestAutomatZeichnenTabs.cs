using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Windows.Controls.Grid;

namespace TestAutomat
{
    public partial class TestAutomat
    {
        private bool _testWurdeSchonMalGestartet;
        public void TabItemFuellen(TabItem tabItemAutomatischerSoftwareTest, DisplayPlc.DisplayPlc displayPlc)
        {
            var autoTestGrid = new Grid
            {
                Name = "AutoTestGrid",
                Background = Brushes.Yellow
            };

            foreach (var row in new[] { 10, 50, 10, 700 })
                autoTestGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(row) });

            foreach (var column in new[] { 10, 150, 100, 10, 400, 400, 100 })
                autoTestGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(column) });

            tabItemAutomatischerSoftwareTest.Content = autoTestGrid;


            var btnPlcWindowOeffnen = new Button
            {
                Width = 60,
                Height = 40,
                Content = new Image
                {
                    Source = new BitmapImage(new Uri("/S7_1200.jpg", UriKind.Relative))
                }
            };

            btnPlcWindowOeffnen.Click += (_, _) => displayPlc.Oeffnen();

            SetColumn(btnPlcWindowOeffnen, 6);
            SetColumnSpan(btnPlcWindowOeffnen, 2);
            SetRow(btnPlcWindowOeffnen, 0);
            SetRowSpan(btnPlcWindowOeffnen, 2);
            autoTestGrid.Children.Add(btnPlcWindowOeffnen);


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

            SetColumn(btnManualTest, 5);
            SetRow(btnManualTest, 1);
            autoTestGrid.Children.Add(btnManualTest);

            if (System.Diagnostics.Debugger.IsAttached)
            {
                btnManualTest.Visibility = Visibility.Visible;
                btnManualTest.IsEnabled = true;
            }

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
                if (_testWurdeSchonMalGestartet)
                {
                    _autoTesterWindow.Show();
                }
                else
                {
                    _testWurdeSchonMalGestartet = true;
                    btnStart.Content = "Test erneut starten?";
                    SetColumnSpan(btnStart, 2);
                }
                TestAutomatStarten(AktuellesProjekt, _datenstruktur);
            };

            SetColumn(btnStart, 1);
            SetRow(btnStart, 1);
            autoTestGrid.Children.Add(btnStart);


            var stackPanel = new StackPanel
            {
                Name = "StackPanel",
                Background = Brushes.GreenYellow
            };

            SetColumn(stackPanel, 1);
            SetColumnSpan(stackPanel, 2);
            SetRow(stackPanel, 3);
            autoTestGrid.Children.Add(stackPanel);

            var webBrowser = new WebBrowser { Name = "WebBrowser" };

            SetColumn(webBrowser, 4);
            SetColumnSpan(webBrowser, 2);
            SetRow(webBrowser, 3);
            autoTestGrid.Children.Add(webBrowser);

            TestProjekteEinfuellen(btnStart, stackPanel, webBrowser);
        }
    }
}