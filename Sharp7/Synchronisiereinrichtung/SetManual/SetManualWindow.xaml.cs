namespace Synchronisiereinrichtung.SetManual
{
    public partial class SetManualWindow
    {
        public SetManualWindow(kraftwerk.ViewModel.ViewModel kraftwerkViewModel)
        {
            InitializeComponent();
            DataContext = kraftwerkViewModel;
        }
    }
}