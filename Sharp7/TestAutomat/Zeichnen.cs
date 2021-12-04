using Kommunikation;
using System.IO;
using TestAutomat.Model;

namespace TestAutomat;

public partial class TestAutomat
{
    public AutoTester MyAutoTester { get; set; }

    private bool _autoTesterWindowAktiv;

    private void TestAutomatStarten(FileSystemInfo aktuellesProjekt, Datenstruktur datenstruktur, IPlc plc)
    {
        if (!_autoTesterWindowAktiv)
        {
            _autoTesterWindowAktiv = true;
            _autoTesterWindow = new AutoTesterWindow();
            _autoTesterWindow.PlcBeschriftungAktualisieren(BeschriftungenPlc);
            _autoTesterWindow.Show();
        }
        else
        {
            _autoTesterWindow.PlcBeschriftungAktualisieren(BeschriftungenPlc);
            _autoTesterWindow.AutoTesterDataGrid.Clear();
            _autoTesterWindow.DataGridId = 0;
        }

        if (MyAutoTester != null) MyAutoTester = null;
        MyAutoTester = new AutoTester(_autoTesterWindow, aktuellesProjekt, datenstruktur, plc);
    }
}