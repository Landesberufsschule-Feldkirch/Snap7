using Kommunikation;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TestAutomat
{
    public partial class PlcWindow
    {

        public PlcWindow(Datenstruktur datenstruktur, FileSystemInfo fileSystemInfo)
        {
            PlcWindowZeichnen();
            InitializeComponent();
        }

        private void PlcWindowZeichnen()
        {
            var plcGrid = new Grid { Name = "PlcGrid" };
            Content = plcGrid;

            plcGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10) });
            for (var i = 0; i < 40; i++) plcGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(45) });

            foreach (var row in new[] { 10, 200, 100, 45, 45, 45, 45, 45, 45, 45, 45, 45, 100, 200 })
                plcGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(row) });

            PlcFensterFormen.PlcRechteckZeichnen(1, 30, 3, 9, Brushes.LightGray, plcGrid);

            PlcFensterFormen.PlcBorderZeichnen(3, 8, 3, 3, 3, 0, 3, 3, Brushes.White, plcGrid);
            PlcFensterFormen.PlcLabelZeichnen(3, 5, Brushes.White, 18, "DI", plcGrid);
            PlcFensterFormen.PlcLabelZeichnen(4, 5, Brushes.White, 16, "a", plcGrid);
            for (var i = 0; i < 8; i++)
            {
                PlcFensterFormen.PlcLabelZeichnen(3 + i, 4, Brushes.White, 16, $".{i}", plcGrid);
                PlcFensterFormen.PlcButtonZeichnen(3 + i, 3, i, "FarbeDi", BackgroundProperty, plcGrid);
                PlcFensterFormen.PlcBezeichnungZeichnen(3 + i, 2, i, "BezDi", VisibilityProperty, plcGrid);
            }

            PlcFensterFormen.PlcBorderZeichnen(13, 8, 3, 3, 3, 0, 3, 3, Brushes.White, plcGrid);
            PlcFensterFormen.PlcLabelZeichnen(13, 5, Brushes.White, 18, "DI", plcGrid);
            PlcFensterFormen.PlcLabelZeichnen(14, 5, Brushes.White, 16, "b", plcGrid);
            for (var i = 0; i < 8; i++)
            {
                PlcFensterFormen.PlcLabelZeichnen(13 + i, 4, Brushes.White, 16, $".{i}", plcGrid);
                PlcFensterFormen.PlcButtonZeichnen(13 + i, 3, 10 + i, "FarbeDi", BackgroundProperty, plcGrid);
                PlcFensterFormen.PlcBezeichnungZeichnen(13 + i, 2, i, "BezDi", VisibilityProperty, plcGrid);
            }

            PlcFensterFormen.PlcBorderZeichnen(3, 8, 9, 3, 3, 3, 3, 0, Brushes.White, plcGrid);
            PlcFensterFormen.PlcLabelZeichnen(3, 9, Brushes.White, 18, "DQ", plcGrid);
            PlcFensterFormen.PlcLabelZeichnen(4, 9, Brushes.White, 16, "a", plcGrid);
            for (var i = 0; i < 8; i++)
            {
                PlcFensterFormen.PlcLabelZeichnen(3 + i, 10, Brushes.White, 16, $".{i}", plcGrid);
                PlcFensterFormen.PlcButtonZeichnen(3 + i, 11, i, "FarbeDa", BackgroundProperty, plcGrid);
                PlcFensterFormen.PlcBezeichnungZeichnen(3 + i, 12, i, "BezDa", VisibilityProperty, plcGrid);
            }

            PlcFensterFormen.PlcBorderZeichnen(13, 8, 9, 3, 3, 3, 3, 0, Brushes.White, plcGrid);
            PlcFensterFormen.PlcLabelZeichnen(13, 9, Brushes.White, 18, "DQ", plcGrid);
            PlcFensterFormen.PlcLabelZeichnen(14, 9, Brushes.White, 16, "b", plcGrid);
            for (var i = 0; i < 8; i++)
            {
                PlcFensterFormen.PlcLabelZeichnen(13 + i, 10, Brushes.White, 16, $".{i}", plcGrid);
                PlcFensterFormen.PlcButtonZeichnen(13 + i, 11, 10 + i, "FarbeDa", BackgroundProperty, plcGrid);
                PlcFensterFormen.PlcBezeichnungZeichnen(13 + i, 12, i, "BezDa", VisibilityProperty, plcGrid);
            }
        }
    }
}
