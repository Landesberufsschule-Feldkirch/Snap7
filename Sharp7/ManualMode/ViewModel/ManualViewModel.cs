namespace ManualMode.ViewModel
{
    public class ManualViewModel
    {
        private readonly ManualMode _manualMode;
        public ManualMode ManualMode => _manualMode;
        public ManualVisuAnzeigen ManVisuAnzeigen { get; set; }
        public ManualViewModel(ManualMode manualMode)
        {
            _manualMode = manualMode;
            ManVisuAnzeigen = new ManualVisuAnzeigen(_manualMode);
        }
    }
}