using Kommunikation;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WordClock
{
    public partial class MainWindow
    {
        public S71200.BetriebsartProjekt BetriebsartProjekt { get; set; }
        public IPlc Plc { get; set; }
        public string VersionInfoLokal { get; set; }
        public string VersionNummer { get; set; }
        public Datenstruktur Datenstruktur { get; set; }

        private const int AnzByteDigInput = 9;
        private const int AnzByteDigOutput = 0;
        private const int AnzByteAnalogInput = 0;
        private const int AnzByteAnalogOutput = 0;

        public MainWindow()
        {
            BetriebsartProjekt = S71200.BetriebsartProjekt.LaborPlatte;
            
            const string versionText = "WorkClock";
            VersionNummer = "V2.0";
            VersionInfoLokal = versionText + " " + VersionNummer;

            Datenstruktur = new Datenstruktur(AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput)
            {
                VersionInputSps = Encoding.ASCII.GetBytes(VersionInfoLokal)
            };

            var viewModel = new ViewModel.ViewModel(this);

            var datenRangieren = new DatenRangieren(viewModel);

            InitializeComponent();
            DataContext = viewModel;

            Plc = new S71200(Datenstruktur, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            for (double i = 0; i < 360; i += 30) RotiertesRechteckHinzufuegen(8, 30, i);
            for (double i = 0; i < 360; i += 6) RotiertesRechteckHinzufuegen(2, 10, i);
        }

        private void RotiertesRechteckHinzufuegen(double breite, double hoehe, double winkel)
        {
            var rect = new Rectangle
            {
                Width = breite,
                Height = hoehe,
                Fill = Brushes.Black
            };

            var rotateTransform = new RotateTransform(winkel, breite / 2, 150);
            rect.RenderTransform = rotateTransform;

            Canvas.SetLeft(rect, 150 - breite / 2);
            Canvas.SetTop(rect, 0);

            CanvasUhr.Children.Add(rect);
        }
    }
}