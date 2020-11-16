// ReSharper disable UnusedMember.Global
namespace VoltmeterMitSiebenSegmentAnzeige.ViewModel
{
    public class ViewModel
    {
        public Model.Voltmeter Voltmeter { get; }
        public VisuAnzeigen VAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            Voltmeter = new Model.Voltmeter();
            VAnzeige = new VisuAnzeigen(mainWindow, Voltmeter);
        }     
    }
}