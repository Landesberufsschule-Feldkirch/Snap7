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
        public double X_aktuell { get; set; }
        public double Y_aktuell { get; set; }
        private readonly double X_oben;
        private readonly double Y_oben;
        private readonly double X_unten;
        private readonly double Y_unten;

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
            FP_Rolle = Rolle;

            Y_oben = Y_FahrzeugPersonen_Oben;

            if (FP_Rolle == Rolle.Fahrzeug)
            {
                X_oben = X_ErstesFahrzeug_Oben + AnzahlFahrzeuge * BreiteFahrzeug;
                if (AnzahlFahrzeuge < 4)
                {
                    X_unten = X_Fahrzeuge_Unten_Links;
                    Y_unten = Y_ErstesFahrzeug_Unten + AnzahlFahrzeuge * HoeheFahrzeug;

                    KurveUnten = new BezierCurve(X_Fahrspur, Y_Fahrspur_unten,
                        X_Fahrspur, Y_Fahrspur_unten + 100,
                        X_unten + 100, Y_unten,
                        X_unten, Y_unten
                        );
                }
                else
                {
                    X_unten = X_Fahrzeuge_Unten_Rechts;
                    Y_unten = Y_ErstesFahrzeug_Unten + (AnzahlFahrzeuge - 4) * HoeheFahrzeug;

                    KurveUnten = new BezierCurve(X_Fahrspur, Y_Fahrspur_unten,
                        X_Fahrspur, Y_Fahrspur_unten + 100,
                        X_unten - 100, Y_unten,
                        X_unten, Y_unten
                        );
                }
                AnzahlFahrzeuge++;
            }
            else
            {
                X_oben = X_ErstePerson_Oben + AnzahlPersonen * BreitePerson;
                X_unten = X_ErstePerson_Unten + AnzahlPersonen * BreitePerson;
                Y_unten = Y_Personen_Unten;
                AnzahlPersonen++;

                KurveUnten = new BezierCurve(X_Fahrspur, Y_Fahrspur_unten,
                       X_Fahrspur, Y_Fahrspur_unten + 100,
                       X_unten, Y_unten - 100,
                       X_unten, Y_unten
                       );
            }

            KurveOben = new BezierCurve(X_oben, Y_oben,
                X_oben, Y_oben + 100,
                X_Fahrspur, Y_Fahrspur_oben - 100,
                X_Fahrspur, Y_Fahrspur_oben
                );

            DraussenParken();

        }
        public void Losfahren()
        {
            if (Bewegung == FahrenRichtung.ObenGeparkt) Bewegung = FahrenRichtung.AbwaertsKurveOben;
            if (Bewegung == FahrenRichtung.UntenGeparkt) Bewegung = FahrenRichtung.AufwaertsKurveUnten;
        }

        public void DraussenParken()
        {
            Bewegung = FahrenRichtung.ObenGeparkt;
        }
        public void DrinnenParken()
        {
            Bewegung = FahrenRichtung.UntenGeparkt;
        }

        bool LichtschrankeUnterbrochen(double Pos)
        {
            if (FP_Rolle == Rolle.Fahrzeug)
            {
                if (Y_aktuell < Pos) return false;
                if (Y_aktuell > Pos + Y_LichtschrankenHoehFahrzeug) return false;
            }
            else
            {
                if (Y_aktuell < Pos) return false;
                if (Y_aktuell > Pos + Y_LichtschrankenHoehePerson) return false;
            }
            return true;
        }

    }
}