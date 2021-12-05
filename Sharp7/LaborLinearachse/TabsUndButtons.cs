using System.Windows;
using System.Windows.Controls;

namespace LaborLinearachse;

public partial class MainWindow
{
    private void BetriebsartProjektChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not TabControl tc) return;
       
        // ReSharper disable once ConvertSwitchStatementToSwitchExpression
        switch (tc.SelectedIndex)
        {
            case 0:
                DtAutoTests.SetBetriebsartProjektLaborplatte();
                break;
            case 1:
                DtAutoTests.SetBetriebsartProjektSimulation();
                break;
            case 2:
                DtAutoTests.SetBetriebsartProjektAutoTest();
                break;
        }

        DtAutoTests?.DisplayPlc.SetBetriebsartProjekt(DtAutoTests?.Datenstruktur);
    }

    private void PlcButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (DtAutoTests == null) return;

        // ReSharper disable once PossibleNullReferenceException
        if (DtAutoTests.DisplayPlc.FensterAktiv) DtAutoTests.DisplayPlcFensterClose(); else DtAutoTests.DisplayPlcFensterOpen();
    }

    private void Schliessen()
    {
        if (DtAutoTests == null) return;

        DtAutoTests.DisplayPlc.TaskBeenden();
        DtAutoTests.TestAutomat.TaskBeenden();
        Application.Current.Shutdown();
    }
}