using ManualMode.Commands;
using System.Windows.Input;

namespace ManualMode.ViewModel
{
    public class ManualViewModel
    {
        public ManualMode ManualMode { get; }

        public ManualVisuAnzeigen ManVisuAnzeigen { get; set; }
        public ManualViewModel(ManualMode manualMode)
        {
            ManualMode = manualMode;
            ManVisuAnzeigen = new ManualVisuAnzeigen(ManualMode);
        }


        private ICommand _btnTasten;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasten => _btnTasten ??= new RelayCommand(ManVisuAnzeigen.BtnTasten);


        private ICommand _btnToggeln;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnToggeln => _btnToggeln ??= new RelayCommand(ManVisuAnzeigen.BtnToggeln);
    }
}