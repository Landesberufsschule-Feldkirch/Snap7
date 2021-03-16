using BeschriftungPlc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Threading;

namespace TestAutomat
{

    public partial class TestAutomat
    {
        public double[] Zeitachse { get; set; }

        public bool[] DatenpunktAktivDa { get; set; }
        public bool[] DatenpunktAktivDi { get; set; }

        public int[] DatenpunktAdresseDa { get; set; }
        public int[] DatenpunktAdresseDi { get; set; }

        public double[] OffsetDa { get; set; }
        public double[] OffsetDi { get; set; }

        public double[] DatenDa00 { get; set; }
        public double[] DatenDa01 { get; set; }
        public double[] DatenDa02 { get; set; }
        public double[] DatenDa03 { get; set; }
        public double[] DatenDa04 { get; set; }
        public double[] DatenDa05 { get; set; }
        public double[] DatenDa06 { get; set; }
        public double[] DatenDa07 { get; set; }
        public double[] DatenDa08 { get; set; }
        public double[] DatenDa09 { get; set; }
        public double[] DatenDa10 { get; set; }
        public double[] DatenDa11 { get; set; }
        public double[] DatenDa12 { get; set; }
        public double[] DatenDa13 { get; set; }
        public double[] DatenDa14 { get; set; }
        public double[] DatenDa15 { get; set; }

        public double[] DatenDi00 { get; set; }
        public double[] DatenDi01 { get; set; }
        public double[] DatenDi02 { get; set; }
        public double[] DatenDi03 { get; set; }
        public double[] DatenDi04 { get; set; }
        public double[] DatenDi05 { get; set; }
        public double[] DatenDi06 { get; set; }
        public double[] DatenDi07 { get; set; }
        public double[] DatenDi08 { get; set; }
        public double[] DatenDi09 { get; set; }
        public double[] DatenDi10 { get; set; }
        public double[] DatenDi11 { get; set; }
        public double[] DatenDi12 { get; set; }
        public double[] DatenDi13 { get; set; }
        public double[] DatenDi14 { get; set; }
        public double[] DatenDi15 { get; set; }


        private PlotWindow _plotWindow;
        private const int GroesseDatenArray = 2048;
        private const int AnzahlDatenpunkte = 2000;
        private const int UpdateZeitDaten = 10;

        private void PlotInitialisieren()
        {
            _plotWindow = new PlotWindow();

            DatenpunktAktivDa = new bool[16];
            DatenpunktAktivDi = new bool[16];
            DatenpunktAdresseDa = new int[16];
            DatenpunktAdresseDi = new int[16];

            Zeitachse = new double[GroesseDatenArray];

            DatenDa00 = new double[GroesseDatenArray];
            DatenDa01 = new double[GroesseDatenArray];
            DatenDa02 = new double[GroesseDatenArray];
            DatenDa03 = new double[GroesseDatenArray];
            DatenDa04 = new double[GroesseDatenArray];
            DatenDa05 = new double[GroesseDatenArray];
            DatenDa06 = new double[GroesseDatenArray];
            DatenDa07 = new double[GroesseDatenArray];
            DatenDa08 = new double[GroesseDatenArray];
            DatenDa09 = new double[GroesseDatenArray];
            DatenDa10 = new double[GroesseDatenArray];
            DatenDa11 = new double[GroesseDatenArray];
            DatenDa12 = new double[GroesseDatenArray];
            DatenDa13 = new double[GroesseDatenArray];
            DatenDa14 = new double[GroesseDatenArray];
            DatenDa15 = new double[GroesseDatenArray];

            DatenDi00 = new double[GroesseDatenArray];
            DatenDi01 = new double[GroesseDatenArray];
            DatenDi02 = new double[GroesseDatenArray];
            DatenDi03 = new double[GroesseDatenArray];
            DatenDi04 = new double[GroesseDatenArray];
            DatenDi05 = new double[GroesseDatenArray];
            DatenDi06 = new double[GroesseDatenArray];
            DatenDi07 = new double[GroesseDatenArray];
            DatenDi08 = new double[GroesseDatenArray];
            DatenDi09 = new double[GroesseDatenArray];
            DatenDi10 = new double[GroesseDatenArray];
            DatenDi11 = new double[GroesseDatenArray];
            DatenDi12 = new double[GroesseDatenArray];
            DatenDi13 = new double[GroesseDatenArray];
            DatenDi14 = new double[GroesseDatenArray];
            DatenDi15 = new double[GroesseDatenArray];

            OffsetDa = new double[16];
            OffsetDi = new double[16];

            var start = new DateTime(2021, 1, 1);

            for (var i = 0; i < GroesseDatenArray; i++)
            {
                var dtNow = start.AddMilliseconds(i * UpdateZeitDaten);
                Zeitachse[i] = dtNow.ToOADate();
            }


            // create a timer to modify the data
            var updateDataTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(UpdateZeitDaten) };
            updateDataTimer.Tick += UpdatePlotData;
            updateDataTimer.Start();

            // create a timer to update the GUI
            var renderTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(50) };
            renderTimer.Tick += RenderPlot;
            renderTimer.Start();

        }
        public void UpdatePlot()
        {
            for (var i = 0; i < 16; i++)
            {
                DatenpunktAktivDa[i] = false;
                DatenpunktAktivDi[i] = false;
                DatenpunktAdresseDa[i] = 0;
                DatenpunktAdresseDi[i] = 0;
                OffsetDa[i] = 0;
                OffsetDi[i] = 0;
            }

            _plotWindow.WpfPlot.plt.Clear();

            var offset = 2.5;
            if (BeschriftungenPlc.BeschriftungenStruktur.DaBeschriftungen != null) offset += 1.5 * BeschriftungenPlc.BeschriftungenStruktur.DaBeschriftungen.DaBeschriftung.Count;
            if (BeschriftungenPlc.BeschriftungenStruktur.DiBeschriftungen != null) offset += 1.5 * BeschriftungenPlc.BeschriftungenStruktur.DiBeschriftungen.DiBeschriftung.Count;


            if (BeschriftungenPlc.BeschriftungenStruktur.DiBeschriftungen != null)
            {
                var diBeschriftung = BeschriftungenPlc.BeschriftungenStruktur.DiBeschriftungen.DiBeschriftung;

                offset = DatenpunktDiPlotEinfuegen(0, diBeschriftung, DatenpunktAktivDi, DatenpunktAdresseDi, offset, OffsetDi, Color.LawnGreen, DatenDi00);
                offset = DatenpunktDiPlotEinfuegen(1, diBeschriftung, DatenpunktAktivDi, DatenpunktAdresseDi, offset, OffsetDi, Color.DarkGreen, DatenDi01);
                offset = DatenpunktDiPlotEinfuegen(2, diBeschriftung, DatenpunktAktivDi, DatenpunktAdresseDi, offset, OffsetDi, Color.LawnGreen, DatenDi02);
                offset = DatenpunktDiPlotEinfuegen(3, diBeschriftung, DatenpunktAktivDi, DatenpunktAdresseDi, offset, OffsetDi, Color.DarkGreen, DatenDi03);
                offset = DatenpunktDiPlotEinfuegen(4, diBeschriftung, DatenpunktAktivDi, DatenpunktAdresseDi, offset, OffsetDi, Color.LawnGreen, DatenDi04);
                offset = DatenpunktDiPlotEinfuegen(5, diBeschriftung, DatenpunktAktivDi, DatenpunktAdresseDi, offset, OffsetDi, Color.DarkGreen, DatenDi05);
                offset = DatenpunktDiPlotEinfuegen(6, diBeschriftung, DatenpunktAktivDi, DatenpunktAdresseDi, offset, OffsetDi, Color.LawnGreen, DatenDi06);
                offset = DatenpunktDiPlotEinfuegen(7, diBeschriftung, DatenpunktAktivDi, DatenpunktAdresseDi, offset, OffsetDi, Color.DarkGreen, DatenDi07);
                offset = DatenpunktDiPlotEinfuegen(8, diBeschriftung, DatenpunktAktivDi, DatenpunktAdresseDi, offset, OffsetDi, Color.LawnGreen, DatenDi08);
                offset = DatenpunktDiPlotEinfuegen(9, diBeschriftung, DatenpunktAktivDi, DatenpunktAdresseDi, offset, OffsetDi, Color.DarkGreen, DatenDi09);
                offset = DatenpunktDiPlotEinfuegen(10, diBeschriftung, DatenpunktAktivDi, DatenpunktAdresseDi, offset, OffsetDi, Color.LawnGreen, DatenDi10);
                offset = DatenpunktDiPlotEinfuegen(11, diBeschriftung, DatenpunktAktivDi, DatenpunktAdresseDi, offset, OffsetDi, Color.DarkGreen, DatenDi11);
                offset = DatenpunktDiPlotEinfuegen(12, diBeschriftung, DatenpunktAktivDi, DatenpunktAdresseDi, offset, OffsetDi, Color.LawnGreen, DatenDi12);
                offset = DatenpunktDiPlotEinfuegen(13, diBeschriftung, DatenpunktAktivDi, DatenpunktAdresseDi, offset, OffsetDi, Color.DarkGreen, DatenDi13);
                offset = DatenpunktDiPlotEinfuegen(14, diBeschriftung, DatenpunktAktivDi, DatenpunktAdresseDi, offset, OffsetDi, Color.LawnGreen, DatenDi14);
                offset = DatenpunktDiPlotEinfuegen(15, diBeschriftung, DatenpunktAktivDi, DatenpunktAdresseDi, offset, OffsetDi, Color.DarkGreen, DatenDi15);
            }

            offset -= 1.5;

            if (BeschriftungenPlc.BeschriftungenStruktur.DaBeschriftungen != null)
            {
                var daBeschriftung = BeschriftungenPlc.BeschriftungenStruktur.DaBeschriftungen.DaBeschriftung;

                offset = DatenpunktDaPlotEinfuegen(0, daBeschriftung, DatenpunktAktivDa, DatenpunktAdresseDa, offset, OffsetDa, Color.Red, DatenDa00);
                offset = DatenpunktDaPlotEinfuegen(1, daBeschriftung, DatenpunktAktivDa, DatenpunktAdresseDa, offset, OffsetDa, Color.Orange, DatenDa01);
                offset = DatenpunktDaPlotEinfuegen(2, daBeschriftung, DatenpunktAktivDa, DatenpunktAdresseDa, offset, OffsetDa, Color.Red, DatenDa02);
                offset = DatenpunktDaPlotEinfuegen(3, daBeschriftung, DatenpunktAktivDa, DatenpunktAdresseDa, offset, OffsetDa, Color.Orange, DatenDa03);
                offset = DatenpunktDaPlotEinfuegen(4, daBeschriftung, DatenpunktAktivDa, DatenpunktAdresseDa, offset, OffsetDa, Color.Red, DatenDa04);
                offset = DatenpunktDaPlotEinfuegen(5, daBeschriftung, DatenpunktAktivDa, DatenpunktAdresseDa, offset, OffsetDa, Color.Orange, DatenDa05);
                offset = DatenpunktDaPlotEinfuegen(6, daBeschriftung, DatenpunktAktivDa, DatenpunktAdresseDa, offset, OffsetDa, Color.Red, DatenDa06);
                offset = DatenpunktDaPlotEinfuegen(7, daBeschriftung, DatenpunktAktivDa, DatenpunktAdresseDa, offset, OffsetDa, Color.Orange, DatenDa07);
                offset = DatenpunktDaPlotEinfuegen(8, daBeschriftung, DatenpunktAktivDa, DatenpunktAdresseDa, offset, OffsetDa, Color.Red, DatenDa08);
                offset = DatenpunktDaPlotEinfuegen(9, daBeschriftung, DatenpunktAktivDa, DatenpunktAdresseDa, offset, OffsetDa, Color.Orange, DatenDa09);
                offset = DatenpunktDaPlotEinfuegen(10, daBeschriftung, DatenpunktAktivDa, DatenpunktAdresseDa, offset, OffsetDa, Color.Red, DatenDa10);
                offset = DatenpunktDaPlotEinfuegen(11, daBeschriftung, DatenpunktAktivDa, DatenpunktAdresseDa, offset, OffsetDa, Color.Orange, DatenDa11);
                offset = DatenpunktDaPlotEinfuegen(12, daBeschriftung, DatenpunktAktivDa, DatenpunktAdresseDa, offset, OffsetDa, Color.Red, DatenDa12);
                offset = DatenpunktDaPlotEinfuegen(13, daBeschriftung, DatenpunktAktivDa, DatenpunktAdresseDa, offset, OffsetDa, Color.Orange, DatenDa13);
                offset = DatenpunktDaPlotEinfuegen(14, daBeschriftung, DatenpunktAktivDa, DatenpunktAdresseDa, offset, OffsetDa, Color.Red, DatenDa14);
                DatenpunktDaPlotEinfuegen(15, daBeschriftung, DatenpunktAktivDa, DatenpunktAdresseDa, offset, OffsetDa, Color.Orange, DatenDa15);
            }

            _plotWindow.WpfPlot.plt.Ticks(dateTimeX: true, dateTimeFormatStringX: "ss.fff");
            _plotWindow.WpfPlot.plt.Legend();

        }
        private double DatenpunktDaPlotEinfuegen(int id, IReadOnlyList<DaDaten> daBeschriftung, IList<bool> datenpunktAktiv, IList<int> datenpunktAdresse, double offset, IList<double> offsetDatenpunkt, Color farbe, double[] datenPlot)
        {
            if (daBeschriftung.Count <= id) return offset;

            datenpunktAktiv[id] = true;
            offsetDatenpunkt[id] = offset;
            datenpunktAdresse[id] = daBeschriftung[id].Bit + 8 * daBeschriftung[id].Byte;
            _plotWindow.WpfPlot.plt.PlotScatter(Zeitachse, datenPlot, farbe, 2, 5, daBeschriftung[id].Bezeichnung);
            return offset - 1.5;
        }
        private double DatenpunktDiPlotEinfuegen(int id, IReadOnlyList<DiDaten> diBeschriftung, IList<bool> datenpunktAktiv, IList<int> datenpunktAdresse, double offset, IList<double> offsetDatenpunkt, Color farbe, double[] datenPlot)
        {
            if (diBeschriftung.Count <= id) return offset;

            datenpunktAktiv[id] = true;
            offsetDatenpunkt[id] = offset;
            datenpunktAdresse[id] = diBeschriftung[id].Bit + 8 * diBeschriftung[id].Byte;
            _plotWindow.WpfPlot.plt.PlotScatter(Zeitachse, datenPlot, farbe, 2, 5, diBeschriftung[id].Bezeichnung);
            return offset - 1.5;
        }
        private void UpdatePlotData(object sender, EventArgs e)
        {
            if (_pltNextDataIndex >= AnzahlDatenpunkte) return;

            DatenDa00[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDa, 0, _datenstruktur.DigOutput, DatenpunktAdresseDa, OffsetDa);
            DatenDa01[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDa, 1, _datenstruktur.DigOutput, DatenpunktAdresseDa, OffsetDa);
            DatenDa02[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDa, 2, _datenstruktur.DigOutput, DatenpunktAdresseDa, OffsetDa);
            DatenDa03[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDa, 3, _datenstruktur.DigOutput, DatenpunktAdresseDa, OffsetDa);
            DatenDa04[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDa, 4, _datenstruktur.DigOutput, DatenpunktAdresseDa, OffsetDa);
            DatenDa05[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDa, 5, _datenstruktur.DigOutput, DatenpunktAdresseDa, OffsetDa);
            DatenDa06[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDa, 6, _datenstruktur.DigOutput, DatenpunktAdresseDa, OffsetDa);
            DatenDa07[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDa, 7, _datenstruktur.DigOutput, DatenpunktAdresseDa, OffsetDa);
            DatenDa08[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDa, 8, _datenstruktur.DigOutput, DatenpunktAdresseDa, OffsetDa);
            DatenDa09[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDa, 9, _datenstruktur.DigOutput, DatenpunktAdresseDa, OffsetDa);
            DatenDa10[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDa, 10, _datenstruktur.DigOutput, DatenpunktAdresseDa, OffsetDa);
            DatenDa11[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDa, 11, _datenstruktur.DigOutput, DatenpunktAdresseDa, OffsetDa);
            DatenDa12[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDa, 12, _datenstruktur.DigOutput, DatenpunktAdresseDa, OffsetDa);
            DatenDa13[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDa, 13, _datenstruktur.DigOutput, DatenpunktAdresseDa, OffsetDa);
            DatenDa14[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDa, 14, _datenstruktur.DigOutput, DatenpunktAdresseDa, OffsetDa);
            DatenDa15[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDa, 15, _datenstruktur.DigOutput, DatenpunktAdresseDa, OffsetDa);

            DatenDi00[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDi, 0, _datenstruktur.DigInput, DatenpunktAdresseDi, OffsetDi);
            DatenDi01[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDi, 1, _datenstruktur.DigInput, DatenpunktAdresseDi, OffsetDi);
            DatenDi02[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDi, 2, _datenstruktur.DigInput, DatenpunktAdresseDi, OffsetDi);
            DatenDi03[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDi, 3, _datenstruktur.DigInput, DatenpunktAdresseDi, OffsetDi);
            DatenDi04[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDi, 4, _datenstruktur.DigInput, DatenpunktAdresseDi, OffsetDi);
            DatenDi05[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDi, 5, _datenstruktur.DigInput, DatenpunktAdresseDi, OffsetDi);
            DatenDi06[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDi, 6, _datenstruktur.DigInput, DatenpunktAdresseDi, OffsetDi);
            DatenDi07[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDi, 7, _datenstruktur.DigInput, DatenpunktAdresseDi, OffsetDi);
            DatenDi08[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDi, 8, _datenstruktur.DigInput, DatenpunktAdresseDi, OffsetDi);
            DatenDi09[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDi, 9, _datenstruktur.DigInput, DatenpunktAdresseDi, OffsetDi);
            DatenDi10[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDi, 10, _datenstruktur.DigInput, DatenpunktAdresseDi, OffsetDi);
            DatenDi11[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDi, 11, _datenstruktur.DigInput, DatenpunktAdresseDi, OffsetDi);
            DatenDi12[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDi, 12, _datenstruktur.DigInput, DatenpunktAdresseDi, OffsetDi);
            DatenDi13[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDi, 13, _datenstruktur.DigInput, DatenpunktAdresseDi, OffsetDi);
            DatenDi14[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDi, 14, _datenstruktur.DigInput, DatenpunktAdresseDi, OffsetDi);
            DatenDi15[_pltNextDataIndex] = DatenpunktAuswerten(DatenpunktAktivDi, 15, _datenstruktur.DigInput, DatenpunktAdresseDi, OffsetDi);

            _pltNextDataIndex++;    // Nur ein einziges Mal aufzeichnen!
        }
        private static double DatenpunktAuswerten(IReadOnlyList<bool> aktiv, int i, IReadOnlyList<byte> datenInOutput, IReadOnlyList<int> datenAdresse, IReadOnlyList<double> offset)
        {
            if (!aktiv[i]) return 0;
            if (BitTesten(datenInOutput, datenAdresse[i])) return 1 + offset[i];
            return offset[i];
        }
        private void RenderPlot(object sender, EventArgs e)
        {
            _plotWindow.WpfPlot.plt.AxisAuto(0);
            _plotWindow.WpfPlot.Render(true);
        }
        private static bool BitTesten(IReadOnlyList<byte> datenArray, int i)
        {
            var ibyte = i / 8;
            var bitMuster = (byte)(1 << i % 8);

            return (datenArray[ibyte] & bitMuster) == bitMuster;
        }
        private void ResetPlot()
        {
            for (var i = 0; i < AnzahlDatenpunkte; i++)
            {
                DatenDa00[i] = OffsetDa[0];
                DatenDa01[i] = OffsetDa[1];
                DatenDa02[i] = OffsetDa[2];
                DatenDa03[i] = OffsetDa[3];
                DatenDa04[i] = OffsetDa[4];
                DatenDa05[i] = OffsetDa[5];
                DatenDa06[i] = OffsetDa[6];
                DatenDa07[i] = OffsetDa[7];
                DatenDa08[i] = OffsetDa[8];
                DatenDa09[i] = OffsetDa[9];
                DatenDa10[i] = OffsetDa[10];
                DatenDa11[i] = OffsetDa[11];
                DatenDa12[i] = OffsetDa[12];
                DatenDa13[i] = OffsetDa[13];
                DatenDa14[i] = OffsetDa[14];
                DatenDa15[i] = OffsetDa[15];

                DatenDi00[i] = OffsetDi[0];
                DatenDi01[i] = OffsetDi[1];
                DatenDi02[i] = OffsetDi[2];
                DatenDi03[i] = OffsetDi[3];
                DatenDi04[i] = OffsetDi[4];
                DatenDi05[i] = OffsetDi[5];
                DatenDi06[i] = OffsetDi[6];
                DatenDi07[i] = OffsetDi[7];
                DatenDi08[i] = OffsetDi[8];
                DatenDi09[i] = OffsetDi[9];
                DatenDi10[i] = OffsetDi[10];
                DatenDi11[i] = OffsetDi[11];
                DatenDi12[i] = OffsetDi[12];
                DatenDi13[i] = OffsetDi[13];
                DatenDi14[i] = OffsetDi[14];
                DatenDi15[i] = OffsetDi[15];
            }

            _pltNextDataIndex = 0;
        }
    }
}