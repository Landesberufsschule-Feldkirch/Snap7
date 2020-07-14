namespace Tiefgarage.Model
{
    using System.Collections.Generic;
    using System.Threading;

    public class AlleFahrzeugePersonen
    {
        public bool B1 { get; set; }
        public bool B2 { get; set; }
        public bool AllesInParkposition { get; set; }
        public int AnzahlAutos { get; set; }
        public int AnzahlPersonen { get; set; }
        public List<FahrzeugPerson> AllePkwPersonen { get; set; }

        public AlleFahrzeugePersonen()
        {
            AllesInParkposition = true;
            AllePkwPersonen = new List<FahrzeugPerson>
            {
                new FahrzeugPerson(FahrzeugPerson.Rolle.Fahrzeug, AnzahlAutos ++),
                new FahrzeugPerson(FahrzeugPerson.Rolle.Fahrzeug, AnzahlAutos ++),
                new FahrzeugPerson(FahrzeugPerson.Rolle.Fahrzeug, AnzahlAutos ++),
                new FahrzeugPerson(FahrzeugPerson.Rolle.Fahrzeug, AnzahlAutos ++),

                new FahrzeugPerson(FahrzeugPerson.Rolle.Person, AnzahlPersonen ++),
                new FahrzeugPerson(FahrzeugPerson.Rolle.Person, AnzahlPersonen ++),
                new FahrzeugPerson(FahrzeugPerson.Rolle.Person, AnzahlPersonen ++),
                new FahrzeugPerson(FahrzeugPerson.Rolle.Person, AnzahlPersonen ++)
            };

            System.Threading.Tasks.Task.Run(AlleFahrzeugePersonenTask);
        }

        private void AlleFahrzeugePersonenTask()
        {
            while (true)
            {
                B1 = false;
                B2 = false;
                AllesInParkposition = true;

                foreach (var fp in AllePkwPersonen)
                {
                    var (b1, b2, park) = fp.Bewegen();

                    B1 |= b1;
                    B2 |= b2;
                    AllesInParkposition &= park;
                }

                Thread.Sleep(10);
            }
        }

        internal void DrinnenParken() { foreach (var fp in AllePkwPersonen) fp.DrinnenParken(); }
        internal void DraussenParken() { foreach (var fp in AllePkwPersonen) fp.DraussenParken(); }
        internal void Auto1() => AllePkwPersonen[0].Losfahren();
        internal void Auto2() => AllePkwPersonen[1].Losfahren();
        internal void Auto3() => AllePkwPersonen[2].Losfahren();
        internal void Auto4() => AllePkwPersonen[3].Losfahren();
        internal void Person1() => AllePkwPersonen[4].Losfahren();
        internal void Person2() => AllePkwPersonen[5].Losfahren();
        internal void Person3() => AllePkwPersonen[6].Losfahren();
        internal void Person4() => AllePkwPersonen[7].Losfahren();

    }
}