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

        public double[] DatenDi00 { get; set; }
        public double[] DatenDi01 { get; set; }
        public double[] DatenDi02 { get; set; }
        public double[] DatenDi03 { get; set; }
        public double[] DatenDi04 { get; set; }
        public double[] DatenDi05 { get; set; }
        public double[] DatenDi06 { get; set; }
        public double[] DatenDi07 { get; set; }

        private PlotWindow _plotWindow;

        private void PlotInitialisieren()
        {
            _plotWindow = new PlotWindow();

            DatenpunktAktivDa = new bool[16];
            DatenpunktAktivDi = new bool[16];
            DatenpunktAdresseDa = new int[16];
            DatenpunktAdresseDi = new int[16];

            Zeitachse = new double[1024];

            DatenDa00 = new double[1024];
            DatenDa01 = new double[1024];
            DatenDa02 = new double[1024];
            DatenDa03 = new double[1024];
            DatenDa04 = new double[1024];
            DatenDa05 = new double[1024];
            DatenDa06 = new double[1024];
            DatenDa07 = new double[1024];

            DatenDi00 = new double[1024];
            DatenDi01 = new double[1024];
            DatenDi02 = new double[1024];
            DatenDi03 = new double[1024];
            DatenDi04 = new double[1024];
            DatenDi05 = new double[1024];
            DatenDi06 = new double[1024];
            DatenDi07 = new double[1024];


            OffsetDa = new double[16];
            OffsetDi = new double[16];

            var start = new DateTime(2021, 1, 1);

            for (var i = 0; i < 1024; i++)
            {
                var dtNow = start.AddMilliseconds(i * 20);
                Zeitachse[i] = dtNow.ToOADate();
            }


            // create a timer to modify the data
            var updateDataTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(20) };
            updateDataTimer.Tick += UpdateData;
            updateDataTimer.Start();

            // create a timer to update the GUI
            var renderTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(20) };
            renderTimer.Tick += Render;
            renderTimer.Start();

        }
        public void UpdateBelegung()
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

            var offset = 1.0;

            if (BeschriftungenPlc.BeschriftungenStruktur.DaBeschriftungen != null)
            {
                var daBeschriftung = BeschriftungenPlc.BeschriftungenStruktur.DaBeschriftungen.DaBeschriftung;

                offset = DatenpunktDaPlotEinfuegen(0, daBeschriftung, DatenpunktAktivDa, DatenpunktAdresseDa, offset, OffsetDa, Color.GreenYellow, DatenDa00);
                offset = DatenpunktDaPlotEinfuegen(1, daBeschriftung, DatenpunktAktivDa, DatenpunktAdresseDa, offset, OffsetDa, Color.Green, DatenDa01);
                offset = DatenpunktDaPlotEinfuegen(2, daBeschriftung, DatenpunktAktivDa, DatenpunktAdresseDa, offset, OffsetDa, Color.Orange, DatenDa02);
                offset = DatenpunktDaPlotEinfuegen(3, daBeschriftung, DatenpunktAktivDa, DatenpunktAdresseDa, offset, OffsetDa, Color.DarkOrange, DatenDa03);
                offset = DatenpunktDaPlotEinfuegen(4, daBeschriftung, DatenpunktAktivDa, DatenpunktAdresseDa, offset, OffsetDa, Color.Salmon, DatenDa04);
                offset = DatenpunktDaPlotEinfuegen(5, daBeschriftung, DatenpunktAktivDa, DatenpunktAdresseDa, offset, OffsetDa, Color.LawnGreen, DatenDa05);
                offset = DatenpunktDaPlotEinfuegen(6, daBeschriftung, DatenpunktAktivDa, DatenpunktAdresseDa, offset, OffsetDa, Color.LawnGreen, DatenDa06);
                offset = DatenpunktDaPlotEinfuegen(7, daBeschriftung, DatenpunktAktivDa, DatenpunktAdresseDa, offset, OffsetDa, Color.LawnGreen, DatenDa07);
            }

            offset += 2;

            if (BeschriftungenPlc.BeschriftungenStruktur.DiBeschriftungen == null) return;

            var diBeschriftung = BeschriftungenPlc.BeschriftungenStruktur.DiBeschriftungen.DiBeschriftung;

            offset = DatenpunktDiPlotEinfuegen(0, diBeschriftung, DatenpunktAktivDi, DatenpunktAdresseDi, offset, OffsetDi, Color.Silver, DatenDi00);
            offset = DatenpunktDiPlotEinfuegen(1, diBeschriftung, DatenpunktAktivDi, DatenpunktAdresseDi, offset, OffsetDi, Color.DarkGray, DatenDi01);
            offset = DatenpunktDiPlotEinfuegen(2, diBeschriftung, DatenpunktAktivDi, DatenpunktAdresseDi, offset, OffsetDi, Color.Red, DatenDi02);
            offset = DatenpunktDiPlotEinfuegen(3, diBeschriftung, DatenpunktAktivDi, DatenpunktAdresseDi, offset, OffsetDi, Color.Red, DatenDi03);
            offset = DatenpunktDiPlotEinfuegen(4, diBeschriftung, DatenpunktAktivDi, DatenpunktAdresseDi, offset, OffsetDi, Color.Red, DatenDi04);
            offset = DatenpunktDiPlotEinfuegen(5, diBeschriftung, DatenpunktAktivDi, DatenpunktAdresseDi, offset, OffsetDi, Color.Red, DatenDi05);
            offset = DatenpunktDiPlotEinfuegen(6, diBeschriftung, DatenpunktAktivDi, DatenpunktAdresseDi, offset, OffsetDi, Color.Red, DatenDi06);
            DatenpunktDiPlotEinfuegen(7, diBeschriftung, DatenpunktAktivDi, DatenpunktAdresseDi, offset, OffsetDi, Color.Red, DatenDi07);

            _plotWindow.WpfPlot.plt.Ticks(dateTimeX: true, dateTimeFormatStringX: "HH:mm:ss.fff");
            _plotWindow.WpfPlot.plt.Legend();

        }
        private double DatenpunktDaPlotEinfuegen(int id, IReadOnlyList<DaDaten> daBeschriftung, IList<bool> datenpunktAktiv, IList<int> datenpunktAdresse, double offset, IList<double> offsetDatenpunkt, Color farbe, double[] datenPlot)
        {
            if (daBeschriftung.Count <= id) return offset;

            datenpunktAktiv[id] = true;
            offsetDatenpunkt[id] = offset;
            datenpunktAdresse[id] = daBeschriftung[id].Bit + 8 * daBeschriftung[id].Byte;
            _plotWindow.WpfPlot.plt.PlotScatter(Zeitachse, datenPlot, farbe, 2, 5, daBeschriftung[id].Bezeichnung);
            return offset + 1.5;
        }
        private double DatenpunktDiPlotEinfuegen(int id, IReadOnlyList<DiDaten> diBeschriftung, IList<bool> datenpunktAktiv, IList<int> datenpunktAdresse, double offset, IList<double> offsetDatenpunkt, Color farbe, double[] datenPlot)
        {
            if (diBeschriftung.Count <= id) return offset;

            datenpunktAktiv[id] = true;
            offsetDatenpunkt[id] = offset;
            datenpunktAdresse[id] = diBeschriftung[id].Bit + 8 * diBeschriftung[id].Byte;
            _plotWindow.WpfPlot.plt.PlotScatter(Zeitachse, datenPlot, farbe, 2, 5, diBeschriftung[id].Bezeichnung);
            return offset + 1.5;
        }

        private void UpdateData(object sender, EventArgs e)
        {
            if (_nextDataIndex >= 1024)
            {
                _nextDataIndex = 0;
            }

            DatenpunktAuswerten(DatenpunktAktivDa, 0, _nextDataIndex, _datenstruktur.DigOutput, DatenpunktAdresseDa, DatenDa00, OffsetDa);
            DatenpunktAuswerten(DatenpunktAktivDa, 1, _nextDataIndex, _datenstruktur.DigOutput, DatenpunktAdresseDa, DatenDa01, OffsetDa);
            DatenpunktAuswerten(DatenpunktAktivDa, 2, _nextDataIndex, _datenstruktur.DigOutput, DatenpunktAdresseDa, DatenDa02, OffsetDa);
            DatenpunktAuswerten(DatenpunktAktivDa, 3, _nextDataIndex, _datenstruktur.DigOutput, DatenpunktAdresseDa, DatenDa03, OffsetDa);
            DatenpunktAuswerten(DatenpunktAktivDa, 4, _nextDataIndex, _datenstruktur.DigOutput, DatenpunktAdresseDa, DatenDa04, OffsetDa);
            DatenpunktAuswerten(DatenpunktAktivDa, 5, _nextDataIndex, _datenstruktur.DigOutput, DatenpunktAdresseDa, DatenDa05, OffsetDa);
            DatenpunktAuswerten(DatenpunktAktivDa, 6, _nextDataIndex, _datenstruktur.DigOutput, DatenpunktAdresseDa, DatenDa06, OffsetDa);
            DatenpunktAuswerten(DatenpunktAktivDa, 7, _nextDataIndex, _datenstruktur.DigOutput, DatenpunktAdresseDa, DatenDa07, OffsetDa);

            DatenpunktAuswerten(DatenpunktAktivDi, 0, _nextDataIndex, _datenstruktur.DigInput, DatenpunktAdresseDi, DatenDi00, OffsetDi);
            DatenpunktAuswerten(DatenpunktAktivDi, 1, _nextDataIndex, _datenstruktur.DigInput, DatenpunktAdresseDi, DatenDi01, OffsetDi);
            DatenpunktAuswerten(DatenpunktAktivDi, 2, _nextDataIndex, _datenstruktur.DigInput, DatenpunktAdresseDi, DatenDi02, OffsetDi);
            DatenpunktAuswerten(DatenpunktAktivDi, 3, _nextDataIndex, _datenstruktur.DigInput, DatenpunktAdresseDi, DatenDi03, OffsetDi);
            DatenpunktAuswerten(DatenpunktAktivDi, 4, _nextDataIndex, _datenstruktur.DigInput, DatenpunktAdresseDi, DatenDi04, OffsetDi);
            DatenpunktAuswerten(DatenpunktAktivDi, 5, _nextDataIndex, _datenstruktur.DigInput, DatenpunktAdresseDi, DatenDi05, OffsetDi);
            DatenpunktAuswerten(DatenpunktAktivDi, 6, _nextDataIndex, _datenstruktur.DigInput, DatenpunktAdresseDi, DatenDi06, OffsetDi);
            DatenpunktAuswerten(DatenpunktAktivDi, 7, _nextDataIndex, _datenstruktur.DigInput, DatenpunktAdresseDi, DatenDi07, OffsetDi);

            _nextDataIndex++;
        }

        private static void DatenpunktAuswerten(IReadOnlyList<bool> aktiv, int i, int index, IReadOnlyList<byte> datenInOutput, IReadOnlyList<int> datenAdresse, IList<double> datenPlot, IReadOnlyList<double> offset)
        {
            if (!aktiv[i]) return;
            if (BitTesten(datenInOutput, datenAdresse[i])) datenPlot[index] = 1 + offset[i]; else datenPlot[index] = offset[i];

        }
        private void Render(object sender, EventArgs e)
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
    }
}