using System.Windows;

namespace LAP_2010_3_Ofentuersteuerung.SetManual
{
    public partial class SetManual : Window
    {
        public SetManual(LAP_2010_3_Ofentuersteuerung.ViewModel.OfensteuerungViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
