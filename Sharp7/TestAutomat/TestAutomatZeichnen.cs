using Kommunikation;
using System.IO;
using TestAutomat.PlcDisplay;

namespace TestAutomat
{
    public partial class TestAutomat
    {
        public AutoTester.Model.AutoTester MyAutoTester { get; set; }

        private bool _autoTesterWindowAktiv;
        private bool _plcWindowAktiv;

        private void TestAutomatStarten(FileSystemInfo aktuellesProjekt, Datenstruktur datenstruktur)
        {
            if (!_autoTesterWindowAktiv)
            {
                _autoTesterWindowAktiv = true;
                _autoTesterWindow = new AutoTesterWindow(aktuellesProjekt, datenstruktur);
                _autoTesterWindow.Show();
            }

            MyAutoTester = new AutoTester.Model.AutoTester(_autoTesterWindow, aktuellesProjekt, datenstruktur);

            if (_plcWindowAktiv) return;

            _plcWindowAktiv = true;
            _plcWindow = new PlcWindow(_datenstruktur, _manualMode, MyAutoTester);
            _plcWindow.Show();
        }
    }
}