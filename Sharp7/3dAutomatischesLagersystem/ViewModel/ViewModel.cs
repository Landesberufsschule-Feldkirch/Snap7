using AutomatischesLagersystem.Commands;
using System.Windows.Input;

namespace AutomatischesLagersystem.ViewModel
{
    public class ViewModel
    {
        public VisuAnzeigen ViAnzeige { get; set; }
        public readonly AutomatischesLagersystem.Model.AutomatischesLagersystem automatischesLagersystem;

        public ViewModel(MainWindow mainWindow)
        {
            automatischesLagersystem = new Model.AutomatischesLagersystem(mainWindow);
            ViAnzeige = new VisuAnzeigen(mainWindow, automatischesLagersystem);
        }

        public Model.AutomatischesLagersystem AutomatischesLagersystem => automatischesLagersystem;

        #region BtnAusraeumen

        private ICommand _btnAusraeumen;

        public ICommand BtnAusraeumen =>
            _btnAusraeumen ??
            (_btnAusraeumen = new RelayCommand(p => ViAnzeige.AllesAusraeumen(), p => true));

        #endregion BtnAusraeumen

        #region BtnEinraeumen

        private ICommand _btnEinraeumen;

        public ICommand BtnEinraeumen =>
            _btnEinraeumen ??
            (_btnEinraeumen = new RelayCommand(p => ViAnzeige.AllesEinraeumen(), p => true));

        #endregion BtnEinraeumen

        #region BtnReset

        private ICommand _btnReset;

        public ICommand BtnReset => _btnReset ?? (_btnReset = new RelayCommand(p => ViAnzeige.AllesReset(), p => true));

        #endregion BtnReset

        #region BtnAktiv

        private ICommand _btnAktiv;

        public ICommand BtnAktiv => _btnAktiv ?? (_btnAktiv = new RelayCommand(p => ViAnzeige.SetButtonsAktiv(), p => true));

        #endregion BtnAktiv

        #region BtnK1

        private ICommand _btnK1;

        public ICommand BtnK1 => _btnK1 ?? (_btnK1 = new RelayCommand(p => ViAnzeige.SetK1(), p => true));

        #endregion BtnK1

        #region BtnK2

        private ICommand _btnK2;

        public ICommand BtnK2 => _btnK2 ?? (_btnK2 = new RelayCommand(p => ViAnzeige.SetK2(), p => true));

        #endregion BtnK2

        #region BtnK3

        private ICommand _btnK3;

        public ICommand BtnK3 => _btnK3 ?? (_btnK3 = new RelayCommand(p => ViAnzeige.SetK3(), p => true));

        #endregion BtnK3

        #region BtnK4

        private ICommand _btnK4;

        public ICommand BtnK4 => _btnK4 ?? (_btnK4 = new RelayCommand(p => ViAnzeige.SetK4(), p => true));

        #endregion BtnK4

        #region BtnK5

        private ICommand _btnK5;

        public ICommand BtnK5 => _btnK5 ?? (_btnK5 = new RelayCommand(p => ViAnzeige.SetK5(), p => true));

        #endregion BtnK5

        #region BtnK6

        private ICommand _btnK6;

        public ICommand BtnK6 => _btnK6 ?? (_btnK6 = new RelayCommand(p => ViAnzeige.SetK6(), p => true));

        #endregion BtnK6

        #region BtnBuchstabe

        private ICommand _btnBuchstabe;

        public ICommand BtnBuchstabe => _btnBuchstabe ?? (_btnBuchstabe = new RelayCommand(ViAnzeige.Buchstabe));

        #endregion BtnBuchstabe
    }
}