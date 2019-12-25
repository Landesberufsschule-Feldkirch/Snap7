using Kommunikation;
using System;
using System.Threading;
using System.Windows;

namespace WordClock
{
    public partial class MainWindow : Window
    {
        public bool FensterAktiv = true;
        private TimeSpan Time;
        readonly DatenRangieren datenRangieren = new DatenRangieren();

        public MainWindow()
        {
            InitializeComponent();

            S7_1200 s7_1200 = new S7_1200(10, 0, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            System.Threading.Tasks.Task.Run(() => ZeitAktualisierenTask());
            System.Threading.Tasks.Task.Run(() => Display_Task(s7_1200, datenRangieren));

            DateTime dateTime = DateTime.Now;
            Time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FensterAktiv = false;
        }

        private void ZeitUebernehmen_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            Time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        private void ZeitAktualisierenTask()
        {
            while (FensterAktiv)
            {
                Dispatcher.Invoke(() =>
                {
                    TimeSpan TimeSpan = new TimeSpan(0, 0, 0, 0, (int)this.sldGeschwindigkeit.Value);
                    Time = new TimeSpan(Time.Ticks + TimeSpan.Ticks);
                });

                datenRangieren.DatumJahr = (ushort)DateTime.Now.Year;
                datenRangieren.DatumMonat = (byte)DateTime.Now.Month;
                datenRangieren.DatumTag = (byte)DateTime.Now.Day;
                datenRangieren.DatumWochentag = (byte)DateTime.Now.DayOfWeek;
                datenRangieren.Stunde = (byte)Time.Hours;
                datenRangieren.Minute = (byte)Time.Minutes;
                datenRangieren.Sekunde = (byte)Time.Seconds;
                datenRangieren.Nanosekunde = 0;

                Thread.Sleep(10);
            }
        }
    }
}