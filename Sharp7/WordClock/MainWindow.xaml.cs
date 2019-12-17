using System;
using System.Windows;

namespace WordClock
{
    public partial class MainWindow : Window
    {

        public bool TaskAktiv;
        public bool DatenRangierenAktiv = true;
        public bool FensterAktiv = true;

        public TimeSpan Time;



        public MainWindow()
        {
            InitializeComponent();
            EinAusgabeFelderInitialisieren();

            System.Threading.Tasks.Task.Run(() => SPS_Pingen_Task());
            System.Threading.Tasks.Task.Run(() => Logikfunktionen_Task());
            System.Threading.Tasks.Task.Run(() => Display_Task());

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
    }
}