namespace TestAutomat
{
    public partial class PlotWindow
    {
        public bool FensterAktiv { get; set; }
        public PlotWindow()
        {
            InitializeComponent();

            Closing += (_, e) =>
            {
                e.Cancel = true;
                Schliessen();
            };
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
            MaxWidth = 1800;
            FensterAktiv = true;
        }
    }
}