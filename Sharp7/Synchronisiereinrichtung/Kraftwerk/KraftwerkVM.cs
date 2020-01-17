using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Synchronisiereinrichtung.Kraftwerk;



namespace Synchronisiereinrichtung.Kraftwerk.ViewModels
{
    internal class KraftwerkViewModel
    {
        private Synchronisiereinrichtung.Kraftwerk.Kraftwerk _Kraftwerk;


        public KraftwerkViewModel()
        {
            _Kraftwerk = new Kraftwerk();
        }




        public Synchronisiereinrichtung.Kraftwerk.Kraftwerk Kraftwerk
        {
            get { return _Kraftwerk; }


        }

        public void TaskFunktion()
        {
            _Kraftwerk.KraftwerkTask();
        }

    }




}
