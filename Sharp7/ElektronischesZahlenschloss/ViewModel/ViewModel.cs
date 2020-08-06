using ElektronischesZahlenschloss.Commands;
using System.Windows.Input;

namespace ElektronischesZahlenschloss.ViewModel
{
    public class ViewModel
    {
        public VisuAnzeigen ViAnzeige { get; set; }
        public readonly ElektronischesZahlenschloss.Model.Zahlenschloss zahlenschloss;

        public ViewModel(MainWindow mainWindow)
        {
            zahlenschloss = new Model.Zahlenschloss(mainWindow);
            ViAnzeige = new VisuAnzeigen(mainWindow, zahlenschloss);
        }

        public Model.Zahlenschloss Zahlenschloss => zahlenschloss;

        #region BtnBuchstabe

        private ICommand _btnBuchstabe;

        public ICommand BtnBuchstabe => _btnBuchstabe ?? (_btnBuchstabe = new RelayCommand(ViAnzeige.Buchstabe));

        #endregion BtnBuchstabe
    }
}