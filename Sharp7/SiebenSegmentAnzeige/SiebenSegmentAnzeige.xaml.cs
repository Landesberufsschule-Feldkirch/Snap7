﻿namespace SiebenSegmentAnzeige
{
    public partial class SiebenSegmentDisplay
    {
        public bool LedA { get; set; }
        public bool LedB { get; set; }
        public bool LedC { get; set; }
        public bool LedD { get; set; }
        public bool LedE { get; set; }
        public bool LedF { get; set; }
        public bool LedG { get; set; }
        public bool LedDp { get; set; }

        public SiebenSegmentDisplay()
        {
InitializeComponent();

            LedA = true;
            LedB = true;
            LedC = true;
            LedD = true;
            LedE = true;
            LedF = true;
            LedG = true;
            LedDp = true;


            var viewModel = new ViewModel.ViewModel(this);

            
            DataContext = viewModel;
        }
    }
}