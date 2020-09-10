using ElektronischesZahlenschloss.Commands;
using System.Windows.Input;

namespace ElektronischesZahlenschloss.ViewModel
{
    public class ViewModel
    {
        private readonly Model.Zahlenschloss _zahlenschloss;
        public Model.Zahlenschloss Zahlenschloss => _zahlenschloss;
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            _zahlenschloss = new Model.Zahlenschloss(mainWindow);
            ViAnzeige = new VisuAnzeigen(mainWindow, _zahlenschloss);
        }


        private ICommand _btnBuchstabe;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnBuchstabe => _btnBuchstabe ??= new RelayCommand(ViAnzeige.Buchstabe);
    }
}