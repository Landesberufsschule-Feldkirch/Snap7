namespace WordClock.ViewModel
{
    using System;
    using System.Windows.Input;
    using WordClock.Commands;

    public class WordClockViewModel
    {
        public readonly Model.Zeiten zeiten;

        private int Geschwindigkeit = 1;
        private TimeSpan timeSpan;

        public WordClockViewModel(MainWindow mainWindow)
        {

            DateTime dateTime = DateTime.Now;
            timeSpan = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second);

            TimeSpan tSpan = new TimeSpan(0, 0, 0, 0, 10 * Geschwindigkeit);
            timeSpan = new TimeSpan(timeSpan.Ticks + tSpan.Ticks);

            zeiten = new Model.Zeiten((ushort)DateTime.Now.Year, (byte)DateTime.Now.Month, (byte)DateTime.Now.Day, (byte)DateTime.Now.DayOfWeek,
                                    (byte)timeSpan.Hours, (byte)timeSpan.Minutes, (byte)timeSpan.Seconds, 0);

            BtnSetActualTime = new WordClockSetActualTime(this);
            SliderSetSpeed = new WordClockSetSpeed(this);
        }

        public Model.Zeiten Zeiten { get { return zeiten; } }


        public bool CanSetActualTime { get { return true; } }
        public bool CanSetSpeed { get { return true; } }

        public ICommand BtnSetActualTime { get; private set; }
        public ICommand SliderSetSpeed { get; private set; }

        public void SetActualTime() { zeiten.SetActualtime(); }
        public void SetSpeed() { zeiten.SetSpeed(); }
    }
}
