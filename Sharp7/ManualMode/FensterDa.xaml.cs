using ManualMode.ViewModel;

namespace ManualMode
{
    public partial class FensterDa
    {
        public FensterDa(Model.ConfigDa configDa,  ManualViewModel mvm)
        {
            var manualViewModel = mvm;
            InitializeComponent();
        }
    }
}
