namespace TestAutomat
{
    public partial class PlotWindow
    {
        public bool FensterAktiv { get; set; }
        public PlotWindow()
        {
            InitializeComponent();
        }

        public void Schliessen()
        {
            FensterAktiv = false;
            Hide();
        }
        public void Oeffnen()
        {
            Show();
            Title = "Plotter";
            MaxWidth = 1200;
            FensterAktiv = true;
        }
    }
}