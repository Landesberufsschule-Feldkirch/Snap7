namespace ElektronischesZahlenschloss
{
    using Sharp7;

    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;

        private enum BitPosAusgang
        {
            P1 = 0, // Lampe rot
            P2      // Lampe grün
        }

        public void RangierenInput(Kommunikation.Datenstruktur datenstruktur)
        {
            S7.SetUSIntAt(datenstruktur.AnalogInput, 0, (byte)_viewModel.Zahlenschloss.Zeichen);
        }

        public void RangierenOutput(Kommunikation.Datenstruktur datenstruktur)
        {
            _viewModel.Zahlenschloss.P1 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P1);
            _viewModel.Zahlenschloss.P2 = S7.GetBitAt(datenstruktur.DigOutput, (int)BitPosAusgang.P2);

            _viewModel.Zahlenschloss.CodeAnzeige = S7.GetUIntAt(datenstruktur.AnalogOutput, 0);
        }

        public DatenRangieren(ViewModel.ViewModel vm) => _viewModel = vm;
    }
}