﻿using Kommunikation;
using System.Windows;

namespace AmpelsteuerungKieswerk
{
    public partial class MainWindow : Window
    {
        public S7_1200 S7_1200 { get; set; }

        private readonly ViewModel.AmpelsteuerungKieswerkViewModel ampelsteuerungKieswerkViewModel;
        private readonly DatenRangieren datenRangieren;

        public MainWindow()
        {
            ampelsteuerungKieswerkViewModel = new ViewModel.AmpelsteuerungKieswerkViewModel(this);
            datenRangieren = new DatenRangieren(this, ampelsteuerungKieswerkViewModel);

            InitializeComponent();
            DataContext = ampelsteuerungKieswerkViewModel;

            S7_1200 = new S7_1200(1, 1, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }
    }
}