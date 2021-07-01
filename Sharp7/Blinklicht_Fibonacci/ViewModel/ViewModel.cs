using System.Windows.Input;
using Blinklicht_Fibonacci.Commands;

namespace Blinklicht_Fibonacci.ViewModel
{
    public class ViewModel
    {
        public Model.BlinklichtFibonacci BlinklichtFibonacci { get; }
        public VisuAnzeigen ViAnzeige { get; set; }

        public ViewModel(MainWindow mw)
        {
            var mainWindow = mw;

            BlinklichtFibonacci = new Model.BlinklichtFibonacci();
            ViAnzeige = new VisuAnzeigen(mainWindow, BlinklichtFibonacci);
        }

        private ICommand _btnTasterS1;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasterS1 => _btnTasterS1 ??= new RelayCommand(_ => ViAnzeige.TasterS1(), _ => true);
    }
}