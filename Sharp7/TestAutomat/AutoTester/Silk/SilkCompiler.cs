using SoftCircuits.Silk;

namespace TestAutomat.AutoTester.Silk
{
    public partial class Silk
    {

        public static AutoTesterWindow AutoTesterWindow { get; set; }

        public static (bool erfolgreich, CompiledProgram program) Compile(string mySourceFile)
        {
            var compiler = new Compiler();            
            
            CompilerRegisterFunctions(compiler);

            return compiler.Compile(mySourceFile, out var program) ? (true, program) : (false, program);
        }

        public static void ReferenzenUebergeben(AutoTesterWindow autoTesterWindow)
        {
            AutoTesterWindow = autoTesterWindow;
        }
    }
}