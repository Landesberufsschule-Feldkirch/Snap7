using Blinker.Commands;
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
        public ICommand BtnTasterS1 => _btnTasterS1 ??= new RelayCommand(p => ViAnzeige.TasterS1(), p => true);

        private ICommand _btnTasterS2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS2 => _btnTasterS2 ??= new RelayCommand(p => ViAnzeige.TasterS2(), p => true);

        private ICommand _btnTasterS3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS3 => _btnTasterS3 ??= new RelayCommand(p => ViAnzeige.TasterS3(), p => true);

        private ICommand _btnTasterS4;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS4 => _btnTasterS4 ??= new RelayCommand(p => ViAnzeige.TasterS4(), p => true);

        private ICommand _btnTasterS5;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS5 => _btnTasterS5 ??= new RelayCommand(p => ViAnzeige.TasterS5(), p => true);
    }
}