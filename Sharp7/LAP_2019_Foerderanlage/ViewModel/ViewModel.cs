﻿namespace LAP_2019_Foerderanlage.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        private readonly Model.Foerderanlage _foerderanlage;
        public Model.Foerderanlage Foerderanlage => _foerderanlage;
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            _foerderanlage = new Model.Foerderanlage(mainWindow);
            ViAnzeige = new VisuAnzeigen(mainWindow, _foerderanlage);
        }



        private ICommand _btnF1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnF1 => _btnF1 ?? (_btnF1 = new RelayCommand(p => _foerderanlage.BtnF1(), p => true));

        private ICommand _btnS0;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS0 => _btnS0 ?? (_btnS0 = new RelayCommand(p => ViAnzeige.SetS0(), p => true));

        private ICommand _btnS1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS1 => _btnS1 ?? (_btnS1 = new RelayCommand(p => ViAnzeige.SetS1(), p => true));

        private ICommand _btnS2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS2 => _btnS2 ?? (_btnS2 = new RelayCommand(p => _foerderanlage.BtnS2(), p => true));

        private ICommand _btnS5;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS5 => _btnS5 ?? (_btnS5 = new RelayCommand(p => ViAnzeige.SetS5(), p => true));

        private ICommand _btnS6;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS6 => _btnS6 ?? (_btnS6 = new RelayCommand(p => ViAnzeige.SetS6(), p => true));

        private ICommand _btnS7;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS7 => _btnS7 ?? (_btnS7 = new RelayCommand(p => ViAnzeige.SetS7(), p => true));

        private ICommand _btnS8;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS8 => _btnS8 ?? (_btnS8 = new RelayCommand(p => ViAnzeige.SetS8(), p => true));

        private ICommand _btnWagenNachLinks;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnWagenNachLinks =>
            _btnWagenNachLinks ??
            (_btnWagenNachLinks = new RelayCommand(p => _foerderanlage.WagenNachLinks(), p => true));

        private ICommand _btnWagenNachRechts;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnWagenNachRechts =>
            _btnWagenNachRechts ?? (_btnWagenNachRechts =
                new RelayCommand(p => _foerderanlage.WagenNachRechts(), p => true));

        private ICommand _btnNachuellen;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnNachuellen =>
            _btnNachuellen ??
            (_btnNachuellen = new RelayCommand(p => _foerderanlage.Nachfuellen(), p => true));

        private ICommand _btnM1Rl;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnM1Rl => _btnM1Rl ?? (_btnM1Rl = new RelayCommand(p => ViAnzeige.SetManualM1_RL(), p => true));

        private ICommand _btnM1Ll;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnM1Ll => _btnM1Ll ?? (_btnM1Ll = new RelayCommand(p => ViAnzeige.SetManualM1_LL(), p => true));

        private ICommand _btnM2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnM2 => _btnM2 ?? (_btnM2 = new RelayCommand(p => ViAnzeige.SetManualM2(), p => true));

        private ICommand _btnK1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnK1 => _btnK1 ?? (_btnK1 = new RelayCommand(p => ViAnzeige.SetManualK1(), p => true));

        private ICommand _btnM1LlK1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnM1LlK1 => _btnM1LlK1 ?? (_btnM1LlK1 = new RelayCommand(p => ViAnzeige.SetManualM1_LL_K1(), p => true));
    }
}