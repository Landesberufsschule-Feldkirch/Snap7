using System.Windows;

namespace PaternosterLager.SetManual
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