using SoftCircuits.Silk;

namespace TestAutomat.AutoTester.Silk
{
    public partial class Silk
    {
        public static void RunProgram(CompiledProgram program)
        {
            var runtime = new Runtime();

            runtime.Begin += Runtime_Begin;
            runtime.Function += Runtime_Function;
            runtime.End += Runtime_End;

            runtime.Execute(program);
        }
    }
}