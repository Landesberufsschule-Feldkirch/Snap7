using System.Threading;

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
                   // LichtschrankenStatusBerechnen(gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].Y_aktuell, gAlleFahrzeugePersonen[FahrzeugPersonGeklickt].Rolle);
                   // FahrzeugPersonenBewegen(gAlleFahrzeugePersonen[FahrzeugPersonGeklickt]);
                }

                Thread.Sleep(10);
            }
        }
    }
}
