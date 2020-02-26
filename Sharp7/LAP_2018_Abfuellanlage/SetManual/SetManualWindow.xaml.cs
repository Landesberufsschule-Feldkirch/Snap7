using System.Windows;

namespace LAP_2018_Abfuellanlage.SetManual
{
    public partial class SetManual : Window
    {
        public SetManual(LAP_2018_Abfuellanlage.ViewModel.AbfuellanlageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
