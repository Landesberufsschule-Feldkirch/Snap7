using System.Windows;

namespace LAP_2010_5_Pumpensteuerung.SetManual
{
    public partial class SetManual : Window
    {
        public SetManual(LAP_2010_5_Pumpensteuerung.ViewModel.PumpensteuerungViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}