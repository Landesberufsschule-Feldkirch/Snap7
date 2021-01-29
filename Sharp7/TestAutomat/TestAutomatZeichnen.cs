using Kommunikation;
using System.IO;
using TestAutomat.PlcDisplay;

namespace TestAutomat
{
    public partial class TestAutomat
    {
        private void TestAutomatStarten(FileSystemInfo aktuellesProjekt, Datenstruktur datenstruktur)
        {
            _autoTesterWindow = new AutoTesterWindow(aktuellesProjekt, datenstruktur);
            _autoTesterWindow.Show();

            _plcWindow = new PlcWindow(_datenstruktur, _manualMode, _autoTesterWindow);
            _plcWindow.Show();
        }
    }
}