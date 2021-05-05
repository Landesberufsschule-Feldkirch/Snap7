using System.Diagnostics;
using Kommunikation;
using SoftCircuits.Silk;

namespace TestAutomat.Silk
{
    public partial class Silk
    {
        public static AutoTesterWindow AutoTesterWindow { get; set; }
        public  static Datenstruktur Datenstruktur { get; set; }
        public static Stopwatch SilkStopwatch { get; set; }
        public  static IPlc Plc { get; set; }

        public static (bool erfolgreich, Compiler compiler, CompiledProgram program) Compile(string mySourceFile)
        {
            var compiler = new Compiler();
            CompilerRegisterFunctions(compiler);
            return compiler.Compile(mySourceFile, out var program) ? (true, compiler, program) : (false, compiler, program);
        }
        public static void ReferenzenUebergeben(AutoTesterWindow autoTesterWindow, Datenstruktur datenstruktur, Stopwatch silkStopwatch, IPlc plc)
        {
            AutoTesterWindow = autoTesterWindow;
            Datenstruktur = datenstruktur;
            SilkStopwatch = silkStopwatch;
            Plc = plc;
        }
    }
}