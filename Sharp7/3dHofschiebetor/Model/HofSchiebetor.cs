using System.Threading;

namespace _3dHofschiebetor.Model
{
    public class HofSchiebeTor
    {
        private readonly MainWindow _mainWindow;

        
      
        public HofSchiebeTor(MainWindow mw)
        {
            _mainWindow = mw;


            System.Threading.Tasks.Task.Run(HofSchiebeTorTask);
        }

        private void HofSchiebeTorTask()
        {
            while (true)
            {
               
                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        public char Zeichen { get; internal set; }
    }
}