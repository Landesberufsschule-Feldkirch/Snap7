namespace Synchronisiereinrichtung.SetManual
{
    public partial class SetManualWindow
    {
        public SetManualWindow(ViewModel.ViewModel kraftwerkViewModel)
        {
            InitializeComponent();
            DataContext = kraftwerkViewModel;
        }
    }
}