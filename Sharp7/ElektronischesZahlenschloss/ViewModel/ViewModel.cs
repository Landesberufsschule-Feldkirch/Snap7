using ElektronischesZahlenschloss.Commands;
using System.Windows.Input;

namespace ElektronischesZahlenschloss.ViewModel
{
    public class ViewModel
    {
        public VisuAnzeigen ViAnzeige { get; set; }
        public readonly Model.Zahlenschloss zahlenschloss;

        public ViewModel(MainWindow mainWindow)
        {
            zahlenschloss = new Model.Zahlenschloss(mainWindow);
            ViAnzeige = new VisuAnzeigen(mainWindow, zahlenschloss);
        }

        // ReSharper disable once UnusedMember.Global
        public Model.Zahlenschloss Zahlenschloss => zahlenschloss;

        #region BtnBuchstabe

        private ICommand _btnBuchstabe;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnBuchstabe => _btnBuchstabe ?? (_btnBuchstabe = new RelayCommand(ViAnzeige.Buchstabe));

        #endregion BtnBuchstabe
    }
}