using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;

namespace LAP_2018_Abfuellanlage.Model
{
    public class AlleFlaschen
    {

        private readonly MainWindow mainWindow;

        public VisuAnzeigen ViAnzeige { get; set; }

        public readonly List<Flaschen> alleFlaschen = new List<Flaschen>();

        
        public bool S1 { get; set; }

        public AlleFlaschen( MainWindow mw )
        {
            mainWindow = mw;

            ViAnzeige = new VisuAnzeigen();


            alleFlaschen.Add(new Flaschen());
            alleFlaschen.Add(new Flaschen());
            alleFlaschen.Add(new Flaschen());
            alleFlaschen.Add(new Flaschen());
            alleFlaschen.Add(new Flaschen());
            alleFlaschen.Add(new Flaschen());

            System.Threading.Tasks.Task.Run(() => AlleFlaschenTask());
        }

        internal void TasterS1()
        {
            S1 = ButtonFunktionPressReleaseAendern( mainWindow.btnS1);
        }

        private void AlleFlaschenTask()
        {
            while (true)
            {

                AnzeigeAktualisieren();

                Thread.Sleep(10);

            }
        }


        private void AnzeigeAktualisieren()
        {

            //
        }

        private bool ButtonFunktionPressReleaseAendern(Button knopf)
        {
            if (knopf.ClickMode == System.Windows.Controls.ClickMode.Press)
            {
                knopf.ClickMode = System.Windows.Controls.ClickMode.Release;
                return true;
            }
            else
            {
                knopf.ClickMode = System.Windows.Controls.ClickMode.Press;
                return false;
            }
        }

    }
}
