using Kommunikation;
using System.Windows;
using System.Windows.Controls;
using AmpelsteuerungKieswerk.Model;

namespace AmpelsteuerungKieswerk
{
    public partial class MainWindow : Window
    {
        public bool TaskAktiv { get; set; }
        public bool DatenRangierenAktiv { get; set; } = true;
        public bool FensterAktiv { get; set; } = true;
        public int FahrzeugPersonGeklickt { get; set; } = -1;


        private ViewModel.AmpelsteuerungKieswerkViewModel ampelsteuerungKieswerkViewModel;

        public MainWindow()
        {

            ampelsteuerungKieswerkViewModel = new ViewModel.AmpelsteuerungKieswerkViewModel(this);

            InitializeComponent();
            DataContext = ampelsteuerungKieswerkViewModel;


            AlleLKWInitialisieren();

            DatenRangieren datenRangieren = new DatenRangieren(this);
            S7_1200 s7_1200 = new S7_1200(1, 1, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            System.Threading.Tasks.Task.Run(() => Logikfunktionen_Task());
            System.Threading.Tasks.Task.Run(() => Display_Task(s7_1200));
        }

   
    }
}