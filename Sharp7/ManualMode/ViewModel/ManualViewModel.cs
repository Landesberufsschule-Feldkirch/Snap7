namespace ManualMode.ViewModel
{
    public class ManualViewModel
    {
        private readonly ManualMode _manualMode;
        public ManualMode ManualMode => _manualMode;
        public ManualVisuAnzeigen ManualVisuAnzeigen { get; set; }
        public ManualViewModel()
        {
            _manualMode = new ManualMode();
            ManualVisuAnzeigen = new ManualVisuAnzeigen(_manualMode);
        }
    }
}