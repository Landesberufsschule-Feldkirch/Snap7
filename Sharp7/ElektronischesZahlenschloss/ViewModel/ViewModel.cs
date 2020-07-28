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

        public Model.Zahlenschloss Zahlenschloss { get { return zahlenschloss; } }

        #region BtnBuchstabe

        private ICommand _btnBuchstabe;

        public ICommand BtnBuchstabe
        {
            get
            {
                if (_btnBuchstabe == null)
                {
                    _btnBuchstabe = new RelayCommand(ViAnzeige.Buchstabe);
                }
                return _btnBuchstabe;
            }
        }

        #endregion BtnBuchstabe
    }
}