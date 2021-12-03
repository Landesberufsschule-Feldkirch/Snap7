using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace Kata.ViewModel;

public partial class ViewModel
{
    private void ViewModelTask()
    {
        while (true)
        {
            if (_kata != null)
            {
                SichtbarkeitUmschalten(_kata.S1, 11);
                SichtbarkeitUmschalten(_kata.S2, 12);
                SichtbarkeitUmschalten(_kata.S3, 13);
                SichtbarkeitUmschalten(_kata.S4, 14);
                SichtbarkeitUmschalten(_kata.S5, 15);
                SichtbarkeitUmschalten(_kata.S6, 16);
                SichtbarkeitUmschalten(_kata.S7, 17);
                SichtbarkeitUmschalten(_kata.S8, 18);

                FarbeUmschalten(_kata.P1, 1, Brushes.LawnGreen, Brushes.White);
                FarbeUmschalten(_kata.P2, 2, Brushes.LawnGreen, Brushes.White);
                FarbeUmschalten(_kata.P3, 3, Brushes.LawnGreen, Brushes.White);
                FarbeUmschalten(_kata.P4, 4, Brushes.LawnGreen, Brushes.White);
                FarbeUmschalten(_kata.P5, 5, Brushes.Yellow, Brushes.White);
                FarbeUmschalten(_kata.P6, 6, Brushes.Yellow, Brushes.White);
                FarbeUmschalten(_kata.P7, 7, Brushes.Red, Brushes.White);
                FarbeUmschalten(_kata.P8, 8, Brushes.Red, Brushes.White);
            }

            if (_mainWindow is { DtAutoTests.PlcDaemon.Plc: { } })
            {
                SpsVersionLokal = _mainWindow.DtAutoTests.GetVersionLokal();
                SpsVersionEntfernt = _mainWindow.DtAutoTests.PlcDaemon.Plc.GetVersion();

                SpsSichtbar = SpsVersionLokal == SpsVersionEntfernt ? Visibility.Hidden : Visibility.Visible;
                SpsColor = _mainWindow.DtAutoTests.PlcDaemon.Plc.GetSpsError() ? Brushes.Red : Brushes.LightGray;
                SpsStatus = _mainWindow.DtAutoTests.PlcDaemon.Plc?.GetSpsStatus();
                FensterTitel = _mainWindow.DtAutoTests.PlcDaemon.Plc.GetPlcBezeichnung() + ": " + SpsVersionLokal;
            }

            Thread.Sleep(10);
        }
        // ReSharper disable once FunctionNeverReturns
    }
}