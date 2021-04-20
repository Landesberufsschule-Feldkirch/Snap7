using SoftCircuits.Silk;

namespace TestAutomat.Silk
{
    // Register intrinsic functions - NOTE that silk internal functions are also available
    // as per https://github.com/SoftCircuits/Silk/blob/master/docs/InternalFunctions.md
    public partial class Silk
    {
        private static void CompilerRegisterFunctions(Compiler compiler)
        {
            // ReSharper disable RedundantArgumentDefaultValue
            compiler.RegisterFunction("Print", 0, Function.NoParameterLimit);
            compiler.RegisterFunction("Debug", 0, Function.NoParameterLimit);
            compiler.RegisterFunction("println", 0, Function.NoParameterLimit);
            compiler.RegisterFunction("Sleep", 1, 1);
            compiler.RegisterFunction("GetDigitaleAusgaenge", 0, 0);
            compiler.RegisterFunction("SetDigitaleEingaenge", 1, 1);
            compiler.RegisterFunction("UpdateAnzeige", 2, 2);
            compiler.RegisterFunction("IncrementDataGridId", 0, 0);
            compiler.RegisterFunction("ResetStopwatch", 0, 0);
            compiler.RegisterFunction("BitmusterTesten", 4, 4);
            compiler.RegisterFunction("Plc2Dec", 1,1);
            compiler.RegisterFunction("SetDataGridBitAnzahl", 2, 2);
            compiler.RegisterFunction("BitmusterBlinktTesten", 8, 8);
            compiler.RegisterFunction("KommentarAnzeigen",1,1);
            compiler.RegisterFunction("VersionAnzeigen", 0, 0);
            compiler.RegisterFunction("TestAblauf", 0, 2);
            compiler.RegisterFunction("SetAnalogerEingang", 3,3);
            compiler.RegisterFunction("SetDiagrammZeitbereich", 1, 1);
            // ReSharper restore RedundantArgumentDefaultValue
        }
    }
}