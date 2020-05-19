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

        #region BtnAufraeumen
        private ICommand _BtnAufraeumen;
        public ICommand BtnAufraeumen
        {
            get
            {
                if (_BtnAufraeumen == null)
                {
                    _BtnAufraeumen = new RelayCommand(p => ViAnzeige.AllesAufraeumen(), p => true);
                }
                return _BtnAufraeumen;
            }
        }
        #endregion BtnAufraeumen

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