using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tiefgarage
{
    public partial class MainWindow
    {
        public bool BitmusterTesten(byte[] ByteArray, byte ByteNummer, UInt16 BitMuster)
        {
            if ((ByteArray[ByteNummer] & BitMuster) == BitMuster) return true;
            else return false;
        }

        void BitmusterSchreiben(bool Bedingung, byte[] ByteArray, byte ByteNummer, UInt16 BitMuster)
        {
            byte BitEin = (byte)(BitMuster & 0xFF);
            byte BitAus = (byte)(~BitMuster & 0xFF);

            if (Bedingung) ByteArray[ByteNummer] |= BitEin;
            else ByteArray[ByteNummer] &= BitAus;
        }

        void EinAusgabeFelderInitialisieren()
        {
            foreach (byte b in DigInput) { DigInput[b] = 0; }
            foreach (byte b in DigOutput) { DigOutput[b] = 0; }

            gAlleFahrzeugePersonen.Add(new AlleFahrzeugePersonen(btn_auto_1, Rolle.Auto, 10, 10, 110, 370, "Draussen parken"));
            gAlleFahrzeugePersonen.Add(new AlleFahrzeugePersonen(btn_auto_2, Rolle.Auto, 100, 10, 110, 420, "Draussen parken"));
            gAlleFahrzeugePersonen.Add(new AlleFahrzeugePersonen(btn_auto_3, Rolle.Auto, 200, 10, 110, 470, "Draussen parken"));
            gAlleFahrzeugePersonen.Add(new AlleFahrzeugePersonen(btn_auto_4, Rolle.Auto, 300, 10, 110, 520, "Draussen parken"));

            gAlleFahrzeugePersonen.Add(new AlleFahrzeugePersonen(btn_person_1, Rolle.Person, 500, 10, 210, 545, "Draussen parken"));
            gAlleFahrzeugePersonen.Add(new AlleFahrzeugePersonen(btn_person_2, Rolle.Person, 540, 10, 240, 545, "Draussen parken"));
            gAlleFahrzeugePersonen.Add(new AlleFahrzeugePersonen(btn_person_3, Rolle.Person, 580, 10, 270, 545, "Draussen parken"));
            gAlleFahrzeugePersonen.Add(new AlleFahrzeugePersonen(btn_person_4, Rolle.Person, 620, 10, 300, 545, "Draussen parken"));

            foreach (AlleFahrzeugePersonen fp in gAlleFahrzeugePersonen)
            {
                fp.draussenParken();
            }

        }

    }
}
