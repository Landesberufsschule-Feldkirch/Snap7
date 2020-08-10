using AutomatischesLagersystem.Commands;
using System.Windows.Input;

namespace AutomatischesLagersystem.ViewModel
{
    public class ViewModel
    {
        private readonly AutomatischesLagersystem.Model.AutomatischesLagersystem _automatischesLagersystem;
        public Model.AutomatischesLagersystem AutomatischesLagersystem => _automatischesLagersystem;
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            _automatischesLagersystem = new Model.AutomatischesLagersystem(mainWindow);
            ViAnzeige = new VisuAnzeigen(mainWindow, _automatischesLagersystem);
        }



        private ICommand _btnAusraeumen;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAusraeumen =>
            _btnAusraeumen ??
            (_btnAusraeumen = new RelayCommand(p => ViAnzeige.AllesAusraeumen(), p => true));

        private ICommand _btnEinraeumen;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnEinraeumen =>
            _btnEinraeumen ??
            (_btnEinraeumen = new RelayCommand(p => ViAnzeige.AllesEinraeumen(), p => true));

        private ICommand _btnReset;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnReset => _btnReset ?? (_btnReset = new RelayCommand(p => ViAnzeige.AllesReset(), p => true));

        private ICommand _btnAktiv;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnAktiv => _btnAktiv ?? (_btnAktiv = new RelayCommand(p => ViAnzeige.SetButtonsAktiv(), p => true));

        private ICommand _btnK1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnK1 => _btnK1 ?? (_btnK1 = new RelayCommand(p => ViAnzeige.SetK1(), p => true));

        private ICommand _btnK2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnK2 => _btnK2 ?? (_btnK2 = new RelayCommand(p => ViAnzeige.SetK2(), p => true));

        private ICommand _btnK3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnK3 => _btnK3 ?? (_btnK3 = new RelayCommand(p => ViAnzeige.SetK3(), p => true));

        private ICommand _btnK4;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnK4 => _btnK4 ?? (_btnK4 = new RelayCommand(p => ViAnzeige.SetK4(), p => true));

        private ICommand _btnK5;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnK5 => _btnK5 ?? (_btnK5 = new RelayCommand(p => ViAnzeige.SetK5(), p => true));

        private ICommand _btnK6;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnK6 => _btnK6 ?? (_btnK6 = new RelayCommand(p => ViAnzeige.SetK6(), p => true));

        private ICommand _btnBuchstabe;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnBuchstabe => _btnBuchstabe ?? (_btnBuchstabe = new RelayCommand(ViAnzeige.Buchstabe));
    }
}