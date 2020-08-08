﻿namespace LAP_2010_3_Ofentuersteuerung.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public VisuAnzeigen ViAnzeige { get; set; }
        public readonly Model.OfentuerSteuerung ofentuerSteuerung;

        public ViewModel(MainWindow mainWindow)
        {
            ofentuerSteuerung = new Model.OfentuerSteuerung();
            ViAnzeige = new VisuAnzeigen(mainWindow, ofentuerSteuerung);
        }

        // ReSharper disable once UnusedMember.Global
        public Model.OfentuerSteuerung OfentuerSteuerung => ofentuerSteuerung;

        #region SetManualQ1

        private ICommand _setManualQ1;

        // ReSharper disable once UnusedMember.Global
        public ICommand SetManualQ1 => _setManualQ1 ?? (_setManualQ1 = new RelayCommand(p => ViAnzeige.SetManualQ1(), p => true));

        #endregion SetManualQ1

        #region SetManualQ2

        private ICommand _setManualQ2;

        // ReSharper disable once UnusedMember.Global
        public ICommand SetManualQ2 => _setManualQ2 ?? (_setManualQ2 = new RelayCommand(p => ViAnzeige.SetManualQ2(), p => true));

        #endregion SetManualQ2

        #region BtnS1

        private ICommand _btnS1;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS1 => _btnS1 ?? (_btnS1 = new RelayCommand(p => ViAnzeige.SetS1(), p => true));

        #endregion BtnS1

        #region BtnS2

        private ICommand _btnS2;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS2 => _btnS2 ?? (_btnS2 = new RelayCommand(p => ViAnzeige.SetS2(), p => true));

        #endregion BtnS2

        #region BtnS3

        private ICommand _btnS3;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS3 => _btnS3 ?? (_btnS3 = new RelayCommand(p => ViAnzeige.SetS3(), p => true));

        #endregion BtnS3

        #region BtnB3

        private ICommand _btnB3;

        // ReSharper disable once UnusedMember.Global
        public ICommand BtnB3 => _btnB3 ?? (_btnB3 = new RelayCommand(p => ViAnzeige.SetB3(), p => true));

        #endregion BtnB3
    }
}