using SoftCircuits.Silk;
using System;

namespace TestAutomat.AutoTester.Silk
{
    public static partial class Silk
    {
        public static void Compile(string mySourceFile)
        {
            var compiler = new Compiler();

            var sourceCode = System.IO.File.ReadAllText(mySourceFile);

            Console.WriteLine("debug - program: " + sourceCode);
            // Register intrinsic functions - NOTE that silk internal functions are also available
            // as per https://github.com/SoftCircuits/Silk/blob/master/docs/InternalFunctions.md
            Console.WriteLine("Registering functions...");
            compiler.RegisterFunction("Print", 0, Function.NoParameterLimit);
            compiler.RegisterFunction("Debug", 0, Function.NoParameterLimit);
            compiler.RegisterFunction("println", 0, Function.NoParameterLimit);
            compiler.RegisterFunction("Sleep", 1, 1);

            Console.WriteLine("Functions registered!");

            if (compiler.Compile(mySourceFile, out var program))
            {
                Console.WriteLine("Compilation successful!");

                RunProgram(program);
            }
            else
            {
                Console.WriteLine("Compile failed!");
            }
        }
        public static void RunProgram(CompiledProgram program)
        {
            var runtime = new Runtime();

            runtime.Begin += Runtime_Begin;
            runtime.Function += Runtime_Function;
            runtime.End += Runtime_End;

            var result = runtime.Execute(program);

            // Console.WriteLine(source + " ran successfully with exit code {0}.", result);
        }
    }
}