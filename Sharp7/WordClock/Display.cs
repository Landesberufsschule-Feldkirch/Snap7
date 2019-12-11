namespace WordClock
{
    public partial class MainWindow
    {
        public void AnzeigeAktualisieren()
        {
            this.Dispatcher.Invoke(() =>
                       {
                           if (FensterAktiv)
                           {
                               secondHand.Angle = Time.Seconds * 6;
                               minuteHand.Angle = Time.Minutes * 6;
                               hourHand.Angle = Time.Hours * 30 + Time.Minutes * 0.5;
                           }
                       });
        }

    }
}
