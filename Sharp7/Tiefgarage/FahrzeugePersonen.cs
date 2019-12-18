using Utilities;

namespace Tiefgarage
{
    public partial class FahrzeugPerson
    {
        public enum Rolle
        {
            Fahrzeug = 0,
            Person
        }

        static int AnzahlFahrzeuge = 0;
        static int AnzahlPersonen = 0;

        public Rolle FP_Rolle { get; set; }
        public Punkt PosAktuell { get; set; }
        public readonly Punkt ParkenOben;
        public readonly Punkt ParkenUnten;
        public readonly Punkt EingangOben;
        public readonly Punkt EingangUnten;

        private readonly BezierCurve KurveOben;
        private readonly BezierCurve KurveUnten;

        private readonly double X_ErstesFahrzeug_Oben = 10;
        private readonly double X_ErstePerson_Oben = 500;
        private readonly double Y_FahrzeugPersonen_Oben = 10;
        private readonly double X_Fahrzeuge_Unten_Links = 110;
        private readonly double X_Fahrzeuge_Unten_Rechts = 810;
        private readonly double Y_ErstesFahrzeug_Unten = 500;

        private readonly double X_ErstePerson_Unten = 250;
        private readonly double Y_Personen_Unten = 650;

        private readonly double BreiteFahrzeug = 100;
        private readonly double HoeheFahrzeug = 50;

        private readonly double BreitePerson = 25;

        private readonly double X_Fahrspur = 350;
        private readonly double Y_Fahrspur_oben = 250;
        private readonly double Y_Fahrspur_unten = 400;

        private readonly double Y_LichtschrankenHoehePerson = 15;
        private readonly double Y_LichtschrankenHoehFahrzeug = 50;

        private readonly double Y_Position_B1 = 300;
        private readonly double Y_Position_B2 = 330;

        public FahrenRichtung Bewegung { get; set; } = FahrenRichtung.ObenGeparkt;

        public FahrzeugPerson(Rolle Rolle)
        {
            double X_oben;
            double Y_oben;
            double X_unten;
            double Y_unten;

            Punkt KontrollPunktUnten1;
            Punkt KontrollPunktUnten2;

            FP_Rolle = Rolle;

            Y_oben = Y_FahrzeugPersonen_Oben;

            if (FP_Rolle == Rolle.Fahrzeug)
            {
                X_oben = X_ErstesFahrzeug_Oben + AnzahlFahrzeuge * BreiteFahrzeug;
                if (AnzahlFahrzeuge < 4)
                {
                    X_unten = X_Fahrzeuge_Unten_Links;
                    Y_unten = Y_ErstesFahrzeug_Unten + AnzahlFahrzeuge * HoeheFahrzeug;

                    KontrollPunktUnten1 = new Punkt(X_Fahrspur, Y_Fahrspur_unten + 100);
                    KontrollPunktUnten2 = new Punkt(X_unten + 100, Y_unten);
                }
                else
                {
                    X_unten = X_Fahrzeuge_Unten_Rechts;
                    Y_unten = Y_ErstesFahrzeug_Unten + (AnzahlFahrzeuge - 4) * HoeheFahrzeug;

                    KontrollPunktUnten1 = new Punkt(X_Fahrspur, Y_Fahrspur_unten + 100);
                    KontrollPunktUnten2 = new Punkt(X_unten - 100, Y_unten);
                }
                AnzahlFahrzeuge++;
            }
            else
            {
                X_oben = X_ErstePerson_Oben + AnzahlPersonen * BreitePerson;
                X_unten = X_ErstePerson_Unten + AnzahlPersonen * BreitePerson;
                Y_unten = Y_Personen_Unten;


                KontrollPunktUnten1 = new Punkt(X_Fahrspur, Y_Fahrspur_unten + 100);
                KontrollPunktUnten2 = new Punkt(X_unten, Y_unten - 100);

                AnzahlPersonen++;
            }

            ParkenOben = new Punkt(X_oben, Y_oben);
            Punkt KontrollPunktOben1 = new Punkt(X_oben, Y_oben + 100);
            Punkt KontrollPunktOben2 = new Punkt(X_Fahrspur, Y_Fahrspur_oben - 100);
            EingangOben = new Punkt(X_Fahrspur, Y_Fahrspur_oben);

            EingangUnten = new Punkt(X_Fahrspur, Y_Fahrspur_unten);
            ParkenUnten = new Punkt(X_unten, Y_unten);


            KurveOben = new BezierCurve(ParkenOben, KontrollPunktOben1, KontrollPunktOben2, EingangOben);
            KurveUnten = new BezierCurve(EingangUnten, KontrollPunktUnten1, KontrollPunktUnten2, ParkenUnten);

            DraussenParken();
        }
        public void Losfahren()
        {
            if (Bewegung == FahrenRichtung.ObenGeparkt) Bewegung = FahrenRichtung.AbwaertsKurveOben;
            if (Bewegung == FahrenRichtung.UntenGeparkt) Bewegung = FahrenRichtung.AufwaertsKurveUnten;
        }

        public void DraussenParken() { Bewegung = FahrenRichtung.ObenGeparkt; }
        public void DrinnenParken() { Bewegung = FahrenRichtung.UntenGeparkt; }

        bool LichtschrankeUnterbrochen(double Pos)
        {
            if (FP_Rolle == Rolle.Fahrzeug)
            {
                if (PosAktuell.Y < Pos) return false;
                if (PosAktuell.Y > Pos + Y_LichtschrankenHoehFahrzeug) return false;
            }
            else
            {
                if (PosAktuell.Y < Pos) return false;
                if (PosAktuell.Y > Pos + Y_LichtschrankenHoehePerson) return false;
            }
            return true;
        }

    }
}