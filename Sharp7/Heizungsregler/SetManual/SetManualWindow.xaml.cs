using System.Windows;

namespace Heizungsregler
{
    public partial class SetManualWindow : Window
    {
        public SetManualWindow(Heizungsregler.ViewModel.ViewModel heizungsreglerViewModel)
        {
            InitializeComponent();
            DataContext = heizungsreglerViewModel;
        }
    }
}