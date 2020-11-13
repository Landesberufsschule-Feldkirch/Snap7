// ReSharper disable UnusedMember.Global
namespace VoltmeterMitSiebenSegmentAnzeige.ViewModel
{
    public class ViewModel
    {
        public Model.Voltmeter Voltmeter { get; }
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            Voltmeter = new Model.Voltmeter();
            ViAnzeige = new VisuAnzeigen(mainWindow, Voltmeter);
        }     
    }
}