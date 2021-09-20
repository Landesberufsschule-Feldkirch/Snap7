using Kommunikation;

namespace _3dHofschiebetor
{
    public class DatenRangieren
    {
        private readonly MainWindow _mainWindow;
        private readonly ViewModel.ViewModel _viewModel;
        private IPlc _plc;

        private enum BitPosAusgang
        {
            Q1 = 0,     // 0.0  Linearachse Rechtslauf
            Q2 = 1,     // 0.1  Linearachse Linkslauf
            P1 = 2,     // 0.2  Meldeleuchte im Taster S1/S2 (weiß) 
            P2 = 3,     // 0.3  Meldeleuchte weiß
            P3 = 4,     // 0.4  Meldeleuchte rot
            P4 = 5       // 0.5  Meldeleuchte grün

        }

        private enum BitPosEingang
        {
            B1 = 0,     // 0.0  Linearachse Endlage links → Öffner
            B2 = 1,     // 0.1  Linearachse Endlage rechts → Öffner
            S2 = 2,     // 0.2  Taster ( ⓪ ) → Öffner 
            S1 = 3,     // 0.3  Taster ( ① ) → Schliesser 
            S4 = 4,     // 0.4  Taster ( Ⅱ ) → Schliesser 
            S3 = 5,     // 0.5  Taster ( Ⅰ ) → Schliesser
            S6 = 6,     // 0.6  Taster ( ↓ ) → Schliesser
            S5 = 7,     // 0.7  Taster ( ↑ ) → Schliesser 
            S8 = 8,     // 1.0  Taster ( － ) → Schliesser 
            S7 = 9,     // 1.1  Taster ( + ) → Schliesser 
            S9 = 10,    // 1.2  Taster ( STOP ) → Öffner 
            S10 = 11,   // 1.3  Not-Halt → Öffner
            S11 = 12     // 1.4  Not-Halt → Schliesser 

        }

        public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
               
            }

          
        }

        public DatenRangieren( MainWindow mw,  ViewModel.ViewModel vm)
        {
            _mainWindow = mw;
            _viewModel = vm;
        }
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}