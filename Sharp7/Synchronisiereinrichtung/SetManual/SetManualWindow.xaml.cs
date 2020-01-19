using System.Windows;

namespace Synchronisiereinrichtung
{
    public partial class SetManualWindow : Window
    {

        public SetManualWindow( Synchronisiereinrichtung.Kraftwerk.ViewModel.KraftwerkViewModel kraftwerkViewModel)
        {
            InitializeComponent();
            DataContext = kraftwerkViewModel;
        }
    }
}
