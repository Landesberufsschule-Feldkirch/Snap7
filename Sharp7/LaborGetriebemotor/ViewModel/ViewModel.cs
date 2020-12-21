using System.Windows.Input;
using LaborGetriebemotor.Commands;
using LaborGetriebemotor.Model;
using TestAutomat.AutoTester;
using TestAutomat.AutoTesterViewModel;

namespace LaborGetriebemotor.ViewModel
{
    public class ViewModel
    {
        public Getriebemotor Getriebemotor { get; }
        public TestAutomat.AutoTester.AutoTester AutoTester { get; }
        public VisuAnzeigen ViAnzeige { get; set; }
        public  TestAutomat.AutoTesterViewModel.AutoTesterViewModel AutoTesterViAnzeige { get; set; }
        
        public ViewModel(MainWindow mainWindow)
        {
            Getriebemotor = new Getriebemotor();
            AutoTester = new AutoTester(mainWindow.AlleOrdnerLesen, AutoTester);
            ViAnzeige = new VisuAnzeigen(mainWindow, Getriebemotor);
            AutoTesterViAnzeige = new AutoTesterViewModel();
        }


        private ICommand _btnTaster;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTaster => _btnTaster ??= new RelayCommand(ViAnzeige.Taster);

    }
}