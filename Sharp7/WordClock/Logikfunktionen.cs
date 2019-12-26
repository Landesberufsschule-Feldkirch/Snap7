using System;
using System.Threading;

namespace WordClock
{
    public class Logikfunktionen
    {
        private int Geschwindigkeit = 1;
        private TimeSpan timeSpan;
        private Zeiten zeiten;

        public Logikfunktionen()
        {
            DateTime dateTime = DateTime.Now;
            timeSpan = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        public void LogikFunktionenTask(bool fensterAktiv)
        {
            while (fensterAktiv)
            {
                TimeSpan tSpan = new TimeSpan(0, 0, 0, 0, 10 * Geschwindigkeit);
                timeSpan = new TimeSpan(timeSpan.Ticks + tSpan.Ticks);

                zeiten = new Zeiten((ushort)DateTime.Now.Year, (byte)DateTime.Now.Month, (byte)DateTime.Now.Day, (byte)DateTime.Now.DayOfWeek,
                                    (byte)timeSpan.Hours, (byte)timeSpan.Minutes, (byte)timeSpan.Seconds, 0);

                Thread.Sleep(10);
            }
        }

        public void setGeschwindigkeit(double wert)
        {
            Geschwindigkeit = (int)wert;
        }

        public void ZeitUebernehmen()
        {
            DateTime dateTime = DateTime.Now;
            timeSpan = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        public Zeiten getZeit()
        {
            return zeiten;
        }
    }
}
