using ManualMode.ViewModel;

namespace ManualMode
{
    public partial class FensterDi
    {
        public FensterDi(Model.ConfigDi configDi, ManualViewModel mvm)
        {
            var manualViewModel = mvm;

            InitializeComponent();
        }
    }
}