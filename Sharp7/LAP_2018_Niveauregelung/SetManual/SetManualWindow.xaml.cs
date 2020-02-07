using System.Windows;

namespace LAP_2018_Niveauregelung.SetManual
{
    public partial class SetManualWindow : Window
    {
        public SetManualWindow(LAP_2018_Niveauregelung.ViewModel.NiveauRegelungViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
