using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WordClock
{

    public partial class MainWindow : Window
    {

        public bool TaskAktiv;
        public bool DatenRangierenAktiv = true;
        public bool FensterAktiv = true;
        TimeSpan Time;
        System.Timers.Timer timer = new System.Timers.Timer(100);
        public MainWindow()
        {
            InitializeComponent();
            EinAusgabeFelderInitialisieren();
            System.Threading.Tasks.Task.Run(() => SPS_Pingen_Task());
            System.Threading.Tasks.Task.Run(() => Logikfunktionen_Task());

            DateTime date = DateTime.Now;
            TimeZone time = TimeZone.CurrentTimeZone;
            TimeSpan difference = time.GetUtcOffset(date);
            Time = difference;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Enabled = true;

        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            TimeSpan time = new TimeSpan(0, 0, 10);
            Time = new TimeSpan(Time.Ticks + time.Ticks);

            Dispatcher.Invoke(() =>
            {
                secondHand.Angle =Time.Seconds * 6;
                minuteHand.Angle = Time.Minutes * 6;
                hourHand.Angle = Time.Hours * 30 + Time.Minutes * 0.5;
            });
        }

        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            VerbindungErstellen();
        }

        private void ButtonDisconnect_Click(object sender, RoutedEventArgs e)
        {
            VerbindungTrennen();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FensterAktiv = false;
        }
    }
}