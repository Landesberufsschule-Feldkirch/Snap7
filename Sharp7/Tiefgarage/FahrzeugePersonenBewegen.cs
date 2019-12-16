namespace Tiefgarage
{
    public partial class FahrzeugPerson
    {
        private readonly double FahrenSchrittweite = 1;

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
        public void Bewegen(FahrzeugPerson fp)
        {
            if (fp.Bewegung < FahrenRichtung.DrinnenParken) Hineinfahren(fp); else Hinausfahren(fp);
        }

        public void Hineinfahren(FahrzeugPerson fp)
        {
            double Limit;

            switch (fp.Bewegung)
            {
                case FahrenRichtung.DraussenParken:
                    fp.Bewegung = FahrenRichtung.DraussenSenkrechtFahren;
                    break;

                case FahrenRichtung.DraussenSenkrechtFahren:
                    if (fp.FP_Rolle == Rolle.Auto) Limit = 180; else Limit = 160;
                    if (fp.Y_aktuell < Limit) fp.Y_aktuell += FahrenSchrittweite;
                    else fp.Bewegung = FahrenRichtung.DraussenWaagrechtFahren;
                    break;

                case FahrenRichtung.DraussenWaagrechtFahren:
                    if (fp.FP_Rolle == Rolle.Auto) Limit = 310; else Limit = 370;
                    if (fp.X_aktuell < 300) fp.X_aktuell += FahrenSchrittweite;
                    else
                    {
                        if (fp.X_aktuell > Limit) fp.X_aktuell -= FahrenSchrittweite;
                        else fp.Bewegung = FahrenRichtung.HineinFahren;
                    }
                    break;

                case FahrenRichtung.HineinFahren:
                    if (fp.Y_aktuell < fp.Y_drinnen) fp.Y_aktuell += FahrenSchrittweite;
                    else fp.Bewegung = FahrenRichtung.DrinnenEinparken;
                    break;

                case FahrenRichtung.DrinnenEinparken:
                    if (fp.X_aktuell > fp.X_drinnen + 1) fp.X_aktuell -= FahrenSchrittweite;
                    else
                    {
                        if (fp.X_aktuell < fp.X_drinnen - 1) fp.X_aktuell += FahrenSchrittweite;
                        else
                        {
                            fp.Bewegung = FahrenRichtung.DrinnenParken;
                            /*
                            FahrzeugPersonGeklickt = -1;
                            Pegel_B1 = true;
                            Pegel_B2 = true;
                            AlleBtnAktivieren();
                            */
                        }
                    }
                    break;

                default:
                    break;
            }
        }

        public void Hinausfahren(FahrzeugPerson fp)
        {
            double Limit;

            switch (fp.Bewegung)
            {
                case FahrenRichtung.DrinnenParken:
                    fp.Bewegung = FahrenRichtung.DrinnenAusparken;
                    break;

                case FahrenRichtung.DrinnenAusparken:
                    if (fp.FP_Rolle == Rolle.Auto) Limit = 310; else Limit = 370;
                    if (fp.X_aktuell < Limit) fp.X_aktuell += FahrenSchrittweite;
                    else
                    {
                        if (fp.X_aktuell > Limit + 1) fp.X_aktuell -= FahrenSchrittweite;
                        else fp.Bewegung = FahrenRichtung.HinausFahren;
                    }
                    break;

                case FahrenRichtung.HinausFahren:
                    if (fp.FP_Rolle == Rolle.Auto) Limit = 180; else Limit = 160;
                    if (fp.Y_aktuell > Limit) fp.Y_aktuell -= FahrenSchrittweite;
                    else fp.Bewegung = FahrenRichtung.DraussenAufDieSeiteFahren;
                    break;

                case FahrenRichtung.DraussenAufDieSeiteFahren:
                    if (fp.X_aktuell > fp.X_draussen + 1) fp.X_aktuell -= FahrenSchrittweite;
                    else
                    {
                        if (fp.X_aktuell < fp.X_draussen - 1) fp.X_aktuell += FahrenSchrittweite;
                        else fp.Bewegung = FahrenRichtung.DraussenEinparken;
                    }
                    break;

                case FahrenRichtung.DraussenEinparken:
                    if (fp.Y_aktuell > fp.Y_draussen) fp.Y_aktuell -= FahrenSchrittweite;
                    else
                    {
                        fp.Bewegung = FahrenRichtung.DraussenParken;
                        /*
                        FahrzeugPersonGeklickt = -1;
                        Pegel_B1 = true;
                        Pegel_B2 = true;
                        AlleBtnAktivieren();
                        */
                    }
                    break;

                default:
                    break;
            }
        }
    }
}