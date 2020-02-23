namespace Tiefgarage.Model
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class AlleFahrzeugePersonen
    {
        public bool B1 { get; set; }
        public bool B2 { get; set; }
        private bool AllesInParkposition = true;

        public int AnzahlAutos { get; set; }
        public int AnzahlPersonen { get; set; }
        public VisuAnzeigen ViAnzeige { get; set; }

        private readonly MainWindow mainWindow;
        private readonly List<FahrzeugPerson> alleFahrzeugePersonen = new List<FahrzeugPerson>();

        public AlleFahrzeugePersonen(MainWindow mw)
        {
            mainWindow = mw;
            ViAnzeige = new VisuAnzeigen();

            alleFahrzeugePersonen.Add(new FahrzeugPerson(FahrzeugPerson.Rolle.Fahrzeug));
            alleFahrzeugePersonen.Add(new FahrzeugPerson(FahrzeugPerson.Rolle.Fahrzeug));
            alleFahrzeugePersonen.Add(new FahrzeugPerson(FahrzeugPerson.Rolle.Fahrzeug));
            alleFahrzeugePersonen.Add(new FahrzeugPerson(FahrzeugPerson.Rolle.Fahrzeug));

            alleFahrzeugePersonen.Add(new FahrzeugPerson(FahrzeugPerson.Rolle.Person));
            alleFahrzeugePersonen.Add(new FahrzeugPerson(FahrzeugPerson.Rolle.Person));
            alleFahrzeugePersonen.Add(new FahrzeugPerson(FahrzeugPerson.Rolle.Person));
            alleFahrzeugePersonen.Add(new FahrzeugPerson(FahrzeugPerson.Rolle.Person));

            System.Threading.Tasks.Task.Run(() => AlleFahrzeugePersonenTask());
        }



        private void AlleFahrzeugePersonenTask()
        {

            while (true)
            {
                B1 = false;
                B2 = false;
                AllesInParkposition = true;

                foreach (FahrzeugPerson fp in alleFahrzeugePersonen)
                {
                    var (b1, b2, park) = fp.Bewegen();

                    B1 |= b1;
                    B2 |= b2;
                    AllesInParkposition &= park;
                }

                AnzeigeAktualisieren();
                Thread.Sleep(10);
            }
        }

        private void AnzeigeAktualisieren()
        {
            ViAnzeige.FarbeB1(B1);
            ViAnzeige.FarbeB2(B2);

            ViAnzeige.AnzahlAutosInDerTiefgarage(AnzahlAutos);
            ViAnzeige.AnzahlPersonenInDerTiefgarage(AnzahlPersonen);

            ViAnzeige.PositionAuto1(alleFahrzeugePersonen[0].AktuellePosition);
            ViAnzeige.PositionAuto2(alleFahrzeugePersonen[1].AktuellePosition);
            ViAnzeige.PositionAuto3(alleFahrzeugePersonen[2].AktuellePosition);
            ViAnzeige.PositionAuto4(alleFahrzeugePersonen[3].AktuellePosition);
            ViAnzeige.PositionPerson1(alleFahrzeugePersonen[4].AktuellePosition);
            ViAnzeige.PositionPerson2(alleFahrzeugePersonen[5].AktuellePosition);
            ViAnzeige.PositionPerson3(alleFahrzeugePersonen[6].AktuellePosition);
            ViAnzeige.PositionPerson4(alleFahrzeugePersonen[7].AktuellePosition);

            ViAnzeige.EnableAuto1 = AllesInParkposition;
            ViAnzeige.EnableAuto2 = AllesInParkposition;
            ViAnzeige.EnableAuto3 = AllesInParkposition;
            ViAnzeige.EnableAuto4 = AllesInParkposition;
            ViAnzeige.EnablePerson1 = AllesInParkposition;
            ViAnzeige.EnablePerson2 = AllesInParkposition;
            ViAnzeige.EnablePerson3 = AllesInParkposition;
            ViAnzeige.EnablePerson4 = AllesInParkposition;

            if (mainWindow.S7_1200 != null)
            {
                if (mainWindow.S7_1200.GetSpsError()) ViAnzeige.SpsColor = "Red"; else ViAnzeige.SpsColor = "LightGray";
                ViAnzeige.SpsStatus = mainWindow.S7_1200?.GetSpsStatus();
            }
        }


        internal void DrinnenParken() { foreach (FahrzeugPerson fp in alleFahrzeugePersonen) fp.DrinnenParken(); }
        internal void DraussenParken() { foreach (FahrzeugPerson fp in alleFahrzeugePersonen) fp.DraussenParken(); }

        internal void Auto1() { alleFahrzeugePersonen[0].Losfahren(); }
        internal void Auto2() { alleFahrzeugePersonen[1].Losfahren(); }
        internal void Auto3() { alleFahrzeugePersonen[2].Losfahren(); }
        internal void Auto4() { alleFahrzeugePersonen[3].Losfahren(); }
        internal void Person1() { alleFahrzeugePersonen[4].Losfahren(); }
        internal void Person2() { alleFahrzeugePersonen[5].Losfahren(); }
        internal void Person3() { alleFahrzeugePersonen[6].Losfahren(); }
        internal void Person4() { alleFahrzeugePersonen[7].Losfahren(); }
    }
}