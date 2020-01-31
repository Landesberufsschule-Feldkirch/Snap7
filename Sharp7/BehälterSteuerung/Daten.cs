using System.Collections.ObjectModel;

namespace BehälterSteuerung
{
    public partial class MainWindow
    {
        private enum BitPosEingang
        {
            B1 = 0,
            B2,
            B3,
            B4,
            B5,
            B6,
            B7,
            B8
        }

        private enum BitPosAusgang
        {
            Q1 = 0,
            Q3,
            Q5,
            Q7,
            P1
        }

        public ObservableCollection<Behaelter> gAlleBehaelter = new ObservableCollection<Behaelter>();

        public void AlleBehaelterInitialisieren()
        {
            gAlleBehaelter.Add(new Behaelter(0.9, img_Q1_Ein, img_Q1_Aus, img_Q2_Ein, img_Q2_Aus, rct_Zuleitung_1b, rct_Ableitung_1a, rct_Behaelter_1_Voll, lbl_B1, lbl_B2, btn_Q2_Ein, btn_Q2_Aus, (int)BitPosEingang.B1, (int)BitPosEingang.B2, (int)BitPosAusgang.Q1));
            gAlleBehaelter.Add(new Behaelter(0.7, img_Q3_Ein, img_Q3_Aus, img_Q4_Ein, img_Q4_Aus, rct_Zuleitung_2b, rct_Ableitung_2a, rct_Behaelter_2_Voll, lbl_B3, lbl_B4, btn_Q4_Ein, btn_Q4_Aus, (int)BitPosEingang.B3, (int)BitPosEingang.B4, (int)BitPosAusgang.Q3));
            gAlleBehaelter.Add(new Behaelter(0.5, img_Q5_Ein, img_Q5_Aus, img_Q6_Ein, img_Q6_Aus, rct_Zuleitung_3b, rct_Ableitung_3a, rct_Behaelter_3_Voll, lbl_B5, lbl_B6, btn_Q6_Ein, btn_Q6_Aus, (int)BitPosEingang.B5, (int)BitPosEingang.B6, (int)BitPosAusgang.Q5));
            gAlleBehaelter.Add(new Behaelter(0.3, img_Q7_Ein, img_Q7_Aus, img_Q8_Ein, img_Q8_Aus, rct_Zuleitung_4b, rct_Ableitung_4a, rct_Behaelter_4_Voll, lbl_B7, lbl_B8, btn_Q8_Ein, btn_Q8_Aus, (int)BitPosEingang.B7, (int)BitPosEingang.B8, (int)BitPosAusgang.Q7));
        }
    }
}