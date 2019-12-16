using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Tiefgarage
{
    public partial class MainWindow
    {
        public ObservableCollection<Button> gAlleButtons = new ObservableCollection<Button>();
        public void AlleFahrzeugePersonenInitialisieren()
        {
            btn_auto_1.Tag = new FahrzeugPerson(FahrzeugPerson.Rolle.Auto, 10, 10, 110, 370);
            btn_auto_2.Tag = new FahrzeugPerson(FahrzeugPerson.Rolle.Auto, 100, 10, 110, 420);
            btn_auto_3.Tag = new FahrzeugPerson(FahrzeugPerson.Rolle.Auto, 200, 10, 110, 470);
            btn_auto_4.Tag = new FahrzeugPerson(FahrzeugPerson.Rolle.Auto, 300, 10, 110, 520);
            btn_person_1.Tag = new FahrzeugPerson(FahrzeugPerson.Rolle.Person, 500, 10, 210, 545);
            btn_person_2.Tag = new FahrzeugPerson(FahrzeugPerson.Rolle.Person, 540, 10, 240, 545);
            btn_person_3.Tag = new FahrzeugPerson(FahrzeugPerson.Rolle.Person, 580, 10, 270, 545);
            btn_person_4.Tag = new FahrzeugPerson(FahrzeugPerson.Rolle.Person, 620, 10, 300, 545);

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