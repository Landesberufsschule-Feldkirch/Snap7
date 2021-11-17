using PlcDatenTypen;
using SoftCircuits.Silk;
using System.Threading;

namespace TestAutomat.Silk
{
    public partial class Silk
    {
        internal static void SetDigitaleEingaengeWord(Uint eingaenge)
        {
            Datenstruktur.DigInput[0] = Simatic.Digital_GetLowByte((uint)eingaenge.GetDec());
            Datenstruktur.DigInput[1] = Simatic.Digital_GetHighByte((uint)eingaenge.GetDec());
        }
        private static void SetDigitaleEingaenge(FunctionEventArgs e)
        {
            SetDigitaleEingaengeWord(new Uint((ulong)e.Parameters[0].ToInteger()));
            Thread.Sleep(100);
        }
        private static void SetAnalogerEingang(FunctionEventArgs e)
        {
            var startByte = e.Parameters[0].ToInteger();
            var analogInput = e.Parameters[1].ToInteger();
            var datenTyp = e.Parameters[2].ToString();

            if (datenTyp != "S7 / 16 Bit / Prozent") return;

            var siemens = Simatic.Analog_2_Int16(analogInput, 100);
            Datenstruktur.AnalogInput[startByte] = Simatic.Digital_GetLowByte((uint)siemens);
            Datenstruktur.AnalogInput[startByte + 1] = Simatic.Digital_GetHighByte((uint)siemens);
        }
        public static void SetDiagrammZeitbereich(FunctionEventArgs e)
        {
            Datenstruktur.DiagrammZeitbereich = e.Parameters[0].ToInteger();
        }
        private static void SetDataGridBitAnzahl()
        {
            _anzahlBitEingaenge = 16;   // e.Parameters[0].ToInteger();
            _anzahlBitAusgaenge = 16;   // e.Parameters[1].ToInteger();
        }
    }
}