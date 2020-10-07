﻿using Sharp7;

namespace LAP_2018_3_Hydraulikaggregat
{
    public class DatenRangieren
    {
        private readonly MainWindow _mainWindow;
        private readonly ViewModel.ViewModel _viewModel;

        private enum BitPosAusgang
        {
            P1 = 0, // Meldeleuchte Störung Motor
            P2,     // Meldeleuchte Überdruck
            P3,     // Meldeleuchte Druck erreicht
            P4,     // Meldeleuchte Ölstand min.
            Q1,     // Netzschütz
            Q2,     // Sternschütz
            Q3      // Dreieckschütz
        }

        private enum BitPosEingang
        {
            B1 = 0, // Sensor Ölstand
            B2,     // Sensor Druck erreicht
            B3,     // Sensor Überdruck
            F1,     // Motorstörung
            S1,     // Taster Start
            S2,     // Taster Stop
            S3      // Taster Quittieren
        }

        public void RangierenInput(Kommunikation.Datenstruktur datenstruktur)
        {
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _viewModel.Hydraulikaggregat.B1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _viewModel.Hydraulikaggregat.B2);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B3, _viewModel.Hydraulikaggregat.B3);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.F1, _viewModel.Hydraulikaggregat.F1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.Hydraulikaggregat.S1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _viewModel.Hydraulikaggregat.S2);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S3, _viewModel.Hydraulikaggregat.S3);
        }

        public void RangierenOutput(Kommunikation.Datenstruktur datenstruktur)
        {
            _viewModel.Hydraulikaggregat.P1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
            _viewModel.Hydraulikaggregat.P2 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2);
            _viewModel.Hydraulikaggregat.P3 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P3);
            _viewModel.Hydraulikaggregat.P4 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P4);

            if (!_mainWindow.DebugWindowAktiv)
            {
                _viewModel.Hydraulikaggregat.Q1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
                _viewModel.Hydraulikaggregat.Q2 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q2);
                _viewModel.Hydraulikaggregat.Q3 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q3);
            }
        }

        public DatenRangieren(MainWindow mw, ViewModel.ViewModel vm)
        {
            _mainWindow = mw;
            _viewModel = vm;
        }
    }
}