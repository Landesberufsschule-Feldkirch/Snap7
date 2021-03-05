using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Kommunikation;
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

            foreach (var column in new[] { 10, 150, 100, 10, 150, 50, 200, 300, 100, 100 })
                autoTestGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(column) });

            tabItemAutomatischerSoftwareTest.Content = autoTestGrid;




            var btnPlcWindowOeffnen = new Button
            {
                Width = 60,
                Height = 40,
                Content = new Image
                {
                    Source = new BitmapImage(new Uri("TestAutomatBilder/IconPlotter.jpg", UriKind.Relative))
                }
            };

            btnPlcWindowOeffnen.Click += (_, _) =>
            {
                if (displayPlc.FensterAktiv) displayPlc.Schliessen();
                else displayPlc.Oeffnen();
            };

            SetColumn(btnPlcWindowOeffnen, 9);
            SetRow(btnPlcWindowOeffnen, 0);
            SetRowSpan(btnPlcWindowOeffnen, 2);
            autoTestGrid.Children.Add(btnPlcWindowOeffnen);


            var plotterWindowOeffnen = new Button
            {
                Width = 60,
                Height = 40,
                Content = new Image
                {
                    Source = new BitmapImage(new Uri("TestAutomatBilder/IconPlotter.jpg", UriKind.Relative))
                }
            };

            SetColumn(plotterWindowOeffnen, 8);
            SetRow(plotterWindowOeffnen, 0);
            SetRowSpan(plotterWindowOeffnen, 2);
            autoTestGrid.Children.Add(plotterWindowOeffnen);






            var btnManualTest = new Button
            {
                Name = "BtnManualTest",
                IsEnabled = false,
                Visibility = Visibility.Hidden,
                Content = "ManualWindow",
                Padding = new Thickness(5, 5, 5, 5),
                BorderThickness = new Thickness(1.0),
                BorderBrush = new SolidColorBrush(Colors.Black),
                Margin = new Thickness(3, 3, 3, 3),
                Width = 200
            };
            btnManualTest.Click += (_, _) => _manualMode.ManualModeStarten();
            SetColumn(btnManualTest, 7);
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



            var btnEinzelSchritt = new Button
            {
                Content = "Einzelschritt",
                FontSize = 22,
                Visibility = Visibility.Hidden,
                Background = Brushes.Silver
            };
            btnEinzelSchritt.Click += (_, _) =>
            {
                if (_datenstruktur.BetriebsartTestablauf == BetriebsartTestablauf.Einzelschritt)
                {
                    _datenstruktur.NaechstenSchrittGehen = true;
                }
            };
            SetColumn(btnEinzelSchritt, 6);
            SetRow(btnEinzelSchritt, 1);
            autoTestGrid.Children.Add(btnEinzelSchritt);



            var lblSingleStep = new Label
            {
                Content = "Single Step",
                FontSize = 22
            };
            SetColumn(lblSingleStep, 4);
            SetRow(lblSingleStep, 1);
            autoTestGrid.Children.Add(lblSingleStep);

            var checkboxSingleStep = new CheckBox { VerticalAlignment = VerticalAlignment.Center };
            checkboxSingleStep.Click += (sender, _) =>
            {
                if (sender is not CheckBox s) return;

                if (s.IsChecked != null && (bool)s.IsChecked)
                {
                    _datenstruktur.BetriebsartTestablauf = BetriebsartTestablauf.Einzelschritt;
                    btnEinzelSchritt.Visibility = Visibility.Visible;
                }

                else
                {
                    _datenstruktur.BetriebsartTestablauf = BetriebsartTestablauf.Automatik;
                    btnEinzelSchritt.Visibility = Visibility.Hidden;
                }

            };
            SetColumn(checkboxSingleStep, 5);
            SetRow(checkboxSingleStep, 1);
            autoTestGrid.Children.Add(checkboxSingleStep);


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
            SetColumnSpan(webBrowser, 5);
            SetRow(webBrowser, 3);
            autoTestGrid.Children.Add(webBrowser);

            TestProjekteEinfuellen(btnStart, stackPanel, webBrowser);
        }
    }
}