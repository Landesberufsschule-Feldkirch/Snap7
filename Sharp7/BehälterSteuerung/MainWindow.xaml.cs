using Kommunikation;
using System.Windows;

namespace BehaelterSteuerung
{
    public partial class MainWindow : Window
    {
     

        public bool FensterAktiv = true;
        public bool Leuchte_P1 = false;

        public double PegelOffset_1;
        public double PegelOffset_2;
        public double PegelOffset_3;
        public double PegelOffset_4;

        public bool Tank_1_AutomatischEntleeren;
        public bool Tank_2_AutomatischEntleeren;
        public bool Tank_3_AutomatischEntleeren;
        public bool Tank_4_AutomatischEntleeren;

       

        private DatenRangieren datenRangieren;

        private BehaelterSteuerung.ViewModel.BehaelterViewModel behaelterViewModel;

        public MainWindow()
        {
            behaelterViewModel = new BehaelterSteuerung.ViewModel.BehaelterViewModel();

            datenRangieren = new DatenRangieren(this);

            InitializeComponent();
            DataContext = behaelterViewModel;


            S7_1200 s7_1200 = new S7_1200(1, 1, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            System.Threading.Tasks.Task.Run(() => Display_Task());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FensterAktiv = false;
        }
        /*
      

        public void AutomatikBetriebStarten(AutomatikModus Modus)
        {
           
            switch (Modus)
           
            
        }
    */
    }
}