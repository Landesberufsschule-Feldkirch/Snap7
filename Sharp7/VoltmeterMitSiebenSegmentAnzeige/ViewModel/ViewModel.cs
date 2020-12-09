// ReSharper disable UnusedMember.Global
namespace VoltmeterMitSiebenSegmentAnzeige.ViewModel
{
    public class ViewModel
    {
        private string _textbox1;

        public string Textbox1
        {
            get => _textbox1;
            set
            {
                _textbox1 = value;
            }
        }

        public Model.Voltmeter Voltmeter { get; }
        public VisuAnzeigen VAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            Voltmeter = new Model.Voltmeter();
            VAnzeige = new VisuAnzeigen(mainWindow, Voltmeter);
        }
    }
}