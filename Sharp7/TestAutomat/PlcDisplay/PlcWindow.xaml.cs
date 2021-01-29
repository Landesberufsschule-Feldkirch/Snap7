using Kommunikation;
using System.Windows.Controls;
using TestAutomat.PlcDisplay.ViewModel;

namespace TestAutomat.PlcDisplay
{
    public partial class PlcWindow
    {
        public PlcDisplayViewModel PlcDisplayViewModel { get; set; }
        public PlcWindow(Datenstruktur datenstruktur, ManualMode.ManualMode manualMode, AutoTesterWindow autoTesterWindow)
        {
            PlcDisplayViewModel = new PlcDisplayViewModel(datenstruktur);

            var plcGrid = new Grid { Name = "PlcGrid" };
            Content = plcGrid;

            PlcZeichnen(plcGrid, BackgroundProperty, manualMode, autoTesterWindow);

            DataContext = PlcDisplayViewModel;
            InitializeComponent();
        }
    }
}