using System.Threading.Tasks;

namespace Tiefgarage
{
    public partial class MainWindow
    {
        public void Logikfunktionen_Task()
        {
            while (FensterAktiv)
            {
                if (FahrzeugPersonGeklickt >= 0)
                {
                    LichtschrankenStatusBerechnen(gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].y_aktuell, gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].Rolle);
                    FahrzeugPersonenBewegen(gAlleFahrzeugePersonen[FahrzeugPersonGeklickt]);
                }

                AnzeigeAktualisieren();

                Task.Delay(100);
            }
        }
    }
}
