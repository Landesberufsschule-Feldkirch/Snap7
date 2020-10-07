namespace AmpelsteuerungKieswerk
{
    using Model;
    using Sharp7;
    using System;

    public class DatenRangieren
    {
        public event EventHandler<AmpelZustandEventArgs> AmpelChangedEvent;

        private readonly ViewModel.ViewModel _viewModel;
        private AmpelZustand _ampelLinks = AmpelZustand.Rot;
        private AmpelZustand _ampelRechts = AmpelZustand.Rot;

        private enum BitPosAusgang
        {
            P1 = 0, // Ampel links rot
            P2,     // Ampel links gelb
            P3,     // Ampel links grün
            P4,     // Ampel rechts rot
            P5,     // Ampel rechts gelb
            P6      // Ampel rechts grün
        }

        private enum BitPosEingang
        {
            B1 = 0,
            B2,
            B3,
            B4
        }

        public void RangierenInput(Kommunikation.Datenstruktur datenstruktur)
        {
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B1, _viewModel.AlleLastKraftWagen.B1);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B2, _viewModel.AlleLastKraftWagen.B2);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B3, _viewModel.AlleLastKraftWagen.B3);
            S7.SetBitAt(datenstruktur.DigInput, (int)BitPosEingang.B4, _viewModel.AlleLastKraftWagen.B4);
        }

        public void RangierenOutput(Kommunikation.Datenstruktur datenstruktur)
        {
            var p1LinksRot = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
            var p2LinksGelb = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2);
            var p3LinksGruen = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P3);
            var p4RechtsRot = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P4);
            var p5RechtsGelb = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P5);
            var p6RechtsGruen = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P6);

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
            AmpelChangedEvent += _viewModel.ViAnzeige.DatenRangieren_AmpelChangedEvent;
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
    }
}