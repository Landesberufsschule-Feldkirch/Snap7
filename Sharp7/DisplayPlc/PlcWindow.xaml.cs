using System.ComponentModel;
using DisplayPlc.ViewModel;
using Kommunikation;
using System.Windows;
using System.Windows.Controls;

namespace DisplayPlc
{
    public partial class PlcWindow
    {
        public DisplayPlcViewModel DisplayPlcViewModel { get; set; }

        public PlcWindow(Datenstruktur datenstruktur, ManualMode.ManualMode manualMode)
        {
            DisplayPlcViewModel = new DisplayPlcViewModel(datenstruktur);

            var plcGrid = new Grid { Name = "PlcGrid", MaxWidth = 700, MaxHeight = 1200, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };

            Content = plcGrid;

            PlcZeichnen(plcGrid, BackgroundProperty, manualMode);
            
            DataContext = DisplayPlcViewModel;
            Closing += PlcWindow_Closing;
        }
        private void PlcWindow_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
        public void Oeffnen()
        {
            Show();
            Title = "PLC";
            MaxWidth = 700;
        }
    }
}