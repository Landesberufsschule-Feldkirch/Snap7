﻿using LAP_2010_1_Kompressoranlage.Commands;

namespace LAP_2010_1_Kompressoranlage.ViewModel
{
    using System.Windows.Input;

    public class ViewModel
    {
        public Model.Kompressoranlage Kompressoranlage { get; }
        public VisuAnzeigen ViAnzeige { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            Kompressoranlage = new Model.Kompressoranlage();
            ViAnzeige = new VisuAnzeigen(mainWindow, Kompressoranlage);
        }


        private ICommand _btnF1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnF1 => _btnF1 ?? (_btnF1 = new RelayCommand(p => Kompressoranlage.BtnF1(), p => true));

        private ICommand _btnS1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS1 => _btnS1 ?? (_btnS1 = new RelayCommand(p => ViAnzeige.SetS1(), p => true));

        private ICommand _btnS2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS2 => _btnS2 ?? (_btnS2 = new RelayCommand(p => ViAnzeige.BtnS2(), p => true));

        private ICommand _btnB1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnB1 => _btnB1 ?? (_btnB1 = new RelayCommand(p => Kompressoranlage.BtnB1(), p => true));
    }
}