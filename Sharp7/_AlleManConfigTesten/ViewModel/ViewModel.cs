namespace _AlleManConfigTesten.ViewModel
{
    public class ViewModel
    {
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            ViAnzeige = new VisuAnzeigen();
        }
    }
}