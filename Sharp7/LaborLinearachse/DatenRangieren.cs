﻿using Kommunikation;

namespace LaborLinearachse
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;
        private IPlc _plc;

        private enum BitPosAusgang
        {
            Q1 = 0,     // 0.0  Linearachse Rechtslauf
            Q2 = 1,     // 0.1  Linearachse Linkslauf
            P1 = 2,     // 0.2  Meldeleuchte im Taster S1/S2 (weiß) 
            P2 = 3,     // 0.3  Meldeleuchte weiß
            P3 = 4,     // 0.4  Meldeleuchte rot
            P4 = 5       // 0.5  Meldeleuchte grün

        }

        private enum BitPosEingang
        {
            B1 = 0,     // 0.0  Linearachse Endlage links → Öffner
            B2 = 1,     // 0.1  Linearachse Endlage rechts → Öffner
            S2 = 2,     // 0.2  Taster ( ⓪ ) → Öffner 
            S1 = 3,     // 0.3  Taster ( ① ) → Schliesser 
            S4 = 4,     // 0.4  Taster ( Ⅱ ) → Schliesser 
            S3 = 5,     // 0.5  Taster ( Ⅰ ) → Schliesser
            S6 = 6,     // 0.6  Taster ( ↓ ) → Schliesser
            S5 = 7,     // 0.7  Taster ( ↑ ) → Schliesser 
            S8 = 8,     // 1.0  Taster ( － ) → Schliesser 
            S7 = 9,     // 1.1  Taster ( + ) → Schliesser 
            S9 = 10,    // 1.2  Taster ( STOP ) → Öffner 
            S10 = 11,   // 1.3  Not-Halt → Öffner
            S11 = 12     // 1.4  Not-Halt → Schliesser 

        }

        public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _viewModel.Linearachse.B1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _viewModel.Linearachse.B2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S1, _viewModel.Linearachse.S1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S2, _viewModel.Linearachse.S2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S3, _viewModel.Linearachse.S3);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S4, _viewModel.Linearachse.S4);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S5, _viewModel.Linearachse.S5);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S6, _viewModel.Linearachse.S6);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S7, _viewModel.Linearachse.S7);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S8, _viewModel.Linearachse.S8);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S9, _viewModel.Linearachse.S9);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S10, _viewModel.Linearachse.S10);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.S11, _viewModel.Linearachse.S11);
            }

            _viewModel.Linearachse.P1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
            _viewModel.Linearachse.P2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2);
            _viewModel.Linearachse.P3 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P3);
            _viewModel.Linearachse.P4 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P4);
            _viewModel.Linearachse.Q1 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q1);
            _viewModel.Linearachse.Q2 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.Q2);
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}