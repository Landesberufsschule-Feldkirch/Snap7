using AutomatischesLagersystem._3D;
using HelixToolkit.Wpf;
using Kommunikation;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace AutomatischesLagersystem
{
    public partial class MainWindow : Window
    {

        public SetManual.SetManualWindow SetManualWindow { get; set; }
        public bool DebugWindowAktiv { get; set; }
        public S7_1200 S7_1200 { get; set; }

        private readonly DatenRangieren datenRangieren;
        private readonly ViewModel.ViewModel viewModel;

        private DreiD dreiD;
     
        public MainWindow()
        {
            viewModel = new ViewModel.ViewModel(this);
            datenRangieren = new DatenRangieren(this, viewModel);

            InitializeComponent();

            DataContext = viewModel;
            S7_1200 = new S7_1200(2, 2, 2, 2, datenRangieren.RangierenInput, datenRangieren.RangierenOutput);

            dreiD = new DreiD(viewPort3d);

         


        }

        private void DebugWindowOeffnen(object sender, RoutedEventArgs e)
        {
            DebugWindowAktiv = true;
            SetManualWindow = new SetManual.SetManualWindow(viewModel);
            SetManualWindow.Show();
        }

       
      
    }
}
