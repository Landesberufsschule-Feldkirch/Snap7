using System.Windows;

namespace LAP_2010_4_Abfuellanlage.SetManual
{
    public partial class SetManual : Window
    {
        public SetManual(LAP_2010_4_Abfuellanlage.ViewModel.ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}