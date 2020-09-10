using System.Windows.Input;

namespace Nadeltelegraph.SetManual.View
{
    public class View
    {
        public Visu Visu { get; set; }
        public View(MainWindow mainWindow) => Visu = new Visu(mainWindow);


        private ICommand _btnTasten;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnTasten => _btnTasten ??= new Command.Command(Visu.KnopfTasten);


        private ICommand _btnToggeln;
        // ReSharper disable once UnusedMember.Global
        public ICommand BtnToggeln => _btnToggeln ??= new Command.Command(Visu.KnopfToggeln);
    }
}