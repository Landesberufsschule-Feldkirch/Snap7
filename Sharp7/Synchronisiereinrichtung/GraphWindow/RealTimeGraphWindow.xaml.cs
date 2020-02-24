﻿using System.Windows;

namespace Synchronisiereinrichtung
{
    public partial class RealTimeGraphWindow : Window
    {
        public RealTimeGraphWindow(Synchronisiereinrichtung.kraftwerk.ViewModel.KraftwerkViewModel kraftwerkViewModel)
        {
            InitializeComponent();
            DataContext = kraftwerkViewModel.Schreiber;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
//
        }
    }
}