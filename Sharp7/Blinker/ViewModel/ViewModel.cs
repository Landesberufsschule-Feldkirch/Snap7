﻿using Blinker.Commands;
using System.Windows.Input;

namespace Blinker.ViewModel
{
    public class ViewModel
    {
        public Model.Blinker Blinker { get; }
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mw)
        {
            var mainWindow = mw;

            Blinker = new Model.Blinker();
            ViAnzeige = new VisuAnzeigen(mainWindow, Blinker);
        }


        private ICommand _btnTasterS1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS1 => _btnTasterS1 ??= new RelayCommand(_ => ViAnzeige.TasterS1(), _ => true);

        private ICommand _btnTasterS2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS2 => _btnTasterS2 ??= new RelayCommand(_ => ViAnzeige.TasterS2(), _ => true);

        private ICommand _btnTasterS3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS3 => _btnTasterS3 ??= new RelayCommand(_ => ViAnzeige.TasterS3(), _ => true);

        private ICommand _btnTasterS4;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS4 => _btnTasterS4 ??= new RelayCommand(_ => ViAnzeige.TasterS4(), _ => true);

        private ICommand _btnTasterS5;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS5 => _btnTasterS5 ??= new RelayCommand(_ => ViAnzeige.TasterS5(), _ => true);
    }
}