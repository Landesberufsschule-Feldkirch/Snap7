using ElektronischesZahlenschloss.Commands;
using System.Windows.Input;

namespace ElektronischesZahlenschloss.ViewModel
{
    public class ViewModel
    {
        public Model.Zahlenschloss Zahlenschloss { get; }
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            Zahlenschloss = new Model.Zahlenschloss();
            ViAnzeige = new VisuAnzeigen(mainWindow, Zahlenschloss);
        }

        private ICommand _btnBuchstabe;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnBuchstabe => _btnBuchstabe ??= new RelayCommand(ViAnzeige.Buchstabe);
    }
}