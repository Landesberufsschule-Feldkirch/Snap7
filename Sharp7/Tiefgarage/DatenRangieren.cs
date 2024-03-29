﻿using Kommunikation;

namespace Tiefgarage
{
    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;
        private IPlc _plc;

        private enum BitPosEingang
        {
            B1 = 0,
            B2 = 1
        }

        public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _viewModel.AlleFahrzeugePersonen.B1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _viewModel.AlleFahrzeugePersonen.B2);
            }

            _viewModel.AlleFahrzeugePersonen.AnzahlAutos = _plc.GetUsIntAt(datenstruktur.DigOutput, 0);
            _viewModel.AlleFahrzeugePersonen.AnzahlPersonen = _plc.GetUsIntAt(datenstruktur.DigOutput, 1);
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}