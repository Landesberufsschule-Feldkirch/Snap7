using System.Windows.Controls;

namespace Tiefgarage
{
    public partial class FahrzeugPerson
    {
        public enum Rolle
        {
            Auto = 0,
            Person
        }
   
        public Rolle FP_Rolle { get; set; }
        public double X_aktuell { get; set; }
        public double Y_aktuell { get; set; }
        public double X_draussen { get; set; }
        public double Y_draussen { get; set; }
        public double X_drinnen { get; set; }
        public double Y_drinnen { get; set; }
        public FahrenRichtung Bewegung { get; set; } = FahrenRichtung.DraussenParken;

        public FahrzeugPerson( Rolle Rolle, double x_draussen, double y_draussen, double x_drinnen, double y_drinnen)
        {
            FP_Rolle = Rolle;
            X_draussen = x_draussen;
            Y_draussen = y_draussen;
            X_drinnen = x_drinnen;
            Y_drinnen = y_drinnen;
        }
         public void Losfahren()
        {

        }

        public void DraussenParken()
        {
            X_aktuell = X_draussen;
            Y_aktuell = Y_draussen;
        }
        public void DrinnenParken()
        {
            X_aktuell = X_drinnen;
            Y_aktuell = Y_drinnen;
        }
        public void UpdatePosition()
        {
           // BtnBezeichnung.SetValue(Canvas.LeftProperty, X_aktuell);
           // BtnBezeichnung.SetValue(Canvas.TopProperty, Y_aktuell);
        }

    }
}