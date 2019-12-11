using System;
using System.Threading.Tasks;

namespace WordClock
{
    public partial class MainWindow
    {
        public void Logikfunktionen_Task()
        {
            while (FensterAktiv)
            {
                Dispatcher.Invoke(() =>
                {
                    TimeSpan TimeSpan = new TimeSpan(0, 0, 0, 0, (int)sldGeschwindigkeit.Value);
                    Time = new TimeSpan(Time.Ticks + TimeSpan.Ticks);
                });

                DatumJahr = (ushort)DateTime.Now.Year;
                DatumMonat = (byte)DateTime.Now.Month;
                DatumTag = (byte) DateTime.Now.Day;
                DatumWochentag = (byte)DateTime.Now.DayOfWeek;
                Stunde = (byte)Time.Hours;
                Minute = (byte)Time.Minutes;
                Sekunde = (byte)Time.Seconds;
                Nanosekunde = 0;

                AnzeigeAktualisieren();

                Task.Delay(100);
            }
        }
    }
}
