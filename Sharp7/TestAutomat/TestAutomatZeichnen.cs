using Kommunikation;
using System.IO;

namespace TestAutomat
{
    public partial class TestAutomat
    {
        public AutoTester.Model.AutoTester MyAutoTester { get; set; }

        private bool _autoTesterWindowAktiv;

        private void TestAutomatStarten(FileSystemInfo aktuellesProjekt, Datenstruktur datenstruktur)
        {
            if (!_autoTesterWindowAktiv)
            {
                _autoTesterWindowAktiv = true;
                _autoTesterWindow = new AutoTesterWindow();
                _autoTesterWindow.Show();
            }
            else
            {
                _autoTesterWindow.AutoTesterDataGrid.Clear();
                _autoTesterWindow.DataGridId = 0;
            }
            MyAutoTester = new AutoTester.Model.AutoTester(_autoTesterWindow, aktuellesProjekt, datenstruktur);
        }
    }
}