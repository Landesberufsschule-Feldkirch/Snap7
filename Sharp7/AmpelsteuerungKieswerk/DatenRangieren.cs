﻿using AmpelsteuerungKieswerk.Model;
using Kommunikation;
using System;

namespace AmpelsteuerungKieswerk
{
    public class DatenRangieren
    {
        public event EventHandler<AmpelZustandEventArgs> AmpelChangedEvent;

        private readonly ViewModel.ViewModel _viewModel;
        private IPlc _plc;
        private AmpelZustand _ampelLinks;
        private AmpelZustand _ampelRechts;

        private enum BitPosAusgang
        {
            P1 = 0,     // Ampel links rot
            P2 = 1,     // Ampel links gelb
            P3 = 2,     // Ampel links grün
            P4 = 3,     // Ampel rechts rot
            P5 = 4,     // Ampel rechts gelb
            P6 = 5,     // Ampel rechts grün
            P7 = 6,     // Oranges Licht links
            P8 = 7      // Oranges Licht rechts
        }
        private enum BitPosEingang
        {
            B1 = 0,
            B2 = 1,
            B3 = 2,
            B4 = 3
        }

        public void Rangieren(Datenstruktur datenstruktur, bool eingaengeRangieren)
        {
            if (eingaengeRangieren)
            {
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _viewModel.AlleLastKraftWagen.B1);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _viewModel.AlleLastKraftWagen.B2);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B3, _viewModel.AlleLastKraftWagen.B3);
                _plc.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B4, _viewModel.AlleLastKraftWagen.B4);
            }

            var p1LinksRot = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
            var p2LinksGelb = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2);
            var p3LinksGruen = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P3);
            var p4RechtsRot = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P4);
            var p5RechtsGelb = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P5);
            var p6RechtsGruen = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P6);
            _viewModel.AlleLastKraftWagen.P7 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P7);
            _viewModel.AlleLastKraftWagen.P8 = _plc.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P8);

            var linkeAmpel = GetAmpelZustand(p1LinksRot, p2LinksGelb, p3LinksGruen);
            var rechteAmpel = GetAmpelZustand(p4RechtsRot, p5RechtsGelb, p6RechtsGruen);

            if (linkeAmpel == _ampelLinks && rechteAmpel == _ampelRechts) return;
            OnAmpelChanged(new AmpelZustandEventArgs(linkeAmpel, rechteAmpel));

            _ampelRechts = rechteAmpel;
            _ampelLinks = linkeAmpel;
        }
        public DatenRangieren(ViewModel.ViewModel vm)
        {
            _viewModel = vm;
            AmpelChangedEvent += _viewModel.ViAnz.DatenRangieren_AmpelChangedEvent;
        }
        private void OnAmpelChanged(AmpelZustandEventArgs e)
        {
            AmpelChangedEvent?.Invoke(this, e);
        }
        private static AmpelZustand GetAmpelZustand(bool rot, bool gelb, bool gruen)
        {
            if (rot && gelb) return AmpelZustand.RotUndGelb;
            if (rot) return AmpelZustand.Rot;
            if (gelb) return AmpelZustand.Gelb;
            return gruen ? AmpelZustand.Gruen : AmpelZustand.Aus;
        }
        public void ReferenzUebergeben(IPlc plc) => _plc = plc;
    }
}