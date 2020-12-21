using System.ComponentModel;
using System.Threading;

namespace TestAutomat.ViewModel
{
    public class AutoTesterVisuAnzeigen : INotifyPropertyChanged
    {
        public AutoTesterVisuAnzeigen()
        {
            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private static void VisuAnzeigenTask()
        {
            while (true)
            {
                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }


        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        // ReSharper disable once UnusedMember.Local
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}