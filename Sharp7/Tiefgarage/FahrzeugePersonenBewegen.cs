namespace Tiefgarage
{
    public partial class MainWindow
    {
        public const double FahrenSchrittweite = 0.01;

        public enum FahrenRichtung
        {
            DraussenParken = 0,
            DraussenSenkrechtFahren,
            DraussenWaagrechtFahren,
            HineinFahren,
            DrinnenEinparken,
            DrinnenParken,
            DrinnenAusparken,
            HinausFahren,
            DraussenAufDieSeiteFahren,
            DraussenEinparken
            // DraussenEingeparkt
        }
        public void FahrzeugPersonenBewegen(AlleFahrzeugePersonen fp)
        {
            if (fp.Bewegung < FahrenRichtung.DrinnenParken) FahrzeugPersonenHineinfahren(fp); else FahrzeugPersonenHinausfahren(fp);
        }

        public void FahrzeugPersonenHineinfahren(AlleFahrzeugePersonen fp)
        {
            double Limit;

            switch (fp.Bewegung)
            {
                case FahrenRichtung.DraussenParken:
                    fp.Bewegung = FahrenRichtung.DraussenSenkrechtFahren;
                    break;

                case FahrenRichtung.DraussenSenkrechtFahren:
                    if (fp.Rolle == Rolle.Auto) Limit = 180; else Limit = 160;
                    if (fp.y_aktuell < Limit) fp.y_aktuell += FahrenSchrittweite;
                    else fp.Bewegung = FahrenRichtung.DraussenWaagrechtFahren;
                    break;

                case FahrenRichtung.DraussenWaagrechtFahren:
                    if (fp.Rolle == Rolle.Auto) Limit = 310; else Limit = 370;
                    if (fp.x_aktuell < 300) fp.x_aktuell += FahrenSchrittweite;
                    else
                    {
                        if (fp.x_aktuell > Limit) fp.x_aktuell -= FahrenSchrittweite;
                        else fp.Bewegung = FahrenRichtung.HineinFahren;
                    }
                    break;

                case FahrenRichtung.HineinFahren:
                    if (fp.y_aktuell < fp.y_drinnen) fp.y_aktuell += FahrenSchrittweite;
                    else fp.Bewegung = FahrenRichtung.DrinnenEinparken;
                    break;

                case FahrenRichtung.DrinnenEinparken:
                    if (fp.x_aktuell > fp.x_drinnen + 1) fp.x_aktuell -= FahrenSchrittweite;
                    else
                    {
                        if (fp.x_aktuell < fp.x_drinnen - 1) fp.x_aktuell += FahrenSchrittweite;
                        else
                        {
                            fp.Bewegung = FahrenRichtung.DrinnenParken;
                            FahrzeugPersonGeklickt = -1;
                            Pegel_B1 = true;
                            Pegel_B2 = true;
                            alleBtnAktivieren();
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        public void FahrzeugPersonenHinausfahren(AlleFahrzeugePersonen fp)
        {
            double Limit;

            switch (fp.Bewegung)
            {
                case FahrenRichtung.DrinnenParken:
                    fp.Bewegung = FahrenRichtung.DrinnenAusparken;
                    break;

                case FahrenRichtung.DrinnenAusparken:
                    if (fp.Rolle == Rolle.Auto) Limit = 310; else Limit = 370;
                    if (fp.x_aktuell < Limit) fp.x_aktuell += FahrenSchrittweite;
                    else
                    {
                        if (fp.x_aktuell > Limit + 1) fp.x_aktuell -= FahrenSchrittweite;
                        else fp.Bewegung = FahrenRichtung.HinausFahren;
                    }
                    break;

                case FahrenRichtung.HinausFahren:
                    if (fp.Rolle == Rolle.Auto) Limit = 180; else Limit = 160;
                    if (fp.y_aktuell > Limit) fp.y_aktuell -= FahrenSchrittweite;
                    else fp.Bewegung = FahrenRichtung.DraussenAufDieSeiteFahren;
                    break;

                case FahrenRichtung.DraussenAufDieSeiteFahren:
                    if (fp.x_aktuell > fp.x_draussen + 1) fp.x_aktuell -= FahrenSchrittweite;
                    else
                    {
                        if (fp.x_aktuell < fp.x_draussen - 1) fp.x_aktuell += FahrenSchrittweite;
                        else fp.Bewegung = FahrenRichtung.DraussenEinparken;
                    }
                    break;

                case FahrenRichtung.DraussenEinparken:
                    if (fp.y_aktuell > fp.y_draussen) fp.y_aktuell -= FahrenSchrittweite;
                    else
                    {
                        fp.Bewegung = FahrenRichtung.DraussenParken;
                        FahrzeugPersonGeklickt = -1;
                        Pegel_B1 = true;
                        Pegel_B2 = true;
                        alleBtnAktivieren();
                    }
                    break;

                default:
                    break;
            }
        }
    }
}