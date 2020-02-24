using System.Windows.Controls;

namespace Nadeltelegraph.Model
{
    public class Nadeltelegraph
    {
        public char Zeichen { get; set; }
        public bool P0 { get; set; }
        public bool P1L { get; set; }
        public bool P1R { get; set; }
        public bool P2L { get; set; }
        public bool P2R { get; set; }
        public bool P3L { get; set; }
        public bool P3R { get; set; }
        public bool P4L { get; set; }
        public bool P4R { get; set; }
        public bool P5L { get; set; }
        public bool P5R { get; set; }

        MainWindow mainWindow;
        public Nadeltelegraph(MainWindow mw)
        {
            mainWindow = mw;
            Zeichen = ' ';
        }

      
        internal void BuchstabeA() { if (ButtonFunktionPressReleaseAendern(mainWindow.btnA)) Zeichen = 'A'; else Zeichen = ' '; }
        internal void BuchstabeB() { if (ButtonFunktionPressReleaseAendern(mainWindow.btnB)) Zeichen = 'B'; else Zeichen = ' '; }
        internal void BuchstabeD() { if (ButtonFunktionPressReleaseAendern(mainWindow.btnD)) Zeichen = 'D'; else Zeichen = ' '; }
        internal void BuchstabeE() { if (ButtonFunktionPressReleaseAendern(mainWindow.btnE)) Zeichen = 'E'; else Zeichen = ' '; }
        internal void BuchstabeF() { if (ButtonFunktionPressReleaseAendern(mainWindow.btnF)) Zeichen = 'F'; else Zeichen = ' '; }
        internal void BuchstabeG() { if (ButtonFunktionPressReleaseAendern(mainWindow.btnG)) Zeichen = 'G'; else Zeichen = ' '; }
        internal void BuchstabeH() { if (ButtonFunktionPressReleaseAendern(mainWindow.btnH)) Zeichen = 'H'; else Zeichen = ' '; }
        internal void BuchstabeI() { if (ButtonFunktionPressReleaseAendern(mainWindow.btnI)) Zeichen = 'I'; else Zeichen = ' '; }
        internal void BuchstabeK() { if (ButtonFunktionPressReleaseAendern(mainWindow.btnK)) Zeichen = 'K'; else Zeichen = ' '; }
        internal void BuchstabeL() { if (ButtonFunktionPressReleaseAendern(mainWindow.btnL)) Zeichen = 'L'; else Zeichen = ' '; }
        internal void BuchstabeM() { if (ButtonFunktionPressReleaseAendern(mainWindow.btnM)) Zeichen = 'M'; else Zeichen = ' '; }
        internal void BuchstabeN() { if (ButtonFunktionPressReleaseAendern(mainWindow.btnN)) Zeichen = 'N'; else Zeichen = ' '; }
        internal void BuchstabeO() { if (ButtonFunktionPressReleaseAendern(mainWindow.btnO)) Zeichen = 'O'; else Zeichen = ' '; }
        internal void BuchstabeP() { if (ButtonFunktionPressReleaseAendern(mainWindow.btnP)) Zeichen = 'P'; else Zeichen = ' '; }
        internal void BuchstabeR() { if (ButtonFunktionPressReleaseAendern(mainWindow.btnR)) Zeichen = 'R'; else Zeichen = ' '; }
        internal void BuchstabeS() { if (ButtonFunktionPressReleaseAendern(mainWindow.btnS)) Zeichen = 'S'; else Zeichen = ' '; }
        internal void BuchstabeT() { if (ButtonFunktionPressReleaseAendern(mainWindow.btnT)) Zeichen = 'T'; else Zeichen = ' '; }
        internal void BuchstabeV() { if (ButtonFunktionPressReleaseAendern(mainWindow.btnV)) Zeichen = 'V'; else Zeichen = ' '; }
        internal void BuchstabeW() { if (ButtonFunktionPressReleaseAendern(mainWindow.btnW)) Zeichen = 'W'; else Zeichen = ' '; }
        internal void BuchstabeY() { if (ButtonFunktionPressReleaseAendern(mainWindow.btnY)) Zeichen = 'Y'; else Zeichen = ' '; }

        private bool ButtonFunktionPressReleaseAendern(Button knopf)
        {
            if (knopf.ClickMode == System.Windows.Controls.ClickMode.Press)
            {
                knopf.ClickMode = System.Windows.Controls.ClickMode.Release;
                return true;
            }
            else
            {
                knopf.ClickMode = System.Windows.Controls.ClickMode.Press;
                return false;
            }
        }
    }
}
