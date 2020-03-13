using System.Threading;

namespace StiegenhausBeleuchtung.Model
{
    public class StiegenhausBeleuchtung
    {

        public bool B_01 { get; set; }
        public bool B_02 { get; set; }
        public bool B_03 { get; set; }
        public bool B_11 { get; set; }
        public bool B_12 { get; set; }
        public bool B_13 { get; set; }
        public bool B_21 { get; set; }
        public bool B_22 { get; set; }
        public bool B_23 { get; set; }
        public bool B_31 { get; set; }
        public bool B_32 { get; set; }
        public bool B_33 { get; set; }
        public bool B_41 { get; set; }
        public bool B_42 { get; set; }
        public bool B_43 { get; set; }


        public bool P_01 { get; set; }
        public bool P_02 { get; set; }
        public bool P_03 { get; set; }
        public bool P_11 { get; set; }
        public bool P_12 { get; set; }
        public bool P_13 { get; set; }
        public bool P_21 { get; set; }
        public bool P_22 { get; set; }
        public bool P_23 { get; set; }
        public bool P_31 { get; set; }
        public bool P_32 { get; set; }
        public bool P_33 { get; set; }
        public bool P_41 { get; set; }
        public bool P_42 { get; set; }
        public bool P_43 { get; set; }
                

        public StiegenhausBeleuchtung()
        {
            System.Threading.Tasks.Task.Run(() => StiegenhausBeleuchtungTask());
        }

        private void StiegenhausBeleuchtungTask()
        {
            while (true)
            {


                Thread.Sleep(10);
            }
        }

        internal void BtnStart() {
            //
            P_01 = true;
        }


    }
}
