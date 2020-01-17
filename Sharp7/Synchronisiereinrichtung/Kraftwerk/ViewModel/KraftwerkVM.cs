using System.Diagnostics;
using System.Windows.Input;
using Synchronisiereinrichtung.Kraftwerk;
using Synchronisiereinrichtung.Commands;



namespace Synchronisiereinrichtung.Kraftwerk.ViewModels
{
    internal class KraftwerkViewModel
    {


        public KraftwerkViewModel()
        {
            _Kraftwerk = new Synchronisiereinrichtung.Kraftwerk.Model.Kraftwerk.Kraftwerk();
            UpdateCommand = new KraftwerkUpdateCommand();
        }

        private Synchronisiereinrichtung.Kraftwerk.Kraftwerk _Kraftwerk;



        public Synchronisiereinrichtung.Kraftwerk.Kraftwerk Kraftwerk
        {
            get { return _Kraftwerk; }
        }

        public bool CanUpdate { 
            get; 
            set; 
        }

        public void SaveChanges()
        {
           
            Debug.Assert(false, "dummytext");
        }

        public ICommand UpdateCommand()
        { 
            get;
             set;
        }


    }




}
