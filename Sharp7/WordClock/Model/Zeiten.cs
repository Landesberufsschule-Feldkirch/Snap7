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

        private double _geschwindigkeitZeit;
        private TimeSpan _timeSpan;

        public Zeiten()
        {
            _geschwindigkeitZeit = 1;

            DateTime dateTime = DateTime.Now;
            _timeSpan = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second);

            DatumJahr = (ushort)dateTime.Year;
            DatumMonat = (byte)dateTime.Month;
            DatumTag = (byte)dateTime.Day;
            DatumWochentag = (byte)dateTime.DayOfWeek;
            Stunde = (byte)dateTime.Hour;
            Minute = (byte)dateTime.Minute;
            Sekunde = (byte)dateTime.Second;

            System.Threading.Tasks.Task.Run(ZeitenTask);
        }

        private void ZeitenTask()
        {
            Stopwatch stopwatch = new Stopwatch();

            while (true)
            {
                var elapsed = (int)stopwatch.ElapsedMilliseconds;
                stopwatch.Restart();

                var tSpan = new TimeSpan(0, 0, 0, 0, elapsed * (int)_geschwindigkeitZeit);
                _timeSpan = new TimeSpan(_timeSpan.Ticks + tSpan.Ticks);

                Stunde = (byte)_timeSpan.Hours;
                Minute = (byte)_timeSpan.Minutes;
                Sekunde = (byte)_timeSpan.Seconds;

                Thread.Sleep(10);
                stopwatch.Stop();
            }
        }

        internal void SetCurrentTime()
        {
            var dateTime = DateTime.Now;
            _timeSpan = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        internal int GetSekunde() => Sekunde;

        internal int GetMinute() => Minute;

        internal int GetStunde() => Stunde;

        internal void SetGeschwindigkeit(double geschwindigkeitSlider) => _geschwindigkeitZeit = geschwindigkeitSlider;
    }
}