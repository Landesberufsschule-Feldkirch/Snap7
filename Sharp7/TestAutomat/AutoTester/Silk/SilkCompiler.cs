using System.Diagnostics;
using SoftCircuits.Silk;

namespace TestAutomat.AutoTester.Silk
{
    public partial class Silk
    {
        public static AutoTesterWindow AutoTesterWindow { get; set; }
        public static Stopwatch SilkStopwatch { get; set; }

        public static (bool erfolgreich, Compiler compiler, CompiledProgram program) Compile(string mySourceFile)
        {
            var compiler = new Compiler();
            CompilerRegisterFunctions(compiler);
            return compiler.Compile(mySourceFile, out var program) ? (true, compiler, program) : (false, compiler, program);
        }
        public static void ReferenzenUebergeben(AutoTesterWindow autoTesterWindow, Stopwatch silkStopwatch)
        {
            AutoTesterWindow = autoTesterWindow;
            SilkStopwatch = silkStopwatch;
        }
    }
}