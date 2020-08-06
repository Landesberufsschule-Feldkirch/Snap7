namespace LAP_2019_Foerderanlage
{
    public partial class SetManualWindow
    {
        public SetManualWindow(LAP_2019_Foerderanlage.ViewModel.ViewModel foerderanlageViewModel)
        {
            InitializeComponent();
            DataContext = foerderanlageViewModel;
        }
    }
}