using System.Windows;

namespace LAP_2018_1_Silosteuerung.SetManual
{
    public partial class SetManualWindow : Window
    {
        public SetManualWindow(LAP_2018_1_Silosteuerung.ViewModel.ViewModel foerderanlageViewModel)
        {
            InitializeComponent();
            DataContext = foerderanlageViewModel;
        }
    }
}
