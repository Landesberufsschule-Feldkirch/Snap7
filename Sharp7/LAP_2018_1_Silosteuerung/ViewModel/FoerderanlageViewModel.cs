using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAP_2018_1_Silosteuerung.ViewModel
{
    public class FoerderanlageViewModel
    {
        public VisuAnzeigen ViAnzeige { get; set; }
        public readonly Model.Foerderanlage foerderanlage;

        public FoerderanlageViewModel(MainWindow mainWindow)
        {
            foerderanlage = new Model.Foerderanlage(mainWindow);
            ViAnzeige = new VisuAnzeigen(mainWindow, foerderanlage);
        }

        public Model.Foerderanlage Foerderanlage { get { return foerderanlage; } }


    }
}
