namespace Tiefgarage.Model;

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
            new(FahrzeugPerson.Rolle.Fahrzeug, AnzahlAutos ++),
            new(FahrzeugPerson.Rolle.Fahrzeug, AnzahlAutos ++),
            new(FahrzeugPerson.Rolle.Fahrzeug, AnzahlAutos ++),
            new(FahrzeugPerson.Rolle.Fahrzeug, AnzahlAutos ++),

            new(FahrzeugPerson.Rolle.Person, AnzahlPersonen ++),
            new(FahrzeugPerson.Rolle.Person, AnzahlPersonen ++),
            new(FahrzeugPerson.Rolle.Person, AnzahlPersonen ++),
            new(FahrzeugPerson.Rolle.Person, AnzahlPersonen ++)
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
        // ReSharper disable once FunctionNeverReturns
    }
    internal void DrinnenParken()
    {
        foreach (var fp in AllePkwPersonen) fp.DrinnenParken();
    }
    internal void DraussenParken()
    {
        foreach (var fp in AllePkwPersonen) fp.DraussenParken();
    }
}