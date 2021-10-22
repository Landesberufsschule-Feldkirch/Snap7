namespace BeschriftungPlc
{
    public class BeschriftungenPlc
    {
        public BeschriftungenStruktur BeschriftungenStruktur { get; set; }

        public BeschriftungenPlc() => BeschriftungenStruktur = new BeschriftungenStruktur();

        public void UpdateBeschriftungen(string ordnerConfigTests)
        {
            BeschriftungenStruktur.PlcDaBeschriftungen(@$"{ordnerConfigTests}\beschriftungDa.json");
            BeschriftungenStruktur.PlcDiBeschriftungen(@$"{ordnerConfigTests}\beschriftungDi.json");
        }
    }
}