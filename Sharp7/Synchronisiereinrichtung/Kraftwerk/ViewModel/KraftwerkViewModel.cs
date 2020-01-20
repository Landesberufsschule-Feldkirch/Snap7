namespace Synchronisiereinrichtung.Kraftwerk.ViewModel
{
    using Synchronisiereinrichtung.Kraftwerk.Command;
    using System.Windows.Input;


    public class KraftwerkViewModel
    {

        private readonly Model.Kraftwerk _Kraftwerk;
     
        public KraftwerkViewModel()
        {
            _Kraftwerk = new Model.Kraftwerk();
            UpdateSchalterReset = new KraftwerkUpdateManualReset(this);
            UpdateSchalterQ1 = new KraftwerkUpdateManualQ1(this);
            UpdateSchalterStart = new KraftwerkUpdateStart(this);
            UpdateSchalterStop = new KraftwerkUpdateStop(this);
        }


        public Model.Kraftwerk Kraftwerk
        {
            get { return _Kraftwerk; }
        }


       

  



        public bool CanUpdateQ1
        {
            get { return true; }
        }
        public bool CanUpdateReset
        {
            get { return true; }
        }

        public bool CanUpdateStart
        {
            get { return true; }
        }
        public bool CanUpdateStop
        {
            get { return true; }
        }


        public ICommand UpdateSchalterReset
        {
            get;
            private set;
        }

        public ICommand UpdateSchalterQ1
        {
            get;
            set;
        }

        public ICommand UpdateSchalterStart
        {
            get;
            private set;
        }

        public ICommand UpdateSchalterStop
        {
            get;
            set;
        }


        internal void SchalterQ1()
        {
            _Kraftwerk.Synchronisieren();
        }


        public void SchalterReset()
        {
            _Kraftwerk.Reset();
        }

        internal void SchalterStart()
        {
            _Kraftwerk.Starten();
        }

        public void SchalterStop()
        {
            _Kraftwerk.Stoppen();
        }





    }
}
