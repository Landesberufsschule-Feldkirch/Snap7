using System.ComponentModel;
using System.Threading;

namespace _3dHofschiebetor.ViewModel
{
    public static class IdEintraege
    {

    }

    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.HofSchiebeTor _hofSchiebeTor;
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.HofSchiebeTor ht)
        {
            _mainWindow = mw;
            _hofSchiebeTor = ht;
            
            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }
        private void VisuAnzeigenTask()
        {
            while (true)
            {

                Thread.Sleep(100);
            }
            // ReSharper disable once FunctionNeverReturns
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}