﻿using LAP_2018_1_Silosteuerung.Commands;
using System.Windows.Input;

namespace LAP_2018_1_Silosteuerung.ViewModel
{
    public class ViewModel
    {
        public Model.Silosteuerung Silosteuerung { get; }
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            Silosteuerung = new Model.Silosteuerung();
            ViAnzeige = new VisuAnzeigen(mainWindow, Silosteuerung);
        }


        private ICommand _btnF1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnF1 => _btnF1 ??= new RelayCommand(p => Silosteuerung.BtnF1(), p => true);

        private ICommand _btnF2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnF2 => _btnF2 ??= new RelayCommand(p => Silosteuerung.BtnF2(), p => true);


        private ICommand _btnS0;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS0 => _btnS0 ??= new RelayCommand(p => ViAnzeige.BtnS0(), p => true);

        private ICommand _btnS1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS1 => _btnS1 ??= new RelayCommand(p => ViAnzeige.BtnS1(), p => true);

        private ICommand _btnS2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS2 => _btnS2 ??= new RelayCommand(p => ViAnzeige.BtnS2(), p => true);

        private ICommand _btnS3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS3 => _btnS3 ??= new RelayCommand(p => ViAnzeige.BtnS3(), p => true);

        private ICommand _btnWagenNachLinks;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnWagenNachLinks =>
            _btnWagenNachLinks ??= new RelayCommand(p => Silosteuerung.WagenNachLinks(), p => true);

        private ICommand _btnWagenNachRechts;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnWagenNachRechts =>
            _btnWagenNachRechts ??= new RelayCommand(p => Silosteuerung.WagenNachRechts(), p => true);
    }
}