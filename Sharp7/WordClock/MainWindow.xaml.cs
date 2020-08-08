using Kommunikation;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WordClock
{
    public partial class MainWindow
    {
        public S7_1200 S71200 { get; set; }
        public string VersionInfo { get; set; }
        public string VersionNummer { get; set; }

        private const int AnzByteDigInput = 9;
        private const int AnzByteDigOutput = 0;
        private const int AnzByteAnalogInput = 0;
        private const int AnzByteAnalogOutput = 0;

        public MainWindow()
        {
            const string versionText = "WorkClock";
            VersionNummer = "V2.0";
            VersionInfo = versionText + " - " + VersionNummer;

            var viewModel = new ViewModel.ViewModel(this);

            var datenRangieren = new DatenRangieren(viewModel);

            InitializeComponent();
            DataContext = viewModel;

            S71200 = new S7_1200(VersionInfo.Length, AnzByteDigInput, AnzByteDigOutput, AnzByteAnalogInput, AnzByteAnalogOutput, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

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