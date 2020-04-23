﻿namespace LAP_2010_4_Abfuellanlage.ViewModel
{
    using BehaelterSteuerung.Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public VisuAnzeigen ViAnzeige { get; set; }
        public readonly LAP_2010_4_Abfuellanlage.Model.AbfuellAnlage abfuellAnlage;

        public ViewModel(MainWindow mainWindow)
        {
            abfuellAnlage = new Model.AbfuellAnlage();
            ViAnzeige = new VisuAnzeigen(mainWindow, abfuellAnlage);
        }

        public Model.AbfuellAnlage AbfuellAnlage { get { return abfuellAnlage; } }

        #region BtnQ1

        private ICommand _btnQ1;

        public ICommand BtnQ1
        {
            get
            {
                if (_btnQ1 == null)
                {
                    _btnQ1 = new RelayCommand(p => ViAnzeige.SetQ1(), p => true);
                }
                return _btnQ1;
            }
        }

        #endregion BtnQ1

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

        #region BtnReset

        private ICommand _btnReset;

        public ICommand BtnReset
        {
            get
            {
                if (_btnReset == null)
                {
                    _btnReset = new RelayCommand(p => abfuellAnlage.AllesReset(), p => true);
                }
                return _btnReset;
            }
        }

        #endregion BtnReset

        #region BtnNachuellen

        private ICommand _btnNachuellen;

        public ICommand BtnNachuellen
        {
            get
            {
                if (_btnNachuellen == null)
                {
                    _btnNachuellen = new RelayCommand(p => abfuellAnlage.Nachfuellen(), p => true);
                }
                return _btnNachuellen;
            }
        }

        #endregion BtnNachuellen

        #region BtnS1

        private ICommand _btnS1;

        public ICommand BtnS1
        {
            get
            {
                if (_btnS1 == null)
                {
                    _btnS1 = new RelayCommand(p => ViAnzeige.SetS1(), p => true);
                }
                return _btnS1;
            }
        }

        #endregion BtnS1

        #region BtnS2

        private ICommand _btnS2;

        public ICommand BtnS2
        {
            get
            {
                if (_btnS2 == null)
                {
                    _btnS2 = new RelayCommand(p => ViAnzeige.BtnS2(), p => true);
                }
                return _btnS2;
            }
        }

        #endregion BtnS2
    }
}