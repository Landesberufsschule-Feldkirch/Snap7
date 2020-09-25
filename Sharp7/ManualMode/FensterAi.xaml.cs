using ManualMode.ViewModel;

namespace ManualMode
{
    public partial class FensterAi
    {
        public FensterAi(Model.ConfigAi configAi,  ManualViewModel mvm)
        {
            var manualViewModel = mvm;
            InitializeComponent();
        }
    }
}
