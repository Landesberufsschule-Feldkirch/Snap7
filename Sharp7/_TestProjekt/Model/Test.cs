using System.Threading;

namespace _TestProjekt.Model
{
    public class Test
    {



        public Test() => System.Threading.Tasks.Task.Run(TestTask);
        private static void TestTask()
        {

            while (true)
            {


                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}