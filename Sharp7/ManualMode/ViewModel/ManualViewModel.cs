namespace ManualMode.ViewModel
{
    public class ManualViewModel
    {
        private readonly Model.ManualMode _manualMode;
        public Model.ManualMode ManualMode => _manualMode;
        public ManualVisuAnzeigen ManualVisuAnzeigen { get; set; }
        public ManualViewModel(MainWindow mainWindow)
        {
            _manualMode = new Model.ManualMode();
            ManualVisuAnzeigen = new ManualVisuAnzeigen(mainWindow, _manualMode);
        }
    }
}