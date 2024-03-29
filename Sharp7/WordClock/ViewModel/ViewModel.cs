﻿// ReSharper disable UnusedMember.Global
namespace WordClock.ViewModel
{
    using Commands;
    using System.Windows.Input;

    public class ViewModel
    {
        public Model.Zeiten Zeiten { get; }
        public VisuAnzeigen ViAnz { get; set; }
        public ViewModel(MainWindow mainWindow)
        {
            Zeiten = new Model.Zeiten();
            ViAnz = new VisuAnzeigen(mainWindow, Zeiten);
        }

        private ICommand _btnSetCurrentTime;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnSetCurrentTime =>
            _btnSetCurrentTime ??= new RelayCommand(_ => Zeiten.SetCurrentTime(), _ => true);
    }
}