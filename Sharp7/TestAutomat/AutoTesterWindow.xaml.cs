using System;
using System.Collections.ObjectModel;
using System.IO;
using TestAutomat.AutoTester.Model;

namespace TestAutomat
{
    public partial class AutoTesterWindow
    {
        public ObservableCollection<TestAusgabe> AutoTesterDataGrid { get; set; }
        public AutoTester.Model.AutoTester AutoTester { get; set; }
        
        public AutoTesterWindow(FileSystemInfo aktuellesProjekt)
        {
            AutoTesterDataGrid = new ObservableCollection<TestAusgabe>();
            AutoTester = new AutoTester.Model.AutoTester(aktuellesProjekt);
            
            InitializeComponent();
            DataGrid.ItemsSource = AutoTesterDataGrid;

            System.Threading.Tasks.Task.Factory.StartNew(
                () =>
                {
                    while (true)
                    {
                        if (AutoTester.GetAnzahlErgebnisse() > 0)
                        {
                            Dispatcher.BeginInvoke((Action)(() => AutoTesterDataGrid.Add(AutoTester.GetTestErgebniss())));
                        }

                        System.Threading.Thread.Sleep(10);
                    }
                    // ReSharper disable once FunctionNeverReturns
                });
        }
    }
}