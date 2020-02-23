﻿using Kommunikation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WordClock
{
    public partial class MainWindow : Window
    {
        private DatenRangieren datenRangieren;
        private ViewModel.WordClockViewModel wordClockViewModel;
        public S7_1200 S7_1200 { get; set; } 

        public MainWindow()
        {

            wordClockViewModel = new ViewModel.WordClockViewModel(this);

            datenRangieren = new DatenRangieren(wordClockViewModel);

            InitializeComponent();
            DataContext = wordClockViewModel;

            S7_1200 = new S7_1200(9, 0, 0, 0, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            for (double i = 0; i < 360; i += 30) RotiertesRechteckHinzufuegen(8, 30, i);
            for (double i = 0; i < 360; i += 6) RotiertesRechteckHinzufuegen(2, 10, i);
        }

        private void RotiertesRechteckHinzufuegen(double breite, double hoehe, double winkel)
        {
            Rectangle rect = new Rectangle();
            rect.Width = breite;
            rect.Height = hoehe;
            rect.Fill = Brushes.Black;

            RotateTransform rotateTransform = new RotateTransform(winkel, breite / 2, 150);
            rect.RenderTransform = rotateTransform;

            Canvas.SetLeft(rect, 150 - breite / 2);
            Canvas.SetTop(rect, 0);

            canvasUhr.Children.Add(rect);
        }
    }
}