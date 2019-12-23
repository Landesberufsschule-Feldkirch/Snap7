using System.Windows;

namespace Synchronisiereinrichtung
{
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
