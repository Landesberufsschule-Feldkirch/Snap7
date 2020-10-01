using System.Windows.Input;
using System.Windows.Media;
using ManualMode.Commands;

namespace ManualMode.ViewModel
{
    public class ManualViewModel
    {
        private readonly ManualMode _manualMode;
        
    
        public ManualMode ManualMode => _manualMode;
        public ManualVisuAnzeigen ManVisuAnzeigen { get; set; }
        public ManualViewModel(ManualMode manualMode)
        {
            _manualMode = manualMode;
            ManVisuAnzeigen = new ManualVisuAnzeigen(_manualMode);
        }


        private ICommand _btnBuchstabe;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnBuchstabe => _btnBuchstabe ?? (_btnBuchstabe = new RelayCommand(ManVisuAnzeigen.Buchstabe));

    }
}