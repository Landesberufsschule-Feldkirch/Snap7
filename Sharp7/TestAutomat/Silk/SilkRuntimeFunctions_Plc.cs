using TestAutomat.Model;

namespace TestAutomat.Silk
{
    public partial class Silk
    {
        private static void PlcColdStart()
        {
            /*
            var status = Plc.ColdStart();
            if (status == 0) DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Erfolgreich, "PLC: Coldstart");
            else 
            */
            DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Fehler, 0, "PLC: ERROR Coldstart");
            AutoTesterWindow.DataGridId++;
        }
        private static void PlcHotStart()
        {
            /*
            var status = Plc.HotStart();
            if (status == 0) DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Erfolgreich, "PLC: Hotstart");            
            else */
            DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Fehler, 0, "PLC: ERROR Hotstart");
            AutoTesterWindow.DataGridId++;
        }
        private static void PlcGetStatus()
        {
            /*
            var (retval, status) = Plc.GetStatus();
            if (retval == 0)
            {
                switch (status)
                {
                    case 0x00:
                        DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.UnbekanntesErgebnis, "PLC: unbekannter Status");
                        break;
                    case 0x08:
                        DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Erfolgreich, "PLC: running");
                        break;
                    case 0x04:
                        DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.Fehler, "PLC: stopped");
                        break;
                }
            }
            else
            {
                DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.UnbekanntesErgebnis, "PLC: Statusabfrage fehlgeschlagen");
            }
            */
            DataGridAnzeigeUpdaten(AutoTester.TestErgebnis.UnbekanntesErgebnis, 0, "PLC: Statusabfrage fehlgeschlagen");
            AutoTesterWindow.DataGridId++;
        }
    }
}