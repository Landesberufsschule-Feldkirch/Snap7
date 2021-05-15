using PlcDatenTypen;
using SoftCircuits.Silk;

namespace TestAutomat.Silk
{
    public partial class Silk
    {
        public static void Runtime_Function(object sender, FunctionEventArgs e)
        {
            switch (e.Name)
            {
                case "BitmusterBlinktTesten": BitmusterBlinktTesten(e); break;
                case "BitmusterTesten": BitmusterTesten(e); break;
                case "GetDigitaleAusgaenge": GetDigitaleAusgaenge(e); break;
                case "IncrementDataGridId": IncrementDataGridId(); break;
                case "KommentarAnzeigen": KommentarAnzeigen(e); break;
                case "Plc2Dec": Plc2Dec(e); break;
                case "PlcColdStart": PlcColdStart(); break;
                case "PlcGetStatus": PlcGetStatus(); break;
                case "PlcHotStart": PlcHotStart(); break;
                case "ResetStopwatch": ResetStopwatch(); break;
                case "SetAnalogerEingang": SetAnalogerEingang(e); break;
                case "SetDataGridBitAnzahl": SetDataGridBitAnzahl(e); break;
                case "SetDiagrammZeitbereich": SetDiagrammZeitbereich(e); break;
                case "SetDigitaleEingaenge": SetDigitaleEingaenge(e); break;
                case "Sleep": Sleep(e); break;
                case "TestAblauf": TestAblauf(e); break;
                case "UpdateAnzeige": UpdateAnzeige(e); break;
                case "VersionAnzeigen": VersionAnzeigen(); break;
            }
        }
        private static void Plc2Dec(FunctionEventArgs e)
        {
            var zahl = e.Parameters[0].ToString();
            var plcZahl = new Uint(zahl);
            e.ReturnValue.SetValue((int)plcZahl.GetDec());
        }
    }
}