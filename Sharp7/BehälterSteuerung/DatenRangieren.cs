using Sharp7;

namespace BehälterSteuerung
{
    public class DatenRangieren
    {
        MainWindow mainWindow;

        enum BitPosAusgang
        {
            Q1 = 0,
            Q3,
            Q5,
            Q7,
            P1
        }

        public void RangierenInput(byte[] digInput, byte[] anInput)
        {
            foreach (Behaelter beh in mainWindow.gAlleBehaelter) beh.BehalterDatenRangierenInput(digInput);
        }

        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
        {
            mainWindow.Leuchte_P1 = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);
            foreach (Behaelter beh in mainWindow.gAlleBehaelter) beh.BehalterDatenRangierenOutput(digOutput);
        }

        public DatenRangieren(MainWindow window)
        {
            mainWindow = window;
        }

    }
}
