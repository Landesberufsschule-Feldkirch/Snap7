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

        public Model.AutomatischesLagersystem AutomatischesLagersystem { get { return automatischesLagersystem; } }

        #region BtnAusraeumen
        private ICommand _BtnAusraeumen;
        public ICommand BtnAusraeumen
        {
            get
            {
                if (_BtnAusraeumen == null)
                {
                    _BtnAusraeumen = new RelayCommand(p => ViAnzeige.AllesAusraeumen(), p => true);
                }
                return _BtnAusraeumen;
            }
        }
        #endregion BtnAufraeumen

        #region BtnEinraeumen
        private ICommand _BtnEinraeumen;
        public ICommand BtnEinraeumen
        {
            get
            {
                if (_BtnEinraeumen == null)
                {
                    _BtnEinraeumen = new RelayCommand(p => ViAnzeige.AllesEinraeumen(), p => true);
                }
                return _BtnEinraeumen;
            }
        }
        #endregion BtnEinraeumen

        #region BtnReset
        private ICommand _btnReset;
        public ICommand BtnReset
        {
            get
            {
                if (_btnReset == null)
                {
                    _btnReset = new RelayCommand(p => ViAnzeige.AllesReset(), p => true);
                }
                return _btnReset;
            }
        }
        #endregion BtnReset

        #region BtnAktiv

        private ICommand _btnAktiv;

        public ICommand BtnAktiv
        {
            get
            {
                if (_btnAktiv == null)
                {
                    _btnAktiv = new RelayCommand(p => ViAnzeige.SetButtonsAktiv(), p => true);
                }
                return _btnAktiv;
            }
        }

        #endregion BtnK1


        #region BtnK1

        private ICommand _btnK1;

        public ICommand BtnK1
        {
            get
            {
                if (_btnK1 == null)
                {
                    _btnK1 = new RelayCommand(p => ViAnzeige.SetK1(), p => true);
                }
                return _btnK1;
            }
        }

        #endregion BtnK1

        #region BtnK2

        private ICommand _btnK2;

        public ICommand BtnK2
        {
            get
            {
                if (_btnK2 == null)
                {
                    _btnK2 = new RelayCommand(p => ViAnzeige.SetK2(), p => true);
                }
                return _btnK2;
            }
        }

        #endregion BtnK2

        #region BtnK3

        private ICommand _btnK3;

        public ICommand BtnK3
        {
            get
            {
                if (_btnK3 == null)
                {
                    _btnK3 = new RelayCommand(p => ViAnzeige.SetK3(), p => true);
                }
                return _btnK3;
            }
        }

        #endregion BtnK3

        #region BtnK4

        private ICommand _btnK4;

        public ICommand BtnK4
        {
            get
            {
                if (_btnK4 == null)
                {
                    _btnK4 = new RelayCommand(p => ViAnzeige.SetK4(), p => true);
                }
                return _btnK4;
            }
        }

        #endregion BtnK4

        #region BtnK5

        private ICommand _btnK5;

        public ICommand BtnK5
        {
            get
            {
                if (_btnK5 == null)
                {
                    _btnK5 = new RelayCommand(p => ViAnzeige.SetK5(), p => true);
                }
                return _btnK5;
            }
        }

        #endregion BtnK5

        #region BtnK6

        private ICommand _btnK6;

        public ICommand BtnK6
        {
            get
            {
                if (_btnK6 == null)
                {
                    _btnK6 = new RelayCommand(p => ViAnzeige.SetK6(), p => true);
                }
                return _btnK6;
            }
        }

        #endregion BtnK6


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