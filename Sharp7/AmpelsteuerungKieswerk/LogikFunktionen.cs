using System.Threading.Tasks;

namespace AmpelsteuerungKieswerk
{
    public partial class MainWindow
    {
        public enum AutomatikModus
        {
            Modus_1234 = 0,
            Modus_1324,
            Modus_1432,
            Modus_4321
        }

        public const double SinkGeschwindigkeit = 0.00003;
        public const double FuellGeschwindigkeit = 0.15 * SinkGeschwindigkeit; // damit alle Behälter gleichzeit leer sein können

        public double PegelOffset_1;
        public double PegelOffset_2;
        public double PegelOffset_3;
        public double PegelOffset_4;

        public const double HoeheFuellBalken = 200.0;

        public bool Tank_1_AutomatischEntleeren;
        public bool Tank_2_AutomatischEntleeren;
        public bool Tank_3_AutomatischEntleeren;
        public bool Tank_4_AutomatischEntleeren;

        public bool AutomatikModusNochAktiv = false;

        public void Logikfunktionen_Task()
        {
            while (FensterAktiv)
            {
                AutomatikModusNochAktiv = false;

                foreach (LKW lkw in gAlleLKW)
                {
 
                }

                if (!AutomatikModusNochAktiv) AutomatikKnoepfeAktivieren();

                this.Dispatcher.Invoke(() =>
                                   {
                                       foreach (LKW lkw in gAlleLKW) lkw.AnzeigeAktualisieren(FensterAktiv);
                                       AbleitungenAnzeigen(FensterAktiv);
                                   });

                Task.Delay(100);
            }
        }

        public void AutomatikBetriebStarten(AutomatikModus Modus)
        {
          
           
        }

    }
}
