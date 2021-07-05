using Schleifmaschine.Commands;
using System.Windows.Input;

namespace Schleifmaschine.ViewModel
{
    public class ViewModel
    {
        public Model.Schleifmaschine Schleifmaschine { get; }
        public VisuAnzeigen ViAnz { get; set; }

        public ViewModel(MainWindow mainWindow)
        {
            Schleifmaschine = new Model.Schleifmaschine();
            ViAnz = new VisuAnzeigen(mainWindow, Schleifmaschine);
        }

        private ICommand _btnTaster;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnz.Taster);

        private ICommand _btnThermorelaisF1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnThermorelaisF1 => _btnThermorelaisF1 ??= new RelayCommand(_ => Schleifmaschine.ThermorelaisF1(), _ => true);


        private ICommand _btnThermorelaisF2;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnThermorelaisF2 => _btnThermorelaisF2 ??= new RelayCommand(_ => Schleifmaschine.ThermorelaisF2(), _ => true);

        private ICommand _btnS3;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnS3 => _btnS3 ??= new RelayCommand(_ => Schleifmaschine.TasterS3(), _ => true);

    }
}