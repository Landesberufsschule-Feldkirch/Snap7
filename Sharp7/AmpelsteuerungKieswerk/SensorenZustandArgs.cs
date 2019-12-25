namespace AmpelsteuerungKieswerk
{
    public enum SensorZustand
    {
        Ein,
        Aus
    }

    public class SensorenZustandArgs
    {
        public bool B1 { get; set; }
        public bool B2 { get; set; }
        public bool B3 { get; set; }
        public bool B4 { get; set; }

        public SensorenZustandArgs(bool b1, bool b2, bool b3, bool b4)
        {
            B1 = b1;
            B2 = b2;
            B3 = b3;
            B4 = b4;
        }
    }
}