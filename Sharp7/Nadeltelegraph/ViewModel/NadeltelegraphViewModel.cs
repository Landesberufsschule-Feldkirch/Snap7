namespace Nadeltelegraph.ViewModel
{
    using Nadeltelegraph.Commands;
    using System.Windows.Input;

    public class NadeltelegraphViewModel
    {
        public readonly Model.Nadeltelegraph nadeltelegraph;

        public NadeltelegraphViewModel(MainWindow mainWindow)
        {
            nadeltelegraph = new Model.Nadeltelegraph(mainWindow);

            BtnBuchstabeA = new NadeltelegraphBuchstabeA(this);
            BtnBuchstabeB = new NadeltelegraphBuchstabeB(this);
            BtnBuchstabeD = new NadeltelegraphBuchstabeD(this);
            BtnBuchstabeE = new NadeltelegraphBuchstabeE(this);
            BtnBuchstabeF = new NadeltelegraphBuchstabeF(this);
            BtnBuchstabeG = new NadeltelegraphBuchstabeG(this);
            BtnBuchstabeH = new NadeltelegraphBuchstabeH(this);
            BtnBuchstabeI = new NadeltelegraphBuchstabeI(this);
            BtnBuchstabeK = new NadeltelegraphBuchstabeK(this);
            BtnBuchstabeL = new NadeltelegraphBuchstabeL(this);
            BtnBuchstabeM = new NadeltelegraphBuchstabeM(this);
            BtnBuchstabeN = new NadeltelegraphBuchstabeN(this);
            BtnBuchstabeO = new NadeltelegraphBuchstabeO(this);
            BtnBuchstabeP = new NadeltelegraphBuchstabeP(this);
            BtnBuchstabeR = new NadeltelegraphBuchstabeR(this);
            BtnBuchstabeS = new NadeltelegraphBuchstabeS(this);
            BtnBuchstabeT = new NadeltelegraphBuchstabeT(this);
            BtnBuchstabeV = new NadeltelegraphBuchstabeV(this);
            BtnBuchstabeW = new NadeltelegraphBuchstabeW(this);
            BtnBuchstabeY = new NadeltelegraphBuchstabeY(this);
        }

        public Model.Nadeltelegraph Nadeltelegraph { get { return nadeltelegraph; } }


        public bool CanBuchstabeA { get { return true; } }
        public bool CanBuchstabeB { get { return true; } }
        public bool CanBuchstabeD { get { return true; } }
        public bool CanBuchstabeE { get { return true; } }
        public bool CanBuchstabeF { get { return true; } }
        public bool CanBuchstabeG { get { return true; } }
        public bool CanBuchstabeH { get { return true; } }
        public bool CanBuchstabeI { get { return true; } }
        public bool CanBuchstabeK { get { return true; } }
        public bool CanBuchstabeL { get { return true; } }
        public bool CanBuchstabeM { get { return true; } }
        public bool CanBuchstabeN { get { return true; } }
        public bool CanBuchstabeO { get { return true; } }
        public bool CanBuchstabeP { get { return true; } }
        public bool CanBuchstabeR { get { return true; } }
        public bool CanBuchstabeS { get { return true; } }
        public bool CanBuchstabeT { get { return true; } }
        public bool CanBuchstabeV { get { return true; } }
        public bool CanBuchstabeW { get { return true; } }
        public bool CanBuchstabeY { get { return true; } }




        public ICommand BtnBuchstabeA { get; private set; }
        public ICommand BtnBuchstabeB { get; private set; }
        public ICommand BtnBuchstabeD { get; private set; }
        public ICommand BtnBuchstabeE { get; private set; }
        public ICommand BtnBuchstabeF { get; private set; }
        public ICommand BtnBuchstabeG { get; private set; }
        public ICommand BtnBuchstabeH { get; private set; }
        public ICommand BtnBuchstabeI { get; private set; }
        public ICommand BtnBuchstabeK { get; private set; }
        public ICommand BtnBuchstabeL { get; private set; }
        public ICommand BtnBuchstabeM { get; private set; }
        public ICommand BtnBuchstabeN { get; private set; }
        public ICommand BtnBuchstabeO { get; private set; }
        public ICommand BtnBuchstabeP { get; private set; }
        public ICommand BtnBuchstabeR { get; private set; }
        public ICommand BtnBuchstabeS { get; private set; }
        public ICommand BtnBuchstabeT { get; private set; }
        public ICommand BtnBuchstabeV { get; private set; }
        public ICommand BtnBuchstabeW { get; private set; }
        public ICommand BtnBuchstabeY { get; private set; }





        internal void BuchstabeA() { nadeltelegraph.BuchstabeA(); }
        internal void BuchstabeB() { nadeltelegraph.BuchstabeB(); }
        internal void BuchstabeD() { nadeltelegraph.BuchstabeD(); }
        internal void BuchstabeE() { nadeltelegraph.BuchstabeE(); }
        internal void BuchstabeF() { nadeltelegraph.BuchstabeF(); }
        internal void BuchstabeG() { nadeltelegraph.BuchstabeG(); }
        internal void BuchstabeH() { nadeltelegraph.BuchstabeH(); }
        internal void BuchstabeI() { nadeltelegraph.BuchstabeI(); }
        internal void BuchstabeK() { nadeltelegraph.BuchstabeK(); }
        internal void BuchstabeL() { nadeltelegraph.BuchstabeL(); }
        internal void BuchstabeM() { nadeltelegraph.BuchstabeM(); }
        internal void BuchstabeN() { nadeltelegraph.BuchstabeN(); }
        internal void BuchstabeO() { nadeltelegraph.BuchstabeO(); }
        internal void BuchstabeP() { nadeltelegraph.BuchstabeP(); }
        internal void BuchstabeR() { nadeltelegraph.BuchstabeR(); }
        internal void BuchstabeS() { nadeltelegraph.BuchstabeS(); }
        internal void BuchstabeT() { nadeltelegraph.BuchstabeT(); }
        internal void BuchstabeV() { nadeltelegraph.BuchstabeV(); }
        internal void BuchstabeW() { nadeltelegraph.BuchstabeW(); }
        internal void BuchstabeY() { nadeltelegraph.BuchstabeY(); }
    }
}