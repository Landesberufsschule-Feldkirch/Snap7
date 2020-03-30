using System.Windows;

namespace LAP_2019_Foerderanlage
{
    public partial class SetManualWindow : Window
    {

        public SetManualWindow(LAP_2019_Foerderanlage.ViewModel.ViewModel foerderanlageViewModel)
        {
            InitializeComponent();
            DataContext = foerderanlageViewModel;
        }
    }
}