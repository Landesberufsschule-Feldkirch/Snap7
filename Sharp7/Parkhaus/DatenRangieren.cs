﻿namespace Parkhaus
{
    using Sharp7;

    public class DatenRangieren
    {
        private readonly ViewModel.ViewModel _viewModel;

       public enum BitPosAusgang
        {
            P1R = 0,
            P1L,
            P2R,
            P2L,
            P3R,
            P3L,
            P4R,
            P4L,
            P5R,
            P5L
        }

        public void RangierenInput(byte[] digInput, byte[] _)
        {
            
        }

        public void RangierenOutput(byte[] digOutput, byte[] _)
        {
          
        }

        public DatenRangieren(MainWindow mainWindow, ViewModel.ViewModel vm) => _viewModel = vm;
    }
}