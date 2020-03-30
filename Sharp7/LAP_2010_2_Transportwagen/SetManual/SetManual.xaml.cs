using System.Windows;

namespace LAP_2010_2_Transportwagen.SetManual
{
    public partial class SetManual : Window
    {
        public SetManual(LAP_2010_2_Transportwagen.ViewModel.ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
