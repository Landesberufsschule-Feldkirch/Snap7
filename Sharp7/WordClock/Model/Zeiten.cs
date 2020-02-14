using System;
using System.Diagnostics;
using System.Threading;

namespace WordClock.Model
{
    public class Zeiten
    {
        public ushort DatumJahr { get; set; }
        public byte DatumMonat { get; set; }
        public byte DatumTag { get; set; }
        public byte DatumWochentag { get; set; }
        public byte Stunde { get; set; }
        public byte Minute { get; set; }
        public byte Sekunde { get; set; }

        public VisuAnzeigen ViAnzeige { get; set; }
        public VisuSollwerte ViSoll { get; set; }

        private TimeSpan timeSpan;

        public Zeiten()
        {
            ViAnzeige = new VisuAnzeigen();
            ViSoll = new VisuSollwerte();

            DateTime dateTime = DateTime.Now;
            timeSpan = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second);

            DatumJahr = (ushort)dateTime.Year;
            DatumMonat = (byte)dateTime.Month;
            DatumTag = (byte)dateTime.Day;
            DatumWochentag = (byte)dateTime.DayOfWeek;
            Stunde = (byte)dateTime.Hour;
            Minute = (byte)dateTime.Minute;
            Sekunde = (byte)dateTime.Second;

            ViAnzeige = new VisuAnzeigen();

            System.Threading.Tasks.Task.Run(() => ZeitenTask());
        }

        private void ZeitenTask()
        {
            Stopwatch stopwatch = new Stopwatch();

            while (true)
            {
                var elapsed = (int)stopwatch.ElapsedMilliseconds;
                stopwatch.Restart();

                TimeSpan tSpan = new TimeSpan(0, 0, 0, 0, elapsed * ViSoll.GeschwindigkeitZeit());
                timeSpan = new TimeSpan(timeSpan.Ticks + tSpan.Ticks);

                Stunde = (byte)timeSpan.Hours;
                Minute = (byte)timeSpan.Minutes;
                Sekunde = (byte)timeSpan.Seconds;

                AnzeigeAktualisieren();
                Thread.Sleep(10);
                stopwatch.Stop();
            }
        }

        private void AnzeigeAktualisieren()
        {
            ViAnzeige.WinkelSekunden = Sekunde * 6;
            ViAnzeige.WinkelMinuten = Minute * 6;
            ViAnzeige.WinkelStunden = Stunde * 30 + Minute * 0.5;
        }

        internal void SetCurrentTime()
        {
            DateTime dateTime = DateTime.Now;
            timeSpan = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second);
        }
    }
}