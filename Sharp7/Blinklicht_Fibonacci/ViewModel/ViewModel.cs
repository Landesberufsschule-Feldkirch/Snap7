using Blinklicht_Fibonacci.Commands;
using System.Windows.Input;

namespace Blinklicht_Fibonacci.ViewModel
{
    public class ViewModel
    {
        public Model.BlinklichtFibonacci BlinklichtFibonacci { get; }
        public VisuAnzeigen ViAnz { get; set; }

        public ViewModel(MainWindow mw)
        {
            var mainWindow = mw;

            BlinklichtFibonacci = new Model.BlinklichtFibonacci();
            ViAnz = new VisuAnzeigen(mainWindow, BlinklichtFibonacci);
        }

        private ICommand _btnTaster;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTaster => _btnTaster ??= new RelayCommand(_ => ViAnz.Taster(), _ => true);
    }
}