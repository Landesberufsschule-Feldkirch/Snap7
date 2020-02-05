using Kommunikation;
using System.Windows;
using System.Windows.Controls;

namespace LAP_2018_Abfuellanlage
{
    public partial class MainWindow : Window
    {
        public bool B1;
        public bool F5 = true;
        public bool M1;
        public bool M2;



        public bool P1;
        public bool P2;

        public bool K1;
        public bool K2;

        public byte PegelByte;
        public ushort PegelInt;

        public double Pegel = 1;
        public bool FensterAktiv = true;

        private Logikfunktionen logikfunktionen;
        private DatenRangieren datenRangieren;

        private LAP_2018_Abfuellanlage.ViewModel.AbfuellanlageViewModel abfuellanlageViewModel;

        public MainWindow()
        {
            abfuellanlageViewModel = new ViewModel.AbfuellanlageViewModel(this);

            logikfunktionen = new Logikfunktionen(this);
            datenRangieren = new DatenRangieren(this);

            InitializeComponent();
            DataContext = abfuellanlageViewModel;

           // AlleFlaschenInitialisieren();
          //  AlleFlaschenParken();

            S7_1200 s7_1200 = new S7_1200(2, 2, 4, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            System.Threading.Tasks.Task.Run(() => logikfunktionen.Logikfunktionen_Task());
        }


      
    }
}