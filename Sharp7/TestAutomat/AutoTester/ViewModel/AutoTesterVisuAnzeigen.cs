using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using TestAutomat.AutoTester.Model;

namespace TestAutomat.AutoTester.ViewModel
{
    public class AutoTesterVisuAnzeigen : INotifyPropertyChanged
    {
        public AutoTesterVisuAnzeigen() => System.Threading.Tasks.Task.Run(AutoTesterVisuAnzeigenTask);
        private static void AutoTesterVisuAnzeigenTask()
        {
            while (true)
            {
                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }
        public void AddEinzelneZeileAnzeigen(TestAusgabe daten) => TestAusgabe.Add(daten);

        private List<TestAusgabe> _testAusgabe = new();
        public List<TestAusgabe> TestAusgabe
        {
            get => _testAusgabe;
            set
            {
                _testAusgabe = value;
                OnPropertyChanged(nameof(TestAusgabe));
            }
        }
        
        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        // ReSharper disable once UnusedMember.Local
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}