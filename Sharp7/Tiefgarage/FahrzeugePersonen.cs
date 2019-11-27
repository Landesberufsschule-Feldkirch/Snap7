using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Tiefgarage
{
    public partial class MainWindow : Window
    {       
        public enum Rolle
        {
            Auto = 0,
            Person
        }
        public enum FahrzeugPerson
        {
            Auto_1 = 0,
            Auto_2,
            Auto_3,
            Auto_4,
            Person_1,
            Person_2,
            Person_3,
            Person_4
        }

        public class AlleFahrzeugePersonen
        {
            private double xy_Delta = 0.1;
            public Button btnBezeichnung { get; set; }
            public Rolle Rolle { get; set; }
            public double x_aktuell { get; set; }
            public double y_aktuell { get; set; }
            public double x_alt { get; set; }
            public double y_alt { get; set; }
            public double x_draussen { get; set; }
            public double y_draussen { get; set; }
            public double x_drinnen { get; set; }
            public double y_drinnen { get; set; }
            public FahrenRichtung Bewegung { get; set; }
            public AlleFahrzeugePersonen(Button btnBezeichnung, Rolle Rolle, double x_draussen, double y_draussen, double x_drinnen, double y_drinnen, FahrenRichtung Bewegung)
            {
                this.btnBezeichnung = btnBezeichnung;
                this.Rolle = Rolle;
                this.x_draussen = x_draussen;
                this.y_draussen = y_draussen;
                this.x_drinnen = x_drinnen;
                this.y_drinnen = y_drinnen;
                this.Bewegung = Bewegung;

                x_alt = 0;
                y_alt = 0;
            }
            public void draussenParken()
            {
                x_aktuell = x_draussen;
                y_aktuell = y_draussen;
            }
            public void drinnenParken()
            {
                x_aktuell = x_drinnen;
                y_aktuell = y_drinnen;
            }
            public void updatePosition()
            {
                if (System.Math.Abs(x_alt - x_aktuell) > xy_Delta){
                    x_alt = x_aktuell;
                    btnBezeichnung.SetValue(Canvas.LeftProperty, x_aktuell);
                }

                if (System.Math.Abs(y_alt - y_aktuell) > xy_Delta)
                {
                    y_alt = y_aktuell;
                    btnBezeichnung.SetValue(Canvas.TopProperty, y_aktuell);
                }
            }

            public void btnAktivieren()
            {
                btnBezeichnung.IsEnabled = true;
            }
            public void btnDeaktivieren()
            {
                btnBezeichnung.IsEnabled = false;
            }
        }

        public ObservableCollection<AlleFahrzeugePersonen> gAlleFahrzeugePersonen = new ObservableCollection<AlleFahrzeugePersonen>();

        public void AlleFahrzeugePersonenInitialisieren()
        {
            gAlleFahrzeugePersonen.Add(new AlleFahrzeugePersonen(btn_auto_1, Rolle.Auto, 10, 10, 110, 370, FahrenRichtung.DraussenParken));
            gAlleFahrzeugePersonen.Add(new AlleFahrzeugePersonen(btn_auto_2, Rolle.Auto, 100, 10, 110, 420, FahrenRichtung.DraussenParken));
            gAlleFahrzeugePersonen.Add(new AlleFahrzeugePersonen(btn_auto_3, Rolle.Auto, 200, 10, 110, 470, FahrenRichtung.DraussenParken));
            gAlleFahrzeugePersonen.Add(new AlleFahrzeugePersonen(btn_auto_4, Rolle.Auto, 300, 10, 110, 520, FahrenRichtung.DraussenParken));

            gAlleFahrzeugePersonen.Add(new AlleFahrzeugePersonen(btn_person_1, Rolle.Person, 500, 10, 210, 545, FahrenRichtung.DraussenParken));
            gAlleFahrzeugePersonen.Add(new AlleFahrzeugePersonen(btn_person_2, Rolle.Person, 540, 10, 240, 545, FahrenRichtung.DraussenParken));
            gAlleFahrzeugePersonen.Add(new AlleFahrzeugePersonen(btn_person_3, Rolle.Person, 580, 10, 270, 545, FahrenRichtung.DraussenParken));
            gAlleFahrzeugePersonen.Add(new AlleFahrzeugePersonen(btn_person_4, Rolle.Person, 620, 10, 300, 545, FahrenRichtung.DraussenParken));

            alleDraussenParken();
        }

        public void alleDraussenParken()
        {
            foreach (AlleFahrzeugePersonen fp in gAlleFahrzeugePersonen) fp.draussenParken();
        }

        public void alleDrinnenParken()
        {
            foreach (AlleFahrzeugePersonen fp in gAlleFahrzeugePersonen) fp.drinnenParken();
        }
    }
}