using System.Windows;

namespace Synchronisiereinrichtung
{
    public partial class SetManualWindow : Window
    {
        public SetManualWindow(Synchronisiereinrichtung.kraftwerk.ViewModel.ViewModel kraftwerkViewModel)
        {
            InitializeComponent();
            DataContext = kraftwerkViewModel;
        }
    }
}