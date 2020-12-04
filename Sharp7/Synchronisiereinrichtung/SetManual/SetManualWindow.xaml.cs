namespace Synchronisiereinrichtung.SetManual
{
    public partial class SetManualWindow
    {
        public SetManualWindow(Kraftwerk.ViewModel.ViewModel kraftwerkViewModel)
        {
            InitializeComponent();
            DataContext = kraftwerkViewModel;
        }
    }
}