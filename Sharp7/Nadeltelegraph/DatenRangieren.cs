namespace Nadeltelegraph
{
    using Nadeltelegraph.Model;
    using Sharp7;
    using System;

    public class DatenRangieren
    {
        private readonly MainWindow mainWindow;
        private readonly ViewModel.NadeltelegraphViewModel viewModel;

     

        private enum BitPosAusgang
        {
           
        }

        private enum BitPosEingang
        {
            P1 = 0,
            P2,
            P3,
            P4,
            P5,
            P6,
            P7,
            P8,
            P9,
            P10
        }

        public void RangierenInput(byte[] digInput, byte[] anInput)
        {

            // mainWindow.lbl_PlcPing.Content = S7_1200.GetSpsStatus();
            /*

            S7.SetBitAt(digInput, (int)BitPosEingang.B1, viewModel.alleLastKraftWagen.B1);
            S7.SetBitAt(digInput, (int)BitPosEingang.B2, viewModel.alleLastKraftWagen.B2);
            S7.SetBitAt(digInput, (int)BitPosEingang.B3, viewModel.alleLastKraftWagen.B3);
            S7.SetBitAt(digInput, (int)BitPosEingang.B4, viewModel.alleLastKraftWagen.B4);
            */
        }

        public void RangierenOutput(byte[] digOutput, byte[] anOutput)
        {
            /*
            var p1_links_rot = S7.GetBitAt(digOutput, (int)BitPosAusgang.P1);
            var p2_links_gelb = S7.GetBitAt(digOutput, (int)BitPosAusgang.P2);
            var p3_links_gruen = S7.GetBitAt(digOutput, (int)BitPosAusgang.P3);
            var p4_rechts_rot = S7.GetBitAt(digOutput, (int)BitPosAusgang.P4);
            var p5_rechts_gelb = S7.GetBitAt(digOutput, (int)BitPosAusgang.P5);
            var p6_rechts_gruen = S7.GetBitAt(digOutput, (int)BitPosAusgang.P6);
            */
           
        }

        public DatenRangieren(MainWindow mw, ViewModel.NadeltelegraphViewModel vm)
        {
            mainWindow = mw;
            viewModel = vm;
        }
    }
}