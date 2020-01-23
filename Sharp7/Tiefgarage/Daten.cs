using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Tiefgarage
{
    public partial class MainWindow
    {
        public ObservableCollection<Button> gAlleButtons = new ObservableCollection<Button>();

        public void AlleFahrzeugePersonenInitialisieren()
        {
            btn_auto_1.Tag = new FahrzeugPerson(FahrzeugPerson.Rolle.Fahrzeug);
            btn_auto_2.Tag = new FahrzeugPerson(FahrzeugPerson.Rolle.Fahrzeug);
            btn_auto_3.Tag = new FahrzeugPerson(FahrzeugPerson.Rolle.Fahrzeug);
            btn_auto_4.Tag = new FahrzeugPerson(FahrzeugPerson.Rolle.Fahrzeug);
            btn_person_1.Tag = new FahrzeugPerson(FahrzeugPerson.Rolle.Person);
            btn_person_2.Tag = new FahrzeugPerson(FahrzeugPerson.Rolle.Person);
            btn_person_3.Tag = new FahrzeugPerson(FahrzeugPerson.Rolle.Person);
            btn_person_4.Tag = new FahrzeugPerson(FahrzeugPerson.Rolle.Person);

            gAlleButtons.Add(btn_auto_1);
            gAlleButtons.Add(btn_auto_2);
            gAlleButtons.Add(btn_auto_3);
            gAlleButtons.Add(btn_auto_4);
            gAlleButtons.Add(btn_person_1);
            gAlleButtons.Add(btn_person_2);
            gAlleButtons.Add(btn_person_3);
            gAlleButtons.Add(btn_person_4);
        }

        public void AlleDraussenParken()
        {
            foreach (Button btn in gAlleButtons)
            {
                var fahrPer = btn.Tag as FahrzeugPerson;
                fahrPer.DraussenParken();
            }
        }

        public void AlleDrinnenParken()
        {
            foreach (Button btn in gAlleButtons)
            {
                var fahrPer = btn.Tag as FahrzeugPerson;
                fahrPer.DrinnenParken();
            }
        }
    }
}