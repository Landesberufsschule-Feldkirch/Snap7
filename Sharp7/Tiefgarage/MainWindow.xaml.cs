﻿using Kommunikation;
using System.Windows;

namespace Tiefgarage
{
    public partial class MainWindow : Window
    {
        public S7_1200 S7_1200 { get; set; }

        private readonly ViewModel.ViewModel viewModel;
        private readonly DatenRangieren datenRangieren;
        private const int anzByteVersion = 10;
        private const int anzByteDigInput = 1;
        private const int anzByteDigOutput = 2;
        private const int anzByteAnalogInput = 0;
        private const int anzByteAnalogOutput = 0;
        public MainWindow()
        {
            viewModel = new ViewModel.ViewModel(this);
            datenRangieren = new DatenRangieren(viewModel);

            InitializeComponent();
            DataContext = viewModel;

            S7_1200 = new S7_1200(anzByteVersion, anzByteDigInput, anzByteDigOutput, anzByteAnalogInput, anzByteAnalogOutput, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);
        }
    }
}