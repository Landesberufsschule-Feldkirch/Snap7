﻿using SoftCircuits.Silk;

namespace TestAutomat.AutoTester.Silk
{
    // Register intrinsic functions - NOTE that silk internal functions are also available
    // as per https://github.com/SoftCircuits/Silk/blob/master/docs/InternalFunctions.md
    public partial class Silk
    {
        private static void CompilerRegisterFunctions(Compiler compiler )
        {
            // ReSharper disable RedundantArgumentDefaultValue
            compiler.RegisterFunction("Print", 0, Function.NoParameterLimit);
            compiler.RegisterFunction("Debug", 0, Function.NoParameterLimit);
            compiler.RegisterFunction("println", 0, Function.NoParameterLimit);
            compiler.RegisterFunction("Sleep", 1, 1);
            compiler.RegisterFunction("GetDigitaleAusgaenge",0,0);
            compiler.RegisterFunction("SetDigitaleEingaenge", 1, 1);
            compiler.RegisterFunction("UpdateAnzeige",0,7);
            // ReSharper restore RedundantArgumentDefaultValue
            
        }
    }
}