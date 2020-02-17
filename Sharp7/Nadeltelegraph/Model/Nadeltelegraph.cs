using System;
using System.Windows.Controls;

namespace Nadeltelegraph.Model
{
    public class Nadeltelegraph
    {
        private readonly MainWindow mainWindow;

        public bool TasteA { get; set; }
        public bool TasteB { get; set; }
        public bool TasteD { get; set; }
        public bool TasteE { get; set; }
        public bool TasteF { get; set; }
        public bool TasteG { get; set; }
        public bool TasteH { get; set; }
        public bool TasteI { get; set; }
        public bool TasteK { get; set; }
        public bool TasteL { get; set; }
        public bool TasteM { get; set; }
        public bool TasteN { get; set; }
        public bool TasteO { get; set; }
        public bool TasteP { get; set; }
        public bool TasteR { get; set; }
        public bool TasteS { get; set; }
        public bool TasteT { get; set; }
        public bool TasteV { get; set; }
        public bool TasteW { get; set; }
        public bool TasteY { get; set; }


        public Nadeltelegraph(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        internal void BuchstabeA() { TasteA = ButtonFunktionPressReleaseAendern(mainWindow.btnA); }
        internal void BuchstabeB() { TasteB = ButtonFunktionPressReleaseAendern(mainWindow.btnB); }
        internal void BuchstabeD() { TasteD = ButtonFunktionPressReleaseAendern(mainWindow.btnD); }
        internal void BuchstabeE() { TasteE = ButtonFunktionPressReleaseAendern(mainWindow.btnE); }
        internal void BuchstabeF() { TasteF = ButtonFunktionPressReleaseAendern(mainWindow.btnF); }
        internal void BuchstabeG() { TasteG = ButtonFunktionPressReleaseAendern(mainWindow.btnG); }
        internal void BuchstabeH() { TasteH = ButtonFunktionPressReleaseAendern(mainWindow.btnH); }
        internal void BuchstabeI() { TasteI = ButtonFunktionPressReleaseAendern(mainWindow.btnI); }
        internal void BuchstabeK() { TasteK = ButtonFunktionPressReleaseAendern(mainWindow.btnK); }
        internal void BuchstabeL() { TasteL = ButtonFunktionPressReleaseAendern(mainWindow.btnL); }
        internal void BuchstabeM() { TasteM = ButtonFunktionPressReleaseAendern(mainWindow.btnM); }
        internal void BuchstabeN() { TasteN = ButtonFunktionPressReleaseAendern(mainWindow.btnN); }
        internal void BuchstabeO() { TasteO = ButtonFunktionPressReleaseAendern(mainWindow.btnO); }
        internal void BuchstabeP() { TasteP = ButtonFunktionPressReleaseAendern(mainWindow.btnP); }
        internal void BuchstabeR() { TasteR = ButtonFunktionPressReleaseAendern(mainWindow.btnR); }
        internal void BuchstabeS() { TasteS = ButtonFunktionPressReleaseAendern(mainWindow.btnS); }
        internal void BuchstabeT() { TasteT = ButtonFunktionPressReleaseAendern(mainWindow.btnT); }
        internal void BuchstabeV() { TasteV = ButtonFunktionPressReleaseAendern(mainWindow.btnV); }
        internal void BuchstabeW() { TasteW = ButtonFunktionPressReleaseAendern(mainWindow.btnW); }
        internal void BuchstabeY() { TasteY = ButtonFunktionPressReleaseAendern(mainWindow.btnY); }

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
