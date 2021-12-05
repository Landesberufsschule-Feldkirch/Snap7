using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace LaborLinearachse.ViewModel;

public partial class ViewModel
{
    private void VisuAnzeigenTask()
    {
        while (true)
        {
            PositionSchlitten = _linearachse.PositionSchlitten;

            SichtbarkeitUmschalten(_linearachse.B1, 1);
            SichtbarkeitUmschalten(_linearachse.B2, 2);

            FarbeUmschalten(_linearachse.P1, 3, Brushes.White, Brushes.LightGray);
            FarbeUmschalten(_linearachse.P2, 4, Brushes.White, Brushes.LightGray);
            FarbeUmschalten(_linearachse.P3, 5, Brushes.Red, Brushes.LightGray);
            FarbeUmschalten(_linearachse.P4, 6, Brushes.GreenYellow, Brushes.LightGray);

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