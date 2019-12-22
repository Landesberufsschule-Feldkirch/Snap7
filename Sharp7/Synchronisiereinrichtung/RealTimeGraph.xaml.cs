using System.Windows;

namespace Synchronisiereinrichtung
{
    /// <summary>
    /// Interaktionslogik für RealTimeGraph.xaml
    /// </summary>
    /// 


    public partial class RealTimeGraph : Window
    {
        ViewModel viewModel = new ViewModel();

        public RealTimeGraph()
        {
            InitializeComponent();
            viewModel.Start();
        }
    }
}
