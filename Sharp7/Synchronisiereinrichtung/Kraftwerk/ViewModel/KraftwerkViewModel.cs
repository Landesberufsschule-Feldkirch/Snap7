namespace Synchronisiereinrichtung.Kraftwerk.ViewModel
{

    using System.Diagnostics;
    using System.Windows.Input;
    using Synchronisiereinrichtung.Kraftwerk.Command;


    public class KraftwerkViewModel
    {

        private readonly Model.Kraftwerk _Kraftwerk;

        public KraftwerkViewModel()
        {
            _Kraftwerk = new Model.Kraftwerk();
            UpdateCommand = new KraftwerkUpdateCommand(this);
        }



        public Model.Kraftwerk Kraftwerk
        {
            get { return _Kraftwerk; }
        }

        public bool CanUpdate
        {
            get { return true; }
        }

        public void SaveChanges()
        {

            Debug.Assert(false, "dummytext");
        }

        public ICommand UpdateCommand()
        {
            // get;
            //   private set;
        }



    }
}
