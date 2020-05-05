using System.Windows;

namespace AutomatischesLagersystem.SetManual
{
    public partial class SetManualWindow : Window
    {
        public SetManualWindow(ViewModel.ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
